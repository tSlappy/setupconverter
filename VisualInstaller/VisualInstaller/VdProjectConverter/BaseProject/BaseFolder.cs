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
    public class BaseFolder
    {
        public List<BaseFolder> mFolders = null;

        public const string coTargetDir = "TARGETDIR";

        public string Id = null;
        public string Path = null; // Absolute path!
        public bool IsAppDir = false; // true if this is directory created inside [TARGETDIR]

        public override string ToString()
        {
            return String.IsNullOrEmpty(Path) ? Id : Path;
        }
    }
}
