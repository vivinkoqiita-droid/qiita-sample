using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace GcAllocationLifetimeSizeSample
{
    /// <summary>
    /// GC・割り当て・寿命・サイズの観測画面。
    /// </summary>
    public partial class MainForm : Form
    {
        private const int LargeObjectThreshold = 85000;
        private const long SafetyHeldBytes = 200L * 1024L * 1024L;

        private readonly object _heldLock = new();
        private readonly List<TrackableBuffer> _heldBuffers = new();
        private readonly System.Windows.Forms.Timer _uiTimer = new();
        private readonly Queue<DeltaSample> _recentSamples = new();

        private CancellationTokenSource? _workerCts;
        private Task? _workerTask;
        private Snapshot _lastUiSnapshot = Snapshot.Empty;
        private Snapshot _sessionBaseline = Snapshot.Empty;
        private string _lastAction = "まだ開始していません";

        private MetricCard _cardAlloc = null!;
        private MetricCard _cardLifetime = null!;
        private MetricCard _cardLarge = null!;
        private MetricCard _cardMemory = null!;
        private MetricCard _cardGen0 = null!;
        private MetricCard _cardGen2 = null!;
        private MetricCard _cardFinalized = null!;
        private MetricCard _cardHeld = null!;


        /// <summary>
        /// 直近ウィンドウ保持サンプル。
        /// </summary>
        private readonly record struct DeltaSample(DateTime At, long CreatedDelta, long ReleaseRequestedDelta, long FinalizedDelta, long LargeAllocDelta, long MemoryDeltaBytes, int Gen0Delta, int Gen1Delta, int Gen2Delta);

        /// <summary>
        /// 直近ウィンドウ集計値。
        /// </summary>
        private readonly record struct RecentAggregate(long CreatedCount, long ReleaseRequestedCount, long FinalizedCount, long LargeAllocCount, long MemoryDeltaBytes, int Gen0Count, int Gen1Count, int Gen2Count);
        /// <summary>
        /// シナリオ種別。
        /// </summary>
        private enum ScenarioKind
        {
            StringConcat,
            SmallAlloc,
            LongLivedHold,
            LargeAlloc,
            LargeHold
        }

        /// <summary>
        /// 実行条件。
        /// </summary>
        private readonly record struct ScenarioConfig(ScenarioKind Kind, int LoopCount, int BufferSize, bool KeepReferences);

        /// <summary>
        /// シナリオ一覧用項目。
        /// </summary>
        private sealed record ScenarioItem(string Text, ScenarioKind Value)
        {
            /// <inheritdoc />
            public override string ToString() => Text;
        }

        /// <summary>
        /// 観測スナップショット。
        /// </summary>
        private sealed class Snapshot
        {
            /// <summary>
            /// 空スナップショット。
            /// </summary>
            public static Snapshot Empty { get; } = new();

            /// <summary>生成累計。</summary>
            public long CreatedCount { get; init; }

            /// <summary>参照解除依頼累計。</summary>
            public long ReleaseRequestedCount { get; init; }

            /// <summary>回収完了累計。</summary>
            public long FinalizedCount { get; init; }

            /// <summary>大きい確保累計。</summary>
            public long LargeAllocCount { get; init; }

            /// <summary>現在総メモリ。</summary>
            public long TotalMemoryBytes { get; init; }

            /// <summary>Gen0 回数累計。</summary>
            public int Gen0Count { get; init; }

            /// <summary>Gen1 回数累計。</summary>
            public int Gen1Count { get; init; }

            /// <summary>Gen2 回数累計。</summary>
            public int Gen2Count { get; init; }

            /// <summary>保持件数。</summary>
            public long HeldCount { get; init; }

            /// <summary>保持バイト数。</summary>
            public long HeldBytes { get; init; }
        }

        /// <summary>
        /// 追跡対象バッファ。
        /// </summary>
        private sealed class TrackableBuffer
        {
            /// <summary>
            /// バッファサイズ。
            /// </summary>
            public int Size { get; }

            /// <summary>
            /// 実バッファ。
            /// </summary>
            public byte[] Buffer { get; }

            /// <summary>
            /// 初期化処理。
            /// </summary>
            /// <param name="size">確保サイズ。</param>
            public TrackableBuffer(int size)
            {
                Size = size;
                Buffer = new byte[size];
                Buffer[0] = 1;
                Metrics.IncrementCreated();

                if (size >= LargeObjectThreshold)
                {
                    Metrics.IncrementLargeAlloc();
                }
            }

            /// <summary>
            /// 回収完了通知。
            /// </summary>
            ~TrackableBuffer()
            {
                Metrics.IncrementFinalized();
            }
        }

        /// <summary>
        /// 観測カウンタ。
        /// </summary>
        private static class Metrics
        {
            private static long _createdCount;
            private static long _releaseRequestedCount;
            private static long _finalizedCount;
            private static long _largeAllocCount;

            /// <summary>生成累計取得。</summary>
            public static long CreatedCount => Interlocked.Read(ref _createdCount);

            /// <summary>参照解除依頼累計取得。</summary>
            public static long ReleaseRequestedCount => Interlocked.Read(ref _releaseRequestedCount);

            /// <summary>回収完了累計取得。</summary>
            public static long FinalizedCount => Interlocked.Read(ref _finalizedCount);

            /// <summary>大きい確保累計取得。</summary>
            public static long LargeAllocCount => Interlocked.Read(ref _largeAllocCount);

            /// <summary>
            /// 生成数加算。
            /// </summary>
            /// <param name="count">加算件数。</param>
            public static void IncrementCreated(long count = 1) => Interlocked.Add(ref _createdCount, count);

            /// <summary>
            /// 参照解除依頼数加算。
            /// </summary>
            /// <param name="count">加算件数。</param>
            public static void IncrementReleaseRequested(long count) => Interlocked.Add(ref _releaseRequestedCount, count);

            /// <summary>
            /// 回収完了数加算。
            /// </summary>
            public static void IncrementFinalized() => Interlocked.Increment(ref _finalizedCount);

            /// <summary>
            /// 大きい確保数加算。
            /// </summary>
            /// <param name="count">加算件数。</param>
            public static void IncrementLargeAlloc(long count = 1) => Interlocked.Add(ref _largeAllocCount, count);

            /// <summary>
            /// 全カウンタ初期化。
            /// </summary>
            public static void Reset()
            {
                Interlocked.Exchange(ref _createdCount, 0);
                Interlocked.Exchange(ref _releaseRequestedCount, 0);
                Interlocked.Exchange(ref _finalizedCount, 0);
                Interlocked.Exchange(ref _largeAllocCount, 0);
            }
        }

        /// <summary>
        /// カード参照セット。
        /// </summary>
        private sealed class MetricCard
        {
            /// <summary>
            /// 初期化処理。
            /// </summary>
            /// <param name="panel">背景パネル。</param>
            /// <param name="titleLabel">タイトル表示。</param>
            /// <param name="valueLabel">値表示。</param>
            /// <param name="noteLabel">補足表示。</param>
            public MetricCard(Panel panel, Label titleLabel, Label valueLabel, Label noteLabel)
            {
                Panel = panel;
                TitleLabel = titleLabel;
                ValueLabel = valueLabel;
                NoteLabel = noteLabel;
            }

            /// <summary>背景パネル。</summary>
            public Panel Panel { get; }

            /// <summary>タイトル表示。</summary>
            public Label TitleLabel { get; }

            /// <summary>値表示。</summary>
            public Label ValueLabel { get; }

            /// <summary>補足表示。</summary>
            public Label NoteLabel { get; }
        }

        /// <summary>
        /// 初期化処理。
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            BindMetricCards();
            InitializeScenarioItems();
            ConfigureTheme();

            _uiTimer.Interval = 200;
            _uiTimer.Tick += UiTimer_Tick;

            ResetAll();
        }

        /// <summary>
        /// カード参照ひも付け。
        /// </summary>
        private void BindMetricCards()
        {
            _cardAlloc = new MetricCard(pnlAllocCard, lblAllocTitle, lblAllocValue, lblAllocNote);
            _cardLifetime = new MetricCard(pnlLifetimeCard, lblLifetimeTitle, lblLifetimeValue, lblLifetimeNote);
            _cardLarge = new MetricCard(pnlLargeCard, lblLargeTitle, lblLargeValue, lblLargeNote);
            _cardMemory = new MetricCard(pnlMemoryCard, lblMemoryTitle, lblMemoryValue, lblMemoryNote);
            _cardGen0 = new MetricCard(pnlGen0Card, lblGen0Title, lblGen0Value, lblGen0Note);
            _cardGen2 = new MetricCard(pnlGen2Card, lblGen2Title, lblGen2Value, lblGen2Note);
            _cardFinalized = new MetricCard(pnlFinalizedCard, lblFinalizedTitle, lblFinalizedValue, lblFinalizedNote);
            _cardHeld = new MetricCard(pnlHeldCard, lblHeldTitle, lblHeldValue, lblHeldNote);
        }

        /// <summary>
        /// テーマ適用処理。
        /// </summary>
        private void ConfigureTheme()
        {
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        }

        /// <summary>
        /// シナリオ一覧初期化。
        /// </summary>
        private void InitializeScenarioItems()
        {
            cboScenario.Items.Clear();
            cboScenario.Items.Add(new ScenarioItem("文字列連結で細かく作る", ScenarioKind.StringConcat));
            cboScenario.Items.Add(new ScenarioItem("小さいオブジェクトを毎回確保", ScenarioKind.SmallAlloc));
            cboScenario.Items.Add(new ScenarioItem("長く残るように保持し続ける", ScenarioKind.LongLivedHold));
            cboScenario.Items.Add(new ScenarioItem("大きい配列を毎回確保", ScenarioKind.LargeAlloc));
            cboScenario.Items.Add(new ScenarioItem("大きい配列を保持し続ける", ScenarioKind.LargeHold));
            cboScenario.SelectedIndex = 0;
        }

        /// <summary>
        /// 読み込み時処理。
        /// </summary>
        private void MainForm_Load(object? sender, EventArgs e)
        {
            UpdateScenarioGuide();
        }

        /// <summary>
        /// シナリオ変更時処理。
        /// </summary>
        private void cboScenario_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateScenarioGuide();
        }

        /// <summary>
        /// 開始ボタン処理。
        /// </summary>
        private void btnStart_Click(object? sender, EventArgs e)
        {
            if (_workerTask is { IsCompleted: false })
            {
                return;
            }

            var config = GetCurrentConfig();
            _workerCts = new CancellationTokenSource();
            _workerTask = Task.Run(() => RunScenarioLoop(config, _workerCts.Token), _workerCts.Token);
            _uiTimer.Start();
            SetRunningState(true);
            _lastAction = $"開始: {GetScenarioText(config.Kind)}";
            lblStatus.Text = "実行中";
            AppendLog($"開始: {GetScenarioText(config.Kind)} / 強さ={config.LoopCount:n0} / サイズ={config.BufferSize:n0}");
        }

        /// <summary>
        /// 停止ボタン処理。
        /// </summary>
        private async void btnStop_Click(object? sender, EventArgs e)
        {
            await StopWorkerAsync();
            _lastAction = "停止";
            lblStatus.Text = "停止中";
            AppendLog("停止");
        }

        /// <summary>
        /// 保持クリア処理。
        /// </summary>
        private void btnClearHeld_Click(object? sender, EventArgs e)
        {
            long releasedCount;
            long releasedBytes;

            lock (_heldLock)
            {
                releasedCount = _heldBuffers.Count;
                releasedBytes = _heldBuffers.Sum(x => (long)x.Size);
                _heldBuffers.Clear();
            }

            if (releasedCount > 0)
            {
                Metrics.IncrementReleaseRequested(releasedCount);
            }

            _lastAction = "保持クリア";
            lblStatus.Text = "保持参照を外しました";
            AppendLog($"保持クリア: {releasedCount:n0} 件 / {FormatMb(releasedBytes)}");
            RefreshNow();
        }

        /// <summary>
        /// 強制GC処理。
        /// </summary>
        private void btnForceGc_Click(object? sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            _lastAction = "GC強制実行";
            lblStatus.Text = "GC強制実行を入れました";
            AppendLog("GC強制実行");
            RefreshNow();
        }

        /// <summary>
        /// リセット処理。
        /// </summary>
        private async void btnReset_Click(object? sender, EventArgs e)
        {
            await StopWorkerAsync();
            ResetAll();
        }

        /// <summary>
        /// 全状態初期化。
        /// </summary>
        private void ResetAll()
        {
            lock (_heldLock)
            {
                _heldBuffers.Clear();
            }

            Metrics.Reset();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            txtLog.Clear();
            _recentSamples.Clear();
            _lastAction = "リセット";
            lblStatus.Text = "待機中";
            _sessionBaseline = TakeSnapshot();
            _lastUiSnapshot = _sessionBaseline;
            SetRunningState(false);
            UpdateScenarioGuide();
            var recent = new RecentAggregate(0, 0, 0, 0, 0, 0, 0, 0);
            UpdateCards(_sessionBaseline, _sessionBaseline, recent);
            UpdateNarrative(_sessionBaseline, _sessionBaseline, recent);
            AppendLog("リセット完了");
        }

        /// <summary>
        /// ワーカー停止処理。
        /// </summary>
        private async Task StopWorkerAsync()
        {
            if (_workerCts == null)
            {
                SetRunningState(false);
                return;
            }

            _workerCts.Cancel();

            try
            {
                if (_workerTask != null)
                {
                    await _workerTask;
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                AppendLog($"停止時例外: {ex.Message}");
            }
            finally
            {
                _workerCts.Dispose();
                _workerCts = null;
                _workerTask = null;
                _uiTimer.Stop();
                SetRunningState(false);
                RefreshNow();
            }
        }

        /// <summary>
        /// 実行中状態反映。
        /// </summary>
        /// <param name="isRunning">実行中フラグ。</param>
        private void SetRunningState(bool isRunning)
        {
            btnStart.Enabled = !isRunning;
            btnStop.Enabled = isRunning;
            cboScenario.Enabled = !isRunning;
            nudLoopCount.Enabled = !isRunning;
            nudBufferSize.Enabled = !isRunning;
            chkKeepReferences.Enabled = !isRunning;
        }

        /// <summary>
        /// 現在条件取得。
        /// </summary>
        /// <returns>実行条件。</returns>
        private ScenarioConfig GetCurrentConfig()
        {
            var item = cboScenario.SelectedItem as ScenarioItem ?? new ScenarioItem("文字列連結で細かく作る", ScenarioKind.StringConcat);
            return new ScenarioConfig(item.Value, (int)nudLoopCount.Value, (int)nudBufferSize.Value, chkKeepReferences.Checked);
        }

        /// <summary>
        /// シナリオ連続実行処理。
        /// </summary>
        /// <param name="config">実行条件。</param>
        /// <param name="token">停止トークン。</param>
        private async Task RunScenarioLoop(ScenarioConfig config, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                switch (config.Kind)
                {
                    case ScenarioKind.StringConcat:
                        RunStringConcatBatch(config.LoopCount);
                        break;
                    case ScenarioKind.SmallAlloc:
                        RunSmallAllocBatch(config.LoopCount, config.KeepReferences);
                        break;
                    case ScenarioKind.LongLivedHold:
                        RunLongLivedBatch(config.LoopCount, Math.Min(config.BufferSize, 8192));
                        break;
                    case ScenarioKind.LargeAlloc:
                        RunLargeAllocBatch(config.LoopCount, config.BufferSize, config.KeepReferences);
                        break;
                    case ScenarioKind.LargeHold:
                        RunLargeHoldBatch(config.LoopCount, config.BufferSize);
                        break;
                }

                if (GetHeldBytes() > SafetyHeldBytes)
                {
                    BeginInvoke(new Action(() =>
                    {
                        lblStatus.Text = "安全停止: 保持量が増えすぎました";
                        AppendLog("安全停止: 保持量が 200 MB を超えたため停止");
                    }));
                    _workerCts?.Cancel();
                    break;
                }

                await Task.Delay(200, token);
            }
        }

        /// <summary>
        /// 文字列連結バッチ。
        /// </summary>
        /// <param name="loopCount">強さ。</param>
        private static void RunStringConcatBatch(int loopCount)
        {
            var outer = Math.Max(10, Math.Min(loopCount / 150, 240));
            for (var i = 0; i < outer; i++)
            {
                var s = string.Empty;
                for (var j = 0; j < 40; j++)
                {
                    s += j.ToString();
                }

                Metrics.IncrementCreated(40);
                GC.KeepAlive(s);
            }
        }

        /// <summary>
        /// 小さい確保バッチ。
        /// </summary>
        /// <param name="loopCount">強さ。</param>
        /// <param name="keepReferences">保持フラグ。</param>
        private void RunSmallAllocBatch(int loopCount, bool keepReferences)
        {
            var count = Math.Max(20, Math.Min(loopCount / 60, 320));
            for (var i = 0; i < count; i++)
            {
                var buffer = new TrackableBuffer(1024);
                if (keepReferences)
                {
                    lock (_heldLock)
                    {
                        _heldBuffers.Add(buffer);
                    }
                }
            }
        }

        /// <summary>
        /// 長寿命保持バッチ。
        /// </summary>
        /// <param name="loopCount">強さ。</param>
        /// <param name="bufferSize">確保サイズ。</param>
        private void RunLongLivedBatch(int loopCount, int bufferSize)
        {
            var count = Math.Max(2, Math.Min(loopCount / 500, 32));
            lock (_heldLock)
            {
                for (var i = 0; i < count; i++)
                {
                    _heldBuffers.Add(new TrackableBuffer(bufferSize));
                }
            }
        }

        /// <summary>
        /// 大きい確保バッチ。
        /// </summary>
        /// <param name="loopCount">強さ。</param>
        /// <param name="bufferSize">確保サイズ。</param>
        /// <param name="keepReferences">保持フラグ。</param>
        private void RunLargeAllocBatch(int loopCount, int bufferSize, bool keepReferences)
        {
            var count = Math.Max(1, Math.Min(loopCount / 3000, 18));
            for (var i = 0; i < count; i++)
            {
                var buffer = new TrackableBuffer(bufferSize);
                if (keepReferences)
                {
                    lock (_heldLock)
                    {
                        _heldBuffers.Add(buffer);
                    }
                }
            }
        }

        /// <summary>
        /// 大きい保持バッチ。
        /// </summary>
        /// <param name="loopCount">強さ。</param>
        /// <param name="bufferSize">確保サイズ。</param>
        private void RunLargeHoldBatch(int loopCount, int bufferSize)
        {
            var count = Math.Max(1, Math.Min(loopCount / 5000, 10));
            lock (_heldLock)
            {
                for (var i = 0; i < count; i++)
                {
                    _heldBuffers.Add(new TrackableBuffer(bufferSize));
                }
            }
        }

        /// <summary>
        /// タイマー更新処理。
        /// </summary>
        private void UiTimer_Tick(object? sender, EventArgs e)
        {
            RefreshNow();
        }

        /// <summary>
        /// 画面更新処理。
        /// </summary>
        private void RefreshNow()
        {
            var current = TakeSnapshot();
            var recent = CaptureRecentAggregate(_lastUiSnapshot, current);
            UpdateCards(_lastUiSnapshot, current, recent);
            UpdateNarrative(_lastUiSnapshot, current, recent);
            AppendLiveSample(_lastUiSnapshot, current, recent);
            _lastUiSnapshot = current;
        }

        /// <summary>
        /// 直近3秒の集計値取得。
        /// </summary>
        /// <param name="previous">前回値。</param>
        /// <param name="current">今回値。</param>
        /// <returns>直近集計値。</returns>
        private RecentAggregate CaptureRecentAggregate(Snapshot previous, Snapshot current)
        {
            var now = DateTime.Now;
            _recentSamples.Enqueue(new DeltaSample(
                now,
                current.CreatedCount - previous.CreatedCount,
                current.ReleaseRequestedCount - previous.ReleaseRequestedCount,
                current.FinalizedCount - previous.FinalizedCount,
                current.LargeAllocCount - previous.LargeAllocCount,
                current.TotalMemoryBytes - previous.TotalMemoryBytes,
                current.Gen0Count - previous.Gen0Count,
                current.Gen1Count - previous.Gen1Count,
                current.Gen2Count - previous.Gen2Count));

            var limit = now.AddSeconds(-3);
            while (_recentSamples.Count > 0 && _recentSamples.Peek().At < limit)
            {
                _recentSamples.Dequeue();
            }

            long created = 0;
            long release = 0;
            long finalized = 0;
            long large = 0;
            long memory = 0;
            int gen0 = 0;
            int gen1 = 0;
            int gen2 = 0;

            foreach (var sample in _recentSamples)
            {
                created += sample.CreatedDelta;
                release += sample.ReleaseRequestedDelta;
                finalized += sample.FinalizedDelta;
                large += sample.LargeAllocDelta;
                memory += sample.MemoryDeltaBytes;
                gen0 += sample.Gen0Delta;
                gen1 += sample.Gen1Delta;
                gen2 += sample.Gen2Delta;
            }

            return new RecentAggregate(created, release, finalized, large, memory, gen0, gen1, gen2);
        }

        /// <summary>
        /// 現在値取得。
        /// </summary>
        /// <returns>観測値スナップショット。</returns>
        private Snapshot TakeSnapshot()
        {
            long heldCount;
            long heldBytes;

            lock (_heldLock)
            {
                heldCount = _heldBuffers.Count;
                heldBytes = _heldBuffers.Sum(x => (long)x.Size);
            }

            return new Snapshot
            {
                CreatedCount = Metrics.CreatedCount,
                ReleaseRequestedCount = Metrics.ReleaseRequestedCount,
                FinalizedCount = Metrics.FinalizedCount,
                LargeAllocCount = Metrics.LargeAllocCount,
                TotalMemoryBytes = GC.GetTotalMemory(false),
                Gen0Count = GC.CollectionCount(0),
                Gen1Count = GC.CollectionCount(1),
                Gen2Count = GC.CollectionCount(2),
                HeldCount = heldCount,
                HeldBytes = heldBytes
            };
        }

        /// <summary>
        /// カード表示更新。
        /// </summary>
        /// <param name="previous">前回値。</param>
        /// <param name="current">今回値。</param>
        private void UpdateCards(Snapshot previous, Snapshot current, RecentAggregate recent)
        {
            var memoryDelta = current.TotalMemoryBytes - previous.TotalMemoryBytes;
            var baselineMemoryDelta = current.TotalMemoryBytes - _sessionBaseline.TotalMemoryBytes;
            var config = GetCurrentConfig();

            UpdateMetricCard(_cardAlloc, $"{recent.CreatedCount:n0} 件 / 3秒", recent.CreatedCount > 0 ? "Allocation Rate 相当" : "いまは落ち着いています", TrendFromDelta(recent.CreatedCount));
            UpdateMetricCard(_cardLifetime, $"{current.HeldCount:n0} 件", $"GC Heap Size 相当 / 保持 {FormatMb(current.HeldBytes)}", current.HeldCount > 0 ? TrendKind.Warning : TrendKind.Good);
            UpdateMetricCard(_cardLarge, $"{recent.LargeAllocCount:n0} 件 / 3秒", recent.LargeAllocCount > 0 ? "LOH Size 相当" : "大きい確保は少ない状態", recent.LargeAllocCount > 0 ? TrendKind.Danger : TrendKind.Stable);
            UpdateMetricCard(_cardMemory, FormatMb(current.TotalMemoryBytes), $"開始時から {FormatSignedMb(baselineMemoryDelta)}", TrendFromMemory(memoryDelta, current.HeldCount));

            if (config.Kind == ScenarioKind.LongLivedHold || config.Kind == ScenarioKind.LargeHold)
            {
                var retainTrend = current.HeldCount > 0 ? TrendKind.Danger : TrendKind.Good;
                var resultText = BuildRetentionResultText(current, recent);
                UpdateMetricCard(_cardGen0, current.HeldCount > 0 ? "あり" : "なし", current.HeldCount > 0 ? "回収を妨げる参照" : "回収の余地あり", retainTrend);
                UpdateMetricCard(_cardGen2, $"{recent.Gen2Count:n0} 回 / 3秒", $"補助指標 / 累計 {current.Gen2Count:n0} 回", recent.Gen2Count > 0 ? TrendKind.Warning : TrendKind.Stable);
                UpdateMetricCard(_cardFinalized, $"{recent.FinalizedCount:n0} 件 / 3秒", resultText, recent.FinalizedCount > 0 ? TrendKind.Good : TrendKind.Stable);
                UpdateMetricCard(_cardHeld, $"{current.HeldCount:n0} 件 / {FormatMb(current.HeldBytes)}", $"参照解除依頼 累計 {current.ReleaseRequestedCount:n0} 件", retainTrend);
                return;
            }

            UpdateMetricCard(_cardGen0, $"{recent.Gen0Count:n0} 回 / 3秒", $"Gen 0 GC Count 相当 / 累計 {current.Gen0Count:n0} 回", recent.Gen0Count > 0 ? TrendKind.Warning : TrendKind.Stable);
            UpdateMetricCard(_cardGen2, $"{recent.Gen2Count:n0} 回 / 3秒", $"Gen 2 GC Count 相当 / 累計 {current.Gen2Count:n0} 回", recent.Gen2Count > 0 ? TrendKind.Danger : TrendKind.Stable);
            UpdateMetricCard(_cardFinalized, $"{recent.FinalizedCount:n0} 件 / 3秒", $"累計 {current.FinalizedCount:n0} 件", recent.FinalizedCount > 0 ? TrendKind.Good : TrendKind.Stable);
            UpdateMetricCard(_cardHeld, $"{current.HeldCount:n0} 件 / {FormatMb(current.HeldBytes)}", $"参照解除依頼 累計 {current.ReleaseRequestedCount:n0} 件", current.HeldCount > 0 ? TrendKind.Warning : TrendKind.Good);
        }

        /// <summary>
        /// 保持系シナリオの結果文生成。
        /// </summary>
        private string BuildRetentionResultText(Snapshot current, RecentAggregate recent)
        {
            if (_lastAction == "GC強制実行" && current.HeldCount > 0)
            {
                return "GCは動いたが保持中件数が残る";
            }

            if (_lastAction == "保持クリア" && current.HeldCount == 0)
            {
                return "保持は外れた / 次の回収待ち";
            }

            if (recent.FinalizedCount > 0)
            {
                return "参照が外れた後に回収が進んだ";
            }

            return current.HeldCount > 0 ? "参照が残るため戻りにくい" : "いまは大きな変化なし";
        }

        /// <summary>
        /// 説明表示更新。
        /// </summary>
        /// <param name="previous">前回値。</param>
        /// <param name="current">今回値。</param>
        private void UpdateNarrative(Snapshot previous, Snapshot current, RecentAggregate recent)
        {
            var config = GetCurrentConfig();
            var memoryDelta = current.TotalMemoryBytes - previous.TotalMemoryBytes;

            if (config.Kind == ScenarioKind.LongLivedHold || config.Kind == ScenarioKind.LargeHold)
            {
                SetScrollableText(txtDiagnosis, BuildRetentionDiagnosisText(current, recent, memoryDelta));
                SetScrollableText(txtSuspects, BuildRetentionSuspectText(current, recent));
                return;
            }

            var diagnosis = new StringBuilder();
            diagnosis.AppendLine($"直近の操作: {_lastAction}");
            diagnosis.AppendLine();

            if (recent.LargeAllocCount > 0)
            {
                diagnosis.AppendLine("大きい配列が続いています。サイズ起因を先に疑う流れです。");
            }
            else if (current.HeldCount > 0 || recent.Gen2Count > 0)
            {
                diagnosis.AppendLine("作ったものが残りやすい状態です。長く残る側を先に見る流れです。");
            }
            else if (recent.CreatedCount > 0 || recent.Gen0Count > 0)
            {
                diagnosis.AppendLine("細かく作っては捨てる流れです。割り当て量が多い側を先に見ます。");
            }
            else
            {
                diagnosis.AppendLine("大きな変化は出ていません。開始して数秒流すと差が見やすくなります。");
            }

            diagnosis.AppendLine();
            diagnosis.AppendLine($"・作られる量: {recent.CreatedCount:n0} 件 / 3秒");
            diagnosis.AppendLine($"・長く残る量: 保持 {current.HeldCount:n0} 件 / 総メモリ {FormatSignedMb(memoryDelta)}");
            diagnosis.AppendLine($"・大きい確保: {recent.LargeAllocCount:n0} 件 / 3秒");
            diagnosis.AppendLine($"・GCの出方: 短いGC {recent.Gen0Count:n0} 回 / 重いGC {recent.Gen2Count:n0} 回 / 回収完了 {recent.FinalizedCount:n0} 件");
            SetScrollableText(txtDiagnosis, diagnosis.ToString());

            SetScrollableText(txtSuspects, BuildSuspectText(config.Kind, current, recent.Gen0Count, recent.Gen2Count, recent.LargeAllocCount));
        }

        /// <summary>
        /// スクロール位置を保ちながら説明文を更新。
        /// </summary>
        /// <param name="box">更新対象。</param>
        /// <param name="text">新しい本文。</param>
        private void SetScrollableText(RichTextBox box, string text)
        {
            if (box.Text == text)
            {
                return;
            }

            var firstVisibleLine = NativeMethods.GetFirstVisibleLine(box);
            var selectionStart = box.SelectionStart;
            var selectionLength = box.SelectionLength;

            box.SuspendLayout();
            box.Text = text;

            if (selectionStart > box.TextLength)
            {
                selectionStart = box.TextLength;
                selectionLength = 0;
            }

            box.SelectionStart = selectionStart;
            box.SelectionLength = selectionLength;
            NativeMethods.SetFirstVisibleLine(box, firstVisibleLine);
            box.ResumeLayout();
        }

        /// <summary>
        /// RichTextBox 操作用ネイティブ呼び出し。
        /// </summary>
        private static class NativeMethods
        {
            private const int EmGetFirstVisibleLine = 0x00CE;
            private const int EmLineScroll = 0x00B6;

            /// <summary>
            /// 先頭表示行取得。
            /// </summary>
            /// <param name="box">対象コントロール。</param>
            /// <returns>先頭表示行。</returns>
            public static int GetFirstVisibleLine(RichTextBox box)
            {
                return (int)SendMessage(box.Handle, EmGetFirstVisibleLine, IntPtr.Zero, IntPtr.Zero);
            }

            /// <summary>
            /// 先頭表示行復元。
            /// </summary>
            /// <param name="box">対象コントロール。</param>
            /// <param name="targetLine">復元先行番号。</param>
            public static void SetFirstVisibleLine(RichTextBox box, int targetLine)
            {
                var currentLine = GetFirstVisibleLine(box);
                var delta = targetLine - currentLine;

                if (delta != 0)
                {
                    SendMessage(box.Handle, EmLineScroll, IntPtr.Zero, (IntPtr)delta);
                }
            }

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        }

        /// <summary>
        /// 強制GCでも戻りにくいケースの説明文生成。
        /// </summary>
        private string BuildRetentionDiagnosisText(Snapshot current, RecentAggregate recent, long memoryDelta)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"直近の操作: {_lastAction}");
            sb.AppendLine();

            if (_lastAction == "GC強制実行" && current.HeldCount > 0)
            {
                sb.AppendLine("GCは動いています。ですが保持中件数が残っているため、回収を妨げる参照がまだあります。");
                sb.AppendLine("いま見るべきものは重いGCの回数ではなく、保持中件数と総メモリが下がるかどうかです。");
            }
            else if (_lastAction == "保持クリア" && current.HeldCount == 0)
            {
                sb.AppendLine("保持参照は外れました。まだ即回収ではありませんが、ここからGCで戻る余地が生まれます。");
            }
            else if (current.HeldCount > 0)
            {
                sb.AppendLine("参照を持ち続けているので、強制GCだけでは戻りにくい状態です。");
            }
            else if (recent.FinalizedCount > 0 || memoryDelta < 0)
            {
                sb.AppendLine("参照が外れた後に回収が進んでいます。GC自体より保持の有無が効いていた可能性が高いです。");
            }
            else
            {
                sb.AppendLine("まず保持中件数を増やしてから、強制GCと保持クリアの違いを見ます。");
            }

            sb.AppendLine();
            sb.AppendLine($"・保持中件数: {current.HeldCount:n0} 件");
            sb.AppendLine($"・総メモリ現在値: {FormatMb(current.TotalMemoryBytes)}");
            sb.AppendLine($"・回収完了: {recent.FinalizedCount:n0} 件 / 3秒");
            sb.AppendLine($"・重いGC: {recent.Gen2Count:n0} 回 / 3秒（補助指標）");
            return sb.ToString();
        }

        /// <summary>
        /// 強制GCでも戻りにくいケースの疑い箇所生成。
        /// </summary>
        private static string BuildRetentionSuspectText(Snapshot current, RecentAggregate recent)
        {
            var sb = new StringBuilder();
            sb.AppendLine("最初に見る項目");
            sb.AppendLine("・GC Heap Size 相当");
            sb.AppendLine("・Gen 2 GC Count 相当");
            sb.AppendLine();
            sb.AppendLine("この状態から疑う場所");
            sb.AppendLine("・static 参照");
            sb.AppendLine("・キャッシュ上限なし");
            sb.AppendLine("・イベント解除漏れ");
            sb.AppendLine("・タイマー / コールバック保持");
            sb.AppendLine();

            if (current.HeldCount > 0)
            {
                sb.AppendLine("いま伝えたいこと");
                sb.AppendLine("・GCを呼んでも、参照が残っているものは回収できない");
                sb.AppendLine($"・保持中件数が {current.HeldCount:n0} 件あるため、いまは戻りにくい状態");
            }
            else if (recent.FinalizedCount > 0)
            {
                sb.AppendLine("いま伝えたいこと");
                sb.AppendLine("・参照を外したあとに回収が進んだ");
                sb.AppendLine("・GC不足ではなく、保持の有無が主因だった可能性");
            }
            else
            {
                sb.AppendLine("いま伝えたいこと");
                sb.AppendLine("・保持をクリアしてから強制GCすると、変化の意味が見やすい");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 時系列ログ追加。
        /// </summary>
        /// <param name="previous">前回値。</param>
        /// <param name="current">今回値。</param>
        private void AppendLiveSample(Snapshot previous, Snapshot current, RecentAggregate recent)
        {
            var config = GetCurrentConfig();
            var memoryDelta = current.TotalMemoryBytes - previous.TotalMemoryBytes;
            string line;

            if (config.Kind == ScenarioKind.LongLivedHold || config.Kind == ScenarioKind.LargeHold)
            {
                line = $"[{DateTime.Now:HH:mm:ss}] 保持 {current.HeldCount,5:n0} / {FormatMb(current.HeldBytes),8} / 回収妨げ {(current.HeldCount > 0 ? "あり" : "なし"),2} / 総メモリ {FormatSignedMb(memoryDelta),8} / 重いGC {recent.Gen2Count,2:n0} / 回収 {recent.FinalizedCount,3:n0}";
            }
            else
            {
                line = $"[{DateTime.Now:HH:mm:ss}] 作る {recent.CreatedCount,5:n0} / 保持 {current.HeldCount,5:n0} / 大きい {recent.LargeAllocCount,3:n0} / 総メモリ {FormatSignedMb(memoryDelta),8} / G0 {recent.Gen0Count,2:n0} / G2 {recent.Gen2Count,2:n0} / 回収 {recent.FinalizedCount,3:n0}";
            }

            txtLog.AppendText(line + Environment.NewLine);

            const int maxLines = 160;
            var lines = txtLog.Lines;
            if (lines.Length > maxLines)
            {
                txtLog.Lines = lines.Skip(lines.Length - maxLines).ToArray();
            }

            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.ScrollToCaret();
        }

        /// <summary>
        /// シナリオ説明更新。
        /// </summary>
        private void UpdateScenarioGuide()
        {
            var config = GetCurrentConfig();
            ApplyScenarioTitles(config.Kind);
            lblScenarioGuide.Text = config.Kind switch
            {
                ScenarioKind.StringConcat => "ここを見る: 作られる量と短いGCが増えやすいか。文字列連結のように、細かい確保が積み上がるとカクつき側へ寄ります。",
                ScenarioKind.SmallAlloc => "ここを見る: 作られる量は多いが、大きい確保は少ないか。毎回 new するだけで、GCに結果が出ることを見るケースです。",
                ScenarioKind.LongLivedHold => "ここを見る: 保持中件数と総メモリ現在値が下がりにくいか。GCを押しても戻らないなら、GC不足ではなく参照保持を疑うケースです。",
                ScenarioKind.LargeAlloc => "ここを見る: 大きい確保と重いGCが増えやすいか。操作時だけ重くなるサイズ起因の疑いを見ます。",
                ScenarioKind.LargeHold => "ここを見る: 大きい確保に加えて保持中件数も増えるか。サイズと寿命が同時に悪化すると、保持クリア前はGCでも戻りにくくなります。",
                _ => string.Empty
            };
        }

        /// <summary>
        /// シナリオごとのカード見出し反映。
        /// </summary>
        /// <param name="kind">シナリオ種別。</param>
        private void ApplyScenarioTitles(ScenarioKind kind)
        {
            if (kind == ScenarioKind.LongLivedHold || kind == ScenarioKind.LargeHold)
            {
                lblMetricsBottomTitle.Text = "強制GCで戻るかを見る";
                _cardGen0.TitleLabel.Text = "回収妨げ";
                _cardGen2.TitleLabel.Text = "重いGC";
                _cardFinalized.TitleLabel.Text = "強制GCの結果";
                _cardHeld.TitleLabel.Text = "保持中";
                return;
            }

            lblMetricsBottomTitle.Text = "GCにどう出ているか";
            _cardGen0.TitleLabel.Text = "短いGC";
            _cardGen2.TitleLabel.Text = "重いGC";
            _cardFinalized.TitleLabel.Text = "回収完了";
            _cardHeld.TitleLabel.Text = "保持中";
        }

        /// <summary>
        /// 疑う場所メッセージ生成。
        /// </summary>
        /// <param name="kind">シナリオ種別。</param>
        /// <param name="current">今回値。</param>
        /// <param name="gen0Delta">Gen0 差分。</param>
        /// <param name="gen2Delta">Gen2 差分。</param>
        /// <param name="largeDelta">大きい確保差分。</param>
        /// <returns>表示文。</returns>
        private static string BuildSuspectText(ScenarioKind kind, Snapshot current, long gen0Delta, long gen2Delta, long largeDelta)
        {
            var lines = new List<string>();

            if (kind == ScenarioKind.StringConcat || gen0Delta > 0)
            {
                lines.Add("・割り当て多: ループ内 string += / ToList / boxing / params object[]");
            }

            if (kind == ScenarioKind.LongLivedHold || current.HeldCount > 0 || gen2Delta > 0)
            {
                lines.Add("・長く残る: static / キャッシュ上限なし / イベント解除漏れ / タイマー保持");
            }

            if (kind == ScenarioKind.LargeAlloc || kind == ScenarioKind.LargeHold || largeDelta > 0)
            {
                lines.Add("・サイズ起因: 画像 / 圧縮 / 通信バッファ / 一括読込 / 200000 bytes 級の配列");
            }

            if (lines.Count == 0)
            {
                lines.Add("・まだ大きな偏りは出ていません。開始して数秒流すと、どこへ寄るか見やすくなります。");
            }

            lines.Add(string.Empty);
            lines.Add("見方");
            lines.Add("・短いGCが増える: 細かく作っては捨てている");
            lines.Add("・重いGCが増える: 長く残るか大きい確保が混ざる");
            lines.Add("・保持中が残る: 強制GCだけでは戻りきらないことがある");

            return string.Join(Environment.NewLine, lines);
        }

        /// <summary>
        /// 操作ログ追加。
        /// </summary>
        /// <param name="message">表示文。</param>
        private void AppendLog(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.ScrollToCaret();
        }

        /// <summary>
        /// シナリオ名変換。
        /// </summary>
        /// <param name="kind">シナリオ種別。</param>
        /// <returns>表示名。</returns>
        private static string GetScenarioText(ScenarioKind kind)
        {
            return kind switch
            {
                ScenarioKind.StringConcat => "文字列連結で細かく作る",
                ScenarioKind.SmallAlloc => "小さいオブジェクトを毎回確保",
                ScenarioKind.LongLivedHold => "長く残るように保持し続ける",
                ScenarioKind.LargeAlloc => "大きい配列を毎回確保",
                ScenarioKind.LargeHold => "大きい配列を保持し続ける",
                _ => "不明"
            };
        }

        /// <summary>
        /// 傾向種別。
        /// </summary>
        private enum TrendKind
        {
            Stable,
            Good,
            Warning,
            Danger
        }

        /// <summary>
        /// 件数差分からの傾向判定。
        /// </summary>
        /// <param name="delta">差分。</param>
        /// <returns>傾向種別。</returns>
        private static TrendKind TrendFromDelta(long delta)
        {
            if (delta > 0)
            {
                return TrendKind.Warning;
            }

            if (delta < 0)
            {
                return TrendKind.Good;
            }

            return TrendKind.Stable;
        }

        /// <summary>
        /// メモリ差分からの傾向判定。
        /// </summary>
        /// <param name="deltaBytes">メモリ差分。</param>
        /// <param name="heldCount">保持件数。</param>
        /// <returns>傾向種別。</returns>
        private static TrendKind TrendFromMemory(long deltaBytes, long heldCount)
        {
            if (heldCount > 0 && deltaBytes >= 0)
            {
                return TrendKind.Warning;
            }

            if (deltaBytes > 512 * 1024)
            {
                return TrendKind.Danger;
            }

            if (deltaBytes < 0)
            {
                return TrendKind.Good;
            }

            return TrendKind.Stable;
        }

        /// <summary>
        /// カード色と文言の更新。
        /// </summary>
        /// <param name="card">対象カード。</param>
        /// <param name="value">値表示。</param>
        /// <param name="note">補足表示。</param>
        /// <param name="trend">傾向種別。</param>
        private static void UpdateMetricCard(MetricCard card, string value, string note, TrendKind trend)
        {
            card.ValueLabel.Text = value;
            card.NoteLabel.Text = note;

            var (backColor, valueColor, titleColor) = trend switch
            {
                TrendKind.Good => (Color.FromArgb(232, 245, 233), Color.FromArgb(27, 94, 32), Color.FromArgb(46, 125, 50)),
                TrendKind.Warning => (Color.FromArgb(255, 248, 225), Color.FromArgb(183, 28, 28), Color.FromArgb(123, 31, 162)),
                TrendKind.Danger => (Color.FromArgb(255, 235, 238), Color.FromArgb(183, 28, 28), Color.FromArgb(183, 28, 28)),
                _ => (Color.FromArgb(248, 249, 252), Color.FromArgb(24, 39, 75), Color.FromArgb(76, 86, 106))
            };

            card.Panel.BackColor = backColor;
            card.ValueLabel.ForeColor = valueColor;
            card.TitleLabel.ForeColor = titleColor;
        }

        /// <summary>
        /// 保持バイト数取得。
        /// </summary>
        /// <returns>保持バイト数。</returns>
        private long GetHeldBytes()
        {
            lock (_heldLock)
            {
                return _heldBuffers.Sum(x => (long)x.Size);
            }
        }

        /// <summary>
        /// MB 表示変換。
        /// </summary>
        /// <param name="bytes">バイト数。</param>
        /// <returns>MB 表示。</returns>
        private static string FormatMb(long bytes)
        {
            return $"{bytes / 1024d / 1024d:0.00} MB";
        }

        /// <summary>
        /// 符号付き MB 表示変換。
        /// </summary>
        /// <param name="bytes">バイト数。</param>
        /// <returns>符号付き MB 表示。</returns>
        private static string FormatSignedMb(long bytes)
        {
            var mb = bytes / 1024d / 1024d;
            return mb >= 0 ? $"+{mb:0.00} MB" : $"{mb:0.00} MB";
        }
    }
}
