namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Faculty")]
    public partial class Faculty
    {
        public Faculty()
        {
            this.Classes = new HashSet<Class>();
            this.Lecturers = new HashSet<Lecturer>();
            this.Specializes = new HashSet<Specialize>();
            this.StudentShipSchoolFaculties = new HashSet<StudentShipSchoolFaculty>();
        }

        public string Acronym { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public string FacultyName { get; set; }

        public string FacultyNameEN { get; set; }

        [Key]
        public int IdFaculty { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }

        [StringLength(4)]
        public string NumberFaculty { get; set; }

        public virtual ICollection<Specialize> Specializes { get; set; }

        public virtual ICollection<StudentShipSchoolFaculty> StudentShipSchoolFaculties { get; set; }
    }
}