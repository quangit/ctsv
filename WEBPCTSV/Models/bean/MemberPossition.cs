namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MemberPossition")]
    public partial class MemberPossition
    {
        public MemberPossition()
        {
            MemberClubs = new HashSet<MemberClub>();
        }

        [Key]
        public int IdMemberPossition { get; set; }

        public string MemberPossitionName { get; set; }

        public virtual ICollection<MemberClub> MemberClubs { get; set; }
    }
}
