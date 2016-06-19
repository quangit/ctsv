using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class DecentralizationGroupBo
    {
        DSAContext context = new DSAContext();
        public List<DecentralizationGroup> getListGroup()
        {
            return context.DecentralizationGroups.ToList();
        }


        public void AddGroup(string groupName)
        {
            DecentralizationGroup group = new DecentralizationGroup();
            DecentralizationBo decentralizationBo = new DecentralizationBo();
            group.DecentralizationGroupName = groupName;
            context.DecentralizationGroups.Add(group);
            context.SaveChanges();
        }
    }
}