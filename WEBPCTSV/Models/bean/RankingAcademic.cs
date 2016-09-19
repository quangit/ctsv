namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RankingAcademic")]
    public partial class RankingAcademic
    {
        public RankingAcademic()
        {
            this.LearningOutComes = new HashSet<LearningOutCome>();
        }

        [Key]
        public string IdRankingAcademic { get; set; }

        public virtual ICollection<LearningOutCome> LearningOutComes { get; set; }

        [StringLength(15)]
        public string MoneyStudentShip { get; set; }

        public string NameRankingAcademic { get; set; }
    }
}