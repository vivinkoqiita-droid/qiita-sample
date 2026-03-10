using ShareUpdateAuditLoggerSample.Models;
using ShareUpdateAuditLoggerSample.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
            lblStatusDetail.Text = "Security ログの 4663 を参照します。監査ログ反映待ちのため数秒遅れて表示される場合があります。";
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
        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            HandleWatcherEvent(e.ChangeType, e.FullPath);
        }

        /// <summary>
        /// ファイル作成通知受信時処理
        /// </summary>
        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            HandleWatcherEvent(e.ChangeType, e.FullPath);
        }

        /// <summary>
        /// ファイル削除通知受信時処理
        /// </summary>
        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            HandleWatcherEvent(e.ChangeType, e.FullPath);
        }

        /// <summary>
        /// ファイル名称変更通知受信時処理
        /// </summary>
        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            HandleWatcherEvent(WatcherChangeTypes.Renamed, e.FullPath, e.OldFullPath);
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
        private void HandleWatcherEvent(WatcherChangeTypes changeType, string fullPath, string oldFullPath = null)
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

            FileAuditLogEntry entry = BuildLogEntry(changeType, fullPath, oldFullPath);
            _entries.Insert(0, entry);
            lblStatusDetail.Text = entry.備考;
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
        /// 画面出力用データ作成処理
        /// </summary>
        /// <param name="changeType">変更種別</param>
        /// <param name="fullPath">変更後フルパス</param>
        /// <param name="oldFullPath">変更前フルパス</param>
        /// <returns>画面出力用データ</returns>
        private FileAuditLogEntry BuildLogEntry(WatcherChangeTypes changeType, string fullPath, string oldFullPath)
        {
            DateTime detectedAt = DateTime.Now;
            int lookBackSeconds = GetLookBackSeconds();
            EventAuditInfo auditInfo = _eventLogReader.FindLatestAuditInfo(fullPath, lookBackSeconds, oldFullPath);
            DateTime? lastWriteTime = TryGetLastWriteTime(fullPath);
            string note = BuildNote(changeType, fullPath, oldFullPath, auditInfo);

            return new FileAuditLogEntry
            {
                検知時刻 = detectedAt,
                変更種別 = changeType.ToString(),
                フルパス = fullPath,
                ファイル名 = Path.GetFileName(fullPath),
                更新時間 = lastWriteTime,
                最終更新者 = auditInfo.ユーザー名,
                ドメイン = auditInfo.ドメイン名,
                プロセス名 = auditInfo.プロセス名,
                イベントID = auditInfo.イベントID,
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
                return string.Format("名前変更: {0} -> {1}", oldFullPath, fullPath);
            }

            if (string.IsNullOrWhiteSpace(auditInfo.ユーザー名))
            {
                return "監査ログ未検出。監査反映待ち、監査設定、検索秒数を確認してください。";
            }

            if (!string.IsNullOrWhiteSpace(auditInfo.アクセス内容))
            {
                return "監査ログ取得: " + auditInfo.アクセス内容;
            }

            return "監査ログを取得しました。";
        }
    }
}