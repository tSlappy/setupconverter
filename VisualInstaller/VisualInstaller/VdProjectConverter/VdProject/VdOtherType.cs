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
    public class VdOtherType: BaseOtherType
    {
        public VdOtherType(XmlNode otherNode, OtherType type)
        {
            Id = otherNode.Name;
            Type = type;
            SourceName = AtributeValue(ref otherNode, "SourcePath");
            if (Type == OtherType.ProjectOutput)
            {
                // Real DestName should be parsed from .csproj file which is really difficult
                // Instead simply keep only file name (without path)
                DestName = AtributeValue(ref otherNode, "TargetName");
                if (String.IsNullOrEmpty(DestName))
                {
                    int iPos = SourceName.LastIndexOf('\\');
                    if (iPos > 0)
                        DestName = SourceName.Substring(iPos + 1);
                    else
                        DestName = SourceName;
                }
            }
            else
                DestName = AtributeValue(ref otherNode, "TargetName");
            FolderId = AtributeValue(ref otherNode, "Folder");
            Exclude = VdProduct.AtrToBool(AtributeValue(ref otherNode, "Exclude"));
            
        }

        private string AtributeValue(ref XmlNode fileNode, string atribute)
        {
            XmlNode node = fileNode.SelectSingleNode(atribute);
            string value = null;
            if (node == null)
                return value;

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
