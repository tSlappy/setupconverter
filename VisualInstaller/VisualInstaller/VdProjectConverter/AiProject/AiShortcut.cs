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
    public class AiShortcut: BaseShortcut
    {
        public AiShortcut(XmlNode shortcutNode, ref List<BaseFile> filesList)
        {
            Id = RowValue(ref shortcutNode, "Shortcut");
            Name = RowValue(ref shortcutNode, "Shortcut");
            //Arguments = RowValue(ref shortcutNode, 5);
            //!Description = TdValue(ref shortcutNode, 0);
            TargetId = GetTargetFile(RowValue(ref shortcutNode, "Target"), filesList);
            FolderId = string.Format("{0}", RowValue(ref shortcutNode, "Directory_"));
          /*  IconId = RowValue(ref shortcutNode, 0);
            if (!String.IsNullOrEmpty(IconId))
                IconIndex = 0; //!TdValue(ref shortcutNode, 0);*/
            WorkingFolderId = string.Format("{0}", RowValue(ref shortcutNode, "WkDir"));
            IsFolder = ((TargetId == FolderId) && (FolderId == WorkingFolderId));
        }

        private string GetTargetFile(string target, List<BaseFile> filesList)
        {
            // Remove [#] from [#PrintCall1.pdf]
            string clean = target.Substring(2, target.Length - 3).Trim();

            foreach (BaseFile file in filesList)
            {
                if (file.Id == clean)
                    return file.DestName;
            }

            return string.Empty;
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
    }
}
