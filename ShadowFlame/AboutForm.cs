using System;
using System.Windows.Forms;
using System.Drawing;

namespace ShadowFlame
{
    public partial class AboutForm : Form
    {
        // New constructor to accept program name, company text and help text
        public AboutForm(string programName, string companyText, string helpText)
    {
        InitializeComponent();
        this.Font = GlobalFontService.Instance.CurrentFont;

        // Set the form title if provided (falls back to existing Title from designer)
        if (!string.IsNullOrWhiteSpace(programName))
        {
            Text = "About " + programName;
        }

        CompanyTB.Text = companyText ?? string.Empty;
        HelpTextTB.Text = helpText ?? string.Empty;
    }

    private void OkBTN_Click(object sender, EventArgs e)
    {
        Close();
    }
    }
}