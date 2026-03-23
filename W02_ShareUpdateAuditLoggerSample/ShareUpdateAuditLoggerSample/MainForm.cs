using ShareUpdateAuditLoggerSample.Models;
using ShareUpdateAuditLoggerSample.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareUpdateAuditLoggerSample
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 画面表示用データ一覧
        /// </summary>
        private readonly BindingList<FileAuditLogEntry> _entries = new BindingList<FileAuditLogEntry>();

        /// <summary>
        /// セキュリティイベントログ参照処理
        /// </summary>
        private readonly FileAuditEventLogReader _eventLogReader = new FileAuditEventLogReader();

        /// <summary>
        /// 同一通知の間引き用辞書
        /// </summary>
        private readonly Dictionary<string, DateTime> _recentNotifications = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 同一通知とみなす最小間隔
        /// </summary>
        private readonly TimeSpan _duplicateInterval = TimeSpan.FromMilliseconds(1500);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeDefaultValues();
        }

        /// <summary>
        /// グリッド初期化処理
        /// </summary>
        private void InitializeGrid()
        {
            dgvLog.AutoGenerateColumns = false;
            dgvLog.DataSource = _entries;
        }

        /// <summary>
        /// 画面初期値設定処理
        /// </summary>
        private void InitializeDefaultValues()
        {
            txtTargetFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            txtLookBackSeconds.Text = "30";
            lblStatusValue.Text = "停止中";
            lblStatusDetail.Text = "待機中";
            btnStop.Enabled = false;
        }

        /// <summary>
        /// 監視対象フォルダー参照ボタン押下時処理
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtTargetFolder.Text;

            if (folderBrowserDialog1.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            txtTargetFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        /// <summary>
        /// 監視開始ボタン押下時処理
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            string accessMessage;
            bool canReadSecurityLog = _eventLogReader.TryCheckSecurityLogAccess(out accessMessage);
            lblStatusDetail.Text = accessMessage;

            if (!canReadSecurityLog)
            {
                MessageBox.Show(this, accessMessage, "Security ログ参照確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string targetFolder = txtTargetFolder.Text.Trim();
            int lookBackSeconds;

            if (!Directory.Exists(targetFolder))
            {
                MessageBox.Show(this, "監視対象フォルダーが見つかりません。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtLookBackSeconds.Text.Trim(), out lookBackSeconds) || lookBackSeconds <= 0)
            {
                MessageBox.Show(this, "検索秒数は 1 以上の整数で入力してください。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            fileSystemWatcher1.Path = targetFolder;
            fileSystemWatcher1.EnableRaisingEvents = true;

            txtTargetFolder.Enabled = false;
            txtLookBackSeconds.Enabled = false;
            btnBrowse.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblStatusValue.Text = "監視中";
            lblStatusDetail.Text = "Security ログの 4663 / 4660 を参照します。監査ログ反映待ちのため数秒遅れて表示される場合があります。";
        }

        /// <summary>
        /// 監視停止ボタン押下時処理
        /// </summary>
        private void btnStop_Click(object sender, EventArgs e)
        {
            StopMonitoring();
        }

        /// <summary>
        /// 一覧クリアボタン押下時処理
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            _entries.Clear();
            _recentNotifications.Clear();
            lblStatusDetail.Text = "一覧をクリアしました。";
        }

        /// <summary>
        /// フォーム終了時処理
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopMonitoring();
        }

        /// <summary>
        /// 監視停止処理
        /// </summary>
        private void StopMonitoring()
        {
            fileSystemWatcher1.EnableRaisingEvents = false;
            txtTargetFolder.Enabled = true;
            txtLookBackSeconds.Enabled = true;
            btnBrowse.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatusValue.Text = "停止中";
            lblStatusDetail.Text = "待機中";
        }

        /// <summary>
        /// ファイル変更通知受信時処理
        /// </summary>
        private async void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            await HandleWatcherEventAsync(e.ChangeType, e.FullPath);
        }

        /// <summary>
        /// ファイル作成通知受信時処理
        /// </summary>
        private async void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            await HandleWatcherEventAsync(e.ChangeType, e.FullPath);
        }

        /// <summary>
        /// ファイル削除通知受信時処理
        /// </summary>
        private async void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            await HandleWatcherEventAsync(e.ChangeType, e.FullPath);
        }

        /// <summary>
        /// ファイル名称変更通知受信時処理
        /// </summary>
        private async void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            await HandleWatcherEventAsync(WatcherChangeTypes.Renamed, e.FullPath, e.OldFullPath);
        }

        /// <summary>
        /// FileSystemWatcher エラー発生時処理
        /// </summary>
        private void fileSystemWatcher1_Error(object sender, ErrorEventArgs e)
        {
            Exception exception = e.GetException();
            string message = exception == null ? "不明な監視エラーです。" : exception.Message;
            lblStatusDetail.Text = "監視エラー: " + message;
        }

        /// <summary>
        /// 監視通知共通処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        private async Task HandleWatcherEventAsync(WatcherChangeTypes changeType, string fullPath, string oldFullPath = null)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                return;
            }

            if (Directory.Exists(fullPath))
            {
                return;
            }

            if (IsDuplicateNotification(changeType, fullPath))
            {
                return;
            }

            DateTime detectedAt = DateTime.Now;
            DateTime? lastWriteTime = TryGetLastWriteTime(fullPath);

            // 先行表示
            FileAuditLogEntry pendingEntry = CreatePendingLogEntry(changeType, fullPath, oldFullPath, detectedAt, lastWriteTime);
            _entries.Insert(0, pendingEntry);
            lblStatusDetail.Text = pendingEntry.備考;

            // 遅延再検索
            EventAuditInfo auditInfo = await ResolveAuditInfoAsync(changeType, fullPath, oldFullPath, detectedAt);
            FileAuditLogEntry resolvedEntry = BuildLogEntry(changeType, fullPath, oldFullPath, detectedAt, lastWriteTime, auditInfo);

            // 後追い置換
            ReplaceEntry(pendingEntry, resolvedEntry);
            lblStatusDetail.Text = resolvedEntry.備考;
        }

        /// <summary>
        /// 同一通知判定処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="fullPath">対象フルパス</param>
        /// <returns>同一通知判定結果</returns>
        private bool IsDuplicateNotification(WatcherChangeTypes changeType, string fullPath)
        {
            string key = changeType + "|" + fullPath;
            DateTime now = DateTime.Now;
            DateTime lastTime;

            if (_recentNotifications.TryGetValue(key, out lastTime))
            {
                if ((now - lastTime) < _duplicateInterval)
                {
                    return true;
                }
            }

            _recentNotifications[key] = now;
            return false;
        }

        /// <summary>
        /// 保留行生成処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        /// <param name="detectedAt">検知時刻</param>
        /// <param name="lastWriteTime">更新時間</param>
        /// <returns>保留行</returns>
        private FileAuditLogEntry CreatePendingLogEntry(WatcherChangeTypes changeType, string fullPath, string oldFullPath, DateTime detectedAt, DateTime? lastWriteTime)
        {
            return new FileAuditLogEntry
            {
                検知時刻 = detectedAt,
                変更種別 = changeType.ToString(),
                フルパス = fullPath,
                ファイル名 = Path.GetFileName(fullPath),
                更新時間 = lastWriteTime,
                最終更新者 = string.Empty,
                ドメイン = string.Empty,
                プロセス名 = string.Empty,
                イベントID = null,
                備考 = BuildPendingNote(changeType, fullPath, oldFullPath)
            };
        }

        /// <summary>
        /// 保留メモ生成処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        /// <returns>保留メモ</returns>
        private string BuildPendingNote(WatcherChangeTypes changeType, string fullPath, string oldFullPath)
        {
            if (changeType == WatcherChangeTypes.Renamed)
            {
                return string.Format("名前変更: {0} -> {1} / 監査ログ検索中", oldFullPath, fullPath);
            }

            return "監査ログ検索中";
        }

        /// <summary>
        /// 監査情報解決処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        /// <param name="detectedAt">検知時刻</param>
        /// <returns>監査情報</returns>
        private async Task<EventAuditInfo> ResolveAuditInfoAsync(WatcherChangeTypes changeType, string fullPath, string oldFullPath, DateTime detectedAt)
        {
            int lookBackSeconds = GetLookBackSeconds();
            int[] retryDelays = GetRetryDelays(changeType);
            EventAuditInfo best = new EventAuditInfo();

            foreach (int retryDelay in retryDelays)
            {
                if (retryDelay > 0)
                {
                    // 監査ログ反映待ち
                    await Task.Delay(retryDelay);
                }

                // 検知時刻基準の近傍照合
                EventAuditInfo current = _eventLogReader.FindLatestAuditInfo(fullPath, lookBackSeconds, detectedAt, changeType, oldFullPath);

                // より強い候補の採用
                if (ShouldReplaceAuditInfo(changeType, detectedAt, current, best))
                {
                    best = current;
                }

                // 十分条件成立時の早期終了
                if (IsResolvedAuditInfo(changeType, current))
                {
                    return current;
                }
            }

            if (string.IsNullOrWhiteSpace(best.ユーザー名) && string.IsNullOrWhiteSpace(best.備考))
            {
                // 全候補未一致時の既定メッセージ
                best.備考 = "監査ログ未検出。監査反映待ち、監査設定、検索秒数を確認してください。";
            }

            return best;
        }

        /// <summary>
        /// 再検索間隔取得処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <returns>再検索間隔一覧</returns>
        private int[] GetRetryDelays(WatcherChangeTypes changeType)
        {
            if (changeType == WatcherChangeTypes.Created)
            {
                return new[] { 0, 500, 1500, 3000 };
            }

            if (changeType == WatcherChangeTypes.Deleted)
            {
                return new[] { 0, 700, 1800, 3500 };
            }

            return new[] { 0, 300, 900 };
        }

        /// <summary>
        /// 監査解決判定処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="auditInfo">監査情報</param>
        /// <returns>解決判定結果</returns>
        private bool IsResolvedAuditInfo(WatcherChangeTypes changeType, EventAuditInfo auditInfo)
        {
            if (auditInfo == null || string.IsNullOrWhiteSpace(auditInfo.ユーザー名))
            {
                return false;
            }

            if (changeType == WatcherChangeTypes.Deleted)
            {
                return auditInfo.イベントID == 4660
                    || ContainsText(auditInfo.備考, "削除")
                    || ContainsText(auditInfo.アクセス内容, "DELETE")
                    || ContainsText(auditInfo.アクセス内容, "削除");
            }

            return true;
        }

        /// <summary>
        /// 監査更新要否判定処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="detectedAt">検知時刻</param>
        /// <param name="current">今回候補</param>
        /// <param name="best">保持候補</param>
        /// <returns>更新要否判定結果</returns>
        private bool ShouldReplaceAuditInfo(WatcherChangeTypes changeType, DateTime detectedAt, EventAuditInfo current, EventAuditInfo best)
        {
            if (current == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(best.ユーザー名) && !string.IsNullOrWhiteSpace(current.ユーザー名))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(current.ユーザー名))
            {
                return string.IsNullOrWhiteSpace(best.ユーザー名) && !string.IsNullOrWhiteSpace(current.備考);
            }

            if (string.IsNullOrWhiteSpace(best.ユーザー名))
            {
                return true;
            }

            if (changeType == WatcherChangeTypes.Deleted)
            {
                // 削除候補優先
                bool currentDeleted = current.イベントID == 4660 || ContainsText(current.備考, "削除");
                bool bestDeleted = best.イベントID == 4660 || ContainsText(best.備考, "削除");

                if (currentDeleted && !bestDeleted)
                {
                    return true;
                }

                if (!currentDeleted && bestDeleted)
                {
                    return false;
                }
            }

            return GetAuditDistance(detectedAt, current) < GetAuditDistance(detectedAt, best);
        }

        /// <summary>
        /// 監査時刻差取得処理
        /// </summary>
        /// <param name="detectedAt">検知時刻</param>
        /// <param name="auditInfo">監査情報</param>
        /// <returns>時刻差ミリ秒</returns>
        private double GetAuditDistance(DateTime detectedAt, EventAuditInfo auditInfo)
        {
            if (auditInfo == null || !auditInfo.イベント時刻.HasValue)
            {
                return double.MaxValue;
            }

            return Math.Abs((detectedAt - auditInfo.イベント時刻.Value).TotalMilliseconds);
        }

        /// <summary>
        /// 一覧行置換処理
        /// </summary>
        /// <param name="before">置換前行</param>
        /// <param name="after">置換後行</param>
        private void ReplaceEntry(FileAuditLogEntry before, FileAuditLogEntry after)
        {
            int index = _entries.IndexOf(before);

            if (index < 0)
            {
                return;
            }

            _entries[index] = after;
        }

        /// <summary>
        /// 部分一致判定処理
        /// </summary>
        /// <param name="source">対象文字列</param>
        /// <param name="keyword">検索文字列</param>
        /// <returns>判定結果</returns>
        private bool ContainsText(string source, string keyword)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(keyword))
            {
                return false;
            }

            return source.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// 画面出力用データ作成処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        /// <param name="detectedAt">検知時刻</param>
        /// <param name="lastWriteTime">更新時間</param>
        /// <param name="auditInfo">監査情報</param>
        /// <returns>画面出力用データ</returns>
        private FileAuditLogEntry BuildLogEntry(WatcherChangeTypes changeType, string fullPath, string oldFullPath, DateTime detectedAt, DateTime? lastWriteTime, EventAuditInfo auditInfo)
        {
            string note = BuildNote(changeType, fullPath, oldFullPath, auditInfo);

            return new FileAuditLogEntry
            {
                検知時刻 = detectedAt,
                変更種別 = changeType.ToString(),
                フルパス = fullPath,
                ファイル名 = Path.GetFileName(fullPath),
                更新時間 = lastWriteTime,
                最終更新者 = auditInfo == null ? string.Empty : auditInfo.ユーザー名,
                ドメイン = auditInfo == null ? string.Empty : auditInfo.ドメイン名,
                プロセス名 = auditInfo == null ? string.Empty : auditInfo.プロセス名,
                イベントID = auditInfo == null ? (int?)null : auditInfo.イベントID,
                備考 = note
            };
        }

        /// <summary>
        /// イベントログ検索秒数取得処理
        /// </summary>
        /// <returns>検索秒数</returns>
        private int GetLookBackSeconds()
        {
            int value;

            if (!int.TryParse(txtLookBackSeconds.Text.Trim(), out value) || value <= 0)
            {
                return 30;
            }

            return value;
        }

        /// <summary>
        /// 最終更新日時取得処理
        /// </summary>
        /// <param name="fullPath">対象フルパス</param>
        /// <returns>最終更新日時</returns>
        private DateTime? TryGetLastWriteTime(string fullPath)
        {
            try
            {
                if (!File.Exists(fullPath))
                {
                    return null;
                }

                return File.GetLastWriteTime(fullPath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 備考作成処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        /// <param name="auditInfo">監査情報</param>
        /// <returns>備考</returns>
        private string BuildNote(WatcherChangeTypes changeType, string fullPath, string oldFullPath, EventAuditInfo auditInfo)
        {
            if (changeType == WatcherChangeTypes.Renamed)
            {
                string renameNote = string.Format("名前変更: {0} -> {1}", oldFullPath, fullPath);

                if (auditInfo != null && !string.IsNullOrWhiteSpace(auditInfo.備考))
                {
                    return renameNote + " / " + auditInfo.備考;
                }

                return renameNote;
            }

            if (auditInfo == null || string.IsNullOrWhiteSpace(auditInfo.ユーザー名))
            {
                return auditInfo != null && !string.IsNullOrWhiteSpace(auditInfo.備考)
                    ? auditInfo.備考
                    : "監査ログ未検出。監査反映待ち、監査設定、検索秒数を確認してください。";
            }

            if (!string.IsNullOrWhiteSpace(auditInfo.備考))
            {
                return auditInfo.備考;
            }

            if (!string.IsNullOrWhiteSpace(auditInfo.アクセス内容))
            {
                return "監査ログ取得: " + auditInfo.アクセス内容;
            }

            return "監査ログを取得しました。";
        }
    }
}
