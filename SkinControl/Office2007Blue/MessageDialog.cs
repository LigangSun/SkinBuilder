using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinControl.Office2007Blue
{
    public class MessageDialog
    {
        public static DialogResult InformationDialog(IWin32Window parent, string message, Exception ex)
        {
            return MessageBox.Show(parent, message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult WarningDialog(IWin32Window parent, string message, string caption, MessageBoxButtons buttons, Exception ex)
        {
            return MessageBox.Show(parent, message, caption, buttons, MessageBoxIcon.Warning);
        }

        public static DialogResult ErrorDialog(IWin32Window parent, string message, string caption, MessageBoxButtons buttons, Exception ex)
        {
            return MessageBox.Show(parent, message, caption, buttons, MessageBoxIcon.Error);
        }

        public static DialogResult QuestionDialog(IWin32Window parent, string message, string caption, MessageBoxButtons buttons, Exception ex)
        {
            return MessageBox.Show(parent, message, caption, buttons, MessageBoxIcon.Question);
        }
    }
}
