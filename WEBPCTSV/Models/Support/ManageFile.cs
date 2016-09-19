namespace WEBPCTSV.Models.Support
{
    using System.IO;

    public class ManageFile
    {
        public void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
    }
}