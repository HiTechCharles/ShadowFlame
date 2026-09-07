using System;
using System.Windows.Forms;
using System.Drawing;

namespace ShadowFlame
{
    partial class AboutForm
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
            LogoPB = new PictureBox();
            CompanyTB = new TextBox();
            HelpTextTB = new TextBox();
            OkBTN = new Button();
            ((System.ComponentModel.ISupportInitialize)LogoPB).BeginInit();
            SuspendLayout();
            // 
            // LogoPB
            // 
            LogoPB.AccessibleName = "A white coffee cup with steam on a red background with the text Brew for You ";
            LogoPB.Location = new Point(12, 12);
            LogoPB.Name = "LogoPB";
            LogoPB.Size = new Size(248, 204);
            LogoPB.SizeMode = PictureBoxSizeMode.Zoom;
            LogoPB.TabIndex = 0;
            LogoPB.TabStop = false;
            // 
            // CompanyTB
            // 
            CompanyTB.BackColor = Color.Teal;
            CompanyTB.Location = new Point(266, 12);
            CompanyTB.Multiline = true;
            CompanyTB.Name = "CompanyTB";
            CompanyTB.ReadOnly = true;
            CompanyTB.Size = new Size(306, 204);
            CompanyTB.TabIndex = 1;
            // 
            // HelpTextTB
            // 
            HelpTextTB.Location = new Point(12, 256);
            HelpTextTB.Multiline = true;
            HelpTextTB.Name = "HelpTextTB";
            HelpTextTB.ReadOnly = true;
            HelpTextTB.ScrollBars = ScrollBars.Vertical;
            HelpTextTB.Size = new Size(560, 423);
            HelpTextTB.TabIndex = 2;
            // 
            // OkBTN
            // 
            OkBTN.Location = new Point(469, 698);
            OkBTN.Name = "OkBTN";
            OkBTN.Size = new Size(103, 51);
            OkBTN.TabIndex = 3;
            OkBTN.Text = "&OK";
            OkBTN.UseVisualStyleBackColor = true;
            // 
            // AboutForm
            // 
            AccessibleName = "About  Dialog Box";
            AutoScaleDimensions = new SizeF(15F, 29F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Teal;
            ClientSize = new Size(584, 761);
            Controls.Add(OkBTN);
            Controls.Add(HelpTextTB);
            Controls.Add(CompanyTB);
            Controls.Add(LogoPB);
            Font = new Font("Tahoma", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.Black;
            Margin = new Padding(6);
            Name = "AboutForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About";
            ((System.ComponentModel.ISupportInitialize)LogoPB).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private PictureBox LogoPB;
        private TextBox CompanyTB;
        private TextBox HelpTextTB;
        private Button OkBTN;
    }
}