namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SemesterYear")]
    public partial class SemesterYear
    {
        public SemesterYear()
        {
            this.Semesters = new HashSet<Semester>();
        }

        public string BeginTime { get; set; }

        public string EndTime { get; set; }

        [Key]
        public string IdSemesterYear { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }

        public string SemesterYearName { get; set; }
    }
}