using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bean
{
    [Table("StudentShipSchool")]
    public class StudentShipSchool
    {
        public StudentShipSchool()
        {
            StudentShipSchoolFaculties = new HashSet<StudentShipSchoolFaculty>();
            ConditionStudentShipSchools = new HashSet<ConditionStudentShipSchool>();
        }
        [Key]
        public int IdStudentShipSchool { get; set; }

        public string StudentShipSchoolName { get; set; }

        public int IdSemester { get; set; }

        [StringLength(15)]
        public string TotalMoney { get; set; }

        public string Status { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual ICollection<ConditionStudentShipSchool> ConditionStudentShipSchools { get; set; }
        public virtual ICollection<StudentShipSchoolFaculty> StudentShipSchoolFaculties{ get; set; }
    }
}