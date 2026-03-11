namespace DesignerSafeControlSample
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button runBrokenButton;
        private System.Windows.Forms.Button runSafeButton;
        private System.Windows.Forms.Button showPropertyButton;
        private System.Windows.Forms.Button showConverterButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel brokenPreviewPanel;
        private System.Windows.Forms.Panel safePreviewPanel;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.Label headlineLabel;
        private System.Windows.Forms.Label brokenTitleLabel;
        private System.Windows.Forms.Label safeTitleLabel;
        private System.Windows.Forms.Label summaryLabel;
        private System.Windows.Forms.Label brokenStateLabel;
        private System.Windows.Forms.Label brokenStageLabel;
        private System.Windows.Forms.Label brokenMessageLabel;
        private System.Windows.Forms.Label safeStateLabel;
        private System.Windows.Forms.Label safeStageLabel;
        private System.Windows.Forms.Label safeMessageLabel;

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
            this.runBrokenButton = new System.Windows.Forms.Button();
            this.runSafeButton = new System.Windows.Forms.Button();
            this.showPropertyButton = new System.Windows.Forms.Button();
            this.showConverterButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.brokenPreviewPanel = new System.Windows.Forms.Panel();
            this.safePreviewPanel = new System.Windows.Forms.Panel();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.headlineLabel = new System.Windows.Forms.Label();
            this.brokenTitleLabel = new System.Windows.Forms.Label();
            this.safeTitleLabel = new System.Windows.Forms.Label();
            this.summaryLabel = new System.Windows.Forms.Label();
            this.brokenStateLabel = new System.Windows.Forms.Label();
            this.brokenStageLabel = new System.Windows.Forms.Label();
            this.brokenMessageLabel = new System.Windows.Forms.Label();
            this.safeStateLabel = new System.Windows.Forms.Label();
            this.safeStageLabel = new System.Windows.Forms.Label();
            this.safeMessageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // runBrokenButton
            // 
            this.runBrokenButton.Location = new System.Drawing.Point(24, 70);
            this.runBrokenButton.Name = "runBrokenButton";
            this.runBrokenButton.Size = new System.Drawing.Size(140, 36);
            this.runBrokenButton.TabIndex = 0;
            this.runBrokenButton.Text = "Broken を設計時生成";
            this.runBrokenButton.UseVisualStyleBackColor = true;
            this.runBrokenButton.Click += new System.EventHandler(this.runBrokenButton_Click);
            // 
            // runSafeButton
            // 
            this.runSafeButton.Location = new System.Drawing.Point(180, 70);
            this.runSafeButton.Name = "runSafeButton";
            this.runSafeButton.Size = new System.Drawing.Size(140, 36);
            this.runSafeButton.TabIndex = 1;
            this.runSafeButton.Text = "Safe を設計時生成";
            this.runSafeButton.UseVisualStyleBackColor = true;
            this.runSafeButton.Click += new System.EventHandler(this.runSafeButton_Click);
            // 
            // showPropertyButton
            // 
            this.showPropertyButton.Location = new System.Drawing.Point(336, 70);
            this.showPropertyButton.Name = "showPropertyButton";
            this.showPropertyButton.Size = new System.Drawing.Size(140, 36);
            this.showPropertyButton.TabIndex = 2;
            this.showPropertyButton.Text = "DefaultValue 保存差";
            this.showPropertyButton.UseVisualStyleBackColor = true;
            this.showPropertyButton.Click += new System.EventHandler(this.showPropertyButton_Click);
            // 
            // showConverterButton
            // 
            this.showConverterButton.Location = new System.Drawing.Point(492, 70);
            this.showConverterButton.Name = "showConverterButton";
            this.showConverterButton.Size = new System.Drawing.Size(160, 36);
            this.showConverterButton.TabIndex = 3;
            this.showConverterButton.Text = "TypeConverter 比較";
            this.showConverterButton.UseVisualStyleBackColor = true;
            this.showConverterButton.Click += new System.EventHandler(this.showConverterButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(668, 70);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(100, 36);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "リセット";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // brokenPreviewPanel
            // 
            this.brokenPreviewPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.brokenPreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brokenPreviewPanel.Location = new System.Drawing.Point(24, 184);
            this.brokenPreviewPanel.Name = "brokenPreviewPanel";
            this.brokenPreviewPanel.Size = new System.Drawing.Size(360, 90);
            this.brokenPreviewPanel.TabIndex = 5;
            // 
            // safePreviewPanel
            // 
            this.safePreviewPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.safePreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.safePreviewPanel.Location = new System.Drawing.Point(408, 184);
            this.safePreviewPanel.Name = "safePreviewPanel";
            this.safePreviewPanel.Size = new System.Drawing.Size(360, 90);
            this.safePreviewPanel.TabIndex = 6;
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(24, 332);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTextBox.Size = new System.Drawing.Size(744, 244);
            this.resultTextBox.TabIndex = 7;
            // 
            // headlineLabel
            // 
            this.headlineLabel.AutoSize = true;
            this.headlineLabel.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.headlineLabel.Location = new System.Drawing.Point(20, 20);
            this.headlineLabel.Name = "headlineLabel";
            this.headlineLabel.Size = new System.Drawing.Size(392, 21);
            this.headlineLabel.TabIndex = 8;
            this.headlineLabel.Text = "継承コントロールの設計時安全化 比較サンプル";
            // 
            // brokenTitleLabel
            // 
            this.brokenTitleLabel.AutoSize = true;
            this.brokenTitleLabel.Location = new System.Drawing.Point(20, 130);
            this.brokenTitleLabel.Name = "brokenTitleLabel";
            this.brokenTitleLabel.Size = new System.Drawing.Size(154, 12);
            this.brokenTitleLabel.TabIndex = 9;
            this.brokenTitleLabel.Text = "Broken 側 結果とプレビュー";
            // 
            // safeTitleLabel
            // 
            this.safeTitleLabel.AutoSize = true;
            this.safeTitleLabel.Location = new System.Drawing.Point(404, 130);
            this.safeTitleLabel.Name = "safeTitleLabel";
            this.safeTitleLabel.Size = new System.Drawing.Size(139, 12);
            this.safeTitleLabel.TabIndex = 10;
            this.safeTitleLabel.Text = "Safe 側 結果とプレビュー";
            // 
            // summaryLabel
            // 
            this.summaryLabel.AutoSize = true;
            this.summaryLabel.Location = new System.Drawing.Point(24, 590);
            this.summaryLabel.Name = "summaryLabel";
            this.summaryLabel.Size = new System.Drawing.Size(532, 12);
            this.summaryLabel.TabIndex = 11;
            this.summaryLabel.Text = "Broken は早い場所で停止、Safe は生成だけ通過、例外文面と保存差は下のログで確認";
            // 
            // brokenStateLabel
            // 
            this.brokenStateLabel.AutoSize = true;
            this.brokenStateLabel.Location = new System.Drawing.Point(24, 150);
            this.brokenStateLabel.Name = "brokenStateLabel";
            this.brokenStateLabel.Size = new System.Drawing.Size(41, 12);
            this.brokenStateLabel.TabIndex = 12;
            this.brokenStateLabel.Text = "未実行";
            // 
            // brokenStageLabel
            // 
            this.brokenStageLabel.AutoSize = true;
            this.brokenStageLabel.Location = new System.Drawing.Point(90, 150);
            this.brokenStageLabel.Name = "brokenStageLabel";
            this.brokenStageLabel.Size = new System.Drawing.Size(62, 12);
            this.brokenStageLabel.TabIndex = 13;
            this.brokenStageLabel.Text = "発生段階:-";
            // 
            // brokenMessageLabel
            // 
            this.brokenMessageLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brokenMessageLabel.Location = new System.Drawing.Point(24, 282);
            this.brokenMessageLabel.Name = "brokenMessageLabel";
            this.brokenMessageLabel.Size = new System.Drawing.Size(360, 42);
            this.brokenMessageLabel.TabIndex = 14;
            this.brokenMessageLabel.Text = "-";
            // 
            // safeStateLabel
            // 
            this.safeStateLabel.AutoSize = true;
            this.safeStateLabel.Location = new System.Drawing.Point(408, 150);
            this.safeStateLabel.Name = "safeStateLabel";
            this.safeStateLabel.Size = new System.Drawing.Size(41, 12);
            this.safeStateLabel.TabIndex = 15;
            this.safeStateLabel.Text = "未実行";
            // 
            // safeStageLabel
            // 
            this.safeStageLabel.AutoSize = true;
            this.safeStageLabel.Location = new System.Drawing.Point(474, 150);
            this.safeStageLabel.Name = "safeStageLabel";
            this.safeStageLabel.Size = new System.Drawing.Size(62, 12);
            this.safeStageLabel.TabIndex = 16;
            this.safeStageLabel.Text = "発生段階:-";
            // 
            // safeMessageLabel
            // 
            this.safeMessageLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.safeMessageLabel.Location = new System.Drawing.Point(408, 282);
            this.safeMessageLabel.Name = "safeMessageLabel";
            this.safeMessageLabel.Size = new System.Drawing.Size(360, 42);
            this.safeMessageLabel.TabIndex = 17;
            this.safeMessageLabel.Text = "-";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 620);
            this.Controls.Add(this.safeMessageLabel);
            this.Controls.Add(this.safeStageLabel);
            this.Controls.Add(this.safeStateLabel);
            this.Controls.Add(this.brokenMessageLabel);
            this.Controls.Add(this.brokenStageLabel);
            this.Controls.Add(this.brokenStateLabel);
            this.Controls.Add(this.summaryLabel);
            this.Controls.Add(this.safeTitleLabel);
            this.Controls.Add(this.brokenTitleLabel);
            this.Controls.Add(this.headlineLabel);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.safePreviewPanel);
            this.Controls.Add(this.brokenPreviewPanel);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.showConverterButton);
            this.Controls.Add(this.showPropertyButton);
            this.Controls.Add(this.runSafeButton);
            this.Controls.Add(this.runBrokenButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Designer Safe Control Sample";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}