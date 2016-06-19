namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypePaperStudent")]
    public partial class TypePaperStudent
    {
        [Key]
        public int IdTypePaperStudent { get; set; }

        public string LinkFile { get; set; }

        public DateTime SubmitTime { get; set; }

        public int IdTypePaper { get; set; }

        public int IdStudent { get; set; }

        public virtual Student Student { get; set; }

        public virtual TypePaper Typepaper { get; set; }
    }
}
