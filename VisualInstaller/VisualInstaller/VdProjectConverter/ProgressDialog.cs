using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SetupProjectConverterGUI
{
    public partial class ProgressDialog : Form
    {
        public ProgressDialog()
        {
            InitializeComponent();
            buttonOK.Visible = false;
        }

        delegate void SetMessageCallback(string text, bool append);
        delegate void SetTitleCallback(string title, string subtitle);
        delegate void EnableButtonCallback(bool enable);
        delegate void ForceCloseCallback();
        delegate void SetProgressCallback(int progress);

        public void SetMessage(string message, bool append)
        {
            // InvokeRequired required compares the thread ID of the calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelMessage.InvokeRequired)
            {
                SetMessageCallback messageCallback = new SetMessageCallback(SetMessage);
                this.Invoke(messageCallback, new object[] { message, append });
            }
            else
            {
                if (append)
                    this.labelMessage.Text += "\n" + message;
                else
                    this.labelMessage.Text = message;
                this.progressBarMain.Value += 10;
            }           
        }

        public void SetTitle(string title, string subtitle)
        {
            // InvokeRequired required compares the thread ID of the calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired || this.labelHead.InvokeRequired)
            {
                SetTitleCallback titleCallback = new SetTitleCallback(SetTitle);
                this.Invoke(titleCallback, new object[] { title, subtitle });
            }
            else
            {                
                this.Text = title;
                this.labelHead.Text = subtitle;
            }
        }

        public void ForceClose()
        {
            // InvokeRequired required compares the thread ID of the calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                ForceCloseCallback enableCallback = new ForceCloseCallback(ForceClose);
                this.Invoke(enableCallback, null);
            }
            else
            {
                this.progressBarMain.Value = this.progressBarMain.Maximum;
                this.Close();
            }
        }

        public void EnableButton(bool enable)
        {
            // InvokeRequired required compares the thread ID of the calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.buttonOK.InvokeRequired)
            {
                EnableButtonCallback enableCallback = new EnableButtonCallback(EnableButton);
                this.Invoke(enableCallback, new object[] { enable });
            }
            else
            {
                this.buttonOK.Visible = enable;
            }
        }

        public void SetProgress(int progress)
        {
            // InvokeRequired required compares the thread ID of the calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.progressBarMain.InvokeRequired)
            {
                SetProgressCallback progressCallback = new SetProgressCallback(SetProgress);
                this.Invoke(progressCallback, new object[] { progress });
            }
            else
            {
                this.progressBarMain.Value = progress;
            }
        }
    }
}
