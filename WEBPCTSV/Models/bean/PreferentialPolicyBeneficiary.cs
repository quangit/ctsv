namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PreferentialPolicyBeneficiary")]
    public partial class PreferentialPolicyBeneficiary
    {
        public PreferentialPolicyBeneficiary()
        {
            this.Students = new HashSet<Student>();
        }

        [Key]
        public int IdPreferentialPolicyBeneficiaries { get; set; }

        public string PreferentialPolicyBeneficiariesName { get; set; }

        public int? PriorityPoint { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}