namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentSocialActivity")]
    public partial class StudentSocialActivity
    {
        [Key]
        public int IdStudentSocialActivities { get; set; }

        public int IdSocialActivity { get; set; }

        public int IdStudent { get; set; }

        public virtual SocialActivity Socialactivity { get; set; }

        public virtual Student Student { get; set; }
    }
}
