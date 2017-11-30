//////////////////////////////////////////////////////////////////////////
// unSigned's Setup Projects Converter                                  //
// Copyright (c) 2016 - 2018 Slappy & unSigned, s. r. o.                //
// http://www.unsignedsw.com, https://github.com/tSlappy/setupconverter //
// All Rights Reserved.                                                 //
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SetupProjectConverter
{
    public class AiFolder: BaseFolder
    {
        public string Parent;
        public bool IsRoot = false;

        public AiFolder(XmlNode folderNode, ref List<BaseFolder> allFolders, BaseFolder parentFolder)
        {
            mFolders = allFolders;

            // Create this
            Id = RowValue(ref folderNode, "Directory");
            Parent = RowValue(ref folderNode, "Directory_Parent");
            Path = string.Format("{0}", GetFolderNamePart(RowValue(ref folderNode, "DefaultDir")));
            IsRoot = RowValue(ref folderNode, "IsPseudoRoot") == "1" ? true : false;
            if (IsRoot)
                Path = string.Format("[{0}]", Path);

            if (Path == "SourceDir")
                Id = null; // Null Id -> do not add this folder
        }

        private string RowValue(ref XmlNode rowNode, string name)
        {
            try
            {
                return rowNode.Attributes.GetNamedItem(name).Value.Trim();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return string.Empty;
        }

        private string GetFolderNamePart(string shortnameFullname)
        {
            string result = shortnameFullname;

            int iPos = shortnameFullname.IndexOf('|');
            if (iPos > -1)
                result = shortnameFullname.Substring(iPos + 1);
            
            // Modify Folder to match correct pattern
            if (result == "APPDIR:.")
                result = "TARGETDIR";

            return result;
        }
    }
}
