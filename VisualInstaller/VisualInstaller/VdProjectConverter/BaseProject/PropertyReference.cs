//////////////////////////////////////////////////////////////////////////
// unSigned's Setup Projects Converter                                  //
// Copyright (c) 2016 - 2018 Slappy & unSigned, s. r. o.                //
// http://www.unsignedsw.com, https://github.com/tSlappy/setupconverter //
// All Rights Reserved.                                                 //
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

// List of supported Properties: http://msdn.microsoft.com/en-us/library/windows/desktop/aa370905%28v=vs.85%29.aspx
namespace SetupProjectConverter
{
    // The following list provides links to more information about system folders that the installer sets at setup.
    public enum SystemFolderProperties
    {
        AdminToolsFolder = 0,   // The full path to the directory that contains administrative tools.
        AppDataFolder,          // The full path to the Roaming folder for the current user.
        CommonAppDataFolder,    // The full path to application data for all users.
        CommonFiles64Folder,    // The full path to the predefined 64-bit Common Files folder.
        CommonFilesFolder,      // The full path to the Common Files folder for the current user.
        DesktopFolder,          // The full path to the Desktop folder.
        FavoritesFolder,        // The full path to the Favorites folder for the current user.
        FontsFolder,            // The full path to the Fonts folder.
        LocalAppDataFolder,     // The full path to the folder that contains local (nonroaming) applications.
        MyPicturesFolder,       // The full path to the Pictures folder.
        NetHoodFolder,          // The full path to the NetHood folder.
        PersonalFolder,         // The full path to the Documents folder for the current user.
        PrintHoodFolder,        // The full path to the PrintHood folder.
        ProgramFiles64Folder,   // The full path to the predefined 64-bit Program Files folder.
        ProgramFilesFolder,     // The full path to the predefined 32-bit Program Files folder.
        ProgramMenuFolder,      // The full path to the Program Menu folder.
        RecentFolder,           // The full path to the Recent folder.
        SendToFolder,           // The full path to the SendTo folder for the current user.
        StartMenuFolder,        // The full path to the Start menu folder.
        StartupFolder,          // The full path to the Startup folder.
        System16Folder,         // The full path to folder for 16-bit system DLLs.
        System64Folder,         // The full path to the predefined System64 folder.
        SystemFolder,           // The full path to the System folder for the current user.
        TempFolder,             // The full path to the Temp folder.
        TemplateFolder,         // The full path to the Template folder for the current user.
        WindowsFolder,          // The full path to the Windows folder.
        WindowsVolume,          // The volume of the Windows folder.
        GAC,                    // Global Assembly Cache folder
        _Count
    }

    // The following list provides links to more information about product-specific properties specified in the Property Table.
    public enum ProductInformationProperties
    {
        ARPHELPLINK = 0,        // Internet address or URL for technical support.
        ARPHELPTELEPHONE,       // Technical support phone numbers.
        DiskPrompt,             // String displayed by a message box that prompts for a disk.
        IsAdminPackage,         // Set to 1 (one) if the current installation is running from a package created through an administrative installation.
        LeftUnit,               // Places units to the left of the number.
        Manufacturer,           // Name of the application manufacturer. (Required)
        MediaSourceDir,        // The installer sets this property to 1 (one) when the installation uses a media source, such as a CD-ROM.
        MSIINSTANCEGUID,       // The presence of this property indicates that a product code changing transform is registered to the product.
        MSINEWINSTANCE,         // This property indicates the installation of a new instance of a product with instance transforms.
        ParentProductCode,      // The installer sets this property for installations that a Concurrent Installation action runs.
        PIDTemplate,            // String used as a template for the PIDKEY property.
        ProductCode,            // A unique identifier for a specific product release. (Required)
        ProductName,            // Human readable name of an application. (Required)
        ProductState,           // Set to the installed state of a product.
        ProductVersion,         // String format of the product version as a numeric value. (Required)
        UpgradeCode,            // A GUID that represents a related set of products.
        _Count
    }

