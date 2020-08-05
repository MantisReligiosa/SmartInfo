using System;
using System.Windows.Forms;

namespace Setup.Managers
{
    internal static class NotificationManager
    {
        public static void ShowErrorMessage(string message, string caption = "Ошибка",
            bool isWizardInstallation = true)
        {
            if (!isWizardInstallation)
                return;

            MessageBox.Show(message, caption,
                MessageBoxButtons.OK, MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }

        public static void ShowExclamationMessage(string message, string caption = "Внимание",
            bool isWizardInstallation = true)
        {
            if (!isWizardInstallation)
                return;

            MessageBox.Show(message, caption,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void ShowExceptionMessage(Exception ex,
            bool isWizardInstallation = true)
        {
            if (!isWizardInstallation)
                return;

            var dialogTypeName = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg";
            var dialogType = typeof(Form).Assembly.GetType(dialogTypeName);
            var dialog = (Form)Activator.CreateInstance(dialogType, new PropertyGrid());
            dialog.Text = "Ошибка";
            var cancelBtn = dialog.Controls.Find("cancelBtn", true);
            cancelBtn[0].Text = "OK";
            var okBtn = dialog.Controls.Find("okBtn", true);
            okBtn[0].Visible = false;
            var layoutPanel = dialog.Controls.Find("buttonTableLayoutPanel", true);
            dialogType.GetProperty("Details").SetValue(dialog, ex.InnerException?.Message, null);
            dialogType.GetProperty("Message").SetValue(dialog, ex.Message, null);

            // Display dialog.
            var result = dialog.ShowDialog();
        }
    }
}
