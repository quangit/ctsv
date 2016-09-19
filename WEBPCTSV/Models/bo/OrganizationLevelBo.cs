namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class OrganizationLevelBo
    {
        readonly DSAContext context = new DSAContext();

        public List<OrganizationLevel> Get()
        {
            return this.context.OrganizationLevels.ToList();
        }
    }
}