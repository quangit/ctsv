namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Specialize")]
    public partial class Specialize
    {
        
        public Specialize()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public int IdSpecialize { get; set; }

        public string SpecializeName { get; set; }

        public int IdFaculty { get; set; }

        public virtual Faculty Faculty { get; set; }

        
        public virtual ICollection<Student> Students { get; set; }
    }
}
