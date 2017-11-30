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
    public enum OtherType
    {
        ProjectOutput = 0
    }

    public class BaseOtherType
    {
        public string Id = null;
        public string SourceName = null;
        public string DestName = null;
        public string FolderId = null;
        public bool Exclude = false;
        public OtherType Type;

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
