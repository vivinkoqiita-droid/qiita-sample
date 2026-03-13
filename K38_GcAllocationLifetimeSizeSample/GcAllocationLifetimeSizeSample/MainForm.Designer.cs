#nullable enable

namespace GcAllocationLifetimeSizeSample
{
    partial class MainForm
    {
        /// <summary>
        /// デザイナー管理コンポーネント。
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        private TableLayoutPanel tlpRoot = null!;
        private Panel pnlHeader = null!;
        private Label lblHeaderTitle = null!;
        private Label lblHeaderSub = null!;
        private Panel pnlControls = null!;
        private TableLayoutPanel tlpControlRoot = null!;
        private TableLayoutPanel tlpInputs = null!;
        private Label lblScenario = null!;
        private Label lblLoop = null!;
        private Label lblSize = null!;
        private TableLayoutPanel tlpButtons = null!;
        private Panel pnlGuide = null!;
        private TableLayoutPanel tlpMain = null!;
        private TableLayoutPanel tlpLeft = null!;
        private Panel pnlMetricsTop = null!;
        private Label lblMetricsTopTitle = null!;
        private Label lblMetricsTopSub = null!;
        private TableLayoutPanel tlpTopCards = null!;
        private Panel pnlMetricsBottom = null!;
        private Label lblMetricsBottomTitle = null!;
        private Label lblMetricsBottomSub = null!;
        private TableLayoutPanel tlpBottomCards = null!;
        private TableLayoutPanel tlpRight = null!;
        private Panel pnlDiagnosis = null!;
        private Label lblDiagnosisTitle = null!;
        private Label lblDiagnosisSub = null!;
        private Panel pnlSuspects = null!;
        private Label lblSuspectsTitle = null!;
        private Label lblSuspectsSub = null!;
        private Panel pnlLog = null!;
        private Label lblLogTitle = null!;
        private Label lblLogSub = null!;

        private ComboBox cboScenario = null!;
        private NumericUpDown nudLoopCount = null!;
        private NumericUpDown nudBufferSize = null!;
        private CheckBox chkKeepReferences = null!;
        private Button btnStart = null!;
        private Button btnStop = null!;
        private Button btnClearHeld = null!;
        private Button btnForceGc = null!;
        private Button btnReset = null!;
        private Label lblStatus = null!;
        private Label lblScenarioGuide = null!;
        private RichTextBox txtDiagnosis = null!;
        private RichTextBox txtSuspects = null!;
        private TextBox txtLog = null!;

        private Panel pnlAllocCard = null!;
        private Label lblAllocTitle = null!;
        private Label lblAllocValue = null!;
        private Label lblAllocNote = null!;
        private Panel pnlLifetimeCard = null!;
        private Label lblLifetimeTitle = null!;
        private Label lblLifetimeValue = null!;
        private Label lblLifetimeNote = null!;
        private Panel pnlLargeCard = null!;
        private Label lblLargeTitle = null!;
        private Label lblLargeValue = null!;
        private Label lblLargeNote = null!;
        private Panel pnlMemoryCard = null!;
        private Label lblMemoryTitle = null!;
        private Label lblMemoryValue = null!;
        private Label lblMemoryNote = null!;
        private Panel pnlGen0Card = null!;
        private Label lblGen0Title = null!;
        private Label lblGen0Value = null!;
        private Label lblGen0Note = null!;
        private Panel pnlGen2Card = null!;
        private Label lblGen2Title = null!;
        private Label lblGen2Value = null!;
        private Label lblGen2Note = null!;
        private Panel pnlFinalizedCard = null!;
        private Label lblFinalizedTitle = null!;
        private Label lblFinalizedValue = null!;
        private Label lblFinalizedNote = null!;
        private Panel pnlHeldCard = null!;
        private Label lblHeldTitle = null!;
        private Label lblHeldValue = null!;
        private Label lblHeldNote = null!;

