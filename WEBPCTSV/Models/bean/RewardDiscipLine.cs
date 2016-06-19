namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RewardDiscipLine")]
    public partial class RewardDiscipLine
    {
        [Key]
        public int IdRewardDiscipline { get; set; }

        public string ReasonRewardDiscipline { get; set; }

        public string FormRewardDiscipline { get; set; }

        public bool? IsRewardDiscipline { get; set; }

        public int IdStudent { get; set; }

        public int IdSemester { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual Student Student { get; set; }
    }
}
