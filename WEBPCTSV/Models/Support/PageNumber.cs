using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.Support
{
    public class PageNumber
    {
        public int PageNumberTotal { get; set; }
        public int PageNumberCurrent { get; set; }
        public string Link { get; set; }
    }
}