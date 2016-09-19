namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LearningOutCome")]
    public partial class LearningOutCome
    {
        public LearningOutCome()
        {
            this.StudentShipSchoolStudents = new HashSet<StudentShipSchoolStudent>();
        }

        public float? CreditNumberUnQualified { get; set; }

        public float? CreditsNumber { get; set; }

        [Key]
        public int IdLearningOutComes { get; set; }

        public string IdRankingAcademic { get; set; }

        public int IdSemester { get; set; }

        public int IdStudent { get; set; }

        public bool isAcceptConsider { get; set; }

        public bool isDisEnableConsider { get; set; }

        public bool IsSchedule { get; set; }

        public float? PointPlus { get; set; }

        public float? Points10Scale { get; set; }

        public float? Points4Scale { get; set; }

        public float? PointsAverageShipSchool { get; set; }

        public float? PointsShipSchool { get; set; }

        public int? PointTraining { get; set; }

        public virtual RankingAcademic RankingAcademic { get; set; }

        public float? RelearnCreditsNumber { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<StudentShipSchoolStudent> StudentShipSchoolStudents { get; set; }

        [StringLength(5)]
        public string TypeClass { get; set; }
    }
}