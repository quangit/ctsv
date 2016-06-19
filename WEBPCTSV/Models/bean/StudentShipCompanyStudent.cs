namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentShipCompanyStudent")]
    public partial class StudentShipCompanyStudent
    {
        [Key]
        public int IdStudentShipCompanyStudent { get; set; }

        public int IdStudent { get; set; }

        public int IdStudentShipCompany { get; set; }

        public virtual Student Student { get; set; }

        public virtual StudentShipCompany StudentShipCompany { get; set; }
    }
}
