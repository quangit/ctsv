using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bean
{
    [Table("StudentshipApplication")]
    public class StudentshipApplication
    {
        public StudentshipApplication()
        { }
        [Key]
        public int IdStudentshipApplication { get; set; }
        public int IdStudent { get; set; }
        public int IdSemester { get; set; }

        public bool IsConsidered { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual Student Student { get; set; }
    }
}