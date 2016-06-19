namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RankingAcademic")]
    public partial class RankingAcademic
    {
        public RankingAcademic()
        {
            LearningOutComes = new HashSet<LearningOutCome>();
        }
        [Key]
        public string IdRankingAcademic { get; set; }

        public string NameRankingAcademic { get; set; }

        [StringLength(15)]
        public string MoneyStudentShip { get; set; }


        public virtual ICollection<LearningOutCome> LearningOutComes { get; set; }
    }
}
