namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AcademicAchievement")]
    public partial class AcademicAchievement
    {
        [Key]
        public int IdAcademicAchievement { get; set; }

        public string NameContest { get; set; }

        public string Reward { get; set; }

        public int IdStudent { get; set; }

        public int IdOrganizationLevel { get; set; }

        public int IdSemester { get; set; }

        public virtual OrganizationLevel OrganizationLevel { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual Student Student { get; set; }
    }
}
