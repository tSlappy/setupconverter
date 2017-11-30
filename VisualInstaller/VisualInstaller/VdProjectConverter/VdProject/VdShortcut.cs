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
    public class VdShortcut: BaseShortcut
    {
        public VdShortcut(XmlNode shortcutNode)
        {
            Id = shortcutNode.Name;
            Name = AtributeValue(ref shortcutNode, "Name");
            Arguments = AtributeValue(ref shortcutNode, "Arguments");
            Description = AtributeValue(ref shortcutNode, "Description");
            TargetId = AtributeValue(ref shortcutNode, "Target");
            FolderId = AtributeValue(ref shortcutNode, "Folder");
            IconId = AtributeValue(ref shortcutNode, "Icon");
            if (!String.IsNullOrEmpty(IconId))
                IconIndex = VdProduct.AtrToInt(AtributeValue(ref shortcutNode, "IconIndex"));
            WorkingFolderId = AtributeValue(ref shortcutNode, "WorkingFolder");
            IsFolder = ((TargetId == FolderId) && (FolderId == WorkingFolderId));
        }

        private string AtributeValue(ref XmlNode shortcutNode, string atribute)
        {
            XmlNode node = shortcutNode.SelectSingleNode(atribute);
            string value = null;
            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                for (int k = 0; k < node.Attributes.Count; k++)
                {
                    XmlNode attribute = node.Attributes.Item(k);
                    if (attribute.Name == "value" || attribute.Name == "Value")
                    {
                        value = attribute.Value.Trim();
                        break;
                    }
                }
            }
            return value;
        }
    }
}
