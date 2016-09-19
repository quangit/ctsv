namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LecturerDocument")]
    public partial class LecturerDocument
    {
        public LecturerDocument()
        {
            this.LecturerDocumentSemesters = new HashSet<LecturerDocumentSemester>();
        }

        [Required]
        [StringLength(50)]
        public string DocumentName { get; set; }

        [Key]
        public int IdLecturerDocument { get; set; }

        public virtual ICollection<LecturerDocumentSemester> LecturerDocumentSemesters { get; set; }

        public string LinkFile { get; set; }
    }
}