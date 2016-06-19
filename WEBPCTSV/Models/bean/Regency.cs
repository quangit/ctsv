namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Regency")]
    public partial class Regency
    {
        public Regency()
        {
            RegencyStudents = new HashSet<RegencyStudent>();
        }

        [Key]
        [MaxLength(10)]
        public string IdRegency { get; set; }

        public string RegencyName { get; set; }

        public int? PriorityPoint { get; set; }

        public float? PlusPoint { get; set; }

        public virtual ICollection<RegencyStudent> RegencyStudents { get; set; }
    }
}
