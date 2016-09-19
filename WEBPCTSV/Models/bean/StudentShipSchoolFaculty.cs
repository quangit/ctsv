namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentShipSchoolFaculty")]
    public partial class StudentShipSchoolFaculty
    {
        public StudentShipSchoolFaculty()
        {
            this.StudentShipSchoolStudents = new HashSet<StudentShipSchoolStudent>();
        }

        public virtual Class Class { get; set; }

        public virtual Faculty Faculty { get; set; }

        public int? IdClass { get; set; }

        public int? IdFaculty { get; set; }

        public int IdStudentShipSchool { get; set; }

        [Key]
        public int IdStudentShipSchoolFaculty { get; set; }

        public virtual StudentShipSchool StudentShipSchool { get; set; }

        public virtual ICollection<StudentShipSchoolStudent> StudentShipSchoolStudents { get; set; }

        [StringLength(15)]
        public string TotalMoney { get; set; }

        [StringLength(15)]
        public string TotalMoneyOld { get; set; }

        public string TuitionFee { get; set; }
    }
}