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
    public enum RegRoot
    {
        HKCR,   // (HKEY_CLASSES_ROOT) 
        HKCU,   // (HKEY_CURRENT_USER) 
        HKLM,   // (HKEY_LOCAL_MACHINE) 
        HKU,    // (HKEY_USERS) 
        HKCC    // (HKEY_CURRENT_CONFIG) 
    }

    public enum RegValueType
    {
        REG_NONE = 0,       // If none (the default setting) is specified, Setup will create the key but not a value. In this case the ValueName and ValueData parameters are ignored.
        REG_SZ = 8,         // If string is specified, Setup will create a string (REG_SZ) value.
        REG_EXPAND_SZ = 2,  // If expandsz is specified, Setup will create an expand-string (REG_EXPAND_SZ) value.
        REG_MULTI_SZ = 1,   // If multisz is specified, Setup will create an multi-string (REG_MULTI_SZ) value.
        REG_DWORD = 3,      // If dword is specified, Setup will create a 32-bit integer (REG_DWORD) value.
        REG_QWORD = 4,      // If qword is specified, Setup will create a 64-bit integer (REG_QWORD) value.
        REG_BINARY = 5      // If binary is specified, Setup will create a binary (REG_BINARY) value.
    }

    public class RegValue
    {
        public string Name = null;
        public RegValueType Type;
        public string Value = null;

        public RegValue(string name, RegValueType type, string value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return String.Format("'{0}' = '{1}'", Name, Value);
        }
    }

    public class BaseRegKey
    {
        public List<BaseRegKey> mRegistryKeys = null;

        public string Id = null;
        public RegRoot Root;
        public string Path = null; // Absolute path!
        public string Key = null;
        public List<RegValue> Values = null;

        public override string ToString()
        {
            return String.IsNullOrEmpty(Path) ? Id : Root.ToString() + " " + Path;
        }
    }
}
