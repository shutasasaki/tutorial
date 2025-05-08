namespace ssksSimulation
{
    partial class frmMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            // ssksSim が IDisposable を実装している場合
            if (disposing && (_ssksSim != null))
            {
                _ssksSim.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.btnClosePort = new System.Windows.Forms.Button();
            this.cboFrequencyWeighting = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboStoreMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboWaveRecMode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboEcho = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTimeWeighting = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudNoiseMeasRangeUpper = new System.Windows.Forms.NumericUpDown();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nudStoreNo = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.cboProfile = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.nudSDCardFreeSize = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudNoiseMeasRangeUpper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStoreNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSDCardFreeSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port";
            // 
            // cboPort
            // 
            this.cboPort.FormattingEnabled = true;
            this.cboPort.Location = new System.Drawing.Point(22, 58);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(162, 20);
            this.cboPort.TabIndex = 1;
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(49, 84);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(58, 23);
            this.btnOpenPort.TabIndex = 2;
            this.btnOpenPort.Text = "Open";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // btnClosePort
            // 
            this.btnClosePort.Location = new System.Drawing.Point(113, 84);
            this.btnClosePort.Name = "btnClosePort";
            this.btnClosePort.Size = new System.Drawing.Size(54, 23);
            this.btnClosePort.TabIndex = 3;
            this.btnClosePort.Text = "Close";
            this.btnClosePort.UseVisualStyleBackColor = true;
            this.btnClosePort.Click += new System.EventHandler(this.btnClosePort_Click);
            // 
            // cboFrequencyWeighting
            // 
            this.cboFrequencyWeighting.FormattingEnabled = true;
            this.cboFrequencyWeighting.Location = new System.Drawing.Point(22, 125);
            this.cboFrequencyWeighting.Name = "cboFrequencyWeighting";
            this.cboFrequencyWeighting.Size = new System.Drawing.Size(162, 20);
            this.cboFrequencyWeighting.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Frequency Weighitng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Store Mode";
            // 
            // cboStoreMode
            // 
            this.cboStoreMode.FormattingEnabled = true;
            this.cboStoreMode.Location = new System.Drawing.Point(22, 184);
            this.cboStoreMode.Name = "cboStoreMode";
            this.cboStoreMode.Size = new System.Drawing.Size(162, 20);
            this.cboStoreMode.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Wave Rec Mode";
            // 
            // cboWaveRecMode
            // 
            this.cboWaveRecMode.FormattingEnabled = true;
            this.cboWaveRecMode.Location = new System.Drawing.Point(22, 239);
            this.cboWaveRecMode.Name = "cboWaveRecMode";
            this.cboWaveRecMode.Size = new System.Drawing.Size(162, 20);
            this.cboWaveRecMode.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Echo";
            // 
            // cboEcho
            // 
            this.cboEcho.FormattingEnabled = true;
            this.cboEcho.Location = new System.Drawing.Point(22, 294);
            this.cboEcho.Name = "cboEcho";
            this.cboEcho.Size = new System.Drawing.Size(162, 20);
            this.cboEcho.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 279);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "Time Weighting";
            // 
            // cboTimeWeighting
            // 
            this.cboTimeWeighting.FormattingEnabled = true;
            this.cboTimeWeighting.Location = new System.Drawing.Point(208, 294);
            this.cboTimeWeighting.Name = "cboTimeWeighting";
            this.cboTimeWeighting.Size = new System.Drawing.Size(139, 20);
            this.cboTimeWeighting.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 333);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "Output Level Range Upper";
            // 
            // nudNoiseMeasRangeUpper
            // 
            this.nudNoiseMeasRangeUpper.Location = new System.Drawing.Point(22, 349);
            this.nudNoiseMeasRangeUpper.Name = "nudNoiseMeasRangeUpper";
            this.nudNoiseMeasRangeUpper.Size = new System.Drawing.Size(162, 19);
            this.nudNoiseMeasRangeUpper.TabIndex = 16;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.SystemColors.Control;
            this.txtMessage.Location = new System.Drawing.Point(208, 59);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(139, 19);
            this.txtMessage.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(206, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "Message";
            // 
            // nudStoreNo
            // 
            this.nudStoreNo.Location = new System.Drawing.Point(208, 184);
            this.nudStoreNo.Name = "nudStoreNo";
            this.nudStoreNo.Size = new System.Drawing.Size(139, 19);
            this.nudStoreNo.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(206, 168);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "StoreName";
            // 
            // cboProfile
            // 
            this.cboProfile.FormattingEnabled = true;
            this.cboProfile.Location = new System.Drawing.Point(208, 125);
            this.cboProfile.Name = "cboProfile";
            this.cboProfile.Size = new System.Drawing.Size(139, 20);
            this.cboProfile.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(206, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "計測データファイル";
            // 
            // nudSDCardFreeSize
            // 
            this.nudSDCardFreeSize.Location = new System.Drawing.Point(208, 239);
            this.nudSDCardFreeSize.Name = "nudSDCardFreeSize";
            this.nudSDCardFreeSize.Size = new System.Drawing.Size(139, 19);
            this.nudSDCardFreeSize.TabIndex = 26;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(206, 223);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 12);
            this.label12.TabIndex = 25;
            this.label12.Text = "SD Card Free Size";
            // 
            // txtTitle
            // 
            this.txtTitle.AutoSize = true;
            this.txtTitle.Location = new System.Drawing.Point(20, 18);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(28, 12);
            this.txtTitle.TabIndex = 27;
            this.txtTitle.Text = "Title";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 387);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.nudSDCardFreeSize);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboProfile);
            this.Controls.Add(this.nudStoreNo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.nudNoiseMeasRangeUpper);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboTimeWeighting);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboEcho);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboWaveRecMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboStoreMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboFrequencyWeighting);
            this.Controls.Add(this.btnClosePort);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.cboPort);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudNoiseMeasRangeUpper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStoreNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSDCardFreeSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Button btnClosePort;
        private System.Windows.Forms.ComboBox cboFrequencyWeighting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboStoreMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboWaveRecMode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboEcho;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboTimeWeighting;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudNoiseMeasRangeUpper;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudStoreNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboProfile;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudSDCardFreeSize;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label txtTitle;
    }
}

