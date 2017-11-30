using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SetupProjectConverter;

namespace SetupProjectConverter
{
    public partial class MainForm : Form
    {
        // P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;
        // ID for the About item on the system menu
        private int SYSMENU_ABOUT_ID = 0x1;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

            // Add the About menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "&About...");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_ABOUT_ID))
            {
                buttonAbout_Click(null, null);
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            InputProject inputProject = InputProject.VdProject;
            if (radioButtonAi.Checked)
                inputProject = InputProject.AiProject;
            if (radioButtonISLE.Checked)
                inputProject = InputProject.IsleProject;

            switch (inputProject)
            {
                case InputProject.VdProject:
                    openFileDialog.Filter = "Setup and Deploy Projects (*.vdproj)|*.vdproj|All files (*.*)|*.*";
                    break;
                case InputProject.IsleProject:
                    openFileDialog.Filter = "InstallShield Limited Edition Projects (*.isl)|*.isl|All files (*.*)|*.*";
                    break;
                case InputProject.AiProject:
                    openFileDialog.Filter = "Advanced Installer Projects (*.aip)|*.aip|All files (*.*)|*.*";
                    break;
            }

            OutputProject outputProject = OutputProject.InnoSetup;
            if (radioButtonNSIS.Checked)
                outputProject = OutputProject.Nsis;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetupProjectConverterApp projectConverter = new SetupProjectConverterApp(openFileDialog.FileName, inputProject, outputProject);
                try
                {
                    projectConverter.ConvertProject();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine("[SetupProjectConverter] ExecConvertVdProjectCommand() - ConvertProject: " + ex.Message);
                    projectConverter.ProgressMessageAppend("Exception: " + ex.Message);
                    projectConverter.ProgressDialog.EnableButton(true);
                }
            }
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("unSigned's Setup Projects Converter\n\nVersion 1.01\n\nCompatible with:\n\nVisual & Installer (www.visual-installer.com)\n\nRAD & Installer (www.rad-installer.com)\n\n\nCopyright (c) 2016 - 2018 Slappy & unSigned, s. r. o.\n\nAll Rights Reserved.\n\nwww.unsignedsw.com", 
                "About...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
