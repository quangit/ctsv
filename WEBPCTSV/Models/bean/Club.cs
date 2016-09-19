namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Club")]
    public partial class Club
    {
        public Club()
        {
            this.ActivityClubs = new HashSet<ActivityClub>();
            this.MemberClubs = new HashSet<MemberClub>();
        }

        public virtual ICollection<ActivityClub> ActivityClubs { get; set; }

        public string ClubName { get; set; }

        [Key]
        public int IdClub { get; set; }

        public virtual ICollection<MemberClub> MemberClubs { get; set; }

        public string Note { get; set; }
    }
}