namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Specialize")]
    public partial class Specialize
    {
        public Specialize()
        {
            this.Students = new HashSet<Student>();
        }

        public virtual Faculty Faculty { get; set; }

        public int IdFaculty { get; set; }

        [Key]
        public int IdSpecialize { get; set; }

        public string SpecializeName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}