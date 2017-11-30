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
    public class VdProduct: BaseProduct
    {
        public VdProduct(XmlNode productNode)
        {
            // Parse the node and fill the data
            if (productNode != null && productNode.HasChildNodes)
            {
                mValues = new Dictionary<ProductInfo, string>(productNode.ChildNodes.Count);

                for (int i = 0; i < productNode.ChildNodes.Count; i++)
                {
                    XmlNode node = productNode.ChildNodes.Item(i);                    
                    for (int j = 0; j < ((int)ProductInfo._Count); j++)
                    {
                        ProductInfo info = (ProductInfo)j;
                        string name = info.ToString();
                        if(node.Name.CompareTo(info.ToString()) == 0)
                        {
                            string value  = null;
                            if(node.Attributes != null && node.Attributes.Count > 0)
                            {
                                for (int k = 0; k < node.Attributes.Count; k++)
                                {
                                    XmlNode attribute = node.Attributes.Item(k);
                                    if (attribute.Name == "value")
                                    {
                                        value = attribute.Value;
                                        break;
                                    }
                                }
                            }
                            mValues.Add(info, value);
                            break;
                        }
                    }
                }
                IsError = false;
            }
            else
                IsError = true;
        }

        public string GetValue(ProductInfo productInfo)
        {
            if (mValues == null)
                return "Missing Property!!!";

            if (mValues.ContainsKey(productInfo))
            {
                string value = null;
                mValues.TryGetValue(productInfo, out value);
                return value;
            }
            else
                return null;
        }

        public static ProjectType GetProjectTypeFromGUID(string GUID, bool isWebSetup)
        {
            if (GUID == "{5443560e-dbb4-11d2-8724-00a0c9a8b90c}")
                return ProjectType.MergeModule;

            if (GUID == "{5443560c-dbb4-11d2-8724-00a0c9a8b90c}")
            {
                if (isWebSetup)
                    return ProjectType.WebSetup;
                else
                    return ProjectType.Setup;
            }

            if (GUID == "{0b7288ca-6892-4441-925d-34d99f5c97bd}")
                return ProjectType.Cab;

            return ProjectType.Unknown;
        }

        public static bool AtrToBool(string atribute)
        {
            if (String.IsNullOrEmpty(atribute))
                return false;
            else
            {
                if (atribute == "FALSE" || atribute == "0")
                    return false;
                else
                    return true;
            }
        }

        public static int AtrToInt(string atribute)
        {
            if (String.IsNullOrEmpty(atribute))
                return 0;
            else
            {
                try
                {
                    int result = 0;
                    if (int.TryParse(atribute, out result))
                        return result;
                    else
                        return 0;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public static string GetInstallerNodeNameFromProjectType(ProjectType projectType)
        {
            // Default value
            string result = "/DeployProject/Deployable/Product";
            switch (projectType)
            {
                // This is only difference
                case ProjectType.MergeModule:
                    result = "/DeployProject/Deployable/Module";
                    break;
            }
            return result;
        }

        public static string GetFilesNodeNameFromProjectType(ProjectType projectType)
        {
            // Default value
            string result = "/DeployProject/Deployable/File";
            switch (projectType)
            {
                // This is only difference
                case ProjectType.Cab:
                    result = "/DeployProject/Deployable/SimpleFile";
                    break;
            }
            return result;
        }
    }
}
