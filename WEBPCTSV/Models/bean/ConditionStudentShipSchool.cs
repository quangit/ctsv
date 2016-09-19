namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ConditionStudentShipSchool")]
    public partial class ConditionStudentShipSchool
    {
        public double CreditNumberStudy1 { get; set; }

        public double creditNumberUnQualified { get; set; }

        [Key]
        public int IdConditionStudentShipSchool { get; set; }

        public int IdStudentShipSchool { get; set; }

        public bool IsSchedule { get; set; }

        public double pointsShipSchool { get; set; }

        public double pointTraining { get; set; }

        public double relearnCreditsNumber { get; set; }

        public virtual StudentShipSchool StudentShipSchool { get; set; }
    }
}