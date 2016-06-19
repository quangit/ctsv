namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentShipSchoolFaculty")]
    public partial class StudentShipSchoolFaculty
    {
        
        public StudentShipSchoolFaculty()
        {
            StudentShipSchoolStudents = new HashSet<StudentShipSchoolStudent>();
        }

        [Key]
        public int IdStudentShipSchoolFaculty { get; set; }

        public int? IdFaculty { get; set; }
        public int? IdClass { get; set; }

        public string TuitionFee { get; set; }

        [StringLength(15)]
        public string TotalMoneyOld { get; set; }

        [StringLength(15)]
        public string TotalMoney { get; set; }
        public int IdStudentShipSchool { get; set; }

        public virtual StudentShipSchool StudentShipSchool { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Class Class { get; set; }

        public virtual ICollection<StudentShipSchoolStudent> StudentShipSchoolStudents { get; set; }
    }
}
