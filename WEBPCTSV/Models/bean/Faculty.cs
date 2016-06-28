namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Faculty")]
    public partial class Faculty
    {
        public Faculty()
        {
            Classes = new HashSet<Class>();
            Lecturers = new HashSet<Lecturer>();
            Specializes = new HashSet<Specialize>();
            StudentShipSchoolFaculties = new HashSet<StudentShipSchoolFaculty>();
        }

        [Key]
        public int IdFaculty { get; set; }

        [StringLength(4)]
        public string NumberFaculty { get; set; }

        public string FacultyName { get; set; }

        public string FacultyNameEN { get; set; }

        public string Acronym { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }

        public virtual ICollection<Specialize> Specializes { get; set; }

        public virtual ICollection<StudentShipSchoolFaculty> StudentShipSchoolFaculties { get; set; }
    }
}
