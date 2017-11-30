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
    public class BaseShortcut
    {
        public string Id = null;
        public string Name = null;
        public string Arguments = null;
        public string TargetId = null;
        public string WorkingFolderId = null;
        public string FolderId = null;
        public string IconId = null;
        public string Description = null;
        public int IconIndex = 0;
        public bool IsFolder = false;

        public override string ToString()
        {
            return String.IsNullOrEmpty(Name) ? Id : Name;
        }
    }
}
