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
    public enum FileType
    {
        File = 0,
        Assembly,
        Font,
        Library // .tlb only!
    }

    public class FileDetails
    {
        // All, values from Register node:
        // vsdrfDoNotRegister:  1
        // vsdrfCOM:            2
        // vsdrfCOMRelativePath 3
        // vsdrfCOMSelfReg:     4
        // vsdrfFont:           5

        public FileType Type = FileType.File;   
        public bool Register = false;

        // For: Assembly
        public String AsmName = null;
        public bool IsInGAC = false;

        // For: Font
        public bool IsOTF = false;

        // For: File
    }

    public class BaseFile
    {
        public string Id = null;
        public string SourceName = null;
        public string DestName = null;
        public string FolderId = null;
        public bool Exclude = false;             
        public FileDetails Details = null;

        public string GetName()
        {
            if (String.IsNullOrEmpty(DestName))
                return SourceName;
            else
                return DestName;
        }

        public override string ToString()
        {
            return String.IsNullOrEmpty(SourceName) ? Id : SourceName;
        }
    }
}
