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
    public class AiProduct: BaseProduct
    {
        public AiProduct(XmlNode productNode)
        {
            // Parse the node and fill the data
            if (productNode != null && productNode.HasChildNodes)
            {
                mValues = new Dictionary<ProductInfo, string>(productNode.ChildNodes.Count);

                for (int i = 0; i < productNode.ChildNodes.Count; i++)
                {
                    XmlNode node = productNode.ChildNodes.Item(i);
                    if (node.Name != "ROW")
                        continue;

                    try
                    {
                        string property = node.Attributes.GetNamedItem("Property").Value.Trim();
                        string value = node.Attributes.GetNamedItem("Value").Value.Trim();

                        for (int j = 0; j < ((int)ProductInfo._Count); j++)
                        {
                            ProductInfo info = (ProductInfo)j;
                            string name = info.ToString();
                            if (property.CompareTo(info.ToString()) == 0)
                            {
                                if (info == ProductInfo.ProductCode)
                                {
                                    // Remove 1033: from GUID
                                    value = value.Substring(5);
                                  //  if (value[0] == '{')
                                  //      value = "{" + value; //
                                }

                                mValues.Add(info, value);
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {                   
                    }
                }
                IsError = false;
            }
            else
                IsError = true;
        }
    }
}
