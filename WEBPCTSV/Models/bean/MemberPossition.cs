namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MemberPossition")]
    public partial class MemberPossition
    {
        public MemberPossition()
        {
            this.MemberClubs = new HashSet<MemberClub>();
        }

        [Key]
        public int IdMemberPossition { get; set; }

        public virtual ICollection<MemberClub> MemberClubs { get; set; }

        public string MemberPossitionName { get; set; }
    }
}