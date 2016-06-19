namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConditionStudentShipSchool")]
    public partial class ConditionStudentShipSchool
    {
        [Key]
        public int IdConditionStudentShipSchool { get; set; }

        public double CreditNumberStudy1 { get; set; }

        public double relearnCreditsNumber { get; set; }

        public double creditNumberUnQualified { get; set; }

        public double pointTraining { get; set; }

        public double pointsShipSchool { get; set; }

        public bool IsSchedule { get; set; }

        public int IdStudentShipSchool { get; set; }

        public virtual StudentShipSchool StudentShipSchool { get; set; }
    }
}
