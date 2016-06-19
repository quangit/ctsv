using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class OrganizationLevelBo
    {
        DSAContext context = new DSAContext();
        public List<OrganizationLevel> Get()
        {
            return context.OrganizationLevels.ToList();
        }
    }
}