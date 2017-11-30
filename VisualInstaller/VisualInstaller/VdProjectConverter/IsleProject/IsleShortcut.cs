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
    public class IsleShortcut: BaseShortcut
    {
        public IsleShortcut(XmlNode shortcutNode)
        {
            Id = TdValue(ref shortcutNode, 0);
            Name = TdValue(ref shortcutNode, 0);
            Arguments = TdValue(ref shortcutNode, 5);
            //!Description = TdValue(ref shortcutNode, 0);
            TargetId = TdValue(ref shortcutNode, 4);
            FolderId = string.Format("[{0}]", TdValue(ref shortcutNode, 1));
            IconId = TdValue(ref shortcutNode, 0);
            if (!String.IsNullOrEmpty(IconId))
                IconIndex = 0; //!TdValue(ref shortcutNode, 0);
            WorkingFolderId = string.Format("[{0}]", TdValue(ref shortcutNode, 11));
            IsFolder = ((TargetId == FolderId) && (FolderId == WorkingFolderId));
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
