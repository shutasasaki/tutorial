using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ssksSimulation
{
    /// <summary>
    /// UI 更新／測定制御／応答送信を担うインターフェイス
    /// （frmMain が実装します）
    /// </summary>
    public interface ICommandProcessorUI
    {
        /// <summary>UI スレッド上で action を実行します。</summary>
        void InvokeOnUI(Action action);

        /// <summary>Echo コンボボックスの現在選択値を取得します。</summary>
        Echo GetEcho();

        /// <summary>FrequencyWeighting を UI に反映します。</summary>
        void SetFrequencyWeighting(FrequencyWeighting mode);

        /// <summary>TimeWeighting を UI に反映します。</summary>
        void SetTimeWeighting(TimeWeighting mode);

        /// <summary>StoreMode を UI に反映します。</summary>
        void SetStoreMode(StoreMode mode);

        /// <summary>WaveRecMode を UI に反映します。</summary>
        void SetWaveRecMode(WaveRecMode mode);

        /// <summary>OutputLevelRangeUpper を UI に反映します。</summary>
        void SetOutputLevelRangeUpper(int value);

        /// <summary>StoreName コードを UI に反映します。</summary>
        void SetStoreName(int fullCode);

        /// <summary>DOD プロファイルの現在選択値を取得します。</summary>
        DODProfile GetSelectedProfile();

        /// <summary>SD Card Free Size の現在値を取得します。</summary>
        int GetSDCardFreeSize();

        /// <summary>測定開始／停止のメッセージを UI に表示します。</summary>
        void UpdateMessage(string message);
    }

    /// <summary>
    /// 受信文字列をパースし、UI 更新／測定制御／応答送信を行う
    /// </summary>
    public class CommandProcessor : IDisposable
    {
        private readonly ssksSim _sim;
        private readonly ICommandProcessorUI _ui;
        private readonly Dictionary<string, Action<string>> _commandHandlers;
        private readonly List<(Regex pattern, Action<Match> handler)> _regexHandlers;
        private bool _isMeasuring = false;

        public CommandProcessor(ssksSim sim, ICommandProcessorUI ui)
        {
            _sim = sim ?? throw new ArgumentNullException(nameof(sim));
            _ui = ui ?? throw new ArgumentNullException(nameof(ui));

            // コマンド振り分けテーブル初期化
            _commandHandlers = new Dictionary<string, Action<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["Echo?"] = _ => HandleEcho(),
                ["DOD?"] = _ => HandleDOD(),
                ["SD Card Free Size?"] = _ => HandleSDCardFreeSize(),
            };                                                                              // :contentReference[oaicite:0]{index=0}

            // 正規表現パターン＋ハンドラ初期化
            _regexHandlers = new List<(Regex, Action<Match>)>
            {
                (new Regex(@"^Store Name,(.+)$",                RegexOptions.IgnoreCase), m => HandleStoreName(m.Groups[1].Value)),
                (new Regex(@"^Frequency Weighting,([ACZ])$",    RegexOptions.IgnoreCase), m => HandleFrequencyWeighting(m.Groups[1].Value)),
                (new Regex(@"^Time Weighting,([FSI])$",         RegexOptions.IgnoreCase), m => HandleTimeWeighting(m.Groups[1].Value)),
                (new Regex(@"^Store Mode,(.+)$",                RegexOptions.IgnoreCase), m => HandleStoreMode(m.Groups[1].Value)),
                (new Regex(@"^Wave Rec Mode,(.+)$",             RegexOptions.IgnoreCase), m => HandleWaveRecMode(m.Groups[1].Value)),
                (new Regex(@"^Output Level Range Upper,(\d+)$", RegexOptions.IgnoreCase), m => HandleOutputLevelRangeUpper(m.Groups[1].Value)),
                (new Regex(@"^Measure,Start$",                  RegexOptions.IgnoreCase), _ => HandleStartMeasure()),
                (new Regex(@"^Measure,Stop$",                   RegexOptions.IgnoreCase), _ => HandleStopMeasure()),
            };                                                                              // :contentReference[oaicite:1]{index=1}

            // イベント登録
            _sim.OnCommandReceived += OnCommandReceived;                                    // :contentReference[oaicite:2]{index=2}
        }

        private void OnCommandReceived(object sender, byte[] e)
        {
            // ASCII→文字列、末尾 CR/LF 削除
            string cmd = Encoding.ASCII.GetString(e).TrimEnd('\r', '\n');

            // 完全一致コマンド
            if (_commandHandlers.TryGetValue(cmd, out var handler))
            {
                handler(cmd);
                return;
            }

            // 正規表現パターンマッチ
            foreach (var (regex, h) in _regexHandlers)
            {
                var m = regex.Match(cmd);
                if (m.Success)
                {
                    h(m);
                    return;
                }
            }

            // どれにもマッチしない場合はエラー
            _sim.Send("CMDERR");
        }

        /// <summary>
        /// 空や null の場合は CMDERR 応答して false を返します。
        /// </summary>
        private bool Validate(string input, out string trimmed)
        {
            trimmed = input?.Trim();
            if (string.IsNullOrEmpty(trimmed))
            {
                _sim.Send("CMDERR");
                return false;
            }
            return true;
        }

        private void HandleEcho()
        {
            _ui.InvokeOnUI(() =>
            {
                var value = _ui.GetEcho();
                _sim.Send(value.ToString());
            });
        }

        private void HandleDOD()
        {
            DODProfile p = _ui.GetSelectedProfile();
            if (p == null) return;

            // プロファイルをカンマ区切りで組み立て
            string resp = string.Join(",",
                p.Lp.ToString("F1"), p.Leq.ToString("F1"), p.Le.ToString("F1"),
                p.Lmax.ToString("F1"), p.Lmin.ToString("F1"), p.Add.ToString("F1"),
                p.L5.ToString("F1"), p.L10.ToString("F1"), p.L50.ToString("F1"),
                p.L90.ToString("F1"), p.SubLp.ToString("F1"),
                p.Over.ToString(), p.Under.ToString()
            );
            _sim.Send(resp);
        }

        private void HandleSDCardFreeSize()
        {
            // NumericUpDown の文字列検証と取得
            _sim.Send(_ui.GetSDCardFreeSize().ToString());
        }

        private void HandleStoreName(string param)
        {
            if (!Validate(param, out var trimmed)) return;
            if (!int.TryParse(trimmed, out int storeNo))
            {
                _sim.Send("CMDERR");
                return;
            }
            // frmMain 側でのオフセット計算と clamping は UI 側で行っても OK
            int fullCode = storeNo;
            _ui.InvokeOnUI(() => _ui.SetStoreName(fullCode));
        }

        private void HandleFrequencyWeighting(string param)
        {
            if (!Validate(param, out var trimmed)) return;
            _ui.InvokeOnUI(() =>
            {
                if (Enum.TryParse<FrequencyWeighting>(trimmed, true, out var mode))
                    
                _ui.SetFrequencyWeighting(mode);
                else
                    _sim.Send("CMDERR");
            });
        }

        private void HandleTimeWeighting(string param)
        {
            if (!Validate(param, out var trimmed)) return;
            _ui.InvokeOnUI(() =>
            {
                if (Enum.TryParse<TimeWeighting>(trimmed, true, out var mode))
                    _ui.SetTimeWeighting(mode);
                else
                    _sim.Send("CMDERR");
            });
        }

        private void HandleStoreMode(string param)
        {
            if (!Validate(param, out var trimmed)) return;
            _ui.InvokeOnUI(() =>
            {
                if (Enum.TryParse<StoreMode>(trimmed, true, out var mode))
                    _ui.SetStoreMode(mode);
                else
                    _sim.Send("CMDERR");
            });
        }

        private void HandleWaveRecMode(string param)
        {
            if (!Validate(param, out var trimmed)) return;
            _ui.InvokeOnUI(() =>
            {
                if (Enum.TryParse<WaveRecMode>(trimmed, true, out var mode))
                    _ui.SetWaveRecMode(mode);
                else
                    _sim.Send("CMDERR");
            });
        }

        private void HandleOutputLevelRangeUpper(string param)
        {
            if (!Validate(param, out var trimmed)) return;
            if (!int.TryParse(trimmed, out var val))
            {
                _sim.Send("CMDERR");
                return;
            }
            _ui.InvokeOnUI(() => _ui.SetOutputLevelRangeUpper(val));
        }

        private void HandleStartMeasure()
        {
            if (_isMeasuring)
            {
                _sim.Send("CMDERR");
                return;
            }
            _isMeasuring = true;
            _sim.Send("Measure,Start");
            _ui.InvokeOnUI(() => _ui.UpdateMessage("Measure,Start\r\n"));
        }

        private void HandleStopMeasure()
        {
            if (!_isMeasuring)
            {
                _sim.Send("CMDERR");
                return;
            }
            _isMeasuring = false;
            _sim.Send("Measure,Stop");
            _ui.InvokeOnUI(() => _ui.UpdateMessage("Measure,Stop\r\n"));
        }

        public void Dispose()
        {
            _sim.OnCommandReceived -= OnCommandReceived;
        }
    }
}
