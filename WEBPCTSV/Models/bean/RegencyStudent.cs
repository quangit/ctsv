namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegencyStudent")]
    public partial class RegencyStudent
    {
        [Key]
        public int IdRegencyStudent { get; set; }

        [MaxLength(10)]
        public string IdRegency { get; set; }

        public int IdStudent { get; set; }

        public virtual Regency Regency { get; set; }

        public virtual Student Student { get; set; }
    }
}
