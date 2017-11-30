//////////////////////////////////////////////////////////////////////////
// unSigned's Setup Projects Converter                                  //
// Copyright (c) 2016 - 2018 Slappy & unSigned, s. r. o.                //
// http://www.unsignedsw.com, https://github.com/tSlappy/setupconverter //
// All Rights Reserved.                                                 //
//////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Generic;
using SetupProjectConverterGUI;
using SetupProjectConverter;

namespace SetupProjectConverter
{
    public class IsleProject: BaseProject
    {
        private const string Indent = " ";

        // Open .isproj file and parse it internally into .xml
        public IsleProject(ref AsynchronousDialog progressDialog, string isleProjFilePath)
        {
            mProgressDialog = progressDialog;
            mProjectFile = isleProjFilePath;
            mInputProject = InputProject.IsleProject;

            ProgressMessage("Loading project '" + isleProjFilePath + "'...");

            ParseIslFile();
        }

        private void ProgressMessage(string message)
        {
            try
            {
            	mProgressDialog.UpdateMessage(message, false);
            }
            catch (Exception)
            {
            	
            }
        }

        private void ParseIslFile()
        {
            // Now simply format the XML Document and load it again
            mXmlData = FormatXMLData(LoadDataFromXML(mProjectFile));
            SaveDataToXML(mXmlData, mProjectFile + ".xml");
            XmlDocument document = new XmlDocument();
            try
            {
                // Load the XmlDocument with the xmlData
                ProgressMessage("Converting project '" + mProjectFile + "' to XML...");
                document.LoadXml(mXmlData);    
    
                // 0. Get Project details
                ProgressMessage("Getting project details...");
                XmlNode propertyNode = GetTable(ref document, "Property");
                ProjectName = TableValue(ref propertyNode, "ProductName");
                ProjectType = SetupProjectConverter.ProjectType.Setup; //!VdProduct.GetProjectTypeFromGUID(TableValue(ref propertyNode, "ProjectType"), VdProduct.AtrToBool(TableValue(ref propertyNode, "IsWebType")));
                XmlNode releaseNode = GetTable(ref document, "ISRelease");
                Output = TableValue(ref releaseNode, "SingleImage", 2);
                /*if (String.IsNullOrEmpty(Output))
                {
                    releaseNode = document.SelectSingleNode("/DeployProject/Configurations/Debug");
                    Output = TableValue(ref releaseNode, "OutputFilename");
                }*/
                               
                // 1. Get list of <Folder>
                ProgressMessage("Creating folders...");
                XmlNode documentComponentyNode = GetTable(ref document, "Component");
                if (documentComponentyNode != null && documentComponentyNode.HasChildNodes)
                {
                    XmlNodeList foldersList = documentComponentyNode.ChildNodes;
                    mFolders = new List<BaseFolder>(foldersList.Count);

                    for (int i = 0; i < foldersList.Count; i++)
                    {
                        if (foldersList.Item(i).Name != "row")
                            continue;

                        IsleFolder folder = new IsleFolder(foldersList.Item(i), ref mFolders, null);
                        if (!String.IsNullOrEmpty(folder.Id))
                        {
                            mFolders.Add(folder);
                            // Is this [TARGETDIR] -> remember it
                            if (folder.Path == "[TARGETDIR]")
                            {
                                XmlNode defaultDirNode = foldersList.Item(i).SelectSingleNode("DefaultLocation");
                                //!DefaultDir = TdbleValue (ref defaultDirNode, "value");
                            }
                        }
                    }
                }

                /*XmlNode documentDirectoryNode = GetTable(ref document, "Directory");
                if (documentDirectoryNode != null && documentDirectoryNode.HasChildNodes)
                {
                    XmlNodeList foldersList = documentDirectoryNode.ChildNodes;
                    mFolders = new List<BaseFolder>(foldersList.Count);

                    for (int i = 0; i < foldersList.Count; i++)
                    {
                        if (foldersList.Item(i).Name != "row")
                            continue;

                        IsleFolder folder = new IsleFolder(foldersList.Item(i), ref mFolders, null);
                        if (!String.IsNullOrEmpty(folder.Id))
                        {
                            mFolders.Add(folder);
                            // Is this [TARGETDIR] -> remember it
                            if (folder.Path == "[TARGETDIR]")
                            {
                                XmlNode defaultDirNode = foldersList.Item(i).SelectSingleNode("DefaultLocation");
                                //!DefaultDir = TdbleValue (ref defaultDirNode, "value");
                            }
                        }
                    }
                }*/

                // 2. Get list of <File>
                ProgressMessage("Creating files...");
                XmlNode documentFileNode = GetTable(ref document, "File");
                if (documentFileNode != null && documentFileNode.HasChildNodes)
                {
                    XmlNodeList filesList = documentFileNode.ChildNodes;
                    mFiles = new List<BaseFile>(filesList.Count);

                    for (int i = 0; i < filesList.Count; i++)
                    {
                        if (filesList.Item(i).Name != "row")
                            continue;

                        IsleFile file = new IsleFile(filesList.Item(i));
                        if (!String.IsNullOrEmpty(file.Id))
                            mFiles.Add(file);
                    }
                }

                // 3. Get list of <Registry>     
                ProgressMessage("Creating registry keys...");
                XmlNode documentRegistryNode = GetTable(ref document, "Registry");
                if (documentRegistryNode != null && documentRegistryNode.HasChildNodes)
                {
                    XmlNodeList regKeyList = documentRegistryNode.ChildNodes;
                    mRegistryKeys = new List<BaseRegKey>(regKeyList.Count);
                    for (int i = 0; i < regKeyList.Count; i++)
                    {
                        if (regKeyList.Item(i).Name != "row")
                            continue;

                        IsleRegKey regKey = new IsleRegKey(ref mRegistryKeys, regKeyList.Item(i));                                                 
                    }
                }
                
                // 4. Get Installer details - different for each ProductType!
                ProgressMessage("Reading product details...");
                XmlNode documentProductNode = GetTable(ref document, "Property");
                mProduct = new IsleProduct(documentProductNode);

                // Hard Coded values
                ProductInfo prInfo = ProductInfo.VSSolutionFolder;
                FileInfo fileInfo = new FileInfo(mProjectFile);
                mProduct.mValues.Add(prInfo, fileInfo.Directory.Parent.FullName);
                DefaultDir = string.Format("[ProgramFilesFolder]\\{0}\\", mProduct.GetValue(ProductInfo.ProductName));
                prInfo = ProductInfo.ISProjectDataFolder;
                fileInfo = new FileInfo(mProjectFile);
                mProduct.mValues.Add(prInfo, fileInfo.Directory.FullName);
                Output = string.Format("{0}\\{1}.exe", mProduct.GetValue(ProductInfo.ISProjectDataFolder), mProduct.GetValue(ProductInfo.ProductName));

                // 5. Get list of <Shortcut>
                ProgressMessage("Creating shortcuts...");
                XmlNode documentShortcutNode = GetTable(ref document, "Shortcut");
                if (documentShortcutNode != null && documentShortcutNode.HasChildNodes)
                {
                    XmlNodeList shortcutsList = documentShortcutNode.ChildNodes;
                    mShortcuts = new List<BaseShortcut>(shortcutsList.Count);

                    for (int i = 0; i < shortcutsList.Count; i++)
                    {
                        if (shortcutsList.Item(i).Name != "row")
                            continue;

                        IsleShortcut shortcut = new IsleShortcut(shortcutsList.Item(i));
                        if (!String.IsNullOrEmpty(shortcut.Id))
                            mShortcuts.Add(shortcut);
                    }
                }

                // 6. Get list of OtherType (e.g. ProjectOutput, ...)
         /*       ProgressMessage("Reading other files...");
                XmlNode projectOutputNode = document.SelectSingleNode("/DeployProject/Deployable/ProjectOutput");
                if (projectOutputNode != null && projectOutputNode.HasChildNodes)
                {
                    XmlNodeList projectOutputList = projectOutputNode.ChildNodes;
                    mOtherTypeObjects = new List<BaseOtherType>(projectOutputList.Count);

                    for (int i = 0; i < projectOutputList.Count; i++)
                    {
                        VdOtherType projectOutput = new VdOtherType(projectOutputList.Item(i), OtherType.ProjectOutput);
                        if (!String.IsNullOrEmpty(projectOutput.Id))
                            mOtherTypeObjects.Add(projectOutput);
                    }
                }*/
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private XmlNode GetTable(ref XmlDocument document, string name)
        {
            XmlNodeList tables = document.SelectNodes("msi/table");
            for (int i = 0; i < tables.Count; i++)
            {
                XmlNode node = tables.Item(i);
                for (int k = 0; k < node.Attributes.Count; k++)
                {
                    XmlNode attribute = node.Attributes.Item(k);
                    if (attribute.Name == "name" && attribute.Value == name)
                        return node;
                }
            }

            return null;
        }

        private string GetXmlElementSafeName(string elementName)
        {
            // There are some exceptions in element name
            // BootstrapperCfg: "BootstrapperCfg:{63ACBE69-63AA-4F98-B2B6-99F9E24495F2}"
            int iPos = elementName.IndexOf(":{");
            if(iPos > -1)
            {
                string name = elementName.Substring(0, iPos);
                return XmlConvert.EncodeLocalName(name);
            }

            // Files: "{1FB2D0AE-D3B9-43D4-B9DD-F88EC61E35DE}:_1B25A3652CF8407893BBA5926E1EDD15"
            iPos = elementName.IndexOf("}:");
            if (iPos > -1)
            {
                string name = elementName.Substring(iPos + 2);
                return XmlConvert.EncodeLocalName(name);
            }
            
            return XmlConvert.EncodeLocalName(elementName);
        }

        private string TableValue(ref XmlNode tableNode, string name, int index = 1)
        {
            try
            {
                for (int i = 0; i < tableNode.ChildNodes.Count; i++)
                {
                    XmlNode rowNode = tableNode.ChildNodes.Item(i);
                    if (rowNode.Name != "row")
                        continue;

                    for (int k = 0; k < rowNode.ChildNodes.Count; k++)
                    {
                        XmlNode tdNode = rowNode.ChildNodes.Item(k);
                        if (!string.IsNullOrEmpty(tdNode.InnerText) && tdNode.InnerText == name)
                        {
                            // Get Value from <td>
                            tdNode = rowNode.ChildNodes.Item(index);
                            return tdNode.InnerText.Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return string.Empty;
        }

        private void WriteElement(XmlReader reader, TextWriter writer)
        {
            string elementName = Escape(XmlConvert.DecodeName(reader.LocalName));
            writer.Write("\"{0}\"", elementName);
            string value = reader.GetAttribute("value");

            if (value != null)
            {
                writer.Write(" = \"{0}:{1}\"", reader.GetAttribute("valueType"), Escape(value));
            }
            writer.WriteLine();
        }

        private void WriteIndent(TextWriter writer, int level)
        {
            for (int i = 0; i < level; ++i)
            {
                writer.Write(Indent);
            }
        }

        private string Escape(string value)
        {
            return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        private string Unescape(string value)
        {
            return value.Replace("\\\"", "\"").Replace("\\\\", "\\");
        }

        #region XML Data manipulation
        public String FormatXMLData(String xmlData)
        {
            String Result = "";

            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the xmlData.
                document.LoadXml(xmlData);

                writer.Formatting = Formatting.Indented;

                // Write the xmlData into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                stream.Flush();

                // Have to rewind the MemoryStream in order to read its contents.
                stream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(stream);

                // Extract the text from the StreamReader.
                String FormattedXML = sReader.ReadToEnd();

                Result = FormattedXML;
            }
            catch (XmlException)
            {
            }

            stream.Close();
            writer.Close();

            return Result;
        }

        public string LoadDataFromXML(string xmlFilePath)
        {
            return System.IO.File.ReadAllText(xmlFilePath);
        }

        public void SaveDataToXML(string xmlData, string xmlFilePath)
        {
            string[] xmlFile = new string[1];
            xmlFile[0] = xmlData;
            System.IO.File.WriteAllLines(xmlFilePath, xmlFile);
        }
        #endregion
    }
}
