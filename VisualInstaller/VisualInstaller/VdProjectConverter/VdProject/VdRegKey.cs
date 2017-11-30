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
    public class VdRegKey: BaseRegKey
    {
        public VdRegKey(ref List<BaseRegKey> allRegistryKeys, RegRoot root, string parentPath, XmlNode keyNode)
        {
            mRegistryKeys = allRegistryKeys;
            Id = keyNode.Name;
            Root = root;
            if (String.IsNullOrEmpty(parentPath))
                Path = ElementValue(ref keyNode, "Name");
            else
                Path += parentPath + "\\" + ElementValue(ref keyNode, "Name");

            XmlNode regValues = keyNode.SelectSingleNode("Values");
            // If there are no <Values then this is only parent of sub-key (so do not add it)
            if (regValues != null && regValues.HasChildNodes)
            {
                Values = new List<RegValue>(regValues.ChildNodes.Count);
                for (int k = 0; k < regValues.ChildNodes.Count; k++)
                {
                    XmlNode valNode = regValues.ChildNodes[k];
                    string name = ElementValue(ref valNode, "Name");
                    RegValueType type = (RegValueType)int.Parse(ElementValue(ref valNode, "ValueTypes"));
                    string value = ElementValue(ref valNode, "Value");
                    Values.Add(new RegValue(name, type, value));
                }

                // Add registry key
                mRegistryKeys.Add(this);
            }
            else
            {
                // Process sub-keys (without adding parent)
                XmlNode subKeys = keyNode.SelectSingleNode("Keys");
                if (subKeys != null && subKeys.HasChildNodes)
                {
                    for (int i = 0; i < subKeys.ChildNodes.Count; i++)
                    {
                        XmlNode childNode = subKeys.ChildNodes[i];
                        VdRegKey regKey = new VdRegKey(ref mRegistryKeys, Root, Path, childNode);
                    }
                }
                else
                {
                    // Add registry key (without Values)
                    mRegistryKeys.Add(this);
                }
            }            
        }

        private string ElementValue(ref XmlNode fileNode, string element)
        {
            XmlNode node = fileNode.SelectSingleNode(element);
            string value = null;
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
