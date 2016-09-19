namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Lecturer")]
    public partial class Lecturer
    {
        public Lecturer()
        {
            this.LecturerClasses = new HashSet<LecturerClass>();
        }

        public Lecturer(
            string lecturerNumber,
            string type,
            string firstName,
            string lastName,
            string degree,
            string academicTitle,
            string position,
            int idFaculty,
            string email,
            string phoneNumber,
            string address)
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

        [StringLength(50)]
        public string AcademicTitle { get; set; }

        public virtual Account Account { get; set; }

        public string Address { get; set; }

        [StringLength(50)]
        public string Degree { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public virtual Faculty Faculty { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        public int IdAccount { get; set; }

        public int IdFaculty { get; set; }

        [Key]
        public int IdLecturer { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public virtual ICollection<LecturerClass> LecturerClasses { get; set; }

        [StringLength(50)]
        public string LecturerNumber { get; set; }

        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(50)]
        public string Type { get; set; }
    }
}