using System;


namespace ShadowFlame
{
    public class DisplayMessage
    {
        public static void Show(string message, string caption, MessageBoxIcon icon)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
        }

        public static void ShowError(string message, string Caption = "Error")
        {
            Show(message, Caption, MessageBoxIcon.Error);
        }

        public static void ShowInfo(string message, string Caption = "Information")
        {
            Show(message, Caption, MessageBoxIcon.Information);
        }

        public static void ShowWarning(string message, string Caption = "Warning")
        {
            Show(message, Caption, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Displays a confirmation dialog with Yes and No buttons.
        /// </summary>
        /// <param name="text">The text to display in the dialog.</param>
        /// <param name="caption">The caption of the dialog.</param>
        /// <returns>True if the user clicked Yes; otherwise, false.</returns>
        public static bool GetConfirmation(string text, string caption = "Confirm")
        {
            var result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }
    }
}
