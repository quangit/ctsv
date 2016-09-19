namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrganizationLevel")]
    public partial class OrganizationLevel
    {
        public OrganizationLevel()
        {
            this.AcademicAchievements = new HashSet<AcademicAchievement>();
        }

        public virtual ICollection<AcademicAchievement> AcademicAchievements { get; set; }

        [Key]
        public int IdOrganizationLevel { get; set; }

        public string OrganizationLevelName { get; set; }

        public float? PlusPoint { get; set; }

        public int? PriorityPoint { get; set; }
    }
}