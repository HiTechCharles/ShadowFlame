using System;
using System.Windows.Forms;
using System.Drawing;

namespace ShadowFlame
{
    partial class ViewReport
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
            this.MainMenuMST = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayPDFVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speakReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportRTB = new System.Windows.Forms.RichTextBox();
            this.MainMenuMST.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuMST
            // 
            this.MainMenuMST.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.MainMenuMST.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MainMenuMST.Location = new System.Drawing.Point(0, 0);
            this.MainMenuMST.Name = "MainMenuMST";
            this.MainMenuMST.Padding = new System.Windows.Forms.Padding(13, 4, 0, 4);
            this.MainMenuMST.Size = new System.Drawing.Size(584, 41);
            this.MainMenuMST.TabIndex = 0;
            this.MainMenuMST.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem,
            this.displayPDFVersionToolStripMenuItem,
            this.speakReportToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(117, 33);
            this.fileToolStripMenuItem.Text = "&Options";
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(416, 34);
            this.copyToClipboardToolStripMenuItem.Text = "&Copy to Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click_1);
            // 
            // displayPDFVersionToolStripMenuItem
            // 
            this.displayPDFVersionToolStripMenuItem.Name = "displayPDFVersionToolStripMenuItem";
            this.displayPDFVersionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.displayPDFVersionToolStripMenuItem.Size = new System.Drawing.Size(416, 34);
            this.displayPDFVersionToolStripMenuItem.Text = "Display PDF version";
            this.displayPDFVersionToolStripMenuItem.Click += new System.EventHandler(this.displayPDFVersionToolStripMenuItem_Click_1);
            // 
            // speakReportToolStripMenuItem
            // 
            this.speakReportToolStripMenuItem.Name = "speakReportToolStripMenuItem";
            this.speakReportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.speakReportToolStripMenuItem.Size = new System.Drawing.Size(416, 34);
            this.speakReportToolStripMenuItem.Text = "&Speak Report";
            this.speakReportToolStripMenuItem.Click += new System.EventHandler(this.speakReportToolStripMenuItem_Click_1);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(416, 34);
            this.exitToolStripMenuItem.Text = "Clos&e";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // ReportRTB
            // 
            this.ReportRTB.Font = new System.Drawing.Font("Cascadia Mono", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportRTB.Location = new System.Drawing.Point(12, 54);
            this.ReportRTB.Name = "ReportRTB";
            this.ReportRTB.ReadOnly = true;
            this.ReportRTB.Size = new System.Drawing.Size(560, 695);
            this.ReportRTB.TabIndex = 1;
            this.ReportRTB.Text = "";
            // 
            // ViewReport
            // 
            this.AccessibleName = "View Report dialog Box";
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(584, 761);
            this.Controls.Add(this.ReportRTB);
            this.Controls.Add(this.MainMenuMST);
            this.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.MainMenuMST;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ViewReport";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Viewer";
            this.MainMenuMST.ResumeLayout(false);
            this.MainMenuMST.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip MainMenuMST;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private ToolStripMenuItem displayPDFVersionToolStripMenuItem;
        private ToolStripMenuItem speakReportToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private RichTextBox ReportRTB;
    }
}