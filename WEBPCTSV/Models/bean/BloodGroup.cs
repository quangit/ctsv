namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BloodGroup")]
    public partial class BloodGroup
    {
        public BloodGroup()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public int IdBloodGroup { get; set; }

        public string BloodGroupName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
