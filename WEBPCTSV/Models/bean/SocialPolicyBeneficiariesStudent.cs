namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SocialPolicyBeneficiariesStudent")]
    public partial class SocialPolicyBeneficiariesStudent
    {
        [Key]
        public int IdSocialPolicyBeneficiariesStudent { get; set; }

        public int IdSocialPolicyBeneficiaries { get; set; }

        public int IdStudent { get; set; }

        public virtual SocialPolicyBeneficiary SocialPolicyBeneficiary { get; set; }

        public virtual Student Student { get; set; }
    }
}
