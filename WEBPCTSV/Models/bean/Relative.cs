namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Relative")]
    public partial class Relative
    {
        [Key]
        public int IdRelatives { get; set; }

        public string relativeName { get; set; }

        public string RelativesLastName { get; set; }

        public string RelativeFirstName { get; set; }

        public string AddtionalRelativesAddress { get; set; }

        public decimal? LandlineNumberRelatives { get; set; }

        public decimal? CellphoneNumberRelatives { get; set; }

        public int IdStudent { get; set; }

        [StringLength(5)]
        public string IdWard { get; set; }

        public virtual Ward Ward { get; set; }

        public virtual Student Student { get; set; }
    }
}
