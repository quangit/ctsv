namespace WEBPCTSV.Models.bean
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudyingAboad")]
    public partial class StudyingAboad
    {
        [StringLength(20)]
        public string DeterminationNumber { get; set; }

        public int idSemester { get; set; }

        public int idStudent { get; set; }

        [Key]
        public int idStudyingAboad { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual Student Student { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TimeStudyingAboad { get; set; }

        public string TypeFunding { get; set; }
    }
}