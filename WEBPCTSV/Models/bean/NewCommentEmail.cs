using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;

namespace WEBPCTSV.Models.bean
{
    public class NewCommentEmail : Email
    {
        public string To { get; set; }
        public string Title { get; set; }
        public string Categories { get; set; }
        public string Comment { get; set; }
        public string Link { get; set; }

    }
}