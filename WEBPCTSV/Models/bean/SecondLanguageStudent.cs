namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecondLanguageStudent")]
    public partial class SecondLanguageStudent
    {
        [Key]
        public int IdSecondLanguageStudent { get; set; }

        [Required]
        [StringLength(128)]
        public string IdSecondLanguage { get; set; }

        public int IdStudent { get; set; }

        public virtual SecondLanguage SecondLanguage { get; set; }

        public virtual Student Student { get; set; }
    }
}
