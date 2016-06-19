namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FamilyComposition")]
    public partial class FamilyComposition
    {
        public FamilyComposition()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public int IdFamilyComposition { get; set; }

        [StringLength(45)]
        public string FamilyCompositioncolName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
