namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class BloodGroupBo
    {
        readonly DSAContext context = new DSAContext();

        public List<BloodGroup> GetBloodGroup()
        {
            return this.context.BloodGroups.ToList();
        }
    }
}