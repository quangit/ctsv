namespace WEBPCTSV.Models.bean
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MemberClub")]
    public partial class MemberClub
    {
        public virtual Club Club { get; set; }

        public int IdClub { get; set; }

        [Key]
        public int IdMemberClub { get; set; }

        public int IdMemberPossition { get; set; }

        public int IdStudent { get; set; }

        public bool? IsActivity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JoinDate { get; set; }

        public virtual MemberPossition MemberPossition { get; set; }

        public virtual Student Student { get; set; }
    }
}