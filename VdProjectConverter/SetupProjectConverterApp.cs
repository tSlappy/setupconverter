//////////////////////////////////////////////////////////////////////////
// unSigned's Setup Projects Converter                                  //
// Copyright (c) 2016 - 2018 Slappy & unSigned, s. r. o.                //
// http://www.unsignedsw.com, https://github.com/tSlappy/setupconverter //
// All Rights Reserved.                                                 //
//////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Diagnostics;
using SetupProjectConverterGUI;

namespace SetupProjectConverter
{
    public enum InputProject
    {
        VdProject = 0,
        IsleProject = 1,
        AiProject = 2
    }

    public enum OutputProject
    {
        Nsis = 0,
        InnoSetup = 1
    }

    class SetupProjectConverterApp
    {
        InputProject mInputProject;
        string mSourceProjectFile = null;
        FileInfo mSourceProjectFileInfo = null;

        OutputProject mOutputProject;
        string mOutputScriptFile = null;

        bool mResult = false;
        AsynchronousDialog mProgressDialog = null;

        BaseProject mProject = null;

        public AsynchronousDialog ProgressDialog
        {
            get { return mProgressDialog; }
        }

        public SetupProjectConverterApp(string sourceFile, InputProject inputProject, OutputProject outputProject)
        {
            mSourceProjectFile = sourceFile;
            mInputProject = inputProject;
            mOutputProject = outputProject;
            mSourceProjectFileInfo = new FileInfo(mSourceProjectFile);

            mProgressDialog = new AsynchronousDialog();
            mProgressDialog.ShowProgressDialog();
        }

        public void ProgressMessage(string message)
        {
            try
            {
                mProgressDialog.UpdateMessage(message, false);
            }
            catch (Exception)
            {

            }
        }

        public void ProgressMessageAppend(string message)
        {
            try
            {
                mProgressDialog.UpdateMessage(message, true);
            }
            catch (Exception)
            {

            }
        }

        public void ConvertProject()
        {
            // Load the project
            switch (mInputProject)
            {
                case InputProject.VdProject:
                    mProject = new VdProject(ref mProgressDialog, mSourceProjectFile);
                    break;
                case InputProject.IsleProject:
                    mProject = new IsleProject(ref mProgressDialog, mSourceProjectFile);
                    break;
                case InputProject.AiProject:
                    mProject = new AiProject(ref mProgressDialog, mSourceProjectFile);
                    break;
        }
            if (mOutputProject == OutputProject.InnoSetup)
            {
                // InnoSetupProject
                ProgressMessage("Creating script file...");
                InnoProject innoProject = new InnoProject();
                innoProject.LoadProject(mProject);
                ProgressMessage("Generating script...");
                mResult = innoProject.Convert();

                if (mResult)
                {
                    mOutputScriptFile = mSourceProjectFileInfo.DirectoryName + "\\" + innoProject.mVdProject.ProjectName + ".iss";
                    ProgressMessage("Saving script as '" + mOutputScriptFile + "'...");
                    mResult = innoProject.SaveScript(mOutputScriptFile);
                }
                else
                {
                    // Display information for user
                    ProgressMessage("Error occurred during generating script!!");
                    mProgressDialog.EnableButton(true);
                }

                if (mResult)
                {
                    // Everything is OK so close the dialog automatically
                    mProgressDialog.CloseDialog("Script '" + mOutputScriptFile + "' created successfully!", 2000);
                    Process.Start(new FileInfo(mOutputScriptFile).DirectoryName);
                }
                else
                {
                    ProgressMessage("Error occurred during saving script as '" + mOutputScriptFile + "'!");
                    mProgressDialog.EnableButton(true);
                }
            }
            else
            {
                // NSISProject
                ProgressMessage("Creating script file...");
                NsisProject nsisProject = new NsisProject();
                nsisProject.LoadProject(mProject);
                ProgressMessage("Generating script...");
                mResult = nsisProject.Convert();

                if (mResult)
                {
                    mOutputScriptFile = mSourceProjectFileInfo.DirectoryName + "\\" + nsisProject.mVdProject.ProjectName + ".nsi";
                    ProgressMessage("Saving script as '" + mOutputScriptFile + "'...");
                    mResult = nsisProject.SaveScript(mOutputScriptFile);
                }
                else
                {
                    // Display information for user
                    ProgressMessage("Error occurred during generating script!!");
                    mProgressDialog.EnableButton(true);
                }

                if (mResult)
                {
                    // Everything is OK so close the dialog automatically
                    mProgressDialog.CloseDialog("Script '" + mOutputScriptFile + "' created successfully!", 2000);
                    Process.Start(new FileInfo(mOutputScriptFile).DirectoryName);
                }
                else
                {
                    ProgressMessage("Error occurred during saving script as '" + mOutputScriptFile + "'!");
                    mProgressDialog.EnableButton(true);
                }
            }
        }
    }
}
