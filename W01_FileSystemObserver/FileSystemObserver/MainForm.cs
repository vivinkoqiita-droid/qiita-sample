using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace FileSystemObserver
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 一覧最大件数
        /// </summary>
        private const int MaxLogCount = 1000;

        /// <summary>
        /// 監視インスタンス
        /// </summary>
        private FileSystemWatcher watcher;

        /// <summary>
        /// 連番保持
        /// </summary>
        private int sequence;

        /// <summary>
        /// 表示中監視ルート
        /// </summary>
        private string currentRootPath = string.Empty;

        /// <summary>
        /// 更新集約判定時間
        /// </summary>
        private static readonly TimeSpan ChangedMergeWindow = TimeSpan.FromMilliseconds(1200);

        public MainForm()
        {
            InitializeComponent();
            InitializeView();
        }

        /// <summary>
        /// 初期表示設定
        /// 既定値反映
        /// 一覧初期化
        /// 監視状態反映
        /// </summary>
        private void InitializeView()
        {
            txtTargetDirectory.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            UpdateMonitoringState(false);
            UpdateHintLabel();
        }

        /// <summary>
        /// フォルダドラッグ進入時処理
        /// フォルダ可否判定
        /// </summary>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = GetDirectoryDropEffect(e.Data);
        }

        /// <summary>
        /// フォルダドロップ時処理
        /// 入力欄反映
        /// </summary>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string directoryPath = ExtractDroppedDirectoryPath(e.Data);
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                return;
            }

            txtTargetDirectory.Text = directoryPath;
            txtTargetDirectory.SelectionStart = txtTargetDirectory.TextLength;
            txtTargetDirectory.SelectionLength = 0;
        }

        /// <summary>
        /// ドロップ可否判定処理
        /// フォルダのみ許可
        /// </summary>
        private DragDropEffects GetDirectoryDropEffect(IDataObject data)
        {
            if (data == null || !data.GetDataPresent(DataFormats.FileDrop))
            {
                return DragDropEffects.None;
            }

            string[] paths = data.GetData(DataFormats.FileDrop) as string[];
            if (paths == null || paths.Length != 1)
            {
                return DragDropEffects.None;
            }

            return Directory.Exists(paths[0]) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        /// <summary>
        /// ドロップフォルダ取得処理
        /// 単一フォルダ抽出
        /// </summary>
        private string ExtractDroppedDirectoryPath(IDataObject data)
        {
            if (GetDirectoryDropEffect(data) == DragDropEffects.None)
            {
                return string.Empty;
            }

            string[] paths = data.GetData(DataFormats.FileDrop) as string[];
            if (paths == null || paths.Length == 0)
            {
                return string.Empty;
            }

            return paths[0];
        }

        /// <summary>
        /// 監視開始押下処理
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartMonitoring();
        }

        /// <summary>
        /// 監視終了押下処理
        /// </summary>
        private void btnStop_Click(object sender, EventArgs e)
        {
            StopMonitoring();
        }

        /// <summary>
        /// 参照押下処理
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "監視対象フォルダ選択";
                dialog.SelectedPath = txtTargetDirectory.Text;
                dialog.ShowNewFolderButton = false;

                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                txtTargetDirectory.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// フォルダ表示押下処理
        /// </summary>
        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            string directoryPath = txtTargetDirectory.Text.Trim();

            if (!Directory.Exists(directoryPath))
            {
                MessageBox.Show(this, "指定フォルダ未存在", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Process.Start("explorer.exe", directoryPath);
        }

        /// <summary>
        /// 一覧消去押下処理
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvEvents.Rows.Clear();
            sequence = 0;
            UpdateCountLabel();
        }

        /// <summary>
        /// 閉じる直前処理
        /// 監視停止
        /// リソース解放
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopMonitoring();
            base.OnFormClosing(e);
        }

        /// <summary>
        /// 監視開始処理
        /// 入力値検証
        /// 監視インスタンス生成
        /// イベント関連付け
        /// 画面状態反映
        /// </summary>
        private void StartMonitoring()
        {
            string directoryPath = txtTargetDirectory.Text.Trim();
            string filter = txtFilter.Text.Trim();

            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                MessageBox.Show(this, "監視対象フォルダ未入力", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTargetDirectory.Focus();
                return;
            }

            if (!Directory.Exists(directoryPath))
            {
                MessageBox.Show(this, "監視対象フォルダ未存在", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTargetDirectory.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(filter))
            {
                filter = "*.*";
                txtFilter.Text = filter;
            }

            StopMonitoring();

            currentRootPath = NormalizeDirectoryPath(directoryPath);

            // 記事: まずはこれだけで動きを見られる章
            watcher = new FileSystemWatcher(currentRootPath)
            {
                // 記事: 通知を絞ると何がよくなるのか章
                Filter = filter,

                // 記事: サブフォルダまで監視したいとき章
                IncludeSubdirectories = chkIncludeSubdirectories.Checked,

                // 記事: まずはこれだけで動きを見られる章
                NotifyFilter =
                    NotifyFilters.FileName |
                    NotifyFilters.DirectoryName |
                    NotifyFilters.LastWrite |
                    NotifyFilters.Size
            };

            // 記事: イベント登録と監視開始章
            watcher.Created += OnCreated;
            watcher.Changed += OnChanged;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            // 記事: イベント登録と監視開始章
            watcher.EnableRaisingEvents = true;

            AddSystemLog("監視開始", currentRootPath, string.Empty, DateTime.Now, "画面操作", "画面", string.Empty, BuildStartNote());
            UpdateMonitoringState(true);
        }

        /// <summary>
        /// 監視終了処理
        /// イベント解除
        /// 監視停止
        /// リソース解放
        /// </summary>
        private void StopMonitoring()
        {
            if (watcher == null)
            {
                UpdateMonitoringState(false);
                return;
            }

            watcher.Created -= OnCreated;
            watcher.Changed -= OnChanged;
            watcher.Deleted -= OnDeleted;
            watcher.Renamed -= OnRenamed;
            watcher.Error -= OnError;
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
            watcher = null;

            AddSystemLog("監視終了", currentRootPath, string.Empty, DateTime.Now, "画面操作", "画面", string.Empty, "監視停止");
            UpdateMonitoringState(false);
        }

        /// <summary>
        /// 監視状態反映処理
        /// ボタン活性制御
        /// 状態表示切替
        /// 入力可否切替
        /// </summary>
        private void UpdateMonitoringState(bool monitoring)
        {
            btnStart.Enabled = !monitoring;
            btnStop.Enabled = monitoring;
            btnBrowse.Enabled = !monitoring;
            txtTargetDirectory.ReadOnly = monitoring;
            txtFilter.ReadOnly = monitoring;
            chkIncludeSubdirectories.Enabled = !monitoring;
            chkMergeChangedEvents.Enabled = !monitoring;

            pnlStatus.Visible = monitoring;
            lblStatus.Visible = monitoring;
            lblStatus.Text = monitoring ? "監視中" : string.Empty;
            lblStatus.ForeColor = monitoring ? Color.White : SystemColors.ControlText;
            pnlStatus.BackColor = monitoring ? Color.FromArgb(46, 125, 50) : SystemColors.Control;
        }

        /// <summary>
        /// 説明ラベル更新処理
        /// </summary>
        private void UpdateHintLabel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("監視先フォルダは入力・参照・ドラッグアンドドロップ対応。 ");
            builder.Append("更新者列は Windows 監査ログ優先。 ");
            builder.Append("監査未設定時は所有者表示。 ");
            builder.Append("短時間の更新をまとめる切替あり。 ");
            builder.Append("取得不可時は不明表示。");
            lblHint.Text = builder.ToString();
        }

        /// <summary>
        /// 監視開始補足生成処理
        /// </summary>
        private string BuildStartNote()
        {
            return string.Format(
                "Filter={0} / IncludeSubdirectories={1} / MergeChanged={2}",
                txtFilter.Text.Trim(),
                chkIncludeSubdirectories.Checked ? "True" : "False",
                chkMergeChangedEvents.Checked ? "True" : "False");
        }

        /// <summary>
        /// 作成イベント受信処理
        /// 記事: 作成・更新・削除・リネームの受け取り章
        /// </summary>
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            RegisterFileEvent("作成", e.FullPath, DateTime.Now, WatcherChangeTypes.Created, string.Empty);
        }

        /// <summary>
        /// 更新イベント受信処理
        /// 記事: 作成・更新・削除・リネームの受け取り章
        /// </summary>
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            RegisterFileEvent("更新", e.FullPath, DateTime.Now, WatcherChangeTypes.Changed, string.Empty);
        }

        /// <summary>
        /// 削除イベント受信処理
        /// 記事: 作成・更新・削除・リネームの受け取り章
        /// </summary>
        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            RegisterFileEvent("削除", e.FullPath, DateTime.Now, WatcherChangeTypes.Deleted, string.Empty);
        }

        /// <summary>
        /// リネームイベント受信処理
        /// 記事: 作成・更新・削除・リネームの受け取り章
        /// </summary>
        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            string notes = string.Format("旧パス: {0}", e.OldFullPath);
            RegisterFileEvent("リネーム", e.FullPath, DateTime.Now, WatcherChangeTypes.Renamed, notes);
        }

        /// <summary>
        /// 監視異常イベント受信処理
        /// 記事: Error は外さない方がよい章
        /// </summary>
        private void OnError(object sender, ErrorEventArgs e)
        {
            Exception exception = e.GetException();
            string notes = exception == null ? "監視異常" : exception.Message;
            AddSystemLog("監視エラー", currentRootPath, string.Empty, DateTime.Now, "監視", "Error", string.Empty, notes);
        }

        /// <summary>
        /// ファイルイベント登録処理
        /// 相対パス生成
        /// 更新者判定
        /// 一覧反映
        /// </summary>
        private void RegisterFileEvent(string eventType, string fullPath, DateTime eventTime, WatcherChangeTypes changeType, string notes)
        {
            UpdaterResolution updater = ResolveUpdater(fullPath, eventTime, changeType);
            string fileName = Path.GetFileName(fullPath);
            string relativePath = GetRelativeDirectoryDisplay(currentRootPath, fullPath);
            DateTime displayTime = GetDisplayTime(fullPath, eventTime);

            FileEventEntry entry = new FileEventEntry
            {
                Number = ++sequence,
                EventType = eventType,
                RelativePath = relativePath,
                FileName = fileName,
                EventTime = displayTime,
                UpdatedBy = updater.DisplayName,
                DetectionSource = updater.Source,
                ProcessName = updater.ProcessName,
                Notes = notes,
                FullPath = fullPath,
                MergeCount = 1
            };

            AddEntry(entry);
        }

        /// <summary>
        /// システム行追加処理
        /// </summary>
        private void AddSystemLog(string eventType, string rootPath, string fileName, DateTime eventTime, string updatedBy, string source, string processName, string notes)
        {
            FileEventEntry entry = new FileEventEntry
            {
                Number = ++sequence,
                EventType = eventType,
                RelativePath = string.IsNullOrWhiteSpace(rootPath) ? "-" : rootPath,
                FileName = string.IsNullOrWhiteSpace(fileName) ? "-" : fileName,
                EventTime = eventTime,
                UpdatedBy = updatedBy,
                DetectionSource = source,
                ProcessName = processName,
                Notes = notes,
                FullPath = rootPath,
                MergeCount = 1
            };

            AddEntry(entry);
        }

        /// <summary>
        /// 一覧反映処理
        /// UI スレッド切替
        /// 行追加
        /// 行数上限制御
        /// </summary>
        private void AddEntry(FileEventEntry entry)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => AddEntry(entry)));
                return;
            }

            if (TryMergeChangedEntry(entry))
            {
                UpdateCountLabel();
                return;
            }

            dgvEvents.Rows.Insert(0, 1);

            DataGridViewRow row = dgvEvents.Rows[0];
            row.Cells[colNumber.Name].Value = entry.Number;
            row.Cells[colEventType.Name].Value = entry.EventType;
            row.Cells[colRelativePath.Name].Value = entry.RelativePath;
            row.Cells[colFileName.Name].Value = entry.FileName;
            row.Cells[colEventTime.Name].Value = entry.EventTime.ToString("yyyy/MM/dd HH:mm:ss.fff");
            row.Cells[colUpdatedBy.Name].Value = entry.UpdatedBy;
            row.Cells[colDetectionSource.Name].Value = entry.DetectionSource;
            row.Cells[colProcessName.Name].Value = entry.ProcessName;
            row.Cells[colNotes.Name].Value = entry.Notes;

            row.Tag = entry;
            ApplyRowStyle(row, entry.EventType);

            while (dgvEvents.Rows.Count > MaxLogCount)
            {
                dgvEvents.Rows.RemoveAt(dgvEvents.Rows.Count - 1);
            }

            UpdateCountLabel();
        }

        /// <summary>
        /// 更新集約判定処理
        /// 同一ファイル短時間更新対象
        /// </summary>
        private bool TryMergeChangedEntry(FileEventEntry entry)
        {
            if (!chkMergeChangedEvents.Checked)
            {
                return false;
            }

            if (!string.Equals(entry.EventType, "更新", StringComparison.Ordinal))
            {
                return false;
            }

            if (dgvEvents.Rows.Count == 0)
            {
                return false;
            }

            DataGridViewRow latestRow = dgvEvents.Rows[0];
            FileEventEntry latestEntry = latestRow.Tag as FileEventEntry;
            if (latestEntry == null)
            {
                return false;
            }

            if (!string.Equals(latestEntry.EventType, entry.EventType, StringComparison.Ordinal))
            {
                return false;
            }

            if (!string.Equals(latestEntry.FullPath, entry.FullPath, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (entry.EventTime - latestEntry.EventTime > ChangedMergeWindow)
            {
                return false;
            }

            latestEntry.EventTime = entry.EventTime;
            latestEntry.UpdatedBy = entry.UpdatedBy;
            latestEntry.DetectionSource = entry.DetectionSource;
            latestEntry.ProcessName = entry.ProcessName;
            latestEntry.MergeCount++;
            latestEntry.Notes = BuildMergedNotes(entry.Notes, latestEntry.MergeCount);

            latestRow.Cells[colEventTime.Index].Value = latestEntry.EventTime.ToString("yyyy/MM/dd HH:mm:ss.fff");
            latestRow.Cells[colUpdatedBy.Index].Value = latestEntry.UpdatedBy;
            latestRow.Cells[colDetectionSource.Index].Value = latestEntry.DetectionSource;
            latestRow.Cells[colProcessName.Index].Value = latestEntry.ProcessName;
            latestRow.Cells[colNotes.Index].Value = latestEntry.Notes;
            ApplyRowStyle(latestRow, latestEntry.EventType);
            return true;
        }

        /// <summary>
        /// 集約補足生成処理
        /// </summary>
        private string BuildMergedNotes(string notes, int mergeCount)
        {
            string prefix = string.Format("短時間更新 {0} 回", mergeCount);
            return string.IsNullOrWhiteSpace(notes) ? prefix : prefix + " / " + notes;
        }

        /// <summary>
        /// 行表示色反映処理
        /// 種別別背景色反映
        /// </summary>
        private void ApplyRowStyle(DataGridViewRow row, string eventType)
        {
            row.DefaultCellStyle.ForeColor = SystemColors.ControlText;
            row.DefaultCellStyle.BackColor = Color.White;

            switch (eventType)
            {
                case "作成":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(227, 242, 253);
                    break;
                case "更新":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 249, 196);
                    break;
                case "削除":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(238, 238, 238);
                    break;
                case "リネーム":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(243, 229, 245);
                    break;
                case "監視エラー":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(183, 28, 28);
                    break;
                case "監視開始":
                case "監視終了":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233);
                    break;
            }
        }

        /// <summary>
        /// 件数ラベル更新処理
        /// </summary>
        private void UpdateCountLabel()
        {
            lblCount.Text = string.Format("表示件数: {0}", dgvEvents.Rows.Count);
        }

        /// <summary>
        /// 更新者判定処理
        /// 監査ログ優先
        /// 所有者フォールバック
        /// 不明補完
        /// </summary>
        private UpdaterResolution ResolveUpdater(string fullPath, DateTime eventTime, WatcherChangeTypes changeType)
        {
            UpdaterResolution resolution;

            if (TryGetAuditUpdater(fullPath, eventTime, out resolution))
            {
                return resolution;
            }

            if (TryGetOwnerUpdater(fullPath, out resolution))
            {
                return resolution;
            }

            resolution = new UpdaterResolution();
            resolution.DisplayName = "不明";
            resolution.Source = "未取得";
            resolution.ProcessName = changeType == WatcherChangeTypes.Deleted ? "削除後未取得" : string.Empty;
            return resolution;
        }

        /// <summary>
        /// 監査ログ更新者取得処理
        /// Security ログ後方確認
        /// 4663 優先
        /// パス一致判定
        /// </summary>
        private bool TryGetAuditUpdater(string fullPath, DateTime eventTime, out UpdaterResolution resolution)
        {
            resolution = null;

            try
            {
                using (EventLog securityLog = new EventLog("Security"))
                {
                    int inspectedCount = 0;
                    DateTime threshold = eventTime.AddSeconds(-15);

                    for (int index = securityLog.Entries.Count - 1; index >= 0 && inspectedCount < 400; index--)
                    {
                        EventLogEntry entry = securityLog.Entries[index];
                        inspectedCount++;

                        if (entry.TimeGenerated < threshold)
                        {
                            break;
                        }

                        if (entry.InstanceId != 4663)
                        {
                            continue;
                        }

                        string[] values = entry.ReplacementStrings;
                        if (values == null || values.Length < 12)
                        {
                            continue;
                        }

                        string objectName = values[6] ?? string.Empty;
                        if (!IsSamePath(fullPath, objectName))
                        {
                            continue;
                        }

                        string accountName = values[1] ?? string.Empty;
                        string accountDomain = values[2] ?? string.Empty;
                        string processName = values[11] ?? string.Empty;

                        resolution = new UpdaterResolution();
                        resolution.DisplayName = BuildAccountDisplay(accountDomain, accountName);
                        resolution.Source = "監査ログ";
                        resolution.ProcessName = NormalizeProcessName(processName);
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// 所有者更新者取得処理
        /// ファイル所有者取得
        /// SYSTEM 表示補正
        /// </summary>
        private bool TryGetOwnerUpdater(string fullPath, out UpdaterResolution resolution)
        {
            resolution = null;

            try
            {
                if (!File.Exists(fullPath) && !Directory.Exists(fullPath))
                {
                    return false;
                }

                FileSystemSecurity security = File.Exists(fullPath)
                    ? (FileSystemSecurity)File.GetAccessControl(fullPath)
                    : Directory.GetAccessControl(fullPath);

                IdentityReference identity = security.GetOwner(typeof(NTAccount));
                string account = identity == null ? string.Empty : identity.Value;
                string displayName = NormalizeAccountDisplay(account);

                resolution = new UpdaterResolution();
                resolution.DisplayName = string.IsNullOrWhiteSpace(displayName) ? "不明" : displayName;
                resolution.Source = "所有者";
                resolution.ProcessName = IsSystemAccount(displayName) ? "System 管理" : string.Empty;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 表示日時取得処理
        /// LastWrite 優先
        /// 欠落時イベント時刻補完
        /// </summary>
        private DateTime GetDisplayTime(string fullPath, DateTime eventTime)
        {
            try
            {
                if (File.Exists(fullPath))
                {
                    return File.GetLastWriteTime(fullPath);
                }

                if (Directory.Exists(fullPath))
                {
                    return Directory.GetLastWriteTime(fullPath);
                }
            }
            catch
            {
            }

            return eventTime;
        }

        /// <summary>
        /// 相対ディレクトリ表示生成処理
        /// 直下補完
        /// </summary>
        private string GetRelativeDirectoryDisplay(string rootPath, string fullPath)
        {
            if (string.IsNullOrWhiteSpace(rootPath) || string.IsNullOrWhiteSpace(fullPath))
            {
                return "-";
            }

            string fileDirectory = Path.GetDirectoryName(fullPath) ?? string.Empty;
            string root = NormalizeDirectoryPath(rootPath);
            string targetDirectory = NormalizeDirectoryPath(fileDirectory);

            if (string.Equals(root, targetDirectory, StringComparison.OrdinalIgnoreCase))
            {
                return "(直下)";
            }

            if (!targetDirectory.StartsWith(root, StringComparison.OrdinalIgnoreCase))
            {
                return fileDirectory;
            }

            string relative = targetDirectory.Substring(root.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return string.IsNullOrWhiteSpace(relative) ? "(直下)" : relative;
        }

        /// <summary>
        /// ディレクトリ正規化処理
        /// 末尾区切り文字除去
        /// </summary>
        private string NormalizeDirectoryPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return string.Empty;
            }

            return path.Trim().TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        /// <summary>
        /// パス一致判定処理
        /// 前後引用符除去
        /// 大文字小文字非区別比較
        /// </summary>
        private bool IsSamePath(string expectedPath, string actualPath)
        {
            string left = TrimQuotes(expectedPath);
            string right = TrimQuotes(actualPath);
            return string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 引用符除去処理
        /// </summary>
        private string TrimQuotes(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return value.Trim().Trim('"');
        }

        /// <summary>
        /// アカウント表示生成処理
        /// </summary>
        private string BuildAccountDisplay(string domain, string accountName)
        {
            string normalizedDomain = string.IsNullOrWhiteSpace(domain) ? string.Empty : domain.Trim();
            string normalizedName = string.IsNullOrWhiteSpace(accountName) ? string.Empty : accountName.Trim();
            string joined = string.IsNullOrWhiteSpace(normalizedDomain)
                ? normalizedName
                : normalizedDomain + "\\" + normalizedName;

            return NormalizeAccountDisplay(joined);
        }

        /// <summary>
        /// アカウント表示正規化処理
        /// SYSTEM 表示統一
        /// </summary>
        private string NormalizeAccountDisplay(string account)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                return string.Empty;
            }

            string normalized = account.Trim();
            if (IsSystemAccount(normalized))
            {
                return "SYSTEM";
            }

            return normalized;
        }

        /// <summary>
        /// システムアカウント判定処理
        /// </summary>
        private bool IsSystemAccount(string account)
        {
            if (string.IsNullOrWhiteSpace(account))
            {
                return false;
            }

            string normalized = account.Trim();
            return normalized.IndexOf("SYSTEM", StringComparison.OrdinalIgnoreCase) >= 0
                || normalized.IndexOf("LOCAL SERVICE", StringComparison.OrdinalIgnoreCase) >= 0
                || normalized.IndexOf("NETWORK SERVICE", StringComparison.OrdinalIgnoreCase) >= 0
                || normalized.IndexOf("TrustedInstaller", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// プロセス名正規化処理
        /// </summary>
        private string NormalizeProcessName(string processName)
        {
            if (string.IsNullOrWhiteSpace(processName))
            {
                return string.Empty;
            }

            try
            {
                return Path.GetFileName(processName.Trim());
            }
            catch
            {
                return processName.Trim();
            }
        }

        /// <summary>
        /// 更新者判定結果
        /// </summary>
        private sealed class UpdaterResolution
        {
            /// <summary>
            /// 表示名
            /// </summary>
            public string DisplayName { get; set; }

            /// <summary>
            /// 判定元
            /// </summary>
            public string Source { get; set; }

            /// <summary>
            /// プロセス名
            /// </summary>
            public string ProcessName { get; set; }
        }
    }
}
