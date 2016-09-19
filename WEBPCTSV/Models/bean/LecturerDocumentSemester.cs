namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LecturerDocumentSemester")]
    public partial class LecturerDocumentSemester
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDocumentSemester { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdLecturerDocument { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSemester { get; set; }

        public virtual LecturerDocument LecturerDocument { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Phase { get; set; }

        public virtual Semester Semester { get; set; }
    }
}