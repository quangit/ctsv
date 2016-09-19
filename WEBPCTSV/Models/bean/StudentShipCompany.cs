namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentShipCompany")]
    public partial class StudentShipCompany
    {
        public StudentShipCompany()
        {
            this.StudentShipCompanyRegisters = new HashSet<StudentShipCompanyRegister>();
            this.StudentShipCompanyStudents = new HashSet<StudentShipCompanyStudent>();
        }

        public string CompanyName { get; set; }

        public int IdSemester { get; set; }

        [Key]
        public int IdStudentShipCompany { get; set; }

        public decimal? Money { get; set; }

        public int? NumberOfStudentShip { get; set; }

        public virtual Semester Semester { get; set; }

        public string StudentShipCompanyName { get; set; }

        public virtual ICollection<StudentShipCompanyRegister> StudentShipCompanyRegisters { get; set; }

        public virtual ICollection<StudentShipCompanyStudent> StudentShipCompanyStudents { get; set; }
    }
}