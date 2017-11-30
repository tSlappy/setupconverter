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
    public enum ProjectType
    {
        Unknown = 0,
        MergeModule, // {5443560e-dbb4-11d2-8724-00a0c9a8b90c}
        Setup,
        WebSetup,
        Cab
    }

    public enum ProductInfo
    {
        //Name, // "Microsoft Visual Studio"
        ProductName = 0, // "CodeSweep"
        ProductCode, // "{02A5D70C-1619-434C-B893-596693727264}"
        //PackageCode, // "{517E3E61-8044-4F0C-BC55-062733CB57D1}"
        //UpgradeCode, // "{3DCFF3CD-272D-4204-ADBF-A506AB0B8C8D}"
        //RestartWWWService, // "FALSE"
        //RemovePreviousVersions, // "FALSE"
        //DetectNewerInstalledVersion, // "TRUE"
        //InstallAllUsers, // "FALSE"
        ProductVersion, // "1.0.0"
        Manufacturer, // "Microsoft"
        //ARPHELPTELEPHONE, // ""
        //ARPHELPLINK, // ""
        Title, // "CodeSweep"
        //Subject, // ""
        //ARPCONTACT, // "Josh Stevens"
        //Keywords, // ""
        //ARPCOMMENTS, // ""
        //ARPURLINFOABOUT, // ""
        //ARPPRODUCTICON, // ""
        //ARPIconIndex, // "0"
        //SearchPath, // ""
        //UseSystemSearchPath, // "TRUE"
        //TargetPlatform, // "0"
        //PreBuildEvent, // ""
        //PostBuildEvent, // ""
        //RunPostBuildEvent, // "0"

        // ISLE only
        VSSolutionFolder,
        ISProjectDataFolder,

        /// <summary>
        /// This is always LAST !
        /// It represents number of items in enum + 1
        /// </summary>
        _Count
    }    

    public class BaseProduct
    {
        public bool IsError = true;    // Error flag, true on any error or missing data
        public Dictionary<ProductInfo, string> mValues = null;

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
    }
}
