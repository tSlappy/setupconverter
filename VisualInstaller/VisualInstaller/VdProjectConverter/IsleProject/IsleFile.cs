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
    public class IsleFile: BaseFile
    {
        public IsleFile(XmlNode fileNode)
        {
            Id = TdValue(ref fileNode, 0);
            SourceName = TdValue(ref fileNode, 8);
            DestName = GetFilenamePart(TdValue(ref fileNode, 2));
            FolderId = TdValue(ref fileNode, 1);
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

        private string GetFilenamePart(string shortnameFullname)
        {
            int iPos = shortnameFullname.IndexOf('|');
            return shortnameFullname.Substring(iPos + 1);
        }
    }
}
