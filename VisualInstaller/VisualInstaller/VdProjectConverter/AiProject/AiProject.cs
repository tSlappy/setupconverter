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
    public class AiProject: BaseProject
    {
        private const string Indent = " ";

        // Open .aip file and parse it internally into .xml
        public AiProject(ref AsynchronousDialog progressDialog, string aipFilePath)
        {
            mProgressDialog = progressDialog;
            mProjectFile = aipFilePath;
            mInputProject = InputProject.IsleProject;

            ProgressMessage("Loading project '" + aipFilePath + "'...");

            ParseAipFile();
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

        private void ParseAipFile()
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
                XmlNode componentNode = GetComponent(ref document, "caphyon.advinst.msicomp.MsiPropsComponent");
                ProjectName = ComponentValue(ref componentNode, "ProductName");
                ProjectType = SetupProjectConverter.ProjectType.Setup; //!VdProduct.GetProjectTypeFromGUID(TableValue(ref propertyNode, "ProjectType"), VdProduct.AtrToBool(TableValue(ref propertyNode, "IsWebType")));
                /*XmlNode releaseNode = GetComponent(ref document, "ISRelease");
                Output = ComponentValue(ref releaseNode, "SingleImage", 2);
                /*if (String.IsNullOrEmpty(Output))
                {
                    releaseNode = document.SelectSingleNode("/DeployProject/Configurations/Debug");
                    Output = TableValue(ref releaseNode, "OutputFilename");
                }*/
                               
                // 1. Get list of <Folder>
                ProgressMessage("Creating folders...");
                XmlNode documentComponentyNode = GetComponent(ref document, "caphyon.advinst.msicomp.MsiDirsComponent");
                if (documentComponentyNode != null && documentComponentyNode.HasChildNodes)
                {
                    XmlNodeList foldersList = documentComponentyNode.ChildNodes;
                    mFolders = new List<BaseFolder>(foldersList.Count);

                    for (int i = 0; i < foldersList.Count; i++)
                    {
                        if (foldersList.Item(i).Name != "ROW")
                            continue;

                        AiFolder folder = new AiFolder(foldersList.Item(i), ref mFolders, null);
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

                // Update Folder paths - add all subdirectories
                foreach (AiFolder folder in mFolders)
                {
                    if (folder.IsRoot)
                        continue;

                    string parentPath = folder.Path;
                    AiFolder parentFolder = GetFolderById(folder.Parent);

                    while (parentFolder != null)
                    {
                        parentPath = parentFolder.Path + "\\" + parentPath;
                        parentFolder = GetFolderById(parentFolder.Parent);
                    }

                    folder.Path = parentPath;
                }

                // Build list of Components (mapping File -> Folder)
                Dictionary<string, string> mComponents = new Dictionary<string, string>();
                ProgressMessage("Creating components...");
                XmlNode componentFileNode = GetComponent(ref document, "caphyon.advinst.msicomp.MsiCompsComponent");
                if (componentFileNode != null && componentFileNode.HasChildNodes)
                {
                    XmlNodeList componentsList = componentFileNode.ChildNodes;

                    for (int i = 0; i < componentsList.Count; i++)
                    {
                        if (componentsList.Item(i).Name != "ROW")
                            continue;

                        string componentName = componentsList.Item(i).Attributes.GetNamedItem("Component").Value.Trim();
                        string directory = componentsList.Item(i).Attributes.GetNamedItem("Directory_").Value.Trim();
                        mComponents.Add(componentName, directory);
                    }
                }

                // 2. Get list of <File>
                ProgressMessage("Creating files...");
                XmlNode documentFileNode = GetComponent(ref document, "caphyon.advinst.msicomp.MsiFilesComponent");
                if (documentFileNode != null && documentFileNode.HasChildNodes)
                {
                    XmlNodeList filesList = documentFileNode.ChildNodes;
                    mFiles = new List<BaseFile>(filesList.Count);

                    for (int i = 0; i < filesList.Count; i++)
                    {
                        if (filesList.Item(i).Name != "ROW")
                            continue;

                        AiFile file = new AiFile(filesList.Item(i), ref mComponents);
                        if (!String.IsNullOrEmpty(file.Id))
                            mFiles.Add(file);
                    }
                }

                // 3. Get list of <Registry>     
                ProgressMessage("Creating registry keys...");
                XmlNode documentRegistryNode = GetComponent(ref document, "caphyon.advinst.msicomp.MsiRegsComponent");
                if (documentRegistryNode != null && documentRegistryNode.HasChildNodes)
                {
                    XmlNodeList regKeyList = documentRegistryNode.ChildNodes;
                    mRegistryKeys = new List<BaseRegKey>(regKeyList.Count);
                    for (int i = 0; i < regKeyList.Count; i++)
                    {
                        if (regKeyList.Item(i).Name != "ROW")
                            continue;

                        AiRegKey regKey = new AiRegKey(ref mRegistryKeys, regKeyList.Item(i));                                                 
                    }
                }
                
                // 4. Get Installer details - different for each ProductType!
                ProgressMessage("Reading product details...");
                XmlNode documentProductNode = GetComponent(ref document, "caphyon.advinst.msicomp.MsiPropsComponent");
                mProduct = new AiProduct(documentProductNode);

                // Hard Coded values
                DefaultDir = string.Format("[ProgramFilesFolder]\\{0}\\", mProduct.GetValue(ProductInfo.ProductName));
                Output = string.Format("{0}\\{1}.exe", new FileInfo(mProjectFile).DirectoryName, mProduct.GetValue(ProductInfo.ProductName));

                // 5. Get list of <Shortcut>
                ProgressMessage("Creating shortcuts...");
                XmlNode documentShortcutNode = GetComponent(ref document, "caphyon.advinst.msicomp.MsiShortsComponent");
                if (documentShortcutNode != null && documentShortcutNode.HasChildNodes)
                {
                    XmlNodeList shortcutsList = documentShortcutNode.ChildNodes;
                    mShortcuts = new List<BaseShortcut>(shortcutsList.Count);

                    for (int i = 0; i < shortcutsList.Count; i++)
                    {
                        if (shortcutsList.Item(i).Name != "ROW")
                            continue;

                        AiShortcut shortcut = new AiShortcut(shortcutsList.Item(i), ref mFiles);
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

        private AiFolder GetFolderById(string id)
        {
            foreach (AiFolder folder in mFolders)
            {
                if (folder.Id == id)
                    return folder;
            }

            return null;
        }

        private XmlNode GetComponent(ref XmlDocument document, string name)
        {
            XmlNodeList components = document.SelectNodes("DOCUMENT/COMPONENT");
            for (int i = 0; i < components.Count; i++)
            {
                XmlNode node = components.Item(i);
                for (int k = 0; k < node.Attributes.Count; k++)
                {
                    XmlNode attribute = node.Attributes.Item(k);
                    if (attribute.Name == "cid" && attribute.Value == name)
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

        private string ComponentValue(ref XmlNode componentNode, string name, int index = 1)
        {
            try
            {
                for (int i = 0; i < componentNode.ChildNodes.Count; i++)
                {
                    for (int k = 0; k < componentNode.ChildNodes.Count; k++)
                    {
                        XmlNode rowNode = componentNode.ChildNodes.Item(k);
                        if (rowNode.Name == "ROW")
                        {
                            // Get Value from Property
                            for (int j = 0; j < rowNode.Attributes.Count; j++)
                            {
                                XmlNode attribute = rowNode.Attributes.Item(j);
                                if (attribute.Name == "Property" && attribute.Value == name)
                                {
                                    attribute = rowNode.Attributes.Item(j + 1);
                                    if (attribute.Name == "Value")
                                    {
                                        return attribute.Value.Trim();
                                    }
                                }
                            }
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
