namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentShipSchool")]
    public class StudentShipSchool
    {
        public StudentShipSchool()
        {
            this.StudentShipSchoolFaculties = new HashSet<StudentShipSchoolFaculty>();
            this.ConditionStudentShipSchools = new HashSet<ConditionStudentShipSchool>();
        }

        public virtual ICollection<ConditionStudentShipSchool> ConditionStudentShipSchools { get; set; }

        public int IdSemester { get; set; }

        [Key]
        public int IdStudentShipSchool { get; set; }

        public virtual Semester Semester { get; set; }

        public string Status { get; set; }

        public virtual ICollection<StudentShipSchoolFaculty> StudentShipSchoolFaculties { get; set; }

        public string StudentShipSchoolName { get; set; }

        [StringLength(15)]
        public string TotalMoney { get; set; }
    }
}