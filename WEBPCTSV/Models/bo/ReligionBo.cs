using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class ReligionBo
    {
        DSAContext context = new DSAContext();
        public List<Religion> GetReligion()
        {
            return context.Religions.ToList();
        }
    }
}