        /// <summary>
        /// リソース破棄処理。
        /// </summary>
        /// <param name="disposing">破棄モード。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// デザイナー初期化処理。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlHeader = new Panel();
            lblHeaderTitle = new Label();
            lblHeaderSub = new Label();
            lblStatus = new Label();
            pnlControls = new Panel();
            tlpControlRoot = new TableLayoutPanel();
            tlpInputs = new TableLayoutPanel();
            lblScenario = new Label();
            cboScenario = new ComboBox();
            lblLoop = new Label();
            nudLoopCount = new NumericUpDown();
            lblSize = new Label();
            nudBufferSize = new NumericUpDown();
            chkKeepReferences = new CheckBox();
            tlpButtons = new TableLayoutPanel();
            btnStart = new Button();
            btnStop = new Button();
            btnClearHeld = new Button();
            btnForceGc = new Button();
            btnReset = new Button();
            pnlGuide = new Panel();
            lblScenarioGuide = new Label();
            tlpMain = new TableLayoutPanel();
            tlpLeft = new TableLayoutPanel();
            pnlMetricsTop = new Panel();
            lblMetricsTopTitle = new Label();
            lblMetricsTopSub = new Label();
            tlpTopCards = new TableLayoutPanel();
            pnlAllocCard = new Panel();
            lblAllocTitle = new Label();
            lblAllocValue = new Label();
            lblAllocNote = new Label();
            pnlLifetimeCard = new Panel();
            lblLifetimeTitle = new Label();
            lblLifetimeValue = new Label();
            lblLifetimeNote = new Label();
            pnlLargeCard = new Panel();
            lblLargeTitle = new Label();
            lblLargeValue = new Label();
            lblLargeNote = new Label();
            pnlMemoryCard = new Panel();
            lblMemoryTitle = new Label();
            lblMemoryValue = new Label();
            lblMemoryNote = new Label();
            pnlMetricsBottom = new Panel();
            lblMetricsBottomTitle = new Label();
            lblMetricsBottomSub = new Label();
            tlpBottomCards = new TableLayoutPanel();
            pnlGen0Card = new Panel();
            lblGen0Title = new Label();
            lblGen0Value = new Label();
            lblGen0Note = new Label();
            pnlGen2Card = new Panel();
            lblGen2Title = new Label();
            lblGen2Value = new Label();
            lblGen2Note = new Label();
            pnlFinalizedCard = new Panel();
            lblFinalizedTitle = new Label();
            lblFinalizedValue = new Label();
            lblFinalizedNote = new Label();
            pnlHeldCard = new Panel();
            lblHeldTitle = new Label();
            lblHeldValue = new Label();
            lblHeldNote = new Label();
            tlpRight = new TableLayoutPanel();
            pnlDiagnosis = new Panel();
            lblDiagnosisTitle = new Label();
            lblDiagnosisSub = new Label();
            txtDiagnosis = new RichTextBox();
            pnlSuspects = new Panel();
            lblSuspectsTitle = new Label();
            lblSuspectsSub = new Label();
            txtSuspects = new RichTextBox();
            pnlLog = new Panel();
            lblLogTitle = new Label();
            lblLogSub = new Label();
            txtLog = new TextBox();
            ((System.ComponentModel.ISupportInitialize)nudLoopCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBufferSize).BeginInit();
            SuspendLayout();
            // MainForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1360, 1020);
            Controls.Add(tlpRoot);
            MinimumSize = new Size(1240, 920);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GC Allocation / Lifetime / Size Sample";
            Load += MainForm_Load;
            // tlpRoot
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlHeader, 0, 0);
            tlpRoot.Controls.Add(pnlControls, 0, 1);
            tlpRoot.Controls.Add(tlpMain, 0, 2);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Padding = new Padding(16);
            tlpRoot.RowCount = 3;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 196F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // pnlHeader
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Controls.Add(lblHeaderSub);
            pnlHeader.Controls.Add(lblStatus);
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Margin = new Padding(0, 0, 0, 12);
            pnlHeader.Padding = new Padding(18, 14, 18, 12);
            // lblHeaderTitle
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblHeaderTitle.ForeColor = Color.FromArgb(24, 39, 75);
            lblHeaderTitle.Location = new Point(18, 10);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(290, 37);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "GCだけを疑わない見える化";
            // lblHeaderSub
            lblHeaderSub.AutoSize = true;
            lblHeaderSub.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblHeaderSub.ForeColor = Color.FromArgb(70, 78, 96);
            lblHeaderSub.Location = new Point(22, 52);
            lblHeaderSub.MaximumSize = new Size(980, 0);
            lblHeaderSub.Name = "lblHeaderSub";
            lblHeaderSub.Size = new Size(686, 19);
            lblHeaderSub.TabIndex = 1;
            lblHeaderSub.Text = "割り当て・長く残る・サイズの3方向を、動き続ける数字で見ます。開始後は放置でも数値が変わります。";
            // lblStatus
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            lblStatus.ForeColor = Color.FromArgb(24, 39, 75);
            lblStatus.Location = new Point(1094, 18);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(200, 24);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "待機中";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // pnlControls
            pnlControls.BackColor = Color.White;
            pnlControls.Controls.Add(tlpControlRoot);
            pnlControls.Dock = DockStyle.Fill;
            pnlControls.Margin = new Padding(0, 0, 0, 12);
            pnlControls.Padding = new Padding(16, 14, 16, 16);
            // tlpControlRoot
            tlpControlRoot.ColumnCount = 1;
            tlpControlRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpControlRoot.Controls.Add(tlpInputs, 0, 0);
            tlpControlRoot.Controls.Add(tlpButtons, 0, 1);
            tlpControlRoot.Controls.Add(pnlGuide, 0, 2);
            tlpControlRoot.Dock = DockStyle.Fill;
            tlpControlRoot.RowCount = 3;
            tlpControlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpControlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            tlpControlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            // tlpInputs
            tlpInputs.ColumnCount = 7;
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 48F));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 56F));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tlpInputs.Controls.Add(lblScenario, 0, 0);
            tlpInputs.Controls.Add(cboScenario, 1, 0);
            tlpInputs.Controls.Add(lblLoop, 2, 0);
            tlpInputs.Controls.Add(nudLoopCount, 3, 0);
            tlpInputs.Controls.Add(lblSize, 4, 0);
            tlpInputs.Controls.Add(nudBufferSize, 5, 0);
            tlpInputs.Controls.Add(chkKeepReferences, 6, 0);
            tlpInputs.Dock = DockStyle.Fill;
            tlpInputs.RowCount = 1;
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // lblScenario
            lblScenario.Anchor = AnchorStyles.Left;
            lblScenario.AutoSize = true;
            lblScenario.Location = new Point(3, 11);
            lblScenario.Name = "lblScenario";
            lblScenario.Size = new Size(55, 15);
            lblScenario.TabIndex = 0;
            lblScenario.Text = "シナリオ";
            // cboScenario
            cboScenario.Dock = DockStyle.Fill;
            cboScenario.DropDownStyle = ComboBoxStyle.DropDownList;
            cboScenario.FormattingEnabled = true;
            cboScenario.Location = new Point(73, 5);
            cboScenario.Margin = new Padding(3, 5, 8, 5);
            cboScenario.Name = "cboScenario";
            cboScenario.Size = new Size(661, 23);
            cboScenario.TabIndex = 1;
            cboScenario.SelectedIndexChanged += cboScenario_SelectedIndexChanged;
            // lblLoop
            lblLoop.Anchor = AnchorStyles.Left;
            lblLoop.AutoSize = true;
            lblLoop.Location = new Point(745, 11);
            lblLoop.Name = "lblLoop";
            lblLoop.Size = new Size(31, 15);
            lblLoop.TabIndex = 2;
            lblLoop.Text = "強さ";
            // nudLoopCount
            nudLoopCount.Dock = DockStyle.Fill;
            nudLoopCount.Location = new Point(790, 5);
            nudLoopCount.Margin = new Padding(5);
            nudLoopCount.Maximum = new decimal(new int[] { 500000, 0, 0, 0 });
            nudLoopCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLoopCount.Name = "nudLoopCount";
            nudLoopCount.Size = new Size(110, 23);
            nudLoopCount.TabIndex = 3;
            nudLoopCount.TextAlign = HorizontalAlignment.Right;
            nudLoopCount.ThousandsSeparator = true;
            nudLoopCount.Value = new decimal(new int[] { 20000, 0, 0, 0 });
            // lblSize
            lblSize.Anchor = AnchorStyles.Left;
            lblSize.AutoSize = true;
            lblSize.Location = new Point(913, 11);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(43, 15);
            lblSize.TabIndex = 4;
            lblSize.Text = "サイズ";
            // nudBufferSize
            nudBufferSize.Dock = DockStyle.Fill;
            nudBufferSize.Location = new Point(964, 5);
            nudBufferSize.Margin = new Padding(3, 5, 8, 5);
            nudBufferSize.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudBufferSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudBufferSize.Name = "nudBufferSize";
            nudBufferSize.Size = new Size(119, 23);
            nudBufferSize.TabIndex = 5;
            nudBufferSize.TextAlign = HorizontalAlignment.Right;
            nudBufferSize.ThousandsSeparator = true;
            nudBufferSize.Value = new decimal(new int[] { 200000, 0, 0, 0 });
            // chkKeepReferences
            chkKeepReferences.Anchor = AnchorStyles.Left;
            chkKeepReferences.AutoSize = true;
            chkKeepReferences.Location = new Point(1097, 9);
            chkKeepReferences.Name = "chkKeepReferences";
            chkKeepReferences.Size = new Size(134, 19);
            chkKeepReferences.TabIndex = 6;
            chkKeepReferences.Text = "作ったものを保持する";
            chkKeepReferences.UseVisualStyleBackColor = true;
            // tlpButtons
            tlpButtons.ColumnCount = 5;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpButtons.Controls.Add(btnStart, 0, 0);
            tlpButtons.Controls.Add(btnStop, 1, 0);
            tlpButtons.Controls.Add(btnClearHeld, 2, 0);
            tlpButtons.Controls.Add(btnForceGc, 3, 0);
            tlpButtons.Controls.Add(btnReset, 4, 0);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Margin = new Padding(0, 4, 0, 4);
            tlpButtons.RowCount = 1;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // btnStart
            btnStart.BackColor = Color.FromArgb(46, 125, 50);
            btnStart.Dock = DockStyle.Fill;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            btnStart.ForeColor = Color.White;
            btnStart.Margin = new Padding(4);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(251, 48);
            btnStart.TabIndex = 0;
            btnStart.Text = "開始";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // btnStop
            btnStop.BackColor = Color.FromArgb(239, 83, 80);
            btnStop.Dock = DockStyle.Fill;
            btnStop.FlatAppearance.BorderSize = 0;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            btnStop.ForeColor = Color.White;
            btnStop.Margin = new Padding(4);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(251, 48);
            btnStop.TabIndex = 1;
            btnStop.Text = "停止";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // btnClearHeld
            btnClearHeld.BackColor = Color.FromArgb(2, 136, 209);
            btnClearHeld.Dock = DockStyle.Fill;
            btnClearHeld.FlatAppearance.BorderSize = 0;
            btnClearHeld.FlatStyle = FlatStyle.Flat;
            btnClearHeld.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            btnClearHeld.ForeColor = Color.White;
            btnClearHeld.Margin = new Padding(4);
            btnClearHeld.Name = "btnClearHeld";
            btnClearHeld.Size = new Size(251, 48);
            btnClearHeld.TabIndex = 2;
            btnClearHeld.Text = "保持クリア";
            btnClearHeld.UseVisualStyleBackColor = false;
            btnClearHeld.Click += btnClearHeld_Click;
            // btnForceGc
            btnForceGc.BackColor = Color.FromArgb(123, 31, 162);
            btnForceGc.Dock = DockStyle.Fill;
            btnForceGc.FlatAppearance.BorderSize = 0;
            btnForceGc.FlatStyle = FlatStyle.Flat;
            btnForceGc.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            btnForceGc.ForeColor = Color.White;
            btnForceGc.Margin = new Padding(4);
            btnForceGc.Name = "btnForceGc";
            btnForceGc.Size = new Size(251, 48);
            btnForceGc.TabIndex = 3;
            btnForceGc.Text = "GC強制実行";
            btnForceGc.UseVisualStyleBackColor = false;
            btnForceGc.Click += btnForceGc_Click;
            // btnReset
            btnReset.Dock = DockStyle.Fill;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnReset.Margin = new Padding(4);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(252, 48);
            btnReset.TabIndex = 4;
            btnReset.Text = "リセット";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // pnlGuide
            pnlGuide.BackColor = Color.FromArgb(248, 249, 252);
            pnlGuide.Controls.Add(lblScenarioGuide);
            pnlGuide.Dock = DockStyle.Fill;
            pnlGuide.Margin = new Padding(0, 4, 0, 0);
            pnlGuide.Padding = new Padding(12, 8, 12, 8);
            // lblScenarioGuide
            lblScenarioGuide.Dock = DockStyle.Fill;
            lblScenarioGuide.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblScenarioGuide.ForeColor = Color.FromArgb(54, 62, 79);
            lblScenarioGuide.Location = new Point(12, 8);
            lblScenarioGuide.Name = "lblScenarioGuide";
            lblScenarioGuide.Size = new Size(1272, 36);
            lblScenarioGuide.TabIndex = 0;
            lblScenarioGuide.Text = "ここを見る: まず左の指標で方向を見て、次に右側の説明で理由を確認し、最後に下のログで流れを追う";
            // tlpMain
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            tlpMain.Controls.Add(tlpLeft, 0, 0);
            tlpMain.Controls.Add(tlpRight, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // tlpLeft
            tlpLeft.ColumnCount = 1;
            tlpLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLeft.Controls.Add(pnlMetricsTop, 0, 0);
            tlpLeft.Controls.Add(pnlMetricsBottom, 0, 1);
            tlpLeft.Dock = DockStyle.Fill;
            tlpLeft.RowCount = 2;
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpLeft.Margin = new Padding(0, 0, 12, 0);
            // pnlMetricsTop
            pnlMetricsTop.BackColor = Color.White;
            pnlMetricsTop.Controls.Add(tlpTopCards);
            pnlMetricsTop.Controls.Add(lblMetricsTopSub);
            pnlMetricsTop.Controls.Add(lblMetricsTopTitle);
            pnlMetricsTop.Dock = DockStyle.Fill;
            pnlMetricsTop.Padding = new Padding(16, 14, 16, 16);
            pnlMetricsTop.Margin = new Padding(0, 0, 0, 12);
            // lblMetricsTopTitle
            lblMetricsTopTitle.Dock = DockStyle.Top;
            lblMetricsTopTitle.Font = new Font("Segoe UI Semibold", 11.5F, FontStyle.Bold, GraphicsUnit.Point);
            lblMetricsTopTitle.ForeColor = Color.FromArgb(24, 39, 75);
            lblMetricsTopTitle.Location = new Point(16, 14);
            lblMetricsTopTitle.Name = "lblMetricsTopTitle";
            lblMetricsTopTitle.Size = new Size(792, 24);
            lblMetricsTopTitle.TabIndex = 0;
            lblMetricsTopTitle.Text = "最初に見る指標";
            // lblMetricsTopSub
            lblMetricsTopSub.Dock = DockStyle.Top;
            lblMetricsTopSub.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblMetricsTopSub.ForeColor = Color.FromArgb(90, 100, 120);
            lblMetricsTopSub.Location = new Point(16, 38);
            lblMetricsTopSub.Name = "lblMetricsTopSub";
            lblMetricsTopSub.Size = new Size(792, 22);
            lblMetricsTopSub.TabIndex = 1;
            lblMetricsTopSub.Text = "割り当て・長く残る・サイズのどれが主因かを先に見る欄";
            // tlpTopCards
            tlpTopCards.ColumnCount = 2;
            tlpTopCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTopCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTopCards.Controls.Add(pnlAllocCard, 0, 0);
            tlpTopCards.Controls.Add(pnlLifetimeCard, 1, 0);
            tlpTopCards.Controls.Add(pnlLargeCard, 0, 1);
            tlpTopCards.Controls.Add(pnlMemoryCard, 1, 1);
            tlpTopCards.Dock = DockStyle.Fill;
            tlpTopCards.Location = new Point(16, 60);
            tlpTopCards.Margin = new Padding(0);
            tlpTopCards.RowCount = 2;
            tlpTopCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTopCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTopCards.Size = new Size(792, 178);
            // pnlAllocCard
            pnlAllocCard.BackColor = Color.FromArgb(248, 249, 252);
            pnlAllocCard.Controls.Add(lblAllocNote);
            pnlAllocCard.Controls.Add(lblAllocValue);
            pnlAllocCard.Controls.Add(lblAllocTitle);
            pnlAllocCard.Dock = DockStyle.Fill;
            pnlAllocCard.Margin = new Padding(6);
            pnlAllocCard.Padding = new Padding(18, 16, 18, 16);
            // lblAllocTitle
            lblAllocTitle.AutoSize = true;
            lblAllocTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblAllocTitle.ForeColor = Color.FromArgb(76, 86, 106);
            lblAllocTitle.Location = new Point(18, 16);
            lblAllocTitle.Text = "作られる量";
            // lblAllocValue
            lblAllocValue.AutoSize = true;
            lblAllocValue.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblAllocValue.ForeColor = Color.FromArgb(24, 39, 75);
            lblAllocValue.Location = new Point(18, 47);
            lblAllocValue.Text = "0 件 / 0.5秒";
            // lblAllocNote
            lblAllocNote.AutoSize = true;
            lblAllocNote.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblAllocNote.ForeColor = Color.FromArgb(90, 100, 120);
            lblAllocNote.Location = new Point(18, 89);
            lblAllocNote.Text = "動きなし";
            // pnlLifetimeCard
            pnlLifetimeCard.BackColor = Color.FromArgb(248, 249, 252);
            pnlLifetimeCard.Controls.Add(lblLifetimeNote);
            pnlLifetimeCard.Controls.Add(lblLifetimeValue);
            pnlLifetimeCard.Controls.Add(lblLifetimeTitle);
            pnlLifetimeCard.Dock = DockStyle.Fill;
            pnlLifetimeCard.Margin = new Padding(6);
            pnlLifetimeCard.Padding = new Padding(18, 16, 18, 16);
            // lblLifetimeTitle
            lblLifetimeTitle.AutoSize = true;
            lblLifetimeTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblLifetimeTitle.ForeColor = Color.FromArgb(76, 86, 106);
            lblLifetimeTitle.Location = new Point(18, 16);
            lblLifetimeTitle.Text = "長く残る量";
            // lblLifetimeValue
            lblLifetimeValue.AutoSize = true;
            lblLifetimeValue.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblLifetimeValue.ForeColor = Color.FromArgb(24, 39, 75);
            lblLifetimeValue.Location = new Point(18, 47);
            lblLifetimeValue.Text = "0 件";
            // lblLifetimeNote
            lblLifetimeNote.AutoSize = true;
            lblLifetimeNote.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblLifetimeNote.ForeColor = Color.FromArgb(90, 100, 120);
            lblLifetimeNote.Location = new Point(18, 89);
            lblLifetimeNote.Text = "保持量 0.00 MB";
            // pnlLargeCard
            pnlLargeCard.BackColor = Color.FromArgb(248, 249, 252);
            pnlLargeCard.Controls.Add(lblLargeNote);
            pnlLargeCard.Controls.Add(lblLargeValue);
            pnlLargeCard.Controls.Add(lblLargeTitle);
            pnlLargeCard.Dock = DockStyle.Fill;
            pnlLargeCard.Margin = new Padding(6);
            pnlLargeCard.Padding = new Padding(18, 16, 18, 16);
            // lblLargeTitle
            lblLargeTitle.AutoSize = true;
            lblLargeTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblLargeTitle.ForeColor = Color.FromArgb(76, 86, 106);
            lblLargeTitle.Location = new Point(18, 16);
            lblLargeTitle.Text = "大きい確保";
            // lblLargeValue
            lblLargeValue.AutoSize = true;
            lblLargeValue.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblLargeValue.ForeColor = Color.FromArgb(24, 39, 75);
            lblLargeValue.Location = new Point(18, 47);
            lblLargeValue.Text = "0 件 / 0.5秒";
            // lblLargeNote
            lblLargeNote.AutoSize = true;
            lblLargeNote.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblLargeNote.ForeColor = Color.FromArgb(90, 100, 120);
            lblLargeNote.Location = new Point(18, 89);
            lblLargeNote.Text = "大きい確保なし";
            // pnlMemoryCard
            pnlMemoryCard.BackColor = Color.FromArgb(248, 249, 252);
            pnlMemoryCard.Controls.Add(lblMemoryNote);
            pnlMemoryCard.Controls.Add(lblMemoryValue);
            pnlMemoryCard.Controls.Add(lblMemoryTitle);
            pnlMemoryCard.Dock = DockStyle.Fill;
            pnlMemoryCard.Margin = new Padding(6);
            pnlMemoryCard.Padding = new Padding(18, 16, 18, 16);
            // lblMemoryTitle
            lblMemoryTitle.AutoSize = true;
            lblMemoryTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblMemoryTitle.ForeColor = Color.FromArgb(76, 86, 106);
            lblMemoryTitle.Location = new Point(18, 16);
            lblMemoryTitle.Text = "総メモリ";
            // lblMemoryValue
            lblMemoryValue.AutoSize = true;
            lblMemoryValue.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblMemoryValue.ForeColor = Color.FromArgb(24, 39, 75);
            lblMemoryValue.Location = new Point(18, 47);
            lblMemoryValue.Text = "0.00 MB";
            // lblMemoryNote
            lblMemoryNote.AutoSize = true;
            lblMemoryNote.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblMemoryNote.ForeColor = Color.FromArgb(90, 100, 120);
            lblMemoryNote.Location = new Point(18, 89);
            lblMemoryNote.Text = "開始時から +0.00 MB";
            // pnlMetricsBottom
            pnlMetricsBottom.BackColor = Color.White;
            pnlMetricsBottom.Controls.Add(tlpBottomCards);
            pnlMetricsBottom.Controls.Add(lblMetricsBottomSub);
            pnlMetricsBottom.Controls.Add(lblMetricsBottomTitle);
            pnlMetricsBottom.Dock = DockStyle.Fill;
            pnlMetricsBottom.Padding = new Padding(16, 14, 16, 16);
            pnlMetricsBottom.Margin = new Padding(0);
            // lblMetricsBottomTitle
            lblMetricsBottomTitle.Dock = DockStyle.Top;
            lblMetricsBottomTitle.Font = new Font("Segoe UI Semibold", 11.5F, FontStyle.Bold, GraphicsUnit.Point);
            lblMetricsBottomTitle.ForeColor = Color.FromArgb(24, 39, 75);
            lblMetricsBottomTitle.Location = new Point(16, 14);
            lblMetricsBottomTitle.Name = "lblMetricsBottomTitle";
            lblMetricsBottomTitle.Size = new Size(792, 24);
            lblMetricsBottomTitle.Text = "GCにどう出ているか";
            // lblMetricsBottomSub
            lblMetricsBottomSub.Dock = DockStyle.Top;
            lblMetricsBottomSub.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblMetricsBottomSub.ForeColor = Color.FromArgb(90, 100, 120);
            lblMetricsBottomSub.Location = new Point(16, 38);
            lblMetricsBottomSub.Name = "lblMetricsBottomSub";
            lblMetricsBottomSub.Size = new Size(792, 22);
            lblMetricsBottomSub.Text = "短いGC・重いGC・回収・保持にどう表れているかを見る欄";
            // tlpBottomCards
            tlpBottomCards.ColumnCount = 2;
            tlpBottomCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBottomCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBottomCards.Controls.Add(pnlGen0Card, 0, 0);
            tlpBottomCards.Controls.Add(pnlGen2Card, 1, 0);
            tlpBottomCards.Controls.Add(pnlFinalizedCard, 0, 1);
            tlpBottomCards.Controls.Add(pnlHeldCard, 1, 1);
            tlpBottomCards.Dock = DockStyle.Fill;
            tlpBottomCards.Location = new Point(16, 60);
            tlpBottomCards.Margin = new Padding(0);
            tlpBottomCards.RowCount = 2;
            tlpBottomCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpBottomCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpBottomCards.Size = new Size(792, 178);
            // pnlGen0Card
            pnlGen0Card.BackColor = Color.FromArgb(248, 249, 252);
            pnlGen0Card.Controls.Add(lblGen0Note);
            pnlGen0Card.Controls.Add(lblGen0Value);
            pnlGen0Card.Controls.Add(lblGen0Title);
            pnlGen0Card.Dock = DockStyle.Fill;
            pnlGen0Card.Margin = new Padding(6);
            pnlGen0Card.Padding = new Padding(18, 16, 18, 16);
            // lblGen0Title
            lblGen0Title.AutoSize = true;
            lblGen0Title.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblGen0Title.ForeColor = Color.FromArgb(76, 86, 106);
            lblGen0Title.Location = new Point(18, 16);
            lblGen0Title.Text = "短いGC";
            // lblGen0Value
            lblGen0Value.AutoSize = true;
            lblGen0Value.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblGen0Value.ForeColor = Color.FromArgb(24, 39, 75);
            lblGen0Value.Location = new Point(18, 47);
            lblGen0Value.Text = "0 回 / 0.5秒";
            // lblGen0Note
            lblGen0Note.AutoSize = true;
            lblGen0Note.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblGen0Note.ForeColor = Color.FromArgb(90, 100, 120);
            lblGen0Note.Location = new Point(18, 89);
            lblGen0Note.Text = "累計 0 回";
            // pnlGen2Card
            pnlGen2Card.BackColor = Color.FromArgb(248, 249, 252);
            pnlGen2Card.Controls.Add(lblGen2Note);
            pnlGen2Card.Controls.Add(lblGen2Value);
            pnlGen2Card.Controls.Add(lblGen2Title);
            pnlGen2Card.Dock = DockStyle.Fill;
            pnlGen2Card.Margin = new Padding(6);
            pnlGen2Card.Padding = new Padding(18, 16, 18, 16);
            // lblGen2Title
            lblGen2Title.AutoSize = true;
            lblGen2Title.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblGen2Title.ForeColor = Color.FromArgb(76, 86, 106);
            lblGen2Title.Location = new Point(18, 16);
            lblGen2Title.Text = "重いGC";
            // lblGen2Value
            lblGen2Value.AutoSize = true;
            lblGen2Value.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblGen2Value.ForeColor = Color.FromArgb(24, 39, 75);
            lblGen2Value.Location = new Point(18, 47);
            lblGen2Value.Text = "0 回 / 0.5秒";
            // lblGen2Note
            lblGen2Note.AutoSize = true;
            lblGen2Note.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblGen2Note.ForeColor = Color.FromArgb(90, 100, 120);
            lblGen2Note.Location = new Point(18, 89);
            lblGen2Note.Text = "累計 0 回";
            // pnlFinalizedCard
            pnlFinalizedCard.BackColor = Color.FromArgb(248, 249, 252);
            pnlFinalizedCard.Controls.Add(lblFinalizedNote);
            pnlFinalizedCard.Controls.Add(lblFinalizedValue);
            pnlFinalizedCard.Controls.Add(lblFinalizedTitle);
            pnlFinalizedCard.Dock = DockStyle.Fill;
            pnlFinalizedCard.Margin = new Padding(6);
            pnlFinalizedCard.Padding = new Padding(18, 16, 18, 16);
            // lblFinalizedTitle
            lblFinalizedTitle.AutoSize = true;
            lblFinalizedTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblFinalizedTitle.ForeColor = Color.FromArgb(76, 86, 106);
            lblFinalizedTitle.Location = new Point(18, 16);
            lblFinalizedTitle.Text = "回収完了";
            // lblFinalizedValue
            lblFinalizedValue.AutoSize = true;
            lblFinalizedValue.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblFinalizedValue.ForeColor = Color.FromArgb(24, 39, 75);
            lblFinalizedValue.Location = new Point(18, 47);
            lblFinalizedValue.Text = "0 件 / 0.5秒";
            // lblFinalizedNote
            lblFinalizedNote.AutoSize = true;
            lblFinalizedNote.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblFinalizedNote.ForeColor = Color.FromArgb(90, 100, 120);
            lblFinalizedNote.Location = new Point(18, 89);
            lblFinalizedNote.Text = "累計 0 件";
            // pnlHeldCard
            pnlHeldCard.BackColor = Color.FromArgb(248, 249, 252);
            pnlHeldCard.Controls.Add(lblHeldNote);
            pnlHeldCard.Controls.Add(lblHeldValue);
            pnlHeldCard.Controls.Add(lblHeldTitle);
            pnlHeldCard.Dock = DockStyle.Fill;
            pnlHeldCard.Margin = new Padding(6);
            pnlHeldCard.Padding = new Padding(18, 16, 18, 16);
            // lblHeldTitle
            lblHeldTitle.AutoSize = true;
            lblHeldTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblHeldTitle.ForeColor = Color.FromArgb(76, 86, 106);
            lblHeldTitle.Location = new Point(18, 16);
            lblHeldTitle.Text = "保持中";
            // lblHeldValue
            lblHeldValue.AutoSize = true;
            lblHeldValue.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            lblHeldValue.ForeColor = Color.FromArgb(24, 39, 75);
            lblHeldValue.Location = new Point(18, 47);
            lblHeldValue.Text = "0 件 / 0.00 MB";
            // lblHeldNote
            lblHeldNote.AutoSize = true;
            lblHeldNote.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblHeldNote.ForeColor = Color.FromArgb(90, 100, 120);
            lblHeldNote.Location = new Point(18, 89);
            lblHeldNote.Text = "参照解除依頼 累計 0 件";
            // tlpRight
            tlpRight.ColumnCount = 1;
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRight.Controls.Add(pnlDiagnosis, 0, 0);
            tlpRight.Controls.Add(pnlSuspects, 0, 1);
            tlpRight.Controls.Add(pnlLog, 0, 2);
            tlpRight.Dock = DockStyle.Fill;
            tlpRight.RowCount = 3;
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 19F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 21F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            // pnlDiagnosis
            pnlDiagnosis.BackColor = Color.White;
            pnlDiagnosis.Controls.Add(txtDiagnosis);
            pnlDiagnosis.Controls.Add(lblDiagnosisSub);
            pnlDiagnosis.Controls.Add(lblDiagnosisTitle);
            pnlDiagnosis.Dock = DockStyle.Fill;
            pnlDiagnosis.Margin = new Padding(0, 0, 0, 12);
            pnlDiagnosis.Padding = new Padding(16, 14, 16, 14);
            // lblDiagnosisTitle
            lblDiagnosisTitle.Dock = DockStyle.Top;
            lblDiagnosisTitle.Font = new Font("Segoe UI Semibold", 11.5F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiagnosisTitle.ForeColor = Color.FromArgb(24, 39, 75);
            lblDiagnosisTitle.Location = new Point(16, 14);
            lblDiagnosisTitle.Name = "lblDiagnosisTitle";
            lblDiagnosisTitle.Size = new Size(498, 24);
            lblDiagnosisTitle.Text = "いま起きていること";
            // lblDiagnosisSub
            lblDiagnosisSub.Dock = DockStyle.Top;
            lblDiagnosisSub.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblDiagnosisSub.ForeColor = Color.FromArgb(90, 100, 120);
            lblDiagnosisSub.Location = new Point(16, 38);
            lblDiagnosisSub.Name = "lblDiagnosisSub";
            lblDiagnosisSub.Size = new Size(498, 20);
            lblDiagnosisSub.Text = "直近の操作と、いま何が起きているかを短く要約";
            // txtDiagnosis
            txtDiagnosis.BackColor = Color.White;
            txtDiagnosis.BorderStyle = BorderStyle.None;
            txtDiagnosis.Cursor = Cursors.Default;
            txtDiagnosis.DetectUrls = false;
            txtDiagnosis.Dock = DockStyle.Fill;
            txtDiagnosis.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtDiagnosis.ForeColor = Color.FromArgb(54, 62, 79);
            txtDiagnosis.Location = new Point(16, 58);
            txtDiagnosis.Margin = new Padding(0);
            txtDiagnosis.Name = "txtDiagnosis";
            txtDiagnosis.ReadOnly = true;
            txtDiagnosis.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtDiagnosis.ShortcutsEnabled = false;
            txtDiagnosis.Size = new Size(498, 76);
            txtDiagnosis.TabIndex = 1;
            txtDiagnosis.Text = "まだ実行していません。";
            // pnlSuspects
            pnlSuspects.BackColor = Color.White;
            pnlSuspects.Controls.Add(txtSuspects);
            pnlSuspects.Controls.Add(lblSuspectsSub);
            pnlSuspects.Controls.Add(lblSuspectsTitle);
            pnlSuspects.Dock = DockStyle.Fill;
            pnlSuspects.Margin = new Padding(0, 0, 0, 12);
            pnlSuspects.Padding = new Padding(16, 14, 16, 14);
            // lblSuspectsTitle
            lblSuspectsTitle.Dock = DockStyle.Top;
            lblSuspectsTitle.Font = new Font("Segoe UI Semibold", 11.5F, FontStyle.Bold, GraphicsUnit.Point);
            lblSuspectsTitle.ForeColor = Color.FromArgb(24, 39, 75);
            lblSuspectsTitle.Location = new Point(16, 14);
            lblSuspectsTitle.Name = "lblSuspectsTitle";
            lblSuspectsTitle.Size = new Size(498, 24);
            lblSuspectsTitle.Text = "この状態から疑う場所";
            // lblSuspectsSub
            lblSuspectsSub.Dock = DockStyle.Top;
            lblSuspectsSub.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblSuspectsSub.ForeColor = Color.FromArgb(90, 100, 120);
            lblSuspectsSub.Location = new Point(16, 38);
            lblSuspectsSub.Name = "lblSuspectsSub";
            lblSuspectsSub.Size = new Size(498, 20);
            lblSuspectsSub.Text = "次に見るコード候補と、読み方の目安";
            // txtSuspects
            txtSuspects.BackColor = Color.White;
            txtSuspects.BorderStyle = BorderStyle.None;
            txtSuspects.Cursor = Cursors.Default;
            txtSuspects.DetectUrls = false;
            txtSuspects.Dock = DockStyle.Fill;
            txtSuspects.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtSuspects.ForeColor = Color.FromArgb(54, 62, 79);
            txtSuspects.Location = new Point(16, 58);
            txtSuspects.Margin = new Padding(0);
            txtSuspects.Name = "txtSuspects";
            txtSuspects.ReadOnly = true;
            txtSuspects.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtSuspects.ShortcutsEnabled = false;
            txtSuspects.Size = new Size(498, 86);
            txtSuspects.TabIndex = 1;
            txtSuspects.Text = "まだ実行していません。";
            // pnlLog
            pnlLog.BackColor = Color.White;
            pnlLog.Controls.Add(txtLog);
            pnlLog.Controls.Add(lblLogSub);
            pnlLog.Controls.Add(lblLogTitle);
            pnlLog.Dock = DockStyle.Fill;
            pnlLog.Padding = new Padding(16, 14, 16, 16);
            // lblLogTitle
            lblLogTitle.Dock = DockStyle.Top;
            lblLogTitle.Font = new Font("Segoe UI Semibold", 11.5F, FontStyle.Bold, GraphicsUnit.Point);
            lblLogTitle.ForeColor = Color.FromArgb(24, 39, 75);
            lblLogTitle.Location = new Point(16, 14);
            lblLogTitle.Name = "lblLogTitle";
            lblLogTitle.Size = new Size(498, 24);
            lblLogTitle.Text = "時系列ログ";
            // lblLogSub
            lblLogSub.Dock = DockStyle.Top;
            lblLogSub.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblLogSub.ForeColor = Color.FromArgb(90, 100, 120);
            lblLogSub.Location = new Point(16, 38);
            lblLogSub.Name = "lblLogSub";
            lblLogSub.Size = new Size(498, 20);
            lblLogSub.Text = "割り当て・保持・GCの変化を時系列で追う欄";
            // txtLog
            txtLog.Dock = DockStyle.Fill;
            txtLog.BackColor = Color.FromArgb(249, 250, 252);
            txtLog.BorderStyle = BorderStyle.FixedSingle;
            txtLog.Font = new Font("Consolas", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            txtLog.Location = new Point(16, 58);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(498, 370);
            txtLog.TabIndex = 1;
            // final resume
            ((System.ComponentModel.ISupportInitialize)nudLoopCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBufferSize).EndInit();
            ResumeLayout(false);
        }
    }
}
