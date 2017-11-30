using System;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;

/*
 * Check for update example.
 * Copyright: mech
 * http://themech.net/2008/05/adding-check-for-update-option-in-csharp/
 * http://themech.net/2008/09/check-for-updates-how-to-download-and-install-a-new-version-of-your-csharp-application/
 */

namespace SetupProjectConverterGUI
{
    class DialogUpdater
    {
        ProgressDialog mProgressDialog = null;

        private readonly AsynchronousDialog mAppUpdater;

        Thread mWorkerThread;
        // Events used to stop worker thread
        readonly ManualResetEvent mEventStopThread;
        readonly ManualResetEvent mEventThreadStopped;

        public DialogUpdater(AsynchronousDialog updater)
        {
            mAppUpdater = updater;
            mEventStopThread = new ManualResetEvent(false);
            mEventThreadStopped = new ManualResetEvent(false);
            mProgressDialog = new ProgressDialog();
        }

        public void OnShowProgressDialog()
        {
            if ((mWorkerThread != null) && (mWorkerThread.IsAlive))
                return;

            mWorkerThread = new Thread(ShowProgressDialogThreadFunction);
            mEventStopThread.Reset();
            mEventThreadStopped.Reset();
            mWorkerThread.SetApartmentState(ApartmentState.STA);
            mWorkerThread.Start();
        }

        // when the worker thread is running - let it know it should stop
        public void StopThread()
        {
            if (mWorkerThread != null && mWorkerThread.IsAlive)
            {
                mEventStopThread.Set();
                while (mWorkerThread.IsAlive)
                {
                    if (WaitHandle.WaitAll((new ManualResetEvent[] { mEventThreadStopped }), 100, true))
                    {
                        break;
                    }
                    Application.DoEvents();
                }
            }
        }

        // internal method - return true when the thread is supposed to stop
        private bool StopWorkerThread()
        {
            if (mEventStopThread.WaitOne(0, true))
            {
                mEventThreadStopped.Set();
                return true;
            }
            return false;
        }

        private void ShowProgressDialogThreadFunction()
        {
            if (StopWorkerThread())
                return;

            // Show progress dialog            
            mProgressDialog.ShowDialog();

            // Thread ends when dialog is closed
        }

        public void UpdateMessage(string message, bool append)
        {
            if (mProgressDialog == null)
            {
                mProgressDialog = new ProgressDialog();
                mProgressDialog.ShowDialog();
            }
            mProgressDialog.SetMessage(message, append);
        }

        internal void CloseDialog(string message, int time)
        {
            if (mProgressDialog != null)
            {
                mProgressDialog.SetMessage(message, false);
                Thread.Sleep(time);
                mProgressDialog.ForceClose();
            }
        }

        public void EnableButton(bool enable)
        {
            mProgressDialog.EnableButton(enable);
        }

        public void SetTitle(string title, string subtitle)
        {
            mProgressDialog.SetTitle(title, subtitle);
        }

        public void SetProgress(int progress)
        {
            mProgressDialog.SetProgress(progress);
        }
    }
}
