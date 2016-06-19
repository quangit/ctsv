using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class BloodGroupBo
    {
        DSAContext context = new DSAContext();

        public List<BloodGroup> GetBloodGroup()
        {
            return context.BloodGroups.ToList();
        }
    }
}