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
    public class IsleRegKey: BaseRegKey
    {
        public IsleRegKey(ref List<BaseRegKey> allRegistryKeys, XmlNode keyNode)
        {
            mRegistryKeys = allRegistryKeys;
            Id = TdValue(ref keyNode, 0);
            int regRoot = int.Parse(TdValue(ref keyNode, 1));
            if (regRoot == -1)
                regRoot = 2; // HKLM
            Root = (RegRoot)regRoot;
            Path = TdValue(ref keyNode, 2);

            Values = new List<RegValue>();
            RegValueType rvType = RegValueType.REG_NONE;
            string value = TdValue(ref keyNode, 4);
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

            Values.Add(new RegValue(TdValue(ref keyNode, 3), rvType, value));

            // Add registry key
            mRegistryKeys.Add(this);                  
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
    }
}
