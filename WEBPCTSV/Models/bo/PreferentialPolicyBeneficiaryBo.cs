using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class PreferentialPolicyBeneficiaryBo
    {
        DSAContext context = new DSAContext();
        public List<PreferentialPolicyBeneficiary> GetListPreferentialPolicyBeneficiary()
        {
            return context.PreferentialPolicyBeneficiaries.ToList();
        }
    }
}