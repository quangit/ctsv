namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AcademicAchievement")]
    public partial class AcademicAchievement
    {
        [Key]
        public int IdAcademicAchievement { get; set; }

        public int IdOrganizationLevel { get; set; }

        public int IdSemester { get; set; }

        public int IdStudent { get; set; }

        public string NameContest { get; set; }

        public virtual OrganizationLevel OrganizationLevel { get; set; }

        public string Reward { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual Student Student { get; set; }
    }
}