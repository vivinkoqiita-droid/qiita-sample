#nullable enable
namespace LinqSample
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        private System.Windows.Forms.SplitContainer splitMain = null!;
        private System.Windows.Forms.TableLayoutPanel tlpData = null!;
        private System.Windows.Forms.GroupBox gbDepartments = null!;
        private System.Windows.Forms.GroupBox gbEmployees = null!;
        private System.Windows.Forms.GroupBox gbOrders = null!;
        private System.Windows.Forms.DataGridView dgvDepartments = null!;
        private System.Windows.Forms.DataGridView dgvEmployees = null!;
        private System.Windows.Forms.DataGridView dgvOrders = null!;

        private System.Windows.Forms.SplitContainer splitRight = null!;
        private System.Windows.Forms.GroupBox gbCommands = null!;
        private System.Windows.Forms.TabControl tabCommands = null!;

        private System.Windows.Forms.SplitContainer splitPreview = null!;
        private System.Windows.Forms.GroupBox gbCode = null!;
        private System.Windows.Forms.Panel pnlCodeBorder = null!;
        private System.Windows.Forms.RichTextBox rtbCode = null!;

        private System.Windows.Forms.GroupBox gbConsole = null!;
        private System.Windows.Forms.TableLayoutPanel tlpConsole = null!;
        private System.Windows.Forms.Panel pnlConsoleBorder = null!;
        private System.Windows.Forms.RichTextBox rtbConsole = null!;
        private System.Windows.Forms.FlowLayoutPanel flpConsoleHeader = null!;
        private System.Windows.Forms.Button btnClear = null!;
        private System.Windows.Forms.Label lblCommandCaption = null!;
        private System.Windows.Forms.Label lblCommand = null!;
        private System.Windows.Forms.Label lblElapsedCaption = null!;
        private System.Windows.Forms.Label lblElapsed = null!;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///  Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            splitMain = new SplitContainer();
            tlpData = new TableLayoutPanel();
            gbDepartments = new GroupBox();
            dgvDepartments = new DataGridView();
            gbEmployees = new GroupBox();
            dgvEmployees = new DataGridView();
            gbOrders = new GroupBox();
            dgvOrders = new DataGridView();
            splitRight = new SplitContainer();
            gbCommands = new GroupBox();
            tabCommands = new TabControl();
            splitPreview = new SplitContainer();
            gbCode = new GroupBox();
            pnlCodeBorder = new Panel();
            rtbCode = new RichTextBox();
            gbConsole = new GroupBox();
            tlpConsole = new TableLayoutPanel();
            flpConsoleHeader = new FlowLayoutPanel();
            btnClear = new Button();
            lblCommandCaption = new Label();
            lblCommand = new Label();
            lblElapsedCaption = new Label();
            lblElapsed = new Label();
            pnlConsoleBorder = new Panel();
            rtbConsole = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            tlpData.SuspendLayout();
            gbDepartments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDepartments).BeginInit();
            gbEmployees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).BeginInit();
            gbOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitRight).BeginInit();
            splitRight.Panel1.SuspendLayout();
            splitRight.Panel2.SuspendLayout();
            splitRight.SuspendLayout();
            gbCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitPreview).BeginInit();
            splitPreview.Panel1.SuspendLayout();
            splitPreview.Panel2.SuspendLayout();
            splitPreview.SuspendLayout();
            gbCode.SuspendLayout();
            pnlCodeBorder.SuspendLayout();
            gbConsole.SuspendLayout();
            tlpConsole.SuspendLayout();
            flpConsoleHeader.SuspendLayout();
            pnlConsoleBorder.SuspendLayout();
            SuspendLayout();
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 0);
            splitMain.Margin = new Padding(4, 5, 4, 5);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(tlpData);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(splitRight);
            splitMain.Size = new Size(1743, 1300);
            splitMain.SplitterDistance = 900;
            splitMain.SplitterWidth = 6;
            splitMain.TabIndex = 0;
            // 
            // tlpData
            // 
            tlpData.ColumnCount = 1;
            tlpData.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpData.Controls.Add(gbDepartments, 0, 0);
            tlpData.Controls.Add(gbEmployees, 0, 1);
            tlpData.Controls.Add(gbOrders, 0, 2);
            tlpData.Dock = DockStyle.Fill;
            tlpData.Location = new Point(0, 0);
            tlpData.Margin = new Padding(4, 5, 4, 5);
            tlpData.Name = "tlpData";
            tlpData.RowCount = 3;
            tlpData.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlpData.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tlpData.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tlpData.Size = new Size(900, 1300);
            tlpData.TabIndex = 0;
            // 
            // gbDepartments
            // 
            gbDepartments.Controls.Add(dgvDepartments);
            gbDepartments.Dock = DockStyle.Fill;
            gbDepartments.Location = new Point(4, 5);
            gbDepartments.Margin = new Padding(4, 5, 4, 5);
            gbDepartments.Name = "gbDepartments";
            gbDepartments.Padding = new Padding(9, 33, 9, 10);
            gbDepartments.Size = new Size(892, 315);
            gbDepartments.TabIndex = 0;
            gbDepartments.TabStop = false;
            gbDepartments.Text = "部門";
            // 
            // dgvDepartments
            // 
            dgvDepartments.AllowUserToAddRows = false;
            dgvDepartments.AllowUserToDeleteRows = false;
            dgvDepartments.ColumnHeadersHeight = 34;
            dgvDepartments.Dock = DockStyle.Fill;
            dgvDepartments.Location = new Point(9, 57);
            dgvDepartments.Margin = new Padding(4, 5, 4, 5);
            dgvDepartments.Name = "dgvDepartments";
            dgvDepartments.ReadOnly = true;
            dgvDepartments.RowHeadersVisible = false;
            dgvDepartments.RowHeadersWidth = 62;
            dgvDepartments.Size = new Size(874, 248);
            dgvDepartments.TabIndex = 0;
            // 
            // gbEmployees
            // 
            gbEmployees.Controls.Add(dgvEmployees);
            gbEmployees.Dock = DockStyle.Fill;
            gbEmployees.Location = new Point(4, 330);
            gbEmployees.Margin = new Padding(4, 5, 4, 5);
            gbEmployees.Name = "gbEmployees";
            gbEmployees.Padding = new Padding(9, 33, 9, 10);
            gbEmployees.Size = new Size(892, 575);
            gbEmployees.TabIndex = 1;
            gbEmployees.TabStop = false;
            gbEmployees.Text = "社員";
            // 
            // dgvEmployees
            // 
            dgvEmployees.AllowUserToAddRows = false;
            dgvEmployees.AllowUserToDeleteRows = false;
            dgvEmployees.ColumnHeadersHeight = 34;
            dgvEmployees.Dock = DockStyle.Fill;
            dgvEmployees.Location = new Point(9, 57);
            dgvEmployees.Margin = new Padding(4, 5, 4, 5);
            dgvEmployees.Name = "dgvEmployees";
            dgvEmployees.ReadOnly = true;
            dgvEmployees.RowHeadersVisible = false;
            dgvEmployees.RowHeadersWidth = 62;
            dgvEmployees.Size = new Size(874, 508);
            dgvEmployees.TabIndex = 0;
            // 
            // gbOrders
            // 
            gbOrders.Controls.Add(dgvOrders);
            gbOrders.Dock = DockStyle.Fill;
            gbOrders.Location = new Point(4, 915);
            gbOrders.Margin = new Padding(4, 5, 4, 5);
            gbOrders.Name = "gbOrders";
            gbOrders.Padding = new Padding(9, 33, 9, 10);
            gbOrders.Size = new Size(892, 380);
            gbOrders.TabIndex = 2;
            gbOrders.TabStop = false;
            gbOrders.Text = "受注";
            // 
            // dgvOrders
            // 
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.ColumnHeadersHeight = 34;
            dgvOrders.Dock = DockStyle.Fill;
            dgvOrders.Location = new Point(9, 57);
            dgvOrders.Margin = new Padding(4, 5, 4, 5);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowHeadersWidth = 62;
            dgvOrders.Size = new Size(874, 313);
            dgvOrders.TabIndex = 0;
            // 
            // splitRight
            // 
            splitRight.Dock = DockStyle.Fill;
            splitRight.Location = new Point(0, 0);
            splitRight.Margin = new Padding(4, 5, 4, 5);
            splitRight.Name = "splitRight";
            splitRight.Orientation = Orientation.Horizontal;
            // 
            // splitRight.Panel1
            // 
            splitRight.Panel1.Controls.Add(gbCommands);
            // 
            // splitRight.Panel2
            // 
            splitRight.Panel2.Controls.Add(splitPreview);
            splitRight.Size = new Size(837, 1300);
            splitRight.SplitterDistance = 921;
            splitRight.SplitterWidth = 7;
            splitRight.TabIndex = 0;
            // 
            // gbCommands
            // 
            gbCommands.Controls.Add(tabCommands);
            gbCommands.Dock = DockStyle.Fill;
            gbCommands.Location = new Point(0, 0);
            gbCommands.Margin = new Padding(4, 5, 4, 5);
            gbCommands.Name = "gbCommands";
            gbCommands.Padding = new Padding(9, 33, 9, 10);
            gbCommands.Size = new Size(837, 921);
            gbCommands.TabIndex = 0;
            gbCommands.TabStop = false;
            gbCommands.Text = "操作";
            // 
            // tabCommands
            // 
            tabCommands.Dock = DockStyle.Fill;
            tabCommands.Location = new Point(9, 57);
            tabCommands.Margin = new Padding(4, 5, 4, 5);
            tabCommands.Name = "tabCommands";
            tabCommands.SelectedIndex = 0;
            tabCommands.Size = new Size(819, 854);
            tabCommands.TabIndex = 0;
            // 
            // splitPreview
            // 
            splitPreview.Dock = DockStyle.Fill;
            splitPreview.Location = new Point(0, 0);
            splitPreview.Margin = new Padding(4, 5, 4, 5);
            splitPreview.Name = "splitPreview";
            splitPreview.Orientation = Orientation.Horizontal;
            // 
            // splitPreview.Panel1
            // 
            splitPreview.Panel1.Controls.Add(gbCode);
            // 
            // splitPreview.Panel2
            // 
            splitPreview.Panel2.Controls.Add(gbConsole);
            splitPreview.Size = new Size(837, 372);
            splitPreview.SplitterDistance = 263;
            splitPreview.SplitterWidth = 7;
            splitPreview.TabIndex = 0;
            // 
            // gbCode
            // 
            gbCode.Controls.Add(pnlCodeBorder);
            gbCode.Dock = DockStyle.Fill;
            gbCode.Location = new Point(0, 0);
            gbCode.Margin = new Padding(4, 5, 4, 5);
            gbCode.Name = "gbCode";
            gbCode.Padding = new Padding(9, 33, 9, 10);
            gbCode.Size = new Size(837, 263);
            gbCode.TabIndex = 0;
            gbCode.TabStop = false;
            gbCode.Text = "コード";
            // 
            // pnlCodeBorder
            // 
            pnlCodeBorder.BorderStyle = BorderStyle.FixedSingle;
            pnlCodeBorder.Controls.Add(rtbCode);
            pnlCodeBorder.Dock = DockStyle.Fill;
            pnlCodeBorder.Location = new Point(9, 57);
            pnlCodeBorder.Margin = new Padding(4, 5, 4, 5);
            pnlCodeBorder.Name = "pnlCodeBorder";
            pnlCodeBorder.Padding = new Padding(1, 2, 1, 2);
            pnlCodeBorder.Size = new Size(819, 196);
            pnlCodeBorder.TabIndex = 0;
            // 
            // rtbCode
            // 
            rtbCode.BackColor = Color.White;
            rtbCode.BorderStyle = BorderStyle.None;
            rtbCode.Dock = DockStyle.Fill;
            rtbCode.Font = new Font("Consolas", 9F);
            rtbCode.Location = new Point(1, 2);
            rtbCode.Margin = new Padding(4, 5, 4, 5);
            rtbCode.Name = "rtbCode";
            rtbCode.ReadOnly = true;
            rtbCode.Size = new Size(815, 190);
            rtbCode.TabIndex = 0;
            rtbCode.Text = "";
            rtbCode.WordWrap = false;
            // 
            // gbConsole
            // 
            gbConsole.Controls.Add(tlpConsole);
            gbConsole.Dock = DockStyle.Fill;
            gbConsole.Location = new Point(0, 0);
            gbConsole.Margin = new Padding(4, 5, 4, 5);
            gbConsole.Name = "gbConsole";
            gbConsole.Padding = new Padding(9, 33, 9, 10);
            gbConsole.Size = new Size(837, 102);
            gbConsole.TabIndex = 0;
            gbConsole.TabStop = false;
            gbConsole.Text = "出力";
            // 
            // tlpConsole
            // 
            tlpConsole.ColumnCount = 1;
            tlpConsole.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpConsole.Controls.Add(flpConsoleHeader, 0, 0);
            tlpConsole.Controls.Add(pnlConsoleBorder, 0, 1);
            tlpConsole.Dock = DockStyle.Fill;
            tlpConsole.Location = new Point(9, 57);
            tlpConsole.Margin = new Padding(4, 5, 4, 5);
            tlpConsole.Name = "tlpConsole";
            tlpConsole.RowCount = 2;
            tlpConsole.RowStyles.Add(new RowStyle(SizeType.Absolute, 67F));
            tlpConsole.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpConsole.Size = new Size(819, 35);
            tlpConsole.TabIndex = 0;
            // 
            // flpConsoleHeader
            // 
            flpConsoleHeader.Controls.Add(btnClear);
            flpConsoleHeader.Controls.Add(lblCommandCaption);
            flpConsoleHeader.Controls.Add(lblCommand);
            flpConsoleHeader.Controls.Add(lblElapsedCaption);
            flpConsoleHeader.Controls.Add(lblElapsed);
            flpConsoleHeader.Dock = DockStyle.Fill;
            flpConsoleHeader.Location = new Point(4, 5);
            flpConsoleHeader.Margin = new Padding(4, 5, 4, 5);
            flpConsoleHeader.Name = "flpConsoleHeader";
            flpConsoleHeader.Padding = new Padding(9, 10, 9, 10);
            flpConsoleHeader.Size = new Size(811, 57);
            flpConsoleHeader.TabIndex = 0;
            flpConsoleHeader.WrapContents = false;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(9, 10);
            btnClear.Margin = new Padding(0, 0, 17, 0);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(129, 47);
            btnClear.TabIndex = 0;
            btnClear.Text = "クリア";
            btnClear.Click += btnClear_Click;
            // 
            // lblCommandCaption
            // 
            lblCommandCaption.AutoSize = true;
            lblCommandCaption.Location = new Point(155, 20);
            lblCommandCaption.Margin = new Padding(0, 10, 9, 0);
            lblCommandCaption.Name = "lblCommandCaption";
            lblCommandCaption.Size = new Size(52, 25);
            lblCommandCaption.TabIndex = 1;
            lblCommandCaption.Text = "実行:";
            // 
            // lblCommand
            // 
            lblCommand.AutoSize = true;
            lblCommand.Location = new Point(216, 20);
            lblCommand.Margin = new Padding(0, 10, 26, 0);
            lblCommand.Name = "lblCommand";
            lblCommand.Size = new Size(19, 25);
            lblCommand.TabIndex = 2;
            lblCommand.Text = "-";
            // 
            // lblElapsedCaption
            // 
            lblElapsedCaption.AutoSize = true;
            lblElapsedCaption.Location = new Point(261, 20);
            lblElapsedCaption.Margin = new Padding(0, 10, 9, 0);
            lblElapsedCaption.Name = "lblElapsedCaption";
            lblElapsedCaption.Size = new Size(88, 25);
            lblElapsedCaption.TabIndex = 3;
            lblElapsedCaption.Text = "実行時間:";
            // 
            // lblElapsed
            // 
            lblElapsed.AutoSize = true;
            lblElapsed.Location = new Point(358, 20);
            lblElapsed.Margin = new Padding(0, 10, 0, 0);
            lblElapsed.Name = "lblElapsed";
            lblElapsed.Size = new Size(19, 25);
            lblElapsed.TabIndex = 4;
            lblElapsed.Text = "-";
            // 
            // pnlConsoleBorder
            // 
            pnlConsoleBorder.BorderStyle = BorderStyle.FixedSingle;
            pnlConsoleBorder.Controls.Add(rtbConsole);
            pnlConsoleBorder.Dock = DockStyle.Fill;
            pnlConsoleBorder.Location = new Point(4, 72);
            pnlConsoleBorder.Margin = new Padding(4, 5, 4, 5);
            pnlConsoleBorder.Name = "pnlConsoleBorder";
            pnlConsoleBorder.Padding = new Padding(1, 2, 1, 2);
            pnlConsoleBorder.Size = new Size(811, 1);
            pnlConsoleBorder.TabIndex = 1;
            // 
            // rtbConsole
            // 
            rtbConsole.BackColor = Color.White;
            rtbConsole.BorderStyle = BorderStyle.None;
            rtbConsole.Dock = DockStyle.Fill;
            rtbConsole.Font = new Font("Consolas", 9F);
            rtbConsole.Location = new Point(1, 2);
            rtbConsole.Margin = new Padding(4, 5, 4, 5);
            rtbConsole.Name = "rtbConsole";
            rtbConsole.ReadOnly = true;
            rtbConsole.Size = new Size(807, 0);
            rtbConsole.TabIndex = 0;
            rtbConsole.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1743, 1300);
            Controls.Add(splitMain);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            Text = "LINQサンプル";
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            tlpData.ResumeLayout(false);
            gbDepartments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDepartments).EndInit();
            gbEmployees.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).EndInit();
            gbOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            splitRight.Panel1.ResumeLayout(false);
            splitRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitRight).EndInit();
            splitRight.ResumeLayout(false);
            gbCommands.ResumeLayout(false);
            splitPreview.Panel1.ResumeLayout(false);
            splitPreview.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitPreview).EndInit();
            splitPreview.ResumeLayout(false);
            gbCode.ResumeLayout(false);
            pnlCodeBorder.ResumeLayout(false);
            gbConsole.ResumeLayout(false);
            tlpConsole.ResumeLayout(false);
            flpConsoleHeader.ResumeLayout(false);
            flpConsoleHeader.PerformLayout();
            pnlConsoleBorder.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}