//////////////////////////////////////////////////////////////////////////
// unSigned's Setup Projects Converter                                  //
// Copyright (c) 2016 - 2018 Slappy & unSigned, s. r. o.                //
// http://www.unsignedsw.com, https://github.com/tSlappy/setupconverter //
// All Rights Reserved.                                                 //
//////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Generic;

using SetupProjectConverterGUI;

namespace SetupProjectConverter
{
    public class BaseProject
    {
        // Input: .vdproj file
        public int mLineCount = 0;
        public string mProjectFile = null;
        public InputProject mInputProject;

        // Output: .xml file and filled data structures
        public string mXmlData = null;
        public List<BaseFile> mFiles = null;
        public List<BaseFolder> mFolders = null;
        public List<BaseRegKey> mRegistryKeys = null;
        public BaseProduct mProduct = null;
        public List<BaseShortcut> mShortcuts = null;
        public List<BaseOtherType> mOtherTypeObjects = null;
        public string ProjectName = null;
        public string Output = null;
        public ProjectType ProjectType;
        public string DefaultDir = null;

        public AsynchronousDialog mProgressDialog = null;

        public string XmlData
        {
            get { return mXmlData; }
        }
        public List<BaseFile> Files
        {
            get { return mFiles; }
        }
        public List<BaseFolder> Folders
        {
            get { return mFolders; }
        }
        public List<BaseRegKey> RegistryKeys
        {
            get { return mRegistryKeys; }
        }

        public List<BaseShortcut> Shortcuts
        {
            get { return mShortcuts; }
        }

        public List<BaseOtherType> OtherTypeObjects
        {
            get { return mOtherTypeObjects; }
        }

        public BaseProduct Product
        {
            get { return mProduct; }
        }

        private void ProgressMessage(string message)
        {
            try
            {
            	mProgressDialog.UpdateMessage(message, false);
            }
            catch (Exception)
            {
            	
            }
        }


        public string GetFolderPathById(string folderID)
        {
            string result = null;
            if (mFolders == null || mFolders.Count == 0)
                result = string.Empty;
            else
            {
                for (int i = 0; i < mFolders.Count; i++)
                {
                    if (mFolders[i].Id == folderID)
                    {
                        result = mFolders[i].Path;
                        break;
                    }
                }
            }

            if ((mInputProject == InputProject.IsleProject) && (result == null))
            {
                // Sometimes folder is not present in Component or Directory table (like [DesktopFolder])
                result = folderID;
            }

            return result;
        }

        public BaseFile GetFileById(string fileID)
        {
            BaseFile result = null;
            if (mFiles == null || mFiles.Count == 0)
                result = null;
            else
            {
                for (int i = 0; i < mFiles.Count; i++)
                {
                    if (mFiles[i].Id == fileID)
                    {
                        result = mFiles[i];
                        break;
                    }
                }
            }
            return result;
        }

        public string GetFileNameById(string fileID)
        {
            string result = null;
            if (mFiles == null || mFiles.Count == 0)
                result = string.Empty;
            else
            {
                for (int i = 0; i < mFiles.Count; i++)
                {
                    if (mFiles[i].Id == fileID)
                    {
                        if (String.IsNullOrEmpty(mFiles[i].DestName))
                            result = mFiles[i].SourceName;
                        else
                            result = mFiles[i].DestName;
                        break;
                    }
                }
            }
            return result;
        }

        // Search in mOtherTypeObjects
        public BaseOtherType GetOtherTypeObjectById(string id)
        {
            BaseOtherType result = null;
            if (mOtherTypeObjects == null || mOtherTypeObjects.Count == 0)
                result = null;
            else
            {
                for (int i = 0; i < mOtherTypeObjects.Count; i++)
                {
                    if (mOtherTypeObjects[i].Id == id)
                    {
                        result = mOtherTypeObjects[i];
                        break;
                    }
                }
            }
            return result;
        }

        /* Currently not used, but correct and working
                public string GetOtherTypeObjectNameById(string id)
                {
                    string result = null;
                    if (mOtherTypeObjects == null || mOtherTypeObjects.Count == 0)
                        result = string.Empty;
                    else
                    {
                        for (int i = 0; i < mOtherTypeObjects.Count; i++)
                        {
                            if (mOtherTypeObjects[i].Id == id)
                            {
                                if (String.IsNullOrEmpty(mOtherTypeObjects[i].DestName))
                                    result = mOtherTypeObjects[i].SourceName;
                                else
                                    result = mOtherTypeObjects[i].DestName;
                                break;
                            }
                        }
                    }
                    return result;
                }*/
    }
}
