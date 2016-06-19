namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentShipCompany")]
    public partial class StudentShipCompany
    {
        
        public StudentShipCompany()
        {
            StudentShipCompanyRegisters = new HashSet<StudentShipCompanyRegister>();
            StudentShipCompanyStudents = new HashSet<StudentShipCompanyStudent>();
        }

        [Key]
        public int IdStudentShipCompany { get; set; }

        public string StudentShipCompanyName { get; set; }

        public string CompanyName { get; set; }

        public decimal? Money { get; set; }

        public int? NumberOfStudentShip { get; set; }

        public int IdSemester { get; set; }

        public virtual Semester Semester { get; set; }

        
        public virtual ICollection<StudentShipCompanyRegister> StudentShipCompanyRegisters { get; set; }

        
        public virtual ICollection<StudentShipCompanyStudent> StudentShipCompanyStudents { get; set; }
    }
}
