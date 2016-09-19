namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentShipCompanyStudent")]
    public partial class StudentShipCompanyStudent
    {
        public int IdStudent { get; set; }

        public int IdStudentShipCompany { get; set; }

        [Key]
        public int IdStudentShipCompanyStudent { get; set; }

        public virtual Student Student { get; set; }

        public virtual StudentShipCompany StudentShipCompany { get; set; }
    }
}