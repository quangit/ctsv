namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Year")]
    public partial class Year
    {
        public Year()
        {
            Semesters = new HashSet<Semester>();

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdYear { get; set; }

        public string YearName { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }

    }
}
