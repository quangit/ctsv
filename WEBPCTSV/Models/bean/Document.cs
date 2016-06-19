namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Document")]
    public partial class Document
    {
        public Document() { }
        public Document(string documentNumber, string note)
        {
            this.DocumentNumber = documentNumber;
            this.Note = note;
        }
        [Key]
        public int IdDocument { get; set; }

        [StringLength(50)]
        public string DocumentNumber { get; set; }

        public string Note { get; set; }

        public int? IdNews { get; set; }

        public virtual News News { get; set; }
    }
}
