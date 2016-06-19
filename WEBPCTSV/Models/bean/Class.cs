namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Class")]
    public partial class Class
    {
        public Class()
        {
            LecturerClasses = new HashSet<LecturerClass>();
            Students = new HashSet<Student>();
            StudentShipSchoolFaculties = new HashSet<StudentShipSchoolFaculty>();
        }

        [Key]
        public int IdClass { get; set; }

        [StringLength(20)]
        [Index(IsUnique = true)]
        public string ClassName { get; set; }

        public int TotalNumberStudent { get; set; }

        public int IdFaculty { get; set; }

        public int IdCourse { get; set; }

        public int NumberMonthSchool { get; set; }

        [StringLength(20)]
        public string MoneyOfMonth { get; set; }

        public virtual Course Course { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<LecturerClass> LecturerClasses { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<StudentShipSchoolFaculty> StudentShipSchoolFaculties { get; set; }
    }
}
