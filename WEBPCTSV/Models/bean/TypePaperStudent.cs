namespace WEBPCTSV.Models.bean
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TypePaperStudent")]
    public partial class TypePaperStudent
    {
        public int IdStudent { get; set; }

        public int IdTypePaper { get; set; }

        [Key]
        public int IdTypePaperStudent { get; set; }

        public string LinkFile { get; set; }

        public virtual Student Student { get; set; }

        public DateTime SubmitTime { get; set; }

        public virtual TypePaper Typepaper { get; set; }
    }
}