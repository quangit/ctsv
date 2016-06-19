namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentShipSchoolStudent")]
    public partial class StudentShipSchoolStudent
    {
        [Key]
        public int IdStudentshipSchoolStudent { get; set; }

        [StringLength(15)]
        public string Money { get; set; }

        public string Rank { get; set; }


        public int IdStudentShipSchoolFaculty { get; set; }

        public int IdLearningOutComes { get; set; }

        public virtual LearningOutCome LearningOutCome { get; set; }

        public virtual StudentShipSchoolFaculty StudentShipSchoolFaculty { get; set; }

    }
}