    // The following list provides links to more information about status properties that are updated by the installer during installation.
    public enum InstallationStatusProperties
    {
        AFTERREBOOT = 0,        // Indicates current installation follows a reboot that the ForceReboot action invokes.
        CostingComplete,        // Indicates whether disk space costing is complete.
        Installed,              // Indicates that a product is already installed.
        MSICHECKCRCS,           // The Installer does a CRC on files only if the MSICHECKCRCS property is set.
        MsiRestartManagerSessionKey, // The Installer sets this property to the session key for the Restart Manager session.
        MsiRunningElevated,     // The Installer sets the value of this property to 1 when the installer is running with elevated privileges.
        MsiSystemRebootPending, // The Installer sets this property to 1 if a restart of the operating system is currently pending.
        MsiUIHideCancel,        // The Installer sets MsiUIHideCancel to 1 when the internal install level includes INSTALLUILEVEL_HIDECANCEL.
        MsiUIProgressOnly,      // The Installer sets MsiUIProgressOnly to 1 when the internal install level includes INSTALLUILEVEL_PROGRESSONLY.
        MsiUISourceResOnly,     // MsiUISourceResOnly to 1 (one) when the internal install level includes INSTALLUILEVEL_SOURCERESONLY.
        NOCOMPANYNAME,          // Suppresses the automatic setting of the COMPANYNAME property.
        NOUSERNAME,             // Suppresses the automatic setting of the USERNAME property.
        OutOfDiskSpace,         // Insufficient disk space to accommodate the installation.
        OutOfNoRbDiskSpace,     // Insufficient disk space with rollback turned off.
        Preselected,            // Features are already selected.
        PrimaryVolumePath,      // The Installer sets the value of this property to the path of the volume that the PRIMARYFOLDER property designates.
        PrimaryVolumeSpaceAvailable, // The Installer sets the value of this property to a string that represents the total number of bytes available on the volume that the PrimaryVolumePath property references.
        PrimaryVolumeSpaceRemaining, // The Installer sets the value of this property to a string that represents the total number of bytes remaining on the volume that the PrimaryVolumePath property references if all the currently selected features are installed.
        PrimaryVolumeSpaceRequired,  // The Installer sets the value of this property to a string that represents the total number of bytes required by all currently selected features on the volume that the PrimaryVolumePath property references.
        ProductLanguage,        // Numeric language identifier (LANGID) for the database. (REQUIRED)
        ReplacedInUseFiles,     // Set if the installer installs over a file that is being held in use.
        RESUME,                 // Resumed installation.
        RollbackDisabled,       // The installer sets this property when rollback is disabled.
        UILevel,                // Indicates the user interface level.
        UpdateStarted,          // Set when changes to the system have begun for this installation.
        UPGRADINGPRODUCTCODE,   // Set by the installer when an upgrade removes an application.
        VersionMsi,             // The installer sets this property to the version of Windows Installer that is run during the installation.
        _Count
    }

    // The following list provides links to more information about the component location properties.
    enum ComponentLocationProperties
    {
        OriginalDatabase = 0,           // The installer sets this property to the launched-from database, the database on the source, or the cached database.
        ParentOriginalDatabase,         // The installer sets this property for installations run by a Concurrent Installation action.
        SourceDir,                      // Root directory that contains the source files.
        TARGETDIR,                      // Specifies the root destination directory for the installation. During an administrative installation this property is the location to copy the installation package.
        INSTALLDIR,                     // Note: This is the same TARGETDIR but works for ISLE
        _Count
    }

    public class PropertyReference
    {
        static string NotSupportedInnoSetup = "[{0}]";
        static string NotSupportedNSIS      = "[{0}]";
        public static string CheckFolderProperty(string property)
        {
            // Is property a MS variable?
            string correctedProperty = null;
            string cleanProperty = property.Replace('[', ' ');
            cleanProperty = cleanProperty.Replace(']', ' ');
            cleanProperty = cleanProperty.Trim();

            for (int i = 0; i < (int)SystemFolderProperties._Count; i++)
            {
                SystemFolderProperties systemFolder = (SystemFolderProperties)i;
                if (systemFolder.ToString() == cleanProperty)
                {
                    // Format the property
                    correctedProperty = "[" + cleanProperty + "]";
                    break;
                }
            }

            for (int i = 0; i < (int)ProductInformationProperties._Count; i++)
            {
                ProductInformationProperties productInfo = (ProductInformationProperties)i;
                if (productInfo.ToString() == cleanProperty)
                {
                    // Format the property
                    correctedProperty = "[" + cleanProperty + "]";
                    break;
                }
            }

            for (int i = 0; i < (int)InstallationStatusProperties._Count; i++)
            {
                InstallationStatusProperties installStatus = (InstallationStatusProperties)i;
                if (installStatus.ToString() == cleanProperty)
                {
                    // Format the property
                    correctedProperty = "[" + cleanProperty + "]";
                    break;
                }
            }

            for (int i = 0; i < (int)ComponentLocationProperties._Count; i++)
            {
                ComponentLocationProperties componentLoc = (ComponentLocationProperties)i;
                if (componentLoc.ToString() == cleanProperty)
                {
                    // Format the property
                    correctedProperty = "[" + cleanProperty + "]";
                    break;
                }
            }
            if (String.IsNullOrEmpty(correctedProperty))
                return property;
            else
                return correctedProperty;
        }

