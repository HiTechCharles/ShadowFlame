namespace ShadowFlame
{
    partial class TTSSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            VoiceCB = new ComboBox();
            VoiceLBL = new Label();
            SpeechTRK = new TrackBar();
            SpeedLBL = new Label();
            VolumeLBL = new Label();
            VolumeTRK = new TrackBar();
            ConsoleSpeechCHK = new CheckBox();
            SampleTXT = new TextBox();
            PreviewBTN = new Button();
            SaveBTN = new Button();
            ((System.ComponentModel.ISupportInitialize)SpeechTRK).BeginInit();
            ((System.ComponentModel.ISupportInitialize)VolumeTRK).BeginInit();
            SuspendLayout();
            // 
            // VoiceCB
            // 
            VoiceCB.DropDownStyle = ComboBoxStyle.DropDownList;
            VoiceCB.FormattingEnabled = true;
            VoiceCB.Location = new Point(208, 22);
            VoiceCB.Name = "VoiceCB";
            VoiceCB.Size = new Size(379, 37);
            VoiceCB.TabIndex = 1;
            // 
            // VoiceLBL
            // 
            VoiceLBL.AutoSize = true;
            VoiceLBL.Location = new Point(20, 30);
            VoiceLBL.Name = "VoiceLBL";
            VoiceLBL.Size = new Size(87, 29);
            VoiceLBL.TabIndex = 0;
            VoiceLBL.Text = "&Voice:";
            VoiceLBL.TextAlign = ContentAlignment.MiddleRight;
            // 
            // SpeechTRK
            // 
            SpeechTRK.Location = new Point(208, 82);
            SpeechTRK.Minimum = -10;
            SpeechTRK.Name = "SpeechTRK";
            SpeechTRK.Size = new Size(379, 45);
            SpeechTRK.TabIndex = 3;
            // 
            // SpeedLBL
            // 
            SpeedLBL.AccessibleRole = AccessibleRole.TitleBar;
            SpeedLBL.AutoSize = true;
            SpeedLBL.Location = new Point(20, 98);
            SpeedLBL.Name = "SpeedLBL";
            SpeedLBL.Size = new Size(95, 29);
            SpeedLBL.TabIndex = 2;
            SpeedLBL.Text = "&Speed:";
            SpeedLBL.TextAlign = ContentAlignment.MiddleRight;
            // 
            // VolumeLBL
            // 
            VolumeLBL.AccessibleRole = AccessibleRole.TitleBar;
            VolumeLBL.AutoSize = true;
            VolumeLBL.Location = new Point(20, 162);
            VolumeLBL.Name = "VolumeLBL";
            VolumeLBL.Size = new Size(103, 29);
            VolumeLBL.TabIndex = 4;
            VolumeLBL.Text = "Vo&lume";
            VolumeLBL.TextAlign = ContentAlignment.MiddleRight;
            // 
            // VolumeTRK
            // 
            VolumeTRK.LargeChange = 10;
            VolumeTRK.Location = new Point(208, 146);
            VolumeTRK.Maximum = 100;
            VolumeTRK.Minimum = 25;
            VolumeTRK.Name = "VolumeTRK";
            VolumeTRK.Size = new Size(379, 45);
            VolumeTRK.TabIndex = 5;
            VolumeTRK.Value = 25;
            // 
            // ConsoleSpeechCHK
            // 
            ConsoleSpeechCHK.AutoSize = true;
            ConsoleSpeechCHK.Location = new Point(20, 223);
            ConsoleSpeechCHK.Name = "ConsoleSpeechCHK";
            ConsoleSpeechCHK.Size = new Size(401, 33);
            ConsoleSpeechCHK.TabIndex = 6;
            ConsoleSpeechCHK.Text = "Speech in &Console Applications";
            ConsoleSpeechCHK.TextAlign = ContentAlignment.MiddleRight;
            ConsoleSpeechCHK.UseVisualStyleBackColor = true;
            // 
            // SampleTXT
            // 
            SampleTXT.Location = new Point(20, 278);
            SampleTXT.Name = "SampleTXT";
            SampleTXT.Size = new Size(552, 36);
            SampleTXT.TabIndex = 7;
            SampleTXT.Text = "This is a test of the selected voice.";
            // 
            // PreviewBTN
            // 
            PreviewBTN.ForeColor = Color.Black;
            PreviewBTN.Location = new Point(20, 334);
            PreviewBTN.Name = "PreviewBTN";
            PreviewBTN.Size = new Size(120, 40);
            PreviewBTN.TabIndex = 8;
            PreviewBTN.Text = "Preview";
            PreviewBTN.UseVisualStyleBackColor = true;
            PreviewBTN.Click += PreviewBTN_Click;
            // 
            // SaveBTN
            // 
            SaveBTN.ForeColor = Color.Black;
            SaveBTN.Location = new Point(467, 334);
            SaveBTN.Name = "SaveBTN";
            SaveBTN.Size = new Size(120, 40);
            SaveBTN.TabIndex = 9;
            SaveBTN.Text = "Save";
            SaveBTN.UseVisualStyleBackColor = true;
            SaveBTN.Click += SaveBTN_Click;
            // 
            // TTSSetup
            // 
            AutoScaleDimensions = new SizeF(15F, 29F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Indigo;
            ClientSize = new Size(599, 386);
            Controls.Add(ConsoleSpeechCHK);
            Controls.Add(VolumeLBL);
            Controls.Add(VolumeTRK);
            Controls.Add(SpeedLBL);
            Controls.Add(SpeechTRK);
            Controls.Add(VoiceLBL);
            Controls.Add(VoiceCB);
            Controls.Add(SampleTXT);
            Controls.Add(PreviewBTN);
            Controls.Add(SaveBTN);
            Font = new Font("Tahoma", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Margin = new Padding(6);
            Name = "TTSSetup";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ShadowFlame Text to Speech Setup";
            ((System.ComponentModel.ISupportInitialize)SpeechTRK).EndInit();
            ((System.ComponentModel.ISupportInitialize)VolumeTRK).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox VoiceCB;
        private Label VoiceLBL;
        private TrackBar SpeechTRK;
        private Label SpeedLBL;
        private Label VolumeLBL;
        private TrackBar VolumeTRK;
        private CheckBox ConsoleSpeechCHK;
        private TextBox SampleTXT;
        private Button PreviewBTN;
        private Button SaveBTN;
    }
}