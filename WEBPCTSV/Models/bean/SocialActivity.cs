namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SocialActivity")]
    public partial class SocialActivity
    {
        public SocialActivity()
        {
            this.StudentSocialActivities = new HashSet<StudentSocialActivity>();
        }

        [Key]
        public int IdSocialActivity { get; set; }

        public string OrganizationName { get; set; }

        public string Place { get; set; }

        public string SocialActivityName { get; set; }

        public virtual ICollection<StudentSocialActivity> StudentSocialActivities { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Time { get; set; }
    }
}