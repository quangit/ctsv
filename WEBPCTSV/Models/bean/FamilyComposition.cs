namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FamilyComposition")]
    public partial class FamilyComposition
    {
        public FamilyComposition()
        {
            this.Students = new HashSet<Student>();
        }

        [StringLength(45)]
        public string FamilyCompositioncolName { get; set; }

        [Key]
        public int IdFamilyComposition { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}