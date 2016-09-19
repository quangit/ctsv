namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class FamilyCompositionBo
    {
        readonly DSAContext context = new DSAContext();

        public List<FamilyComposition> GetListFamilyComposition()
        {
            return this.context.FamilyCompositions.ToList();
        }
    }
}