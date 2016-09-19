namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class DecentralizationGroupBo
    {
        readonly DSAContext context = new DSAContext();

        public void AddGroup(string groupName)
        {
            DecentralizationGroup group = new DecentralizationGroup();
            DecentralizationBo decentralizationBo = new DecentralizationBo();
            group.DecentralizationGroupName = groupName;
            this.context.DecentralizationGroups.Add(group);
            this.context.SaveChanges();
        }

        public List<DecentralizationGroup> getListGroup()
        {
            return this.context.DecentralizationGroups.ToList();
        }
    }
}