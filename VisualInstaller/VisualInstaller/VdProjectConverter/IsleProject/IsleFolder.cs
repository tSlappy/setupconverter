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
    public class IsleFolder: BaseFolder
    {
        public string Guid;

        public IsleFolder(XmlNode folderNode, ref List<BaseFolder> allFolders, BaseFolder parentFolder)
        {
            mFolders = allFolders;

            // Create this
            Id = TdValue(ref folderNode, 0);
            Guid = TdValue(ref folderNode, 1);  
            Path = string.Format("[{0}]", TdValue(ref folderNode, 2));          
        }

        private string TdValue(ref XmlNode rowNode, int index)
        {
            try
            {
                return rowNode.ChildNodes.Item(index).InnerText.Trim();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return string.Empty;
        }
    }
}
