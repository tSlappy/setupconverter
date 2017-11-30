//////////////////////////////////////////////////////////////////////////
// unSigned's Setup Projects Converter                                  //
// Copyright (c) 2016 - 2018 Slappy & unSigned, s. r. o.                //
// http://www.unsignedsw.com, https://github.com/tSlappy/setupconverter //
// All Rights Reserved.                                                 //
//////////////////////////////////////////////////////////////////////////

// .vdproj [constants]:
// http://msdn.microsoft.com/en-us/library/windows/desktop/aa370905%28v=vs.85%29.aspx#system_folder_properties

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Generic;

using SetupProjectConverterGUI;

namespace SetupProjectConverter
{
    public class VdProject: BaseProject
    {
        /// <summary>
        /// Matches "key" [= "8:value"] with support for backslash escapes.
        /// </summary>
        private static readonly Regex ElementRegex = new Regex(@"^""([^""\\]*(?:\\.[^""\\]*)*)""(?:\s*=\s*""(\d+):([^""\\]*(?:\\.[^""\\]*)*)"")?$", RegexOptions.Singleline);
        private const string Indent = " ";

        // Open .vdproj file and parse it internally into .xml
        public VdProject(ref AsynchronousDialog progressDialog, string vdProjFilePath)
        {
            mProgressDialog = progressDialog;
            mProjectFile = vdProjFilePath;
            mInputProject = InputProject.VdProject;

            ProgressMessage("Loading project '" + vdProjFilePath + "'...");
            using (StreamReader reader = System.IO.File.OpenText(mProjectFile))
            using (XmlWriter writer = XmlWriter.Create(mProjectFile + ".xml"))
            {
                string line;
                bool prevLineElement = false;
                string prevLine = null;

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    mLineCount++;
                    Match match = ElementRegex.Match(line);

                    if (match.Success)
                    {
                        if (prevLineElement)
                            writer.WriteEndElement();

                        string elementName = Unescape(match.Groups[1].Value);
                        string fixedElementName = GetXmlElementSafeName(elementName);

                        writer.WriteStartElement(fixedElementName);

                        if (match.Groups[2].Success)
                        {
                            //writer.WriteAttributeString("valueType", match.Groups[2].Value);
                            string value = Unescape(match.Groups[3].Value);
                            writer.WriteAttributeString("value", value);
                        }
                        prevLineElement = true;
                    }
                    else
                    {
                        if (prevLineElement && line != "{")
                            writer.WriteEndElement();

                        prevLineElement = false;

                        if (line == "}")
                        {
                            if (prevLine == "{")
                                writer.WriteWhitespace(" ");
                            writer.WriteEndElement();
                        }
                    }
                    prevLine = line;
                }
            }

            CreateXmlDataAndParseTree();
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

