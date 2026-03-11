﻿namespace ListDictionaryHashSetBench
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblItemCount = new Label();
            numItemCount = new NumericUpDown();
            lblRepeatCount = new Label();
            numRepeatCount = new NumericUpDown();
            btnRunAll = new Button();
            btnRunSelected = new Button();
            btnClearResults = new Button();
            dgvResults = new DataGridView();
            lblStatusTitle = new Label();
            lblStatus = new Label();
            txtSummary = new TextBox();
            timerAnimation = new System.Windows.Forms.Timer(components);
            lblDescription = new Label();
            ((System.ComponentModel.ISupportInitialize)numItemCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRepeatCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            // 
            // lblItemCount
            // 
            lblItemCount.AutoSize = true;
            lblItemCount.Location = new Point(18, 18);
            lblItemCount.Name = "lblItemCount";
            lblItemCount.Size = new Size(55, 15);
            lblItemCount.TabIndex = 0;
            lblItemCount.Text = "対象件数";
            // 
            // numItemCount
            // 
            numItemCount.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            numItemCount.Location = new Point(67, 15);
            numItemCount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numItemCount.Minimum = new decimal(new int[] { 10000, 0, 0, 0 });
            numItemCount.Name = "numItemCount";
            numItemCount.Size = new Size(100, 23);
            numItemCount.TabIndex = 1;
            numItemCount.ThousandsSeparator = true;
            numItemCount.Value = new decimal(new int[] { 100000, 0, 0, 0 });
            // 
            // lblRepeatCount
            // 
            lblRepeatCount.AutoSize = true;
            lblRepeatCount.Location = new Point(184, 18);
            lblRepeatCount.Name = "lblRepeatCount";
            lblRepeatCount.Size = new Size(55, 15);
            lblRepeatCount.TabIndex = 2;
            lblRepeatCount.Text = "反復回数";
            // 
            // numRepeatCount
            // 
            numRepeatCount.Location = new Point(245, 15);
            numRepeatCount.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numRepeatCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numRepeatCount.Name = "numRepeatCount";
            numRepeatCount.Size = new Size(61, 23);
            numRepeatCount.TabIndex = 3;
            numRepeatCount.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnRunAll
            // 
            btnRunAll.Location = new Point(329, 14);
            btnRunAll.Name = "btnRunAll";
            btnRunAll.Size = new Size(104, 26);
            btnRunAll.TabIndex = 4;
            btnRunAll.Text = "全7件を実行";
            btnRunAll.UseVisualStyleBackColor = true;
            btnRunAll.Click += btnRunAll_Click;
            // 
            // btnRunSelected
            // 
            btnRunSelected.Location = new Point(439, 14);
            btnRunSelected.Name = "btnRunSelected";
            btnRunSelected.Size = new Size(104, 26);
            btnRunSelected.TabIndex = 5;
            btnRunSelected.Text = "選択行を実行";
            btnRunSelected.UseVisualStyleBackColor = true;
            btnRunSelected.Click += btnRunSelected_Click;
            // 
            // btnClearResults
            // 
            btnClearResults.Location = new Point(549, 14);
            btnClearResults.Name = "btnClearResults";
            btnClearResults.Size = new Size(104, 26);
            btnClearResults.TabIndex = 6;
            btnClearResults.Text = "結果クリア";
            btnClearResults.UseVisualStyleBackColor = true;
            btnClearResults.Click += btnClearResults_Click;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.AllowUserToResizeRows = false;
            dgvResults.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Location = new Point(18, 78);
            dgvResults.MultiSelect = false;
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.Size = new Size(1198, 321);
            dgvResults.TabIndex = 8;
            dgvResults.SelectionChanged += dgvResults_SelectionChanged;
            // 
            // lblStatusTitle
            // 
            lblStatusTitle.AutoSize = true;
            lblStatusTitle.Location = new Point(676, 19);
            lblStatusTitle.Name = "lblStatusTitle";
            lblStatusTitle.Size = new Size(31, 15);
            lblStatusTitle.TabIndex = 9;
            lblStatusTitle.Text = "状態";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(713, 19);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(43, 15);
            lblStatus.TabIndex = 10;
            lblStatus.Text = "待機中";
            // 
            // txtSummary
            // 
            txtSummary.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtSummary.Location = new Point(18, 432);
            txtSummary.Multiline = true;
            txtSummary.Name = "txtSummary";
            txtSummary.ReadOnly = true;
            txtSummary.ScrollBars = ScrollBars.Vertical;
            txtSummary.Size = new Size(1198, 241);
            txtSummary.TabIndex = 11;
            // 
            // timerAnimation
            // 
            timerAnimation.Interval = 220;
            timerAnimation.Tick += timerAnimation_Tick;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(18, 50);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(376, 15);
            lblDescription.TabIndex = 7;
            lblDescription.Text = "記事に合わせた7つの処理を、List / Dictionary / HashSet で同条件比較する";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1234, 690);
            Controls.Add(txtSummary);
            Controls.Add(lblStatus);
            Controls.Add(lblStatusTitle);
            Controls.Add(dgvResults);
            Controls.Add(lblDescription);
            Controls.Add(btnClearResults);
            Controls.Add(btnRunSelected);
            Controls.Add(btnRunAll);
            Controls.Add(numRepeatCount);
            Controls.Add(lblRepeatCount);
            Controls.Add(numItemCount);
            Controls.Add(lblItemCount);
            MinimumSize = new Size(1250, 729);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "K39 List / Dictionary / HashSet 比較";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)numItemCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRepeatCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblItemCount;
        private NumericUpDown numItemCount;
        private Label lblRepeatCount;
        private NumericUpDown numRepeatCount;
        private Button btnRunAll;
        private Button btnRunSelected;
        private Button btnClearResults;
        private DataGridView dgvResults;
        private Label lblStatusTitle;
        private Label lblStatus;
        private TextBox txtSummary;
        private System.Windows.Forms.Timer timerAnimation;
        private Label lblDescription;
    }
}