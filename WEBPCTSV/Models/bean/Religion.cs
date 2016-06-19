namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Religion")]
    public partial class Religion
    {
        public Religion()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public int IdReligion { get; set; }

        public string ReligionName { get; set; }

        
        public virtual ICollection<Student> Students { get; set; }
    }
}
