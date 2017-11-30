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
    public class AiFile: BaseFile
    {
        public AiFile(XmlNode fileNode, ref Dictionary<string, string> components)
        {
            Id = RowValue(ref fileNode, "File");
            SourceName = RowValue(ref fileNode, "SourcePath");
            DestName = GetFilenamePart(RowValue(ref fileNode, "FileName"));
            FolderId = components[RowValue(ref fileNode, "Component_")];
            //!Exclude = VdProduct.AtrToBool(TdValue(ref fileNode, "Exclude"));
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

      /*      if (Details.Type == FileType.Font)
            {
                // Should be registered?                
                if (TdValue(ref fileNode, "Register") == "5") // vsdrfFont
                    Details.Register = true;
            }
            // Type library (.tlb). 
            if (SourceName.Contains(".tlb") || DestName.Contains(".tlb"))
            {
                Details.Type = FileType.Library;
                int regFile = int.Parse(TdValue(ref fileNode, "Register"));
                if (regFile > 1 && regFile < 5)
                    Details.Register = true;
            }

            // Is assembly?
            if (TdValue(ref fileNode, "AssemblyRegister") != null)
            {
                Details.Type = FileType.Assembly;
                int regFile = int.Parse(TdValue(ref fileNode, "AssemblyRegister"));
                if (regFile > 1 && regFile < 5)
                    Details.Register = true;
                Details.IsInGAC = VdProduct.AtrToBool(TdValue(ref fileNode, "AssemblyIsInGAC"));
                Details.AsmName = TdValue(ref fileNode, "AssemblyAsmDisplayName");
            }

            // Register File?
            if(Details.Type == FileType.File)
            {
                int regFile = int.Parse(TdValue(ref fileNode, "Register"));
                if (regFile > 1 && regFile < 5)
                    Details.Register = true;
            }*/
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

        private string GetFilenamePart(string shortnameFullname)
        {
            int iPos = shortnameFullname.IndexOf('|');
            return shortnameFullname.Substring(iPos + 1);
        }
    }
}
