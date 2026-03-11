namespace FileSystemObserver
{
    partial class MainForm
    {
        /// <summary>
        /// デザイナー管理コンポーネント
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 入力領域パネル
        /// </summary>
        private System.Windows.Forms.Panel pnlTop;

        /// <summary>
        /// 画面見出し
        /// </summary>
        private System.Windows.Forms.Label lblTitle;

        /// <summary>
        /// 対象フォルダ見出し
        /// </summary>
        private System.Windows.Forms.Label lblTargetDirectory;

        /// <summary>
        /// 対象フォルダ入力
        /// </summary>
        private System.Windows.Forms.TextBox txtTargetDirectory;

        /// <summary>
        /// フォルダ参照ボタン
        /// </summary>
        private System.Windows.Forms.Button btnBrowse;

        /// <summary>
        /// フォルダ表示ボタン
        /// </summary>
        private System.Windows.Forms.Button btnOpenDirectory;

        /// <summary>
        /// フィルタ見出し
        /// </summary>
        private System.Windows.Forms.Label lblFilter;

        /// <summary>
        /// フィルタ入力
        /// </summary>
        private System.Windows.Forms.TextBox txtFilter;

        /// <summary>
        /// サブフォルダ対象チェック
        /// </summary>
        private System.Windows.Forms.CheckBox chkIncludeSubdirectories;

        /// <summary>
        /// 更新集約チェック
        /// </summary>
        private System.Windows.Forms.CheckBox chkMergeChangedEvents;

        /// <summary>
        /// 監視開始ボタン
        /// </summary>
        private System.Windows.Forms.Button btnStart;

        /// <summary>
        /// 監視終了ボタン
        /// </summary>
        private System.Windows.Forms.Button btnStop;

        /// <summary>
        /// 一覧消去ボタン
        /// </summary>
        private System.Windows.Forms.Button btnClear;

        /// <summary>
        /// 状態表示パネル
        /// </summary>
        private System.Windows.Forms.Panel pnlStatus;

        /// <summary>
        /// 状態表示ラベル
        /// </summary>
        private System.Windows.Forms.Label lblStatus;

        /// <summary>
        /// 補足説明ラベル
        /// </summary>
        private System.Windows.Forms.Label lblHint;

        /// <summary>
        /// 件数表示ラベル
        /// </summary>
        private System.Windows.Forms.Label lblCount;

        /// <summary>
        /// 一覧表示グリッド
        /// </summary>
        private System.Windows.Forms.DataGridView dgvEvents;

        /// <summary>
        /// 番号列
        /// </summary>
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;

        /// <summary>
        /// 種別列
        /// </summary>
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventType;

        /// <summary>
        /// 相対パス列
        /// </summary>
        private System.Windows.Forms.DataGridViewTextBoxColumn colRelativePath;

        /// <summary>
        /// ファイル名列
        /// </summary>
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;

        /// <summary>
        /// 更新日時列
        /// </summary>
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventTime;

        /// <summary>
        /// 更新者列
        /// </summary>
        private System.Windows.Forms.DataGridViewTextBoxColumn colUpdatedBy;

        /// <summary>
        /// 判定元列
        /// </summary>
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetectionSource;

        /// <summary>
        /// プロセス列
        /// </summary>
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcessName;

        /// <summary>
        /// 補足列
        /// </summary>
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;

        /// <summary>
        /// リソース解放処理
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナー生成コード

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.chkIncludeSubdirectories = new System.Windows.Forms.CheckBox();
            this.chkMergeChangedEvents = new System.Windows.Forms.CheckBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnOpenDirectory = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtTargetDirectory = new System.Windows.Forms.TextBox();
            this.lblTargetDirectory = new System.Windows.Forms.Label();
            this.dgvEvents = new System.Windows.Forms.DataGridView();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRelativePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUpdatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetectionSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblCount);
            this.pnlTop.Controls.Add(this.lblHint);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.pnlStatus);
            this.pnlTop.Controls.Add(this.lblStatus);
            this.pnlTop.Controls.Add(this.btnClear);
            this.pnlTop.Controls.Add(this.btnStop);
            this.pnlTop.Controls.Add(this.btnStart);
            this.pnlTop.Controls.Add(this.chkMergeChangedEvents);
            this.pnlTop.Controls.Add(this.chkIncludeSubdirectories);
            this.pnlTop.Controls.Add(this.txtFilter);
            this.pnlTop.Controls.Add(this.lblFilter);
            this.pnlTop.Controls.Add(this.btnOpenDirectory);
            this.pnlTop.Controls.Add(this.btnBrowse);
            this.pnlTop.Controls.Add(this.txtTargetDirectory);
            this.pnlTop.Controls.Add(this.lblTargetDirectory);
            this.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1484, 156);
            this.pnlTop.TabIndex = 0;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.Location = new System.Drawing.Point(1262, 126);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(210, 20);
            this.lblCount.TabIndex = 13;
            this.lblCount.Text = "表示件数: 0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHint
            // 
            this.lblHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHint.ForeColor = System.Drawing.Color.DimGray;
            this.lblHint.Location = new System.Drawing.Point(16, 125);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(1040, 21);
            this.lblHint.TabIndex = 12;
            this.lblHint.Text = "説明";
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(171, 21);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.Text = "ファイル変更監視ビューア";
            // 
            // pnlStatus
            // 
            this.pnlStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatus.Location = new System.Drawing.Point(1212, 122);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(16, 16);
            this.pnlStatus.TabIndex = 11;
            this.pnlStatus.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Location = new System.Drawing.Point(1236, 117);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(110, 26);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "監視中";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(1304, 69);
            this.btnClear.Name = "btnClear";
            this.btnClear.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClear.Size = new System.Drawing.Size(168, 42);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "一覧消去";
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(1124, 69);
            this.btnStop.Name = "btnStop";
            this.btnStop.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnStop.Size = new System.Drawing.Size(168, 42);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "監視終了";
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(944, 69);
            this.btnStart.Name = "btnStart";
            this.btnStart.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnStart.Size = new System.Drawing.Size(168, 42);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "監視開始";
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // chkIncludeSubdirectories
            // 
            this.chkIncludeSubdirectories.AutoSize = true;
            this.chkIncludeSubdirectories.Location = new System.Drawing.Point(426, 79);
            this.chkIncludeSubdirectories.Name = "chkIncludeSubdirectories";
            this.chkIncludeSubdirectories.Size = new System.Drawing.Size(137, 19);
            this.chkIncludeSubdirectories.TabIndex = 6;
            this.chkIncludeSubdirectories.Text = "サブフォルダも対象";
            this.chkIncludeSubdirectories.UseVisualStyleBackColor = true;
            // 
            // chkMergeChangedEvents
            // 
            this.chkMergeChangedEvents.AutoSize = true;
            this.chkMergeChangedEvents.Location = new System.Drawing.Point(583, 79);
            this.chkMergeChangedEvents.Name = "chkMergeChangedEvents";
            this.chkMergeChangedEvents.Size = new System.Drawing.Size(137, 19);
            this.chkMergeChangedEvents.TabIndex = 15;
            this.chkMergeChangedEvents.Text = "更新をまとめて表示";
            this.chkMergeChangedEvents.UseVisualStyleBackColor = true;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(92, 76);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(316, 23);
            this.txtFilter.TabIndex = 5;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(16, 79);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(59, 15);
            this.lblFilter.TabIndex = 4;
            this.lblFilter.Text = "フィルタ";
            // 
            // btnOpenDirectory
            // 
            this.btnOpenDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenDirectory.Location = new System.Drawing.Point(1304, 15);
            this.btnOpenDirectory.Name = "btnOpenDirectory";
            this.btnOpenDirectory.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOpenDirectory.Size = new System.Drawing.Size(168, 42);
            this.btnOpenDirectory.TabIndex = 3;
            this.btnOpenDirectory.Text = "フォルダ表示";
            this.btnOpenDirectory.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenDirectory.UseVisualStyleBackColor = true;
            this.btnOpenDirectory.Click += new System.EventHandler(this.btnOpenDirectory_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(1124, 15);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnBrowse.Size = new System.Drawing.Size(168, 42);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "参照";
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtTargetDirectory
            // 
            this.txtTargetDirectory.AllowDrop = true;
            this.txtTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetDirectory.Location = new System.Drawing.Point(92, 39);
            this.txtTargetDirectory.Name = "txtTargetDirectory";
            this.txtTargetDirectory.Size = new System.Drawing.Size(1008, 23);
            this.txtTargetDirectory.TabIndex = 1;
            // 
            // lblTargetDirectory
            // 
            this.lblTargetDirectory.AutoSize = true;
            this.lblTargetDirectory.Location = new System.Drawing.Point(16, 42);
            this.lblTargetDirectory.Name = "lblTargetDirectory";
            this.lblTargetDirectory.Size = new System.Drawing.Size(59, 15);
            this.lblTargetDirectory.TabIndex = 0;
            this.lblTargetDirectory.Text = "監視先";
            // 
            // dgvEvents
            // 
            this.dgvEvents.AllowUserToAddRows = false;
            this.dgvEvents.AllowUserToDeleteRows = false;
            this.dgvEvents.AllowUserToResizeRows = false;
            this.dgvEvents.BackgroundColor = System.Drawing.Color.White;
            this.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumber,
            this.colEventType,
            this.colRelativePath,
            this.colFileName,
            this.colEventTime,
            this.colUpdatedBy,
            this.colDetectionSource,
            this.colProcessName,
            this.colNotes});
            this.dgvEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEvents.Location = new System.Drawing.Point(0, 156);
            this.dgvEvents.MultiSelect = false;
            this.dgvEvents.Name = "dgvEvents";
            this.dgvEvents.ReadOnly = true;
            this.dgvEvents.RowHeadersVisible = false;
            this.dgvEvents.RowTemplate.Height = 21;
            this.dgvEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEvents.Size = new System.Drawing.Size(1484, 605);
            this.dgvEvents.TabIndex = 1;
            // 
            // colNumber
            // 
            this.colNumber.HeaderText = "No";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Width = 60;
            // 
            // colEventType
            // 
            this.colEventType.HeaderText = "種別";
            this.colEventType.Name = "colEventType";
            this.colEventType.ReadOnly = true;
            this.colEventType.Width = 90;
            // 
            // colRelativePath
            // 
            this.colRelativePath.HeaderText = "相対パス";
            this.colRelativePath.Name = "colRelativePath";
            this.colRelativePath.ReadOnly = true;
            this.colRelativePath.Width = 260;
            // 
            // colFileName
            // 
            this.colFileName.HeaderText = "ファイル名";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            this.colFileName.Width = 220;
            // 
            // colEventTime
            // 
            this.colEventTime.HeaderText = "更新日時";
            this.colEventTime.Name = "colEventTime";
            this.colEventTime.ReadOnly = true;
            this.colEventTime.Width = 170;
            // 
            // colUpdatedBy
            // 
            this.colUpdatedBy.HeaderText = "判定ユーザー";
            this.colUpdatedBy.Name = "colUpdatedBy";
            this.colUpdatedBy.ReadOnly = true;
            this.colUpdatedBy.Width = 170;
            // 
            // colDetectionSource
            // 
            this.colDetectionSource.HeaderText = "判定元";
            this.colDetectionSource.Name = "colDetectionSource";
            this.colDetectionSource.ReadOnly = true;
            this.colDetectionSource.Width = 90;
            // 
            // colProcessName
            // 
            this.colProcessName.HeaderText = "プロセス";
            this.colProcessName.Name = "colProcessName";
            this.colProcessName.ReadOnly = true;
            this.colProcessName.Width = 140;
            // 
            // colNotes
            // 
            this.colNotes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNotes.HeaderText = "補足";
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1484, 761);
            this.Controls.Add(this.dgvEvents);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MinimumSize = new System.Drawing.Size(1180, 620);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ファイル変更監視ビューア";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
