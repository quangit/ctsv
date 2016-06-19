namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SemesterYear")]
    public partial class SemesterYear
    {
        public SemesterYear()
        {
            Semesters = new HashSet<Semester>();
        }

        [Key]
        public string IdSemesterYear { get; set; }

        public string SemesterYearName { get; set; }

        public string BeginTime { get; set; }

        public string EndTime { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
    }
}
