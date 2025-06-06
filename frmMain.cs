using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.CompilerServices;

namespace ssksSimulation
{
    public partial class frmMain: Form,ICommandProcessorUI
    {
        private static readonly int _noiseMeasRangeDefaultValue = 60;
        private static readonly int _noiseMeasRangeDefaultLowwer = 0;
        private static readonly int _noiseMeasRangeDefaultUpper = 130;
        private static readonly int _storeNameMax = 9999;
        private static readonly int _storeNameMin = 0;
        private static readonly int _sdCardFreeSizeMax = 32768;
        private static readonly int _sdCardFreeSizeMin = 0;
        private static readonly int _sdCardFreeSizeDefault = 18293;
        private static readonly int _storeNameBaseOffset = 3000;

        private ssksSim _ssksSim;

        private List<DODProfile> _profiles;

        private CommandProcessor _cmdProcessor;
        public frmMain(string meterName)
        {
            InitializeComponent();
            this.Text = meterName;
            txtTitle.Text = meterName;

            InitializeUIComponents();


        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _cmdProcessor?.Dispose();
        }

        private void InitializeUIComponents()
        {
            var ports = SerialPort.GetPortNames();
            cboPort.Items.AddRange(ports);
            if (ports.Length > 0)
            {
                cboPort.SelectedIndex = 0;
            }

            var freqSetting = Enum.GetNames(typeof(FrequencyWeighting));
            cboFrequencyWeighting.Items.AddRange(freqSetting);
            cboFrequencyWeighting.SelectedIndex = 0;

            var storeMode = Enum.GetNames(typeof(StoreMode));
            cboStoreMode.Items.AddRange(storeMode);
            cboStoreMode.SelectedIndex = 0;

            var waveRecMode = Enum.GetNames(typeof(WaveRecMode));
            cboWaveRecMode.Items.AddRange(waveRecMode);
            cboWaveRecMode.SelectedIndex = 0;

            var echo = Enum.GetNames(typeof(Echo));
            cboEcho.Items.AddRange(echo);
            cboEcho.SelectedIndex = 1;//デフォルトでOn

            var timeWeighting = Enum.GetNames(typeof(TimeWeighting));
            cboTimeWeighting.Items.AddRange(timeWeighting);
            cboTimeWeighting.SelectedIndex = 0;

            nudNoiseMeasRangeUpper.Value = _noiseMeasRangeDefaultValue;
            nudNoiseMeasRangeUpper.Maximum = _noiseMeasRangeDefaultUpper;

            nudStoreNo.Minimum = _storeNameMin;
            nudStoreNo.Maximum = _storeNameMax;
            nudStoreNo.Value = 3001;

            nudSDCardFreeSize.Maximum = _sdCardFreeSizeMax;
            nudSDCardFreeSize.Minimum = _sdCardFreeSizeMin;
            nudSDCardFreeSize.Value = _sdCardFreeSizeDefault;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SetButtonEnableOnClose();

            try
            {
                var json = File.ReadAllText("DODProfiles.json");
                _profiles = JsonConvert.DeserializeObject<List<DODProfile>>(json);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("DODProfiles.json not found. Loading with empty profiles.");
                _profiles = new List<DODProfile>();
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Error parsing DODProfiles.json: {ex.Message}");
                _profiles = new List<DODProfile>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
                _profiles = new List<DODProfile>();
            }

            cboProfile.DataSource = _profiles;
            cboProfile.DisplayMember = nameof(DODProfile.Name);
            if (_profiles.Count > 0) cboProfile.SelectedIndex = 0;

        }


        public  void InvokeOnUI(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        public Echo GetEcho()
        {
            Echo e = default;
            cboEcho.InvokeIfRequired(() =>
            {
                e = (Echo)cboEcho.SelectedIndex;
            });
            return e;
        }

        public DODProfile GetSelectedProfile()
        {
            DODProfile profile = null;
            cboProfile.InvokeIfRequired(() =>
            {
                profile = (DODProfile)cboProfile.SelectedItem;
            });
            return profile;
        }
        public void SetStoreName(int fullCode)
        {
            nudStoreNo.InvokeIfRequired(() =>
            {

                decimal min = nudStoreNo.Minimum;
                decimal max = nudStoreNo.Maximum;

                decimal clamped = Math.Min(Math.Max(fullCode + _storeNameBaseOffset, min), max);

                
                nudStoreNo.Value = clamped;
            });
        }

        public void SetFrequencyWeighting(FrequencyWeighting mode)
        {
            cboFrequencyWeighting.InvokeIfRequired(() =>
            {
                cboFrequencyWeighting.SelectedIndex = (int)mode;
            });
        }

        public void SetTimeWeighting(TimeWeighting mode)
        {
            cboTimeWeighting.InvokeIfRequired(() =>
            {
                cboTimeWeighting.SelectedIndex = (int)mode;
            });
        }

        public void SetStoreMode(StoreMode mode)
        {
            cboStoreMode.InvokeIfRequired(() => { cboStoreMode.SelectedIndex = (int)mode; });
        }

        public void SetWaveRecMode(WaveRecMode mode)
        {
            cboWaveRecMode.InvokeIfRequired(() => { cboWaveRecMode.SelectedIndex = (int)mode; });
        }

        public void SetOutputLevelRangeUpper(int value)
        {
            nudNoiseMeasRangeUpper.InvokeIfRequired(() =>
            {
                if (value < _noiseMeasRangeDefaultLowwer || value > _noiseMeasRangeDefaultUpper)
                {
                    nudNoiseMeasRangeUpper.Value = _noiseMeasRangeDefaultUpper;
                }
                else
                {
                    nudNoiseMeasRangeUpper.Value = value;
                }
            });
        }

        public int GetSDCardFreeSize()
        {
            int freeSize = 0;
            nudSDCardFreeSize.InvokeIfRequired(() =>
            {
                freeSize = (int)nudSDCardFreeSize.Value;
            });

            return freeSize;
        }

        public void UpdateMessage(string message)
        {

            txtMessage.InvokeIfRequired(() =>
            {
                txtMessage.Clear();
                txtMessage.AppendText(message);
            });
        }


        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            string port = cboPort.SelectedItem.ToString();
            
            var serialPort = new SerialPort(port);
            serialPort.BaudRate = 38400;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;

            _ssksSim = new ssksSim(serialPort);
            _cmdProcessor = new CommandProcessor(_ssksSim, this);

            _ssksSim.Open();
            SetButtonEnableOnOpen();
        }


        /// <summary>
        /// ポートが閉じている状態のボタンの有効無効を設定する。
        /// </summary>
        private void SetButtonEnableOnClose()
        {
            btnOpenPort.Enabled = true;
            btnClosePort.Enabled = false;

        }

        /// <summary>
        /// ポートが開いている状態のボタンの有効無効を設定する。
        /// </summary>
        private void SetButtonEnableOnOpen()
        {
            btnOpenPort.Enabled = false;
            btnClosePort.Enabled = true;

        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            _ssksSim.Close();
            SetButtonEnableOnClose();
        }

        

    }
}
