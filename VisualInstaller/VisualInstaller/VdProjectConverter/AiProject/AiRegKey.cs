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
    public class AiRegKey: BaseRegKey
    {
        public AiRegKey(ref List<BaseRegKey> allRegistryKeys, XmlNode keyNode)
        {
            mRegistryKeys = allRegistryKeys;
            Id = RowValue(ref keyNode, "Registry");
            int regRoot = int.Parse(RowValue(ref keyNode, "Root"));
            if (regRoot == -1)
                regRoot = 2; // HKLM
            Root = (RegRoot)regRoot;
            Path = RowValue(ref keyNode, "Key");

            Values = new List<RegValue>();
            RegValueType rvType = RegValueType.REG_NONE;
            string value = RowValue(ref keyNode, "Value");
            if (value.Contains("#x"))
            {
                rvType = RegValueType.REG_BINARY;
                value = value.Substring(2).Trim();
            }
            if (value.IndexOf('#') > -1)
            {
                rvType = RegValueType.REG_DWORD;
                value = value.Substring(1).Trim();
            }

            Values.Add(new RegValue(RowValue(ref keyNode, "Name"), rvType, value));

            // Add registry key
            mRegistryKeys.Add(this);                  
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
    }
}
