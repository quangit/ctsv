namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Year")]
    public partial class Year
    {
        public Year()
        {
            this.Semesters = new HashSet<Semester>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdYear { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }

        public string YearName { get; set; }
    }
}