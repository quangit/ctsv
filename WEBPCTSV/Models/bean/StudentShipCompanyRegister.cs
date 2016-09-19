namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentShipCompanyRegister")]
    public partial class StudentShipCompanyRegister
    {
        public int idStudent { get; set; }

        public int idStudentShipCompany { get; set; }

        [Key]
        public int idStudentShipCompanyRegister { get; set; }

        public virtual Student Student { get; set; }

        public virtual StudentShipCompany StudentShipCompany { get; set; }
    }
}