using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsUiFreezeSample
{
    /// <summary>
    /// UI応答遅延監視クラス
    /// </summary>
    public sealed class UiLagMonitor : IDisposable
    {
        private readonly Control uiControl;
        private readonly Func<string> getLastAction;
        private readonly int thresholdMs;
        private readonly Action<string> logger;
        private readonly System.Threading.Timer timer;
        private long sequence;

        /// <summary>
        /// 監視初期化
        /// </summary>
        /// <param name="uiControl">監視対象UIコントロール</param>
        /// <param name="getLastAction">直近操作取得処理</param>
        /// <param name="intervalMs">監視頻度ミリ秒</param>
        /// <param name="thresholdMs">遅延判定しきい値ミリ秒</param>
        /// <param name="logger">ログ出力先</param>
        public UiLagMonitor(Control uiControl, Func<string> getLastAction, int intervalMs, int thresholdMs, Action<string> logger)
        {
            this.uiControl = uiControl;
            this.getLastAction = getLastAction;
            this.thresholdMs = thresholdMs;
            this.logger = logger;
            timer = new System.Threading.Timer(OnTimer, null, 0, intervalMs);
        }

        /// <summary>
        /// 監視タイマー処理
        /// </summary>
        /// <param name="state">タイマー状態</param>
        private void OnTimer(object? state)
        {
            if (uiControl.IsDisposed || !uiControl.IsHandleCreated)
            {
                return;
            }

            var id = Interlocked.Increment(ref sequence);
            // BeginInvoke投入時点と実行時点の差分計測
            var start = Stopwatch.GetTimestamp();

            try
            {
                uiControl.BeginInvoke(new Action(() =>
                {
                    var end = Stopwatch.GetTimestamp();
                    var elapsedMs = (end - start) * 1000.0 / Stopwatch.Frequency;

                    // UIメッセージ処理滞留時のみ記録
                    if (elapsedMs >= thresholdMs)
                    {
                        logger($"ui_lag seq={id} lag={elapsedMs:F0}ms action={getLastAction()}");
                    }
                }));
            }
            catch (ObjectDisposedException)
            {
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// <summary>
        /// 監視終了処理
        /// </summary>
        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
