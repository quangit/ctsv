namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class PreferentialPolicyBeneficiaryBo
    {
        readonly DSAContext context = new DSAContext();

        public List<PreferentialPolicyBeneficiary> GetListPreferentialPolicyBeneficiary()
        {
            return this.context.PreferentialPolicyBeneficiaries.ToList();
        }
    }
}