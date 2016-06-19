namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lecturer")]
    public partial class Lecturer
    {
        public Lecturer()
        {
            LecturerClasses = new HashSet<LecturerClass>();
        }
        public Lecturer(string lecturerNumber, string type, string firstName, string lastName, string degree, string academicTitle, string position, int idFaculty, string email, string phoneNumber, string address)
        {
            // TODO: Complete member initialization
            this.LecturerNumber = lecturerNumber;
            this.Type = type;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Degree = degree;
            this.AcademicTitle = academicTitle;
            this.Position = position;
            this.IdFaculty = idFaculty;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
        }
        [Key]
        public int IdLecturer { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(50)]
        public string LecturerNumber { get; set; }

        [StringLength(50)]
        public string Degree { get; set; }

        [StringLength(50)]
        public string AcademicTitle { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public int IdFaculty { get; set; }

        public int IdAccount { get; set; }

        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(11)]
        public string PhoneNumber { get; set; }

        public virtual Account Account { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<LecturerClass> LecturerClasses { get; set; }
    }
}
