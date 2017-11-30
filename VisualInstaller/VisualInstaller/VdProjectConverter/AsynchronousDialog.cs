using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace SetupProjectConverterGUI
{
    public class AsynchronousDialog
    {
        DialogUpdater mDialogUpdater = null;

        public AsynchronousDialog()
        {
            // Create an objects that will manage our check for update process
            mDialogUpdater = new DialogUpdater(this);
        }

        public void Stop()
        {
            mDialogUpdater.StopThread();
        }

        public void ShowProgressDialog()
        {
            mDialogUpdater.OnShowProgressDialog();
        }

        public void UpdateMessage(string message, bool append)
        {
            mDialogUpdater.UpdateMessage(message, append);
        }

        public void CloseDialog(string message, int time)
        {
            mDialogUpdater.CloseDialog(message, time);
        }

        public void EnableButton(bool enable)
        {
            mDialogUpdater.EnableButton(enable);
        }

        public void SetTitle(string title, string subtitle)
        {
            mDialogUpdater.SetTitle(title, subtitle);
        }

        public void SetProgress(int progress)
        {
            mDialogUpdater.SetProgress(progress);
        }
    }
}
