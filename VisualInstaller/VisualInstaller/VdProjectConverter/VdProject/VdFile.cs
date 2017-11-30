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
    public class VdFile: BaseFile
    {
        public VdFile(XmlNode fileNode)
        {
            Id = fileNode.Name;
            SourceName = AtributeValue(ref fileNode, "SourcePath");
            DestName = AtributeValue(ref fileNode, "TargetName");
            FolderId = AtributeValue(ref fileNode, "Folder");
            Exclude = VdProduct.AtrToBool(AtributeValue(ref fileNode, "Exclude"));
            if (Details == null)
                Details = new FileDetails();

            // Font
            if (SourceName.Contains(".ttf") || DestName.Contains(".ttf"))
            {
                Details.Type = FileType.Font;
                Details.IsOTF = false;
            }
            if (SourceName.Contains(".otf") || DestName.Contains(".otf"))
            {
                Details.Type = FileType.Font;
                Details.IsOTF = true;
            }

            if (Details.Type == FileType.Font)
            {
                // Should be registered?                
                if (AtributeValue(ref fileNode, "Register") == "5") // vsdrfFont
                    Details.Register = true;
            }
            // Type library (.tlb). 
            if (SourceName.Contains(".tlb") || DestName.Contains(".tlb"))
            {
                Details.Type = FileType.Library;
                int regFile = int.Parse(AtributeValue(ref fileNode, "Register"));
                if (regFile > 1 && regFile < 5)
                    Details.Register = true;
            }

            // Is assembly?
            if (AtributeValue(ref fileNode, "AssemblyRegister") != null)
            {
                Details.Type = FileType.Assembly;
                int regFile = int.Parse(AtributeValue(ref fileNode, "AssemblyRegister"));
                if (regFile > 1 && regFile < 5)
                    Details.Register = true;
                Details.IsInGAC = VdProduct.AtrToBool(AtributeValue(ref fileNode, "AssemblyIsInGAC"));
                Details.AsmName = AtributeValue(ref fileNode, "AssemblyAsmDisplayName");
            }

            // Register File?
            if(Details.Type == FileType.File)
            {
                int regFile = int.Parse(AtributeValue(ref fileNode, "Register"));
                if (regFile > 1 && regFile < 5)
                    Details.Register = true;
            }
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
