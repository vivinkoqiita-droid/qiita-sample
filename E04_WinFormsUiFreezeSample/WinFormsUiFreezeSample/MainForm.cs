using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsUiFreezeSample
{
    /// <summary>
    /// UIフリーズの見え方比較用メイン画面
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 心拍表示更新用タイマー
        /// </summary>
        private readonly System.Windows.Forms.Timer heartbeatTimer;
        private double heartbeatPhase;
        private string lastAction = "none";
        private UiLagMonitor? lagMonitor;

        /// <summary>
        /// 画面初期化
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            heartbeatTimer = new System.Windows.Forms.Timer();
            heartbeatTimer.Interval = 16;
            heartbeatTimer.Tick += HeartbeatTimer_Tick;
        }

        /// <summary>
        /// 初回表示時の開始処理
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            heartbeatTimer.Start();
            lagMonitor = new UiLagMonitor(this, () => lastAction, 180, 220, AppendLog);
            AppendLog("起動完了");
        }

        /// <summary>
        /// 終了時の後始末
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            lagMonitor?.Dispose();
            lagMonitor = null;
            heartbeatTimer.Stop();
            heartbeatTimer.Dispose();
            base.OnFormClosed(e);
        }

        /// <summary>
        /// UIスレッド生存確認用心拍更新
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private void HeartbeatTimer_Tick(object? sender, EventArgs e)
        {
            // 心拍波形位相加算
            heartbeatPhase += 0.045;
            // 単調増加では見え味が弱いため疑似波形合成
            var normalized = (Math.Sin(heartbeatPhase) + 1.0) / 2.0;
            var wave = (Math.Sin(heartbeatPhase * 2.2) + 1.0) / 2.0;
            var value = (int)Math.Round((normalized * 0.72 + wave * 0.28) * prgHeartbeat.Maximum);

            lblHeartbeat.Text = $"心拍: {DateTime.Now:HH:mm:ss.fff}";
            prgHeartbeat.Value = value;
        }

        /// <summary>
        /// 直近操作名の記録
        /// </summary>
        /// <param name="actionName">操作名</param>
        private void MarkAction(string actionName)
        {
            lastAction = actionName;
            AppendLog($"操作: {actionName}");
        }

        /// <summary>
        /// 状態表示更新
        /// </summary>
        /// <param name="text">状態文言</param>
        private void SetStatus(string text)
        {
            lblStatusValue.Text = text;
            UpdateStatusTone(text);
            AppendLog($"状態: {text}");
        }

        /// <summary>
        /// 状態表示色調整
        /// </summary>
        /// <param name="text">状態文言</param>
        private void UpdateStatusTone(string text)
        {
            if (text.Contains("危険", StringComparison.Ordinal) || text.Contains("タイムアウト", StringComparison.Ordinal))
            {
                lblStatusValue.BackColor = Color.FromArgb(255, 240, 224);
                lblStatusValue.ForeColor = Color.FromArgb(160, 82, 45);
                return;
            }

            if (text.Contains("完了", StringComparison.Ordinal))
            {
                lblStatusValue.BackColor = Color.FromArgb(232, 247, 237);
                lblStatusValue.ForeColor = Color.FromArgb(46, 125, 50);
                return;
            }

            if (text.Contains("待機", StringComparison.Ordinal))
            {
                lblStatusValue.BackColor = Color.FromArgb(245, 247, 250);
                lblStatusValue.ForeColor = Color.FromArgb(70, 80, 95);
                return;
            }

            lblStatusValue.BackColor = Color.FromArgb(232, 242, 253);
            lblStatusValue.ForeColor = Color.FromArgb(25, 118, 210);
        }

        /// <summary>
        /// ログ追記
        /// </summary>
        /// <param name="message">ログ文言</param>
        private void AppendLog(string message)
        {
            if (txtLog.IsDisposed)
            {
                return;
            }

            var line = $"{DateTime.Now:HH:mm:ss.fff} {message}{Environment.NewLine}";

            // 背景スレッドからの呼び出し考慮
            if (txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke(new Action(() =>
                {
                    txtLog.AppendText(line);
                }));
                return;
            }

            txtLog.AppendText(line);
        }

        /// <summary>
        /// 作業進捗初期化
        /// </summary>
        private void ResetProgress()
        {
            ReportProgress(0);
        }

        /// <summary>
        /// 作業進捗反映
        /// </summary>
        /// <param name="value">進捗値</param>
        private void ReportProgress(int value)
        {
            var normalized = Math.Max(0, Math.Min(prgWork.Maximum, value));

            // 進捗更新元がUIスレッド外のケース考慮
            if (prgWork.InvokeRequired)
            {
                prgWork.BeginInvoke(new Action(() => prgWork.Value = normalized));
                return;
            }

            prgWork.Value = normalized;
        }

        /// <summary>
        /// CPU負荷再現処理
        /// </summary>
        /// <param name="milliseconds">実行時間ミリ秒</param>
        private static void HeavyCpuWork(int milliseconds)
        {
            var sw = Stopwatch.StartNew();
            double total = 0;

            // I/O待機ではなくCPU占有を見せるための空回し
            while (sw.ElapsedMilliseconds < milliseconds)
            {
                for (var i = 1; i < 20000; i++)
                {
                    total += Math.Sqrt(i);
                }
            }

            GC.KeepAlive(total);
        }

        /// <summary>
        /// 非同期I/O待機相当の疑似処理
        /// </summary>
        /// <param name="milliseconds">総待機時間ミリ秒</param>
        /// <returns>待機完了タスク</returns>
        private async Task SimulatedAsyncOperation(int milliseconds)
        {
            var step = Math.Max(1, milliseconds / 10);

            for (var i = 1; i <= 10; i++)
            {
                // Task.Delay使用による待機中UI解放
                await Task.Delay(step);
                ReportProgress(i * 10);
            }
        }

        /// <summary>
        /// UIスレッド上の重い処理再現
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private async void btnHeavyOnUi_Click(object sender, EventArgs e)
        {
            MarkAction("UIで重い処理");
            ResetProgress();
            SetStatus("UIスレッドを3秒占有中");

            // ここがUIスレッド占有点
            HeavyCpuWork(3000);
            ReportProgress(100);
            SetStatus("完了");
            await Task.CompletedTask;
        }

        /// <summary>
        /// Task.RunによるCPU処理退避例
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private async void btnTaskRunCpu_Click(object sender, EventArgs e)
        {
            MarkAction("Task.RunでCPU処理");
            ResetProgress();
            SetStatus("別スレッドでCPU処理中");

            // CPU処理だけを別スレッドへ退避
            await Task.Run(() =>
            {
                for (var i = 1; i <= 10; i++)
                {
                    HeavyCpuWork(250);
                    ReportProgress(i * 10);
                }
            });

            SetStatus("完了");
        }

        /// <summary>
        /// async awaitによる待機継続例
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private async void btnAsyncIo_Click(object sender, EventArgs e)
        {
            MarkAction("async I/O待機");
            ResetProgress();
            SetStatus("Task.Delayで待機中");

            await SimulatedAsyncOperation(3000);
            SetStatus("完了");
        }

        /// <summary>
        /// UIスレッド上のWait再現
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private void btnWait_Click(object sender, EventArgs e)
        {
            MarkAction(".Waitで待機");
            ResetProgress();
            SetStatus("危険なWaitを3.5秒だけ再現中");

            AppendLog(".Waitはデッドロック化しやすいため、このサンプルでは3.5秒で復帰させる");

            // UIスレッド側の同期待ち化
            var completed = SimulatedAsyncOperation(3000).Wait(3500);
            if (!completed)
            {
                AppendLog(".Waitが継続待ちになったためタイムアウト復帰");
                SetStatus("タイムアウト復帰");
                return;
            }

            SetStatus("完了");
        }

        /// <summary>
        /// Invoke完了待ち連鎖の再現
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private void btnInvokeWait_Click(object sender, EventArgs e)
        {
            MarkAction("Invoke + Wait");
            ResetProgress();
            SetStatus("危険な待ちの鎖を3秒だけ再現中");

            // 背景側はInvoke完了待ち
            var worker = Task.Run(() =>
            {
                Thread.Sleep(300);
                Invoke(new Action(() =>
                {
                    AppendLog("InvokeでUI更新");
                    ReportProgress(50);
                }));
            });

            // UI側もworker完了待ち
            var completed = worker.Wait(3000);
            if (!completed)
            {
                AppendLog("Invoke完了待ちがUI側Waitと重なりタイムアウト復帰");
                SetStatus("タイムアウト復帰");
                return;
            }

            ReportProgress(100);
            SetStatus("完了");
        }

        /// <summary>
        /// BeginInvokeによる待たないUI更新例
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private async void btnBeginInvoke_Click(object sender, EventArgs e)
        {
            MarkAction("BeginInvokeで更新");
            ResetProgress();
            SetStatus("待たないUI更新を実行中");

            // CPU処理だけを別スレッドへ退避
            await Task.Run(() =>
            {
                for (var i = 1; i <= 10; i++)
                {
                    var progress = i * 10;
                    // UI更新だけを投げて背景側は待機継続なし
                    BeginInvoke(new Action(() =>
                    {
                        AppendLog($"BeginInvoke更新: {progress}%");
                        ReportProgress(progress);
                    }));
                    Thread.Sleep(150);
                }
            });

            SetStatus("完了");
        }

        /// <summary>
        /// ログ消去
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント引数</param>
        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            AppendLog("ログをクリア");
            SetStatus("待機");
        }
    }
}
