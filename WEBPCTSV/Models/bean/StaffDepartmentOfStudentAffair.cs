namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StaffDepartmentOfStudentAffair")]
    public partial class StaffDepartmentOfStudentAffair
    {
        [Key]
        public int IdStaffDepartmentOfStudentAffairs { get; set; }

        public string StaffName { get; set; }

        public decimal? IdentityCardNumber { get; set; }

        public decimal? PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public int IdAccount { get; set; }

        public virtual Account Account { get; set; }
    }
}
