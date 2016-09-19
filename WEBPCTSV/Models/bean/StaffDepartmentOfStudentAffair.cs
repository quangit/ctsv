namespace WEBPCTSV.Models.bean
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StaffDepartmentOfStudentAffair")]
    public partial class StaffDepartmentOfStudentAffair
    {
        public virtual Account Account { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public string Email { get; set; }

        public int IdAccount { get; set; }

        public decimal? IdentityCardNumber { get; set; }

        [Key]
        public int IdStaffDepartmentOfStudentAffairs { get; set; }

        public decimal? PhoneNumber { get; set; }

        public string StaffName { get; set; }
    }
}