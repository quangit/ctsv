namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SocialPolicyBeneficiariesStudent")]
    public partial class SocialPolicyBeneficiariesStudent
    {
        public int IdSocialPolicyBeneficiaries { get; set; }

        [Key]
        public int IdSocialPolicyBeneficiariesStudent { get; set; }

        public int IdStudent { get; set; }

        public virtual SocialPolicyBeneficiary SocialPolicyBeneficiary { get; set; }

        public virtual Student Student { get; set; }
    }
}