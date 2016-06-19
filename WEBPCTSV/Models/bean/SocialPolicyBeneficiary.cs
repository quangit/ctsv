namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SocialPolicyBeneficiary")]
    public partial class SocialPolicyBeneficiary
    {
        public SocialPolicyBeneficiary()
        {
            SocialPolicyBeneficiariesStudents = new HashSet<SocialPolicyBeneficiariesStudent>();
        }

        [Key]
        public int idSocialPolicyBeneficiaries { get; set; }

        public string SocialPolicyBeneficiariesName { get; set; }

        public int? PriorityPoint { get; set; }

        public virtual ICollection<SocialPolicyBeneficiariesStudent> SocialPolicyBeneficiariesStudents { get; set; }
    }
}
