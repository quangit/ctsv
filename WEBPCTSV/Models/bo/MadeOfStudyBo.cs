using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class MadeOfStudyBo
    {
        DSAContext context = new DSAContext();
        public List<MadeOfStudy> GetListMadeOfStudy()
        {
            return context.MadeOfStudies.ToList();
        }
    }
}