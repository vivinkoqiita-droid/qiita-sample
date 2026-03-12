namespace WinFormsUiFreezeSample
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel rootLayout;
        private GroupBox grpCases;
        private FlowLayoutPanel pnlCaseButtons;
        private Button btnHeavyOnUi;
        private Button btnTaskRunCpu;
        private Button btnAsyncIo;
        private Button btnWait;
        private Button btnInvokeWait;
        private Button btnBeginInvoke;
        private Button btnClearLog;
        private GroupBox grpMonitor;
        private TableLayoutPanel monitorLayout;
        private Label lblStatusTitle;
        private Label lblStatusValue;
        private Label lblHeartbeatTitle;
        private Label lblHeartbeat;
        private Label lblWorkTitle;
        private SmoothProgressBar prgWork;
        private Label lblHeartbeatBarTitle;
        private SmoothProgressBar prgHeartbeat;
        private TextBox txtDescription;
        private GroupBox grpLog;
        private TextBox txtLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.grpCases = new System.Windows.Forms.GroupBox();
            this.pnlCaseButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHeavyOnUi = new System.Windows.Forms.Button();
            this.btnTaskRunCpu = new System.Windows.Forms.Button();
            this.btnAsyncIo = new System.Windows.Forms.Button();
            this.btnWait = new System.Windows.Forms.Button();
            this.btnInvokeWait = new System.Windows.Forms.Button();
            this.btnBeginInvoke = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.grpMonitor = new System.Windows.Forms.GroupBox();
            this.monitorLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.lblHeartbeatTitle = new System.Windows.Forms.Label();
            this.lblHeartbeat = new System.Windows.Forms.Label();
            this.lblWorkTitle = new System.Windows.Forms.Label();
            this.prgWork = new WinFormsUiFreezeSample.SmoothProgressBar();
            this.lblHeartbeatBarTitle = new System.Windows.Forms.Label();
            this.prgHeartbeat = new WinFormsUiFreezeSample.SmoothProgressBar();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.rootLayout.SuspendLayout();
            this.grpCases.SuspendLayout();
            this.pnlCaseButtons.SuspendLayout();
            this.grpMonitor.SuspendLayout();
            this.monitorLayout.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.ColumnCount = 2;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.grpCases, 0, 0);
            this.rootLayout.Controls.Add(this.grpMonitor, 1, 0);
            this.rootLayout.Controls.Add(this.grpLog, 0, 1);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(0, 0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.Padding = new System.Windows.Forms.Padding(10);
            this.rootLayout.RowCount = 2;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 325F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Size = new System.Drawing.Size(1366, 768);
            this.rootLayout.TabIndex = 0;
            this.rootLayout.SetColumnSpan(this.grpLog, 2);
            // 
            // grpCases
            // 
            this.grpCases.Controls.Add(this.pnlCaseButtons);
            this.grpCases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCases.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpCases.Location = new System.Drawing.Point(13, 13);
            this.grpCases.Name = "grpCases";
            this.grpCases.Padding = new System.Windows.Forms.Padding(12);
            this.grpCases.Size = new System.Drawing.Size(314, 319);
            this.grpCases.TabIndex = 0;
            this.grpCases.TabStop = false;
            this.grpCases.Text = "比較ボタン";
            // 
            // pnlCaseButtons
            // 
            this.pnlCaseButtons.Controls.Add(this.btnHeavyOnUi);
            this.pnlCaseButtons.Controls.Add(this.btnTaskRunCpu);
            this.pnlCaseButtons.Controls.Add(this.btnAsyncIo);
            this.pnlCaseButtons.Controls.Add(this.btnWait);
            this.pnlCaseButtons.Controls.Add(this.btnInvokeWait);
            this.pnlCaseButtons.Controls.Add(this.btnBeginInvoke);
            this.pnlCaseButtons.Controls.Add(this.btnClearLog);
            this.pnlCaseButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCaseButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlCaseButtons.Location = new System.Drawing.Point(12, 30);
            this.pnlCaseButtons.Name = "pnlCaseButtons";
            this.pnlCaseButtons.Padding = new System.Windows.Forms.Padding(6);
            this.pnlCaseButtons.Size = new System.Drawing.Size(290, 277);
            this.pnlCaseButtons.TabIndex = 0;
            this.pnlCaseButtons.WrapContents = false;
            // 
            // btnHeavyOnUi
            // 
            this.btnHeavyOnUi.BackColor = System.Drawing.Color.FromArgb(255, 235, 238);
            this.btnHeavyOnUi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeavyOnUi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnHeavyOnUi.ForeColor = System.Drawing.Color.FromArgb(183, 28, 28);
            this.btnHeavyOnUi.Location = new System.Drawing.Point(9, 9);
            this.btnHeavyOnUi.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.btnHeavyOnUi.Name = "btnHeavyOnUi";
            this.btnHeavyOnUi.Size = new System.Drawing.Size(260, 34);
            this.btnHeavyOnUi.TabIndex = 0;
            this.btnHeavyOnUi.Text = "UIで重い処理 | 危険";
            this.btnHeavyOnUi.UseVisualStyleBackColor = false;
            this.btnHeavyOnUi.Click += new System.EventHandler(this.btnHeavyOnUi_Click);
            // 
            // btnTaskRunCpu
            // 
            this.btnTaskRunCpu.BackColor = System.Drawing.Color.FromArgb(232, 245, 233);
            this.btnTaskRunCpu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaskRunCpu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTaskRunCpu.ForeColor = System.Drawing.Color.FromArgb(27, 94, 32);
            this.btnTaskRunCpu.Location = new System.Drawing.Point(9, 54);
            this.btnTaskRunCpu.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.btnTaskRunCpu.Name = "btnTaskRunCpu";
            this.btnTaskRunCpu.Size = new System.Drawing.Size(260, 34);
            this.btnTaskRunCpu.TabIndex = 1;
            this.btnTaskRunCpu.Text = "Task.RunでCPU処理 | 改善";
            this.btnTaskRunCpu.UseVisualStyleBackColor = false;
            this.btnTaskRunCpu.Click += new System.EventHandler(this.btnTaskRunCpu_Click);
            // 
            // btnAsyncIo
            // 
            this.btnAsyncIo.BackColor = System.Drawing.Color.FromArgb(227, 242, 253);
            this.btnAsyncIo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsyncIo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAsyncIo.ForeColor = System.Drawing.Color.FromArgb(13, 71, 161);
            this.btnAsyncIo.Location = new System.Drawing.Point(9, 99);
            this.btnAsyncIo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.btnAsyncIo.Name = "btnAsyncIo";
            this.btnAsyncIo.Size = new System.Drawing.Size(260, 34);
            this.btnAsyncIo.TabIndex = 2;
            this.btnAsyncIo.Text = "async I/O待機 | 改善";
            this.btnAsyncIo.UseVisualStyleBackColor = false;
            this.btnAsyncIo.Click += new System.EventHandler(this.btnAsyncIo_Click);
            // 
            // btnWait
            // 
            this.btnWait.BackColor = System.Drawing.Color.FromArgb(255, 243, 224);
            this.btnWait.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWait.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnWait.ForeColor = System.Drawing.Color.FromArgb(191, 54, 12);
            this.btnWait.Location = new System.Drawing.Point(9, 144);
            this.btnWait.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.btnWait.Name = "btnWait";
            this.btnWait.Size = new System.Drawing.Size(260, 34);
            this.btnWait.TabIndex = 3;
            this.btnWait.Text = ".Waitで待機 | 危険";
            this.btnWait.UseVisualStyleBackColor = false;
            this.btnWait.Click += new System.EventHandler(this.btnWait_Click);
            // 
            // btnInvokeWait
            // 
            this.btnInvokeWait.BackColor = System.Drawing.Color.FromArgb(255, 248, 225);
            this.btnInvokeWait.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvokeWait.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnInvokeWait.ForeColor = System.Drawing.Color.FromArgb(255, 111, 0);
            this.btnInvokeWait.Location = new System.Drawing.Point(9, 189);
            this.btnInvokeWait.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.btnInvokeWait.Name = "btnInvokeWait";
            this.btnInvokeWait.Size = new System.Drawing.Size(260, 34);
            this.btnInvokeWait.TabIndex = 4;
            this.btnInvokeWait.Text = "Invoke + Wait | 危険";
            this.btnInvokeWait.UseVisualStyleBackColor = false;
            this.btnInvokeWait.Click += new System.EventHandler(this.btnInvokeWait_Click);
            // 
            // btnBeginInvoke
            // 
            this.btnBeginInvoke.BackColor = System.Drawing.Color.FromArgb(243, 229, 245);
            this.btnBeginInvoke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBeginInvoke.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBeginInvoke.ForeColor = System.Drawing.Color.FromArgb(106, 27, 154);
            this.btnBeginInvoke.Location = new System.Drawing.Point(9, 234);
            this.btnBeginInvoke.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.btnBeginInvoke.Name = "btnBeginInvoke";
            this.btnBeginInvoke.Size = new System.Drawing.Size(260, 34);
            this.btnBeginInvoke.TabIndex = 5;
            this.btnBeginInvoke.Text = "BeginInvokeで更新 | 参考";
            this.btnBeginInvoke.UseVisualStyleBackColor = false;
            this.btnBeginInvoke.Click += new System.EventHandler(this.btnBeginInvoke_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClearLog.ForeColor = System.Drawing.Color.FromArgb(69, 90, 100);
            this.btnClearLog.Location = new System.Drawing.Point(9, 279);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(260, 34);
            this.btnClearLog.TabIndex = 6;
            this.btnClearLog.Text = "ログをクリア";
            this.btnClearLog.UseVisualStyleBackColor = false;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // grpMonitor
            // 
            this.grpMonitor.Controls.Add(this.monitorLayout);
            this.grpMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMonitor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpMonitor.Location = new System.Drawing.Point(333, 13);
            this.grpMonitor.Name = "grpMonitor";
            this.grpMonitor.Padding = new System.Windows.Forms.Padding(12);
            this.grpMonitor.Size = new System.Drawing.Size(1020, 319);
            this.grpMonitor.TabIndex = 1;
            this.grpMonitor.TabStop = false;
            this.grpMonitor.Text = "観測";
            // 
            // monitorLayout
            // 
            this.monitorLayout.ColumnCount = 2;
            this.monitorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.monitorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.monitorLayout.Controls.Add(this.lblStatusTitle, 0, 0);
            this.monitorLayout.Controls.Add(this.lblStatusValue, 1, 0);
            this.monitorLayout.Controls.Add(this.lblHeartbeatTitle, 0, 1);
            this.monitorLayout.Controls.Add(this.lblHeartbeat, 1, 1);
            this.monitorLayout.Controls.Add(this.lblWorkTitle, 0, 2);
            this.monitorLayout.Controls.Add(this.prgWork, 1, 2);
            this.monitorLayout.Controls.Add(this.lblHeartbeatBarTitle, 0, 3);
            this.monitorLayout.Controls.Add(this.prgHeartbeat, 1, 3);
            this.monitorLayout.Controls.Add(this.txtDescription, 0, 4);
            this.monitorLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorLayout.Location = new System.Drawing.Point(12, 30);
            this.monitorLayout.Name = "monitorLayout";
            this.monitorLayout.RowCount = 5;
            this.monitorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.monitorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.monitorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.monitorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.monitorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.monitorLayout.Size = new System.Drawing.Size(996, 277);
            this.monitorLayout.TabIndex = 0;
            this.monitorLayout.SetColumnSpan(this.txtDescription, 2);
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatusTitle.Location = new System.Drawing.Point(3, 13);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(42, 19);
            this.lblStatusTitle.TabIndex = 0;
            this.lblStatusTitle.Text = "状態";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.lblStatusValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(70, 80, 95);
            this.lblStatusValue.Location = new System.Drawing.Point(153, 6);
            this.lblStatusValue.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.lblStatusValue.Size = new System.Drawing.Size(840, 34);
            this.lblStatusValue.TabIndex = 1;
            this.lblStatusValue.Text = "待機";
            this.lblStatusValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeartbeatTitle
            // 
            this.lblHeartbeatTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHeartbeatTitle.AutoSize = true;
            this.lblHeartbeatTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeartbeatTitle.Location = new System.Drawing.Point(3, 57);
            this.lblHeartbeatTitle.Name = "lblHeartbeatTitle";
            this.lblHeartbeatTitle.Size = new System.Drawing.Size(42, 19);
            this.lblHeartbeatTitle.TabIndex = 2;
            this.lblHeartbeatTitle.Text = "心拍";
            // 
            // lblHeartbeat
            // 
            this.lblHeartbeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeartbeat.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeartbeat.ForeColor = System.Drawing.Color.FromArgb(13, 71, 161);
            this.lblHeartbeat.Location = new System.Drawing.Point(153, 54);
            this.lblHeartbeat.Name = "lblHeartbeat";
            this.lblHeartbeat.Size = new System.Drawing.Size(840, 24);
            this.lblHeartbeat.TabIndex = 3;
            this.lblHeartbeat.Text = "心拍: --:--:--.---";
            this.lblHeartbeat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWorkTitle
            // 
            this.lblWorkTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblWorkTitle.AutoSize = true;
            this.lblWorkTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWorkTitle.Location = new System.Drawing.Point(3, 96);
            this.lblWorkTitle.Name = "lblWorkTitle";
            this.lblWorkTitle.Size = new System.Drawing.Size(70, 19);
            this.lblWorkTitle.TabIndex = 4;
            this.lblWorkTitle.Text = "作業進捗";
            // 
            // prgWork
            // 
            this.prgWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgWork.EndColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.prgWork.Location = new System.Drawing.Point(153, 92);
            this.prgWork.Maximum = 100;
            this.prgWork.Name = "prgWork";
            this.prgWork.ShowShine = true;
            this.prgWork.Size = new System.Drawing.Size(840, 32);
            this.prgWork.StartColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.prgWork.TabIndex = 5;
            this.prgWork.TrackColor = System.Drawing.Color.FromArgb(236, 240, 245);
            this.prgWork.Value = 0;
            // 
            // lblHeartbeatBarTitle
            // 
            this.lblHeartbeatBarTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHeartbeatBarTitle.AutoSize = true;
            this.lblHeartbeatBarTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeartbeatBarTitle.Location = new System.Drawing.Point(3, 136);
            this.lblHeartbeatBarTitle.Name = "lblHeartbeatBarTitle";
            this.lblHeartbeatBarTitle.Size = new System.Drawing.Size(98, 19);
            this.lblHeartbeatBarTitle.TabIndex = 6;
            this.lblHeartbeatBarTitle.Text = "心拍プログレス";
            // 
            // prgHeartbeat
            // 
            this.prgHeartbeat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgHeartbeat.EndColor = System.Drawing.Color.FromArgb(0, 184, 148);
            this.prgHeartbeat.Location = new System.Drawing.Point(153, 132);
            this.prgHeartbeat.Maximum = 1000;
            this.prgHeartbeat.Name = "prgHeartbeat";
            this.prgHeartbeat.ShowShine = true;
            this.prgHeartbeat.Size = new System.Drawing.Size(840, 32);
            this.prgHeartbeat.StartColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.prgHeartbeat.TabIndex = 7;
            this.prgHeartbeat.TrackColor = System.Drawing.Color.FromArgb(236, 240, 245);
            this.prgHeartbeat.Value = 0;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDescription.Location = new System.Drawing.Point(3, 171);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(990, 103);
            this.txtDescription.TabIndex = 8;
            this.txtDescription.Text = "色の意味\r\n- 赤/橙: UIが止まりやすい危険例\r\n- 青/緑: UIを止めにくい改善例\r\n- 紫: UI更新方法の比較\r\n\r\n見る点\r\n- 心拍プログレスが止まるか\r\n- 作業進捗が途中で動くか\r\n- ログの順番がどう変わるか\r\n\r\n補足\r\n- .Wait と Invoke + Wait は学習用にタイムアウト復帰を入れている\r\n- 実務コードでは無期限待ちのまま残ると閉じられない固まり方になりやすい";
            // 
            // grpLog
            // 
            this.grpLog.Controls.Add(this.txtLog);
            this.grpLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLog.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpLog.Location = new System.Drawing.Point(13, 338);
            this.grpLog.Name = "grpLog";
            this.grpLog.Padding = new System.Windows.Forms.Padding(12);
            this.grpLog.Size = new System.Drawing.Size(1340, 417);
            this.grpLog.TabIndex = 2;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "ログ";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(252, 253, 255);
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtLog.Location = new System.Drawing.Point(12, 30);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1316, 375);
            this.txtLog.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.rootLayout);
            this.MinimumSize = new System.Drawing.Size(1180, 720);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinForms UIフリーズ学習サンプル";
            this.rootLayout.ResumeLayout(false);
            this.grpCases.ResumeLayout(false);
            this.pnlCaseButtons.ResumeLayout(false);
            this.grpMonitor.ResumeLayout(false);
            this.monitorLayout.ResumeLayout(false);
            this.monitorLayout.PerformLayout();
            this.grpLog.ResumeLayout(false);
            this.grpLog.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
