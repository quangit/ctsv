namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Area")]
    public partial class Area
    {
        public Area()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        [StringLength(6)]
        public string IdArea { get; set; }

        public string AreaName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
