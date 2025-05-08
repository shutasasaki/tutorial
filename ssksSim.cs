using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ssksSimulation
{
    public class ssksSim : IDisposable
    {
        #region fields
        private readonly string _TimeFormat = "HH:mm:ss.fff";
        private readonly object _communicationLock = new object();
        private MemoryStream _rxBuff;
        private bool disposedValue = false;
        #endregion

        #region constructors
        public ssksSim(SerialPort serialPort)
        {
            _rxBuff = new MemoryStream();
            this.SerialPort = serialPort;
            this.SerialPort.NewLine = "\r\n";

            serialPort.DataReceived += SerialPort_DataReceived;
        }
        #endregion

        #region events
        public event EventHandler<byte[]> OnCommandReceived;
        #endregion

        #region properties
        public SerialPort SerialPort { get; private set; }
        public static byte[] Terminator { get; } = new byte[] { 0x0D, 0x0A };

        #endregion

        #region methods

        public void Open()
        {
            try
            {
                DebugMessage("Open");
                this.SerialPort.Open();
            }
            catch (Exception ex)
            {
                DebugMessage("Open", $"Error: {ex.Message}");
            }
        }
        public void Close()
        {
            try
            {
                DebugMessage("Close");
                this.SerialPort.Close();
            }
            catch (Exception ex)
            {
                DebugMessage("Close", $"Error: {ex.Message}");
            }
        }
        public void Send(string tx, bool addNewLine = true)
        {
            lock (_communicationLock)
            {
                DebugMessage(nameof(Send), tx);
                if (!this.SerialPort.IsOpen)
                {
                    DebugMessage(nameof(Send), "Port is not open.");
                    return;
                }

                try
                {
                    if (addNewLine)
                    {
                        this.SerialPort.WriteLine(tx); // NewLine("\r\n")が自動で付加される
                    }
                    else
                    {
                        this.SerialPort.Write(tx);
                    }
                }
                catch (Exception ex)
                {
                    DebugMessage(nameof(Send), $"Error: {ex.Message}");
                    // 必要に応じて上位に例外を伝播させるか、エラーイベントを発火させることも検討
                }
            }
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //受信データ読み込み
            int rxLength = this.SerialPort.BytesToRead;
            byte[] rx = new byte[rxLength];
            this.SerialPort.Read(rx, 0, rxLength);

            //バッファ書き込み
            _rxBuff.Write(rx, 0, rxLength);

            //終端文字までを抽出
            if(ExtractFrame(ref _rxBuff,Terminator,out byte[] rxFrame))
            {
                OnCommandReceived?.Invoke(this, rxFrame);
            }
        }

        /// <summary>
        /// rxBuff の先頭から terminator シーケンス（例：\r\n）を含む最初のフレームを抽出し、
        /// rxBuff からはその分を除去します。
        /// </summary>
        /// <param name="rxBuff">受信バッファ</param>
        /// <param name="terminator">終端シーケンス（例：new byte[]{0x0D,0x0A}）</param>
        /// <param name="rxFrame">抽出されたフレーム（terminator 含む）</param>
        /// <returns>terminator が見つかれば true（rxFrame にセット）、見つからなければ false</returns>
        private bool ExtractFrame(ref MemoryStream rxBuff, byte[] terminator, out byte[] rxFrame)
        {
            rxFrame = null;

            // バッファを byte[] に
            byte[] buffer = rxBuff.ToArray();

            // terminator シーケンスの開始位置を検索
            int idx = IndexOfSequence(buffer, terminator);
            if (idx < 0) return false;

            // フレーム長 = terminator の開始位置 + terminator の長さ
            int frameLength = idx + terminator.Length;
            rxFrame = new byte[frameLength];
            Array.Copy(buffer, 0, rxFrame, 0, frameLength);

            // バッファから抽出分を削除して残りを再構築
            rxBuff.SetLength(0);
            rxBuff.Write(buffer, frameLength, buffer.Length - frameLength);

            return true;
        }

        /// <summary>
        /// バイト配列 buffer 中から pattern を先頭から順に探し、
        /// 最初に一致したインデックスを返す。なければ -1。
        /// </summary>
        private int IndexOfSequence(byte[] buffer, byte[] pattern)
        {
            int limit = buffer.Length - pattern.Length;
            for (int i = 0; i <= limit; i++)
            {
                bool match = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (buffer[i + j] != pattern[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match) return i;
            }
            return -1;
        }




        private void DebugMessage(string methodName, string message = "")
        {
            Debug.WriteLine($"{DateTime.Now.ToString(_TimeFormat)} [{methodName}] {message}");
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // マネージドリソースの破棄
                    if (SerialPort != null)
                    {
                        SerialPort.DataReceived -= SerialPort_DataReceived; // イベントハンドラの解除
                        if (SerialPort.IsOpen)
                        {
                            SerialPort.Close();
                        }
                        SerialPort.Dispose();
                        SerialPort = null; // null代入は必須ではないが、アクセスを防ぐ意味では有効
                    }
                    if (_rxBuff != null)
                    {
                        _rxBuff.Dispose();
                        _rxBuff = null;
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // ファイナライザがある場合はコメント解除
        }
        #endregion
    }
}
