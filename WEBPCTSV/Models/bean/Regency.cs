namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Regency")]
    public partial class Regency
    {
        public Regency()
        {
            this.RegencyStudents = new HashSet<RegencyStudent>();
        }

        [Key]
        [MaxLength(10)]
        public string IdRegency { get; set; }

        public float? PlusPoint { get; set; }

        public int? PriorityPoint { get; set; }

        public string RegencyName { get; set; }

        public virtual ICollection<RegencyStudent> RegencyStudents { get; set; }
    }
}