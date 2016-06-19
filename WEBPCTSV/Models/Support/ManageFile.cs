using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.Support
{
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