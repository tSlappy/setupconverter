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
    public class IsleProduct: BaseProduct
    {
        public IsleProduct(XmlNode productNode)
        {
            // Parse the node and fill the data
            if (productNode != null && productNode.HasChildNodes)
            {
                mValues = new Dictionary<ProductInfo, string>(productNode.ChildNodes.Count);

                for (int i = 0; i < productNode.ChildNodes.Count; i++)
                {
                    XmlNode node = productNode.ChildNodes.Item(i);
                    if (node.Name != "row")
                        continue;

                    XmlNode td1 = node.ChildNodes.Item(0);
                    XmlNode td2 = node.ChildNodes.Item(1);

                    for (int j = 0; j < ((int)ProductInfo._Count); j++)
                    {
                        ProductInfo info = (ProductInfo)j;
                        string name = info.ToString();
                        if (td1.InnerText.CompareTo(info.ToString()) == 0)
                        {
                            string value = td2.InnerText.Trim();
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
    }
}
