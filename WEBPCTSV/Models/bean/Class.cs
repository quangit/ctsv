namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Class")]
    public partial class Class
    {
        public Class()
        {
            this.LecturerClasses = new HashSet<LecturerClass>();
            this.Students = new HashSet<Student>();
            this.StudentShipSchoolFaculties = new HashSet<StudentShipSchoolFaculty>();
        }

        [StringLength(20)]
        [Index(IsUnique = true)]
        public string ClassName { get; set; }

        public virtual Course Course { get; set; }

        public virtual Faculty Faculty { get; set; }

        [Key]
        public int IdClass { get; set; }

        public int IdCourse { get; set; }

        public int IdFaculty { get; set; }

        public virtual ICollection<LecturerClass> LecturerClasses { get; set; }

        [StringLength(20)]
        public string MoneyOfMonth { get; set; }

        public int NumberMonthSchool { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<StudentShipSchoolFaculty> StudentShipSchoolFaculties { get; set; }

        public int TotalNumberStudent { get; set; }
    }
}