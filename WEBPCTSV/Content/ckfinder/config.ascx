<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="false" Inherits="CKFinder.Settings.ConfigFile" %>
<%@ Import Namespace="CKFinder.Settings" %>
<script runat="server">

        /**
	 * This function must check the user session to be sure that he/she is
	 * authorized to upload and access files using CKFinder.
	 */

    public override bool CheckAuthentication()
    {
        // WARNING : DO NOT simply return "true". By doing so, you are allowing
        // "anyone" to upload and list the files in your server. You must implement
        // some kind of session validation here. Even something very simple as...
        //
        //		return ( Session[ "IsAuthorized" ] != null && (bool)Session[ "IsAuthorized" ] == true );
        //
        // ... where Session[ "IsAuthorized" ] is set to "true" as soon as the
        // user logs on your system.

        return true;
    }

        /**
	 * All configuration settings must be defined here.
	 */

    public override void SetConfig()
    {
        // Paste your license name and key here. If left blank, CKFinder will
        // be fully functional, in Demo Mode.
        this.LicenseName = "";
        this.LicenseKey = "";

        // The base URL used to reach files in CKFinder through the browser.
        //BaseUrl = "~/Content/ckfinder/userfiles";
        this.BaseUrl = "~/document";
        // The phisical directory in the server where the file will end up. If
        // blank, CKFinder attempts to resolve BaseUrl.
        this.BaseDir = "";

        // Optional: enable extra plugins (remember to copy .dll files first).
        this.Plugins = new string[] { // "CKFinder.Plugins.FileEditor, CKFinder_FileEditor",
                                        // "CKFinder.Plugins.ImageResize, CKFinder_ImageResize",
                                        // "CKFinder.Plugins.Watermark, CKFinder_Watermark"
                                    };
        // Settings for extra plugins.
        this.PluginSettings = new Hashtable();
        this.PluginSettings.Add("ImageResize_smallThumb", "90x90");
        this.PluginSettings.Add("ImageResize_mediumThumb", "120x120");
        this.PluginSettings.Add("ImageResize_largeThumb", "180x180");
        // Name of the watermark image in plugins/watermark folder
        this.PluginSettings.Add("Watermark_source", "logo.gif");
        this.PluginSettings.Add("Watermark_marginRight", "5");
        this.PluginSettings.Add("Watermark_marginBottom", "5");
        this.PluginSettings.Add("Watermark_quality", "90");
        this.PluginSettings.Add("Watermark_transparency", "80");

        // Thumbnail settings.
        // "Url" is used to reach the thumbnails with the browser, while "Dir"
        // points to the physical location of the thumbnail files in the server.
        this.Thumbnails.Url = this.BaseUrl + "_thumbs/";
        if (this.BaseDir != "")
        {
            this.Thumbnails.Dir = this.BaseDir + "_thumbs/";
        }
        this.Thumbnails.Enabled = true;
        this.Thumbnails.DirectAccess = false;
        this.Thumbnails.MaxWidth = 100;
        this.Thumbnails.MaxHeight = 100;
        this.Thumbnails.Quality = 80;

        // Set the maximum size of uploaded images. If an uploaded image is
        // larger, it gets scaled down proportionally. Set to 0 to disable this
        // feature.
        //Images.MaxWidth = 1600;
        //Images.MaxHeight = 1200;
        this.Images.Quality = 80;
        this.Images.MaxWidth = 8000;
        this.Images.MaxHeight = 5000;
        // Indicates that the file size (MaxSize) for images must be checked only
        // after scaling them. Otherwise, it is checked right after uploading.
        this.CheckSizeAfterScaling = true;

        // Increases the security on an IIS web server.
        // If enabled, CKFinder will disallow creating folders and uploading files whose names contain characters
        // that are not safe under an IIS 6.0 web server.
        this.DisallowUnsafeCharacters = true;

        // Due to security issues with Apache modules, it is recommended to leave the
        // following setting enabled. It can be safely disabled on IIS.
        this.ForceSingleExtension = true;

        // For security, HTML is allowed in the first Kb of data for files having the
        // following extensions only.
        this.HtmlExtensions = new string[] { "html", "htm", "xml", "js" };

        // Folders to not display in CKFinder, no matter their location. No
        // paths are accepted, only the folder name.
        // The * and ? wildcards are accepted.
        this.HideFolders = new string[] { ".svn", "CVS" };

        // Files to not display in CKFinder, no matter their location. No
        // paths are accepted, only the file name, including extension.
        // The * and ? wildcards are accepted.
        this.HideFiles = new string[] { ".*" };

        // Perform additional checks for image files.
        this.SecureImageUploads = true;

        // The session variable name that CKFinder must use to retrieve the
        // "role" of the current user. The "role" is optional and can be used
        // in the "AccessControl" settings (bellow in this file).
        this.RoleSessionVar = "CKFinder_UserRole";

        // ACL (Access Control) settings. Used to restrict access or features
        // to specific folders.
        // Several "AccessControl.Add()" calls can be made, which return a
        // single ACL setting object to be configured. All properties settings
        // are optional in that object.
        // Subfolders inherit their default settings from their parents' definitions.
        //
        //	- The "Role" property accepts the special "*" value, which means
        //	  "everybody".
        //	- The "ResourceType" attribute accepts the special value "*", which
        //	  means "all resource types".
        AccessControl acl = this.AccessControl.Add();
        acl.Role = "*";
        acl.ResourceType = "*";
        acl.Folder = "/";

        acl.FolderView = true;
        acl.FolderCreate = true;
        acl.FolderRename = true;
        acl.FolderDelete = true;

        acl.FileView = true;
        acl.FileUpload = true;
        acl.FileRename = true;
        acl.FileDelete = true;

        // Resource Type settings.
        // A resource type is nothing more than a way to group files under
        // different paths, each one having different configuration settings.
        // Each resource type name must be unique.
        // When loading CKFinder, the "type" querystring parameter can be used
        // to display a specific type only. If "type" is omitted in the URL,
        // the "DefaultResourceTypes" settings is used (may contain the
        // resource type names separated by a comma). If left empty, all types
        // are loaded.

        this.DefaultResourceTypes = "";

        ResourceType type;

        type = this.ResourceType.Add("Files");
        type.Url = this.BaseUrl + "files/";
        type.Dir = this.BaseDir == "" ? "" : this.BaseDir + "files/";
        type.MaxSize = 0;
        type.AllowedExtensions = new string[] { "7z", "aiff", "asf", "avi", "bmp", "csv", "doc", "docx", "fla", "flv", "gif", "gz", "gzip", "jpeg", "jpg", "mid", "mov", "mp3", "mp4", "mpc", "mpeg", "mpg", "ods", "odt", "pdf", "png", "ppt", "pptx", "pxd", "qt", "ram", "rar", "rm", "rmi", "rmvb", "rtf", "sdc", "sitd", "swf", "sxc", "sxw", "tar", "tgz", "tif", "tiff", "txt", "vsd", "wav", "wma", "wmv", "xls", "xlsx", "zip" };
        type.DeniedExtensions = new string[] { };

        type = this.ResourceType.Add("Images");
        type.Url = this.BaseUrl + "images/";
        type.Dir = this.BaseDir == "" ? "" : this.BaseDir + "images/";
        type.MaxSize = 0;
        type.AllowedExtensions = new string[] { "bmp", "gif", "jpeg", "jpg", "png" };
        type.DeniedExtensions = new string[] { };

        type = this.ResourceType.Add("Flash");
        type.Url = this.BaseUrl + "flash/";
        type.Dir = this.BaseDir == "" ? "" : this.BaseDir + "flash/";
        type.MaxSize = 0;
        type.AllowedExtensions = new string[] { "swf", "flv" };
        type.DeniedExtensions = new string[] { };
    }

    [__ReSharperSynthetic]
    private void __ReSharper_Data_Bind__Conversion<T>(T _, T expression)
    {
    }

    [__ReSharperSynthetic]
    private void __ReSharper_Data_Bind__Conversion<T>(T _, object expression)
    {
    }

    [__ReSharperSynthetic]
    private void __ReSharper_Render(object expression)
    {
    }

</script>