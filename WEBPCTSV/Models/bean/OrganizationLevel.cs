namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrganizationLevel")]
    public partial class OrganizationLevel
    {
        public OrganizationLevel()
        {
            AcademicAchievements = new HashSet<AcademicAchievement>();
        }

        [Key]
        public int IdOrganizationLevel { get; set; }

        public string OrganizationLevelName { get; set; }

        public int? PriorityPoint { get; set; }

        public float? PlusPoint { get; set; }

        public virtual ICollection<AcademicAchievement> AcademicAchievements { get; set; }
    }
}
