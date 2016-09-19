namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RewardDiscipLine")]
    public partial class RewardDiscipLine
    {
        public string FormRewardDiscipline { get; set; }

        [Key]
        public int IdRewardDiscipline { get; set; }

        public int IdSemester { get; set; }

        public int IdStudent { get; set; }

        public bool? IsRewardDiscipline { get; set; }

        public string ReasonRewardDiscipline { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual Student Student { get; set; }
    }
}