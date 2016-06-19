namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SocialActivity")]
    public partial class SocialActivity
    {
        public SocialActivity()
        {
            StudentSocialActivities = new HashSet<StudentSocialActivity>();
        }

        [Key]
        public int IdSocialActivity { get; set; }

        public string SocialActivityName { get; set; }

        public string OrganizationName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Time { get; set; }

        public string Place { get; set; }

        public virtual ICollection<StudentSocialActivity> StudentSocialActivities { get; set; }
    }
}
