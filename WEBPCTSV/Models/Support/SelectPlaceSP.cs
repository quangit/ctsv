using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.Support
{
    public class SelectPlaceSP
    {
        public SelectPlaceSP(string name, string note,Ward ward)
        {
            this.name = name;
            this.note = note;
            this.ward = ward;

        }
        public string name { get; set; }
        public string note { get; set; }

        public Ward ward { get; set; }
    }
}