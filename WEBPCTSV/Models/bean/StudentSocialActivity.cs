namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentSocialActivity")]
    public partial class StudentSocialActivity
    {
        public int IdSocialActivity { get; set; }

        public int IdStudent { get; set; }

        [Key]
        public int IdStudentSocialActivities { get; set; }

        public virtual SocialActivity Socialactivity { get; set; }

        public virtual Student Student { get; set; }
    }
}