        public static string GetVariableEquivalent(string variable, bool isInno)
        {
            Type type = null;
            object property = null;
            string result = null;
            bool correctType = false;

            try
            {
                var whichType = (SystemFolderProperties)Enum.Parse(typeof(SystemFolderProperties), variable);                
                property = (SystemFolderProperties)whichType;
                type = property.GetType();
                correctType = true;
            }
            catch (Exception)
            {
                correctType = false;
            }

            if (!correctType)
            {
                try
                {
                    var whichType = (ProductInformationProperties)Enum.Parse(typeof(ProductInformationProperties), variable);
                    property = (ProductInformationProperties)whichType;
                    type = property.GetType();
                    correctType = true;
                }
                catch (Exception)
                {
                    correctType = false;
                }
            }

            if (!correctType)
            {
                try
                {
                    var whichType = (InstallationStatusProperties)Enum.Parse(typeof(InstallationStatusProperties), variable);
                    property = (InstallationStatusProperties)whichType;
                    type = property.GetType();
                    correctType = true;
                }
                catch (Exception)
                {
                    correctType = false;
                }
            }

            if (!correctType)
            {
                try
                {
                    var whichType = (ComponentLocationProperties)Enum.Parse(typeof(ComponentLocationProperties), variable);
                    property = (ComponentLocationProperties)whichType;
                    type = property.GetType();
                    correctType = true;
                }
                catch (Exception)
                {
                    correctType = false;
                }
            }

            // Nothing found!
            if (!correctType)
                return variable;
            
            if (type == typeof(SystemFolderProperties))
            {                
                switch ((SystemFolderProperties)property)
                {
                    case SystemFolderProperties.AdminToolsFolder: // The full path to the directory that contains administrative tools.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = "$ADMINTOOLS\\";
                        break;
                    case SystemFolderProperties.AppDataFolder:   // The full path to the Roaming folder for the current user.
                        if (isInno)
                            result = "{userappdata}\\";
                        else
                            result = "$APPDATA\\";
                        break;
                    case SystemFolderProperties.CommonAppDataFolder: // The full path to application data for all users.
                        if (isInno)
                            result = "{commonappdata}\\";
                        else
                            result = "$APPDATA\\";
                        break;
                    case SystemFolderProperties.CommonFiles64Folder: // The full path to the predefined 64-bit Common Files folder.
                        if (isInno)
                            result = "{cf64}\\";
                        else
                            result = "$COMMONFILES64\\";
                        break;
                    case SystemFolderProperties.CommonFilesFolder:   // The full path to the Common Files folder for the current user.
                        if (isInno)
                            result = "{cf32}\\";
                        else
                            result = "$COMMONFILES,\\";
                        break;
                    case SystemFolderProperties.DesktopFolder:   // The full path to the Desktop folder.
                        if (isInno)
                            result = "{userdesktop}\\";
                        else
                            result = "$DESKTOP\\";
                        break;
                    case SystemFolderProperties.FavoritesFolder: // The full path to the Favorites folder for the current user.
                        if (isInno)
                            result = "{userfavorites}\\";
                        else
                            result = "$FAVORITES\\";
                        break;
                    case SystemFolderProperties.FontsFolder:     // The full path to the Fonts folder.
                        if (isInno)
                            result = "{fonts}\\";
                        else
                            result = "$FONTS\\";
                        break;
                    case SystemFolderProperties.LocalAppDataFolder:  // The full path to the folder that contains local (nonroaming) applications.
                        if (isInno)
                            result = "{localappdata}\\";
                        else
                            result = "$LOCALAPPDATA\\";
                        break;
                    case SystemFolderProperties.MyPicturesFolder:    // The full path to the Pictures folder.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable); // !!!
                        else
                            result = "$PICTURES\\";
                        break;
                    case SystemFolderProperties.NetHoodFolder:   // The full path to the NetHood folder.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable); // !!!
                        else
                            result = "$NETHOOD\\";
                        break;
                    case SystemFolderProperties.PersonalFolder:  // The full path to the Documents folder for the current user.
                        if (isInno)
                            result = "{userdocs}\\";
                        else
                            result = "$DOCUMENTS\\";
                        break;
                    case SystemFolderProperties.PrintHoodFolder: // The full path to the PrintHood folder.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable); // !!!
                        else
                            result = "$PRINTHOOD\\";
                        break;
                    case SystemFolderProperties.ProgramFiles64Folder:   // The full path to the predefined 64-bit Program Files folder.
                        if (isInno)
                            result = "{pf64}\\";
                        else
                            result = "$PROGRAMFILES64\\";
                        break;
                    case SystemFolderProperties.ProgramFilesFolder:  // The full path to the predefined 32-bit Program Files folder.
                        if (isInno)
                            result = "{pf}\\";
                        else
                            result = "$PROGRAMFILES\\";
                        break;
                    case SystemFolderProperties.ProgramMenuFolder:   // The full path to the Program Menu folder.
                        if (isInno)
                            result = "{userprograms}\\";
                        else
                            result = "$SMPROGRAMS\\";
                        break;
                    case SystemFolderProperties.RecentFolder:    // The full path to the Recent folder.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable); // !!!
                        else
                            result = "$RECENT\\";
                        break;
                    case SystemFolderProperties.SendToFolder:    // The full path to the SendTo folder for the current user.
                        if (isInno)
                            result = "{sendto}\\";
                        else
                            result = "$SENDTO\\";
                        break;
                    case SystemFolderProperties.StartMenuFolder: // The full path to the Start menu folder.
                        if (isInno)
                            result = "{group}\\";
                        else
                            result = "$STARTMENU\\";
                        break;
                    case SystemFolderProperties.StartupFolder:   // The full path to the Startup folder.
                        if (isInno)
                            result = "{userstartup}\\";
                        else
                            result = "$SMSTARTUP\\";
                        break;
                    case SystemFolderProperties.System16Folder:  // The full path to folder for 16-bit system DLLs.
                        if (isInno)
                            result = "{sys}\\";
                        else
                            result = "$SYSDIR\\";
                        break;
                    case SystemFolderProperties.System64Folder:  // The full path to the predefined System64 folder.
                        if (isInno)
                            result = "{syswow64}\\";
                        else
                            result = "$SYSDIR\\";
                        break;
                    case SystemFolderProperties.SystemFolder:    // The full path to the System folder for the current user.
                        if (isInno)
                            result = "{sys}\\";
                        else
                            result = "$SYSDIR\\";
                        break;
                    case SystemFolderProperties.TempFolder:      // The full path to the Temp folder.
                        if (isInno)
                            result = "{tmp}\\";
                        else
                            result = "$TEMP\\";
                        break;
                    case SystemFolderProperties.TemplateFolder:  // The full path to the Template folder for the current user.
                        if (isInno)
                            result = "{usertemplates}\\";
                        else
                            result = "$TEMPLATES\\";
                        break;
                    case SystemFolderProperties.WindowsFolder:   // The full path to the Windows folder.
                        if (isInno)
                            result = "{win}\\";
                        else
                            result = "$WINDIR\\";
                        break;
                    case SystemFolderProperties.WindowsVolume:   // The volume of the Windows folder.
                        if (isInno)
                            result = "{sd}\\";
                        else
                            result = String.Format(NotSupportedNSIS, variable); // !!!
                        break;
                    case SystemFolderProperties.GAC:   // The GAC folder
                        if (isInno)
                            result = "{app}\\GAC\\";
                        else
                            result = "$INSTDIR\\GAC\\";
                        break;
                }
            }
            else if (type == typeof(ProductInformationProperties))
            {
                switch ((ProductInformationProperties)property)
                {
                    case ProductInformationProperties.ARPHELPLINK: // Internet address or URL for technical support.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.ARPHELPTELEPHONE:    // Technical support phone numbers.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.DiskPrompt:      // String displayed by a message box that prompts for a disk.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.IsAdminPackage:  // Set to 1 (one) if the current installation is running from a package created through an administrative installation.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.LeftUnit:        // Places units to the left of the number.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.Manufacturer:    // Name of the application manufacturer. (Required)
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable); // !!!
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.MediaSourceDir: // The installer sets this property to 1 (one) when the installation uses a media source, such as a CD-ROM.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.MSIINSTANCEGUID:    // The presence of this property indicates that a product code changing transform is registered to the product.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.MSINEWINSTANCE:  // This property indicates the installation of a new instance of a product with instance transforms.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.ParentProductCode:   // The installer sets this property for installations that a Concurrent Installation action runs.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.PIDTemplate:     // String used as a template for the PIDKEY property.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.ProductCode:     // A unique identifier for a specific product release. (Required)
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable); // !!!
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.ProductName:     // Human readable name of an application. (Required)
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable); // !!!
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.ProductState:    // Set to the installed state of a product.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.ProductVersion:  // String format of the product version as a numeric value. (Required)
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable); // !!!
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ProductInformationProperties.UpgradeCode:     // A GUID that represents a related set of products.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                }
            }
            else if (type == typeof(InstallationStatusProperties))
            {            
                switch ((InstallationStatusProperties)property)
                {
                    case InstallationStatusProperties.AFTERREBOOT: // Indicates current installation follows a reboot that the ForceReboot action invokes.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.CostingComplete: // Indicates whether disk space costing is complete.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.Installed:       // Indicates that a product is already installed.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.MSICHECKCRCS:    // The Installer does a CRC on files only if the MSICHECKCRCS property is set.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.MsiRestartManagerSessionKey: // The Installer sets this property to the session key for the Restart Manager session.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.MsiRunningElevated:  // The Installer sets the value of this property to 1 when the installer is running with elevated privileges.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.MsiSystemRebootPending: // The Installer sets this property to 1 if a restart of the operating system is currently pending.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.MsiUIHideCancel: // The Installer sets MsiUIHideCancel to 1 when the internal install level includes INSTALLUILEVEL_HIDECANCEL.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.MsiUIProgressOnly:   // The Installer sets MsiUIProgressOnly to 1 when the internal install level includes INSTALLUILEVEL_PROGRESSONLY.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.MsiUISourceResOnly:  // MsiUISourceResOnly to 1 (one) when the internal install level includes INSTALLUILEVEL_SOURCERESONLY.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.NOCOMPANYNAME:   // Suppresses the automatic setting of the COMPANYNAME property.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.NOUSERNAME:      // Suppresses the automatic setting of the USERNAME property.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.OutOfDiskSpace:  // Insufficient disk space to accommodate the installation.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.OutOfNoRbDiskSpace:  // Insufficient disk space with rollback turned off.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.Preselected:     // Features are already selected.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.PrimaryVolumePath:   // The Installer sets the value of this property to the path of the volume that the PRIMARYFOLDER property designates.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.PrimaryVolumeSpaceAvailable: // The Installer sets the value of this property to a string that represents the total number of bytes available on the volume that the PrimaryVolumePath property references.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.PrimaryVolumeSpaceRemaining: // The Installer sets the value of this property to a string that represents the total number of bytes remaining on the volume that the PrimaryVolumePath property references if all the currently selected features are installed.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.PrimaryVolumeSpaceRequired:  // The Installer sets the value of this property to a string that represents the total number of bytes required by all currently selected features on the volume that the PrimaryVolumePath property references.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.ProductLanguage: // Numeric language identifier (LANGID) for the database. (REQUIRED)
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.ReplacedInUseFiles:  // Set if the installer installs over a file that is being held in use.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.RESUME: // Resumed installation.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.RollbackDisabled:    // The installer sets this property when rollback is disabled.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.UILevel: // Indicates the user interface level.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.UpdateStarted:   // Set when changes to the system have begun for this installation.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.UPGRADINGPRODUCTCODE:   // Set by the installer when an upgrade removes an application.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case InstallationStatusProperties.VersionMsi:      // The installer sets this property to the version of Windows Installer that is run during the installation.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;

                }
            }
            else if (type == typeof(ComponentLocationProperties))
            {
                switch((ComponentLocationProperties)property)
                {
                    case ComponentLocationProperties.OriginalDatabase:    // The installer sets this property to the launched-from database, the database on the source, or the cached database.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ComponentLocationProperties.ParentOriginalDatabase:  // The installer sets this property for installations run by a Concurrent Installation action.
                        if (isInno)
                            result = String.Format(NotSupportedInnoSetup, variable);
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ComponentLocationProperties.SourceDir:        // Root directory that contains the source files.
                        if (isInno)
                            result = "{src}\\";
                        else
                            result = String.Format(NotSupportedNSIS, variable);
                        break;
                    case ComponentLocationProperties.INSTALLDIR: 
                    case ComponentLocationProperties.TARGETDIR:        // Specifies the root destination directory for the installation. During an administrative installation this property is the location to copy the installation package.
                        if (isInno)
                            result = "{app}\\";
                        else
                            result = "$INSTDIR\\";
                        break;
                }
            }

            return result;
        }
    }
}
