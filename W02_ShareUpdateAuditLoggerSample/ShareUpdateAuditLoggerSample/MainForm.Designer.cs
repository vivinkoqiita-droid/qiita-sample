namespace ShareUpdateAuditLoggerSample
{
    partial class MainForm
    {
        /// <summary>
        /// デザイナー保持コンテナー
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// リソース解放処理
        /// </summary>
        /// <param name="disposing">解放判定</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー初期化処理
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTargetFolder = new System.Windows.Forms.Label();
            this.txtTargetFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.colDetectedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChangeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFullPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastWriteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDomain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblLookBackSeconds = new System.Windows.Forms.Label();
            this.txtLookBackSeconds = new System.Windows.Forms.TextBox();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.lblStatusDetail = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTargetFolder
            // 
            this.lblTargetFolder.AutoSize = true;
            this.lblTargetFolder.Location = new System.Drawing.Point(12, 15);
            this.lblTargetFolder.Name = "lblTargetFolder";
            this.lblTargetFolder.Size = new System.Drawing.Size(81, 12);
            this.lblTargetFolder.TabIndex = 0;
            this.lblTargetFolder.Text = "監視対象フォルダー";
            // 
            // txtTargetFolder
            // 
            this.txtTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetFolder.Location = new System.Drawing.Point(99, 12);
            this.txtTargetFolder.Name = "txtTargetFolder";
            this.txtTargetFolder.Size = new System.Drawing.Size(688, 19);
            this.txtTargetFolder.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(793, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "参照";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(99, 39);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(90, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "監視開始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(195, 39);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(90, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "監視停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(291, 39);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "一覧クリア";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDetectedAt,
            this.colChangeType,
            this.colFullPath,
            this.colFileName,
            this.colLastWriteTime,
            this.colLastUser,
            this.colDomain,
            this.colProcessName,
            this.colEventId,
            this.colNote});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLog.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLog.Location = new System.Drawing.Point(14, 97);
            this.dgvLog.MultiSelect = false;
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowTemplate.Height = 21;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(854, 390);
            this.dgvLog.TabIndex = 10;
            // 
            // colDetectedAt
            // 
            this.colDetectedAt.DataPropertyName = "検知時刻表示";
            this.colDetectedAt.HeaderText = "検知時刻";
            this.colDetectedAt.Name = "colDetectedAt";
            this.colDetectedAt.ReadOnly = true;
            this.colDetectedAt.Width = 140;
            // 
            // colChangeType
            // 
            this.colChangeType.DataPropertyName = "変更種別";
            this.colChangeType.HeaderText = "変更種別";
            this.colChangeType.Name = "colChangeType";
            this.colChangeType.ReadOnly = true;
            this.colChangeType.Width = 80;
            // 
            // colFullPath
            // 
            this.colFullPath.DataPropertyName = "フルパス";
            this.colFullPath.HeaderText = "フルパス";
            this.colFullPath.Name = "colFullPath";
            this.colFullPath.ReadOnly = true;
            this.colFullPath.Width = 280;
            // 
            // colFileName
            // 
            this.colFileName.DataPropertyName = "ファイル名";
            this.colFileName.HeaderText = "ファイル名";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            this.colFileName.Width = 120;
            // 
            // colLastWriteTime
            // 
            this.colLastWriteTime.DataPropertyName = "更新時間表示";
            this.colLastWriteTime.HeaderText = "更新時間";
            this.colLastWriteTime.Name = "colLastWriteTime";
            this.colLastWriteTime.ReadOnly = true;
            this.colLastWriteTime.Width = 140;
            // 
            // colLastUser
            // 
            this.colLastUser.DataPropertyName = "最終更新者";
            this.colLastUser.HeaderText = "最終更新者";
            this.colLastUser.Name = "colLastUser";
            this.colLastUser.ReadOnly = true;
            this.colLastUser.Width = 120;
            // 
            // colDomain
            // 
            this.colDomain.DataPropertyName = "ドメイン";
            this.colDomain.HeaderText = "ドメイン";
            this.colDomain.Name = "colDomain";
            this.colDomain.ReadOnly = true;
            this.colDomain.Width = 80;
            // 
            // colProcessName
            // 
            this.colProcessName.DataPropertyName = "プロセス名";
            this.colProcessName.HeaderText = "プロセス名";
            this.colProcessName.Name = "colProcessName";
            this.colProcessName.ReadOnly = true;
            this.colProcessName.Width = 140;
            // 
            // colEventId
            // 
            this.colEventId.DataPropertyName = "イベントID";
            this.colEventId.HeaderText = "イベントID";
            this.colEventId.Name = "colEventId";
            this.colEventId.ReadOnly = true;
            this.colEventId.Width = 80;
            // 
            // colNote
            // 
            this.colNote.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNote.DataPropertyName = "備考";
            this.colNote.HeaderText = "備考";
            this.colNote.Name = "colNote";
            this.colNote.ReadOnly = true;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = false;
            this.fileSystemWatcher1.IncludeSubdirectories = true;
            this.fileSystemWatcher1.NotifyFilter = ((System.IO.NotifyFilters)((((System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.DirectoryName)
            | System.IO.NotifyFilters.LastWrite)
            | System.IO.NotifyFilters.Size)));
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
            this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
            this.fileSystemWatcher1.Error += new System.IO.ErrorEventHandler(this.fileSystemWatcher1_Error);
            this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
            // 
            // lblLookBackSeconds
            // 
            this.lblLookBackSeconds.AutoSize = true;
            this.lblLookBackSeconds.Location = new System.Drawing.Point(400, 44);
            this.lblLookBackSeconds.Name = "lblLookBackSeconds";
            this.lblLookBackSeconds.Size = new System.Drawing.Size(115, 12);
            this.lblLookBackSeconds.TabIndex = 7;
            this.lblLookBackSeconds.Text = "イベントログ検索秒数";
            // 
            // txtLookBackSeconds
            // 
            this.txtLookBackSeconds.Location = new System.Drawing.Point(521, 41);
            this.txtLookBackSeconds.Name = "txtLookBackSeconds";
            this.txtLookBackSeconds.Size = new System.Drawing.Size(54, 19);
            this.txtLookBackSeconds.TabIndex = 8;
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Location = new System.Drawing.Point(12, 75);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(29, 12);
            this.lblStatusTitle.TabIndex = 9;
            this.lblStatusTitle.Text = "状態";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.AutoSize = true;
            this.lblStatusValue.Location = new System.Drawing.Point(47, 75);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(11, 12);
            this.lblStatusValue.TabIndex = 10;
            this.lblStatusValue.Text = "-";
            // 
            // lblStatusDetail
            // 
            this.lblStatusDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusDetail.AutoEllipsis = true;
            this.lblStatusDetail.Location = new System.Drawing.Point(110, 70);
            this.lblStatusDetail.Name = "lblStatusDetail";
            this.lblStatusDetail.Size = new System.Drawing.Size(758, 21);
            this.lblStatusDetail.TabIndex = 11;
            this.lblStatusDetail.Text = "待機中";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 499);
            this.Controls.Add(this.lblStatusDetail);
            this.Controls.Add(this.lblStatusValue);
            this.Controls.Add(this.lblStatusTitle);
            this.Controls.Add(this.txtLookBackSeconds);
            this.Controls.Add(this.lblLookBackSeconds);
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtTargetFolder);
            this.Controls.Add(this.lblTargetFolder);
            this.MinimumSize = new System.Drawing.Size(896, 538);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "共有更新監査ログサンプル";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTargetFolder;
        private System.Windows.Forms.TextBox txtTargetFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lblLookBackSeconds;
        private System.Windows.Forms.TextBox txtLookBackSeconds;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.Label lblStatusDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetectedAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChangeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFullPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastWriteTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDomain;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
    }
}
