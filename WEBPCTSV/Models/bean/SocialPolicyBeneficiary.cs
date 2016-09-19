namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SocialPolicyBeneficiary")]
    public partial class SocialPolicyBeneficiary
    {
        public SocialPolicyBeneficiary()
        {
            this.SocialPolicyBeneficiariesStudents = new HashSet<SocialPolicyBeneficiariesStudent>();
        }

        [Key]
        public int idSocialPolicyBeneficiaries { get; set; }

        public int? PriorityPoint { get; set; }

        public string SocialPolicyBeneficiariesName { get; set; }

        public virtual ICollection<SocialPolicyBeneficiariesStudent> SocialPolicyBeneficiariesStudents { get; set; }
    }
}