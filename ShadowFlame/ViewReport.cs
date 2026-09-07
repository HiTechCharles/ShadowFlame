using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using ShadowFlame;

namespace ShadowFlame
{
    public partial class ViewReport : Form
    {
        private string _Title;
        private string _textFilePath;
        private string _pdfFilePath;

        // constructor accepts text and PDF file paths. If PDF path is null or doesn't exist,
        // disable the Display PDF menu option.
        public ViewReport(string Title, string textFilePath, string pdfFilePath)
        {
            InitializeComponent();
            this.Font = GlobalFontService.Instance.CurrentFont;
            this.MainMenuMST.Font = GlobalFontService.Instance.CurrentFont; 
            this.Text = Title;


            _textFilePath = textFilePath;
            _pdfFilePath = pdfFilePath;

            // Load text file into the RichTextBox if provided and exists.
            // Perform file I/O off the UI thread to avoid freezing the parent form when the file
            // is large or temporarily locked. Update the RichTextBox on the UI thread when done.
            if (!string.IsNullOrWhiteSpace(_textFilePath) && File.Exists(_textFilePath))
            {
                ReportRTB.Text = "Loading report...";
                Task.Run(() =>
                {
                    try
                    {
                        var content = File.ReadAllText(_textFilePath);
                        if (ReportRTB.IsHandleCreated)
                            ReportRTB.Invoke((Action)(() => ReportRTB.Text = content));
                        else
                            ReportRTB.Text = content;
                    }
                    catch
                    {
                        if (ReportRTB.IsHandleCreated)
                            ReportRTB.Invoke((Action)(() => ReportRTB.Text = string.Empty));
                        else
                            ReportRTB.Text = string.Empty;
                    }
                });
            }

            // Disable Display PDF menu option when PDF path is null/empty or file not found
            if (string.IsNullOrWhiteSpace(_pdfFilePath) || !File.Exists(_pdfFilePath))
            {
                displayPDFVersionToolStripMenuItem.Enabled = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ReportRTB.Text);
        }

        private void displayPDFVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_pdfFilePath) || !File.Exists(_pdfFilePath))
            {
                MessageBox.Show("PDF version not available.", "PDF Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = _pdfFilePath,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open PDF: {ex.Message}", "Open PDF Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void speakReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = ReportRTB?.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                DisplayMessage.ShowInfo("There is no text to speak.", "Speak Report");
                return;
            }
            try
            {
                await WindowsTTS.SpeakAsync(text).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError($"Text-to-speech failed: {ex.Message}", "Speak Report");
            }
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void copyToClipboardToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Clipboard.SetText(ReportRTB.Text);
        }

        private void displayPDFVersionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = _pdfFilePath,
                UseShellExecute = true
            });
        }

        private async void speakReportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                await ShadowFlame.WindowsTTS.SpeakAsync(ReportRTB.Text).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                DisplayMessage.ShowError($"Text-to-speech failed: {ex.Message}", "Speak Report");
            }
        }
    }
}
