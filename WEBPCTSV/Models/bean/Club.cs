namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Club")]
    public partial class Club
    {
        public Club()
        {
            ActivityClubs = new HashSet<ActivityClub>();
            MemberClubs = new HashSet<MemberClub>();
        }

        [Key]
        public int IdClub { get; set; }

        public string ClubName { get; set; }

        public string Note { get; set; }

        public virtual ICollection<ActivityClub> ActivityClubs { get; set; }

        public virtual ICollection<MemberClub> MemberClubs { get; set; }
    }
}
