namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LecturerClassDocument")]
    public partial class LecturerClassDocument
    {
        public LecturerClassDocument()
        {
        }

        public LecturerClassDocument(int idLecturerClass, int idDocumentSemester, bool isApproved)
        {
            // TODO: Complete member initialization
            this.IdLecturerClass = idLecturerClass;
            this.IdDocumentSemester = idDocumentSemester;
            this.IsApproved = isApproved;
        }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdDocumentSemester { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdLecturerClass { get; set; }

        public bool IsApproved { get; set; }
    }
}