        private void CreateXmlDataAndParseTree()
        {
            // Now simply format the XML Document and load it again
            mXmlData = FormatXMLData(LoadDataFromXML(mProjectFile + ".xml"));
            SaveDataToXML(mXmlData, mProjectFile + ".xml");
            XmlDocument document = new XmlDocument();
            try
            {
                // Load the XmlDocument with the xmlData
                ProgressMessage("Converting project '" + mProjectFile + "' to XML...");
                document.LoadXml(mXmlData);     
    
                // 0. Get Project details
                ProgressMessage("Getting project details...");
                XmlNode projectNode = document.SelectSingleNode("/DeployProject");
                ProjectName = ElementValue(ref projectNode, "ProjectName");
                ProjectType = VdProduct.GetProjectTypeFromGUID(ElementValue(ref projectNode, "ProjectType"), VdProduct.AtrToBool(ElementValue(ref projectNode, "IsWebType")));
                XmlNode releaseNode = document.SelectSingleNode("/DeployProject/Configurations/Release");
                Output = ElementValue(ref releaseNode, "OutputFilename");
                if (String.IsNullOrEmpty(Output))
                {
                    releaseNode = document.SelectSingleNode("/DeployProject/Configurations/Debug");
                    Output = ElementValue(ref releaseNode, "OutputFilename");
                }
                               
                // 1. Get list of <Folder>
                ProgressMessage("Creating folders...");
                XmlNode documentFolderNode = document.SelectSingleNode("/DeployProject/Deployable/Folder");
                if (documentFolderNode != null && documentFolderNode.HasChildNodes)
                {
                    XmlNodeList foldersList = documentFolderNode.ChildNodes;
                    mFolders = new List<BaseFolder>(foldersList.Count);

                    for (int i = 0; i < foldersList.Count; i++)
                    {
                        VdFolder folder = new VdFolder(foldersList.Item(i), ref mFolders, null);
                        if (!String.IsNullOrEmpty(folder.Id))
                        {
                            mFolders.Add(folder);
                            // Is this [TARGETDIR] -> remember it
                            if (folder.Path == "[TARGETDIR]")
                            {
                                XmlNode defaultDirNode = foldersList.Item(i).SelectSingleNode("DefaultLocation");
                                DefaultDir = folder.Atribute(ref defaultDirNode, "value");
                            }
                        }
                    }
                }

                // 2. Get list of <File>
                ProgressMessage("Creating files...");
                XmlNode documentFileNode = document.SelectSingleNode(VdProduct.GetFilesNodeNameFromProjectType(ProjectType));
                if (documentFileNode != null && documentFileNode.HasChildNodes)
                {
                    XmlNodeList filesList = documentFileNode.ChildNodes;
                    mFiles = new List<BaseFile>(filesList.Count);

                    for (int i = 0; i < filesList.Count; i++)
                    {
                        VdFile file = new VdFile(filesList.Item(i));
                        if (!String.IsNullOrEmpty(file.Id))
                            mFiles.Add(file);
                    }
                }

                // 3. Get list of <Registry>     
                ProgressMessage("Creating registry keys...");           
                XmlNode documentRegistryNode = document.SelectSingleNode("/DeployProject/Deployable/Registry");
                if (documentRegistryNode != null && documentRegistryNode.HasChildNodes)
                {
                    XmlNodeList regKeyList = documentRegistryNode.ChildNodes;
                    mRegistryKeys = new List<BaseRegKey>(regKeyList.Count);
                    for (int i = 0; i < regKeyList.Count; i++)
                    {
                        try
                        {
                            var root = (RegRoot)Enum.Parse(typeof(RegRoot), regKeyList.Item(i).Name);
                            XmlNode keysNode = regKeyList.Item(i).SelectSingleNode("Keys");
                            for (int j = 0; j < keysNode.ChildNodes.Count; j++)
                            {
                                VdRegKey regKey = new VdRegKey(ref mRegistryKeys, root, string.Empty, keysNode.ChildNodes[j]);
                            }                            
                        }
                        catch (Exception)
                        {

                        }                      
                    }
                }
                
                // 4. Get Installer details - different for each ProductType!
                ProgressMessage("Reading product details...");
                XmlNode documentProductNode = document.SelectSingleNode(VdProduct.GetInstallerNodeNameFromProjectType(ProjectType));
                mProduct = new VdProduct(documentProductNode);

                // 5. Get list of <Shortcut>
                ProgressMessage("Creating shortcuts...");
                XmlNode documentShortcutNode = document.SelectSingleNode("/DeployProject/Deployable/Shortcut");
                if (documentShortcutNode != null && documentShortcutNode.HasChildNodes)
                {
                    XmlNodeList shortcutsList = documentShortcutNode.ChildNodes;
                    mShortcuts = new List<BaseShortcut>(shortcutsList.Count);

                    for (int i = 0; i < shortcutsList.Count; i++)
                    {                        
                        VdShortcut shortcut = new VdShortcut(shortcutsList.Item(i));
                        if (!String.IsNullOrEmpty(shortcut.Id))
                            mShortcuts.Add(shortcut);
                    }
                }

                // 6. Get list of OtherType (e.g. ProjectOutput, ...)
                ProgressMessage("Reading other files...");
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
                }
            }
            catch (Exception)
            {
            }
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
