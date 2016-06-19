namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LecturerDocument")]
    public partial class LecturerDocument
    {
        public LecturerDocument()
        {
            LecturerDocumentSemesters = new HashSet<LecturerDocumentSemester>();
        }

        [Key]
        public int IdLecturerDocument { get; set; }

        [Required]
        [StringLength(50)]
        public string DocumentName { get; set; }

        public string LinkFile { get; set; }

        public virtual ICollection<LecturerDocumentSemester> LecturerDocumentSemesters { get; set; }
    }
}
