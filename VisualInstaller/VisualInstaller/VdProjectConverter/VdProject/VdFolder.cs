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
    public class VdFolder: BaseFolder
    {
        public VdFolder(XmlNode folderNode, ref List<BaseFolder> allFolders, VdFolder parentFolder)
        {
            mFolders = allFolders;

            // Create this
            Id = folderNode.Name;
            XmlNode propertyNode = folderNode.SelectSingleNode("Property");
            XmlNode nameNode = folderNode.SelectSingleNode("Name");
            string property = PropertyReference.CheckFolderProperty(Atribute(ref propertyNode, "value"));
            string name = Atribute(ref nameNode, "value");

            // Now resolve Path for this folder
            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(property))
            {
                if (name[0] == '#' && property[0] != '_')
                {
                    // Use <Property value
                    if (parentFolder == null)
                    {
                        Path = property;
                    }
                    else
                    {
                        Path = parentFolder.Path + "\\" + Path;
                    }
                }
                else
                {
                    // Use <Name value
                    if (parentFolder == null)
                    {
                        Path = name;
                    }
                    else
                    {
                        Path = parentFolder.Path + "\\" + name;
                    }
                }
            }
            if (!String.IsNullOrEmpty(Path))
            {
                if (Path.Contains(coTargetDir))
                    IsAppDir = true;
            }


            // Add sub-folders for this (recursively)
            XmlNode documentFolderNode = folderNode.SelectSingleNode("Folders");
            if (documentFolderNode.HasChildNodes)
            {
                XmlNodeList foldersList = documentFolderNode.ChildNodes;
                for (int i = 0; i < foldersList.Count; i++)
                {
                    VdFolder folder = new VdFolder(foldersList.Item(i), ref mFolders, this);
                    mFolders.Add(folder);
                }
            }          
        }

        public string Atribute(ref XmlNode node, string atribute)
        {
            string value = null;
            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                for (int k = 0; k < node.Attributes.Count; k++)
                {
                    XmlNode attribute = node.Attributes.Item(k);
                    if (attribute.Name == atribute)
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
