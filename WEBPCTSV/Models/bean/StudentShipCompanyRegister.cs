namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentShipCompanyRegister")]
    public partial class StudentShipCompanyRegister
    {
        [Key]
        public int idStudentShipCompanyRegister { get; set; }

        public int idStudentShipCompany { get; set; }

        public int idStudent { get; set; }

        public virtual Student Student { get; set; }

        public virtual StudentShipCompany StudentShipCompany { get; set; }
    }
}
