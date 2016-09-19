namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Document")]
    public partial class Document
    {
        public Document()
        {
        }

        public Document(string documentNumber, string note)
        {
            this.DocumentNumber = documentNumber;
            this.Note = note;
        }

        [StringLength(50)]
        public string DocumentNumber { get; set; }

        [Key]
        public int IdDocument { get; set; }

        public int? IdNews { get; set; }

        public virtual News News { get; set; }

        public string Note { get; set; }
    }
}