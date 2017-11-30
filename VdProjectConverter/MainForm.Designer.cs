namespace SetupProjectConverter
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonStart = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.groupBoxSource = new System.Windows.Forms.GroupBox();
            this.radioButtonAi = new System.Windows.Forms.RadioButton();
            this.pictureBoxAi = new System.Windows.Forms.PictureBox();
            this.radioButtonISLE = new System.Windows.Forms.RadioButton();
            this.radioButtonVDProj = new System.Windows.Forms.RadioButton();
            this.pictureBoxISLE = new System.Windows.Forms.PictureBox();
            this.pictureBoxVDProj = new System.Windows.Forms.PictureBox();
            this.groupBoxTarget = new System.Windows.Forms.GroupBox();
            this.radioButtonInnoSetup = new System.Windows.Forms.RadioButton();
            this.radioButtonNSIS = new System.Windows.Forms.RadioButton();
            this.pictureBoxInnoSetup = new System.Windows.Forms.PictureBox();
            this.pictureBoxNSIS = new System.Windows.Forms.PictureBox();
            this.groupBoxSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxISLE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVDProj)).BeginInit();
            this.groupBoxTarget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInnoSetup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNSIS)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(520, 262);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(166, 33);
            this.buttonStart.TabIndex = 13;
            this.buttonStart.Text = "&Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Setup Projects (*.vdproj)|*.vdproj|All files (*.*)|*.*";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(14, 322);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(669, 17);
            this.labelInfo.TabIndex = 14;
            this.labelInfo.Text = "Select Source installation system (left), Target (right) and click Start button t" +
    "o open the file and convert it.";
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(12, 262);
            this.buttonAbout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(166, 33);
            this.buttonAbout.TabIndex = 15;
            this.buttonAbout.Text = "&About...";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // groupBoxSource
            // 
            this.groupBoxSource.Controls.Add(this.radioButtonAi);
            this.groupBoxSource.Controls.Add(this.pictureBoxAi);
            this.groupBoxSource.Controls.Add(this.radioButtonISLE);
            this.groupBoxSource.Controls.Add(this.radioButtonVDProj);
            this.groupBoxSource.Controls.Add(this.pictureBoxISLE);
            this.groupBoxSource.Controls.Add(this.pictureBoxVDProj);
            this.groupBoxSource.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSource.Name = "groupBoxSource";
            this.groupBoxSource.Size = new System.Drawing.Size(329, 233);
            this.groupBoxSource.TabIndex = 20;
            this.groupBoxSource.TabStop = false;
            this.groupBoxSource.Text = "Source";
            // 
            // radioButtonAi
            // 
            this.radioButtonAi.AutoSize = true;
            this.radioButtonAi.Location = new System.Drawing.Point(76, 165);
            this.radioButtonAi.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonAi.Name = "radioButtonAi";
            this.radioButtonAi.Size = new System.Drawing.Size(145, 21);
            this.radioButtonAi.TabIndex = 25;
            this.radioButtonAi.Text = "&Advanced Installer";
            this.radioButtonAi.UseVisualStyleBackColor = true;
            // 
            // pictureBoxAi
            // 
            this.pictureBoxAi.Image = global::SetupProjectConverter.Properties.Resources.advanced;
            this.pictureBoxAi.Location = new System.Drawing.Point(5, 157);
            this.pictureBoxAi.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxAi.Name = "pictureBoxAi";
            this.pictureBoxAi.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxAi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxAi.TabIndex = 24;
            this.pictureBoxAi.TabStop = false;
            // 
            // radioButtonISLE
            // 
            this.radioButtonISLE.AutoSize = true;
            this.radioButtonISLE.Location = new System.Drawing.Point(76, 99);
            this.radioButtonISLE.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonISLE.Name = "radioButtonISLE";
            this.radioButtonISLE.Size = new System.Drawing.Size(200, 21);
            this.radioButtonISLE.TabIndex = 23;
            this.radioButtonISLE.Text = "InstallShield &Limited Edition";
            this.radioButtonISLE.UseVisualStyleBackColor = true;
            // 
            // radioButtonVDProj
            // 
            this.radioButtonVDProj.AutoSize = true;
            this.radioButtonVDProj.Checked = true;
            this.radioButtonVDProj.Location = new System.Drawing.Point(76, 38);
            this.radioButtonVDProj.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonVDProj.Name = "radioButtonVDProj";
            this.radioButtonVDProj.Size = new System.Drawing.Size(247, 21);
            this.radioButtonVDProj.TabIndex = 22;
            this.radioButtonVDProj.TabStop = true;
            this.radioButtonVDProj.Text = "Setup and &Deploy Project (.vdproj)";
            this.radioButtonVDProj.UseVisualStyleBackColor = true;
            // 
            // pictureBoxISLE
            // 
            this.pictureBoxISLE.Image = global::SetupProjectConverter.Properties.Resources.isle;
            this.pictureBoxISLE.Location = new System.Drawing.Point(5, 91);
            this.pictureBoxISLE.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxISLE.Name = "pictureBoxISLE";
            this.pictureBoxISLE.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxISLE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxISLE.TabIndex = 21;
            this.pictureBoxISLE.TabStop = false;
            // 
            // pictureBoxVDProj
            // 
            this.pictureBoxVDProj.Image = global::SetupProjectConverter.Properties.Resources.vdproject;
            this.pictureBoxVDProj.Location = new System.Drawing.Point(5, 26);
            this.pictureBoxVDProj.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxVDProj.Name = "pictureBoxVDProj";
            this.pictureBoxVDProj.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxVDProj.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxVDProj.TabIndex = 20;
            this.pictureBoxVDProj.TabStop = false;
            // 
            // groupBoxTarget
            // 
            this.groupBoxTarget.Controls.Add(this.radioButtonInnoSetup);
            this.groupBoxTarget.Controls.Add(this.radioButtonNSIS);
            this.groupBoxTarget.Controls.Add(this.pictureBoxInnoSetup);
            this.groupBoxTarget.Controls.Add(this.pictureBoxNSIS);
            this.groupBoxTarget.Location = new System.Drawing.Point(347, 12);
            this.groupBoxTarget.Name = "groupBoxTarget";
            this.groupBoxTarget.Size = new System.Drawing.Size(339, 153);
            this.groupBoxTarget.TabIndex = 21;
            this.groupBoxTarget.TabStop = false;
            this.groupBoxTarget.Text = "Target";
            // 
            // radioButtonInnoSetup
            // 
            this.radioButtonInnoSetup.AutoSize = true;
            this.radioButtonInnoSetup.Checked = true;
            this.radioButtonInnoSetup.Location = new System.Drawing.Point(81, 99);
            this.radioButtonInnoSetup.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonInnoSetup.Name = "radioButtonInnoSetup";
            this.radioButtonInnoSetup.Size = new System.Drawing.Size(97, 21);
            this.radioButtonInnoSetup.TabIndex = 16;
            this.radioButtonInnoSetup.TabStop = true;
            this.radioButtonInnoSetup.Text = "&Inno Setup";
            this.radioButtonInnoSetup.UseVisualStyleBackColor = true;
            // 
            // radioButtonNSIS
            // 
            this.radioButtonNSIS.AutoSize = true;
            this.radioButtonNSIS.Location = new System.Drawing.Point(81, 38);
            this.radioButtonNSIS.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonNSIS.Name = "radioButtonNSIS";
            this.radioButtonNSIS.Size = new System.Drawing.Size(60, 21);
            this.radioButtonNSIS.TabIndex = 15;
            this.radioButtonNSIS.Text = "&NSIS";
            this.radioButtonNSIS.UseVisualStyleBackColor = true;
            // 
            // pictureBoxInnoSetup
            // 
            this.pictureBoxInnoSetup.Image = global::SetupProjectConverter.Properties.Resources.innosetup;
            this.pictureBoxInnoSetup.Location = new System.Drawing.Point(5, 91);
            this.pictureBoxInnoSetup.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxInnoSetup.Name = "pictureBoxInnoSetup";
            this.pictureBoxInnoSetup.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxInnoSetup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxInnoSetup.TabIndex = 14;
            this.pictureBoxInnoSetup.TabStop = false;
            // 
            // pictureBoxNSIS
            // 
            this.pictureBoxNSIS.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNSIS.Image")));
            this.pictureBoxNSIS.Location = new System.Drawing.Point(5, 26);
            this.pictureBoxNSIS.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxNSIS.Name = "pictureBoxNSIS";
            this.pictureBoxNSIS.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxNSIS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxNSIS.TabIndex = 13;
            this.pictureBoxNSIS.TabStop = false;
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 348);
            this.Controls.Add(this.groupBoxTarget);
            this.Controls.Add(this.groupBoxSource);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "unSigned\'s Setup Projects Converter";
            this.groupBoxSource.ResumeLayout(false);
            this.groupBoxSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxISLE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVDProj)).EndInit();
            this.groupBoxTarget.ResumeLayout(false);
            this.groupBoxTarget.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInnoSetup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNSIS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.GroupBox groupBoxSource;
        private System.Windows.Forms.RadioButton radioButtonISLE;
        private System.Windows.Forms.RadioButton radioButtonVDProj;
        private System.Windows.Forms.PictureBox pictureBoxISLE;
        private System.Windows.Forms.PictureBox pictureBoxVDProj;
        private System.Windows.Forms.GroupBox groupBoxTarget;
        private System.Windows.Forms.RadioButton radioButtonInnoSetup;
        private System.Windows.Forms.RadioButton radioButtonNSIS;
        private System.Windows.Forms.PictureBox pictureBoxInnoSetup;
        private System.Windows.Forms.PictureBox pictureBoxNSIS;
        private System.Windows.Forms.RadioButton radioButtonAi;
        private System.Windows.Forms.PictureBox pictureBoxAi;
    }
}