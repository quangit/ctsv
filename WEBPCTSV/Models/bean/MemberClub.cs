namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MemberClub")]
    public partial class MemberClub
    {
        [Key]
        public int IdMemberClub { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JoinDate { get; set; }

        public bool? IsActivity { get; set; }

        public int IdClub { get; set; }

        public int IdMemberPossition { get; set; }

        public int IdStudent { get; set; }

        public virtual Club Club { get; set; }

        public virtual MemberPossition MemberPossition { get; set; }

        public virtual Student Student { get; set; }
    }
}
