namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    {
        
        public State()
        {
            Ethnics = new HashSet<Ethnic>();
            Provinces = new HashSet<Province>();
        }

        [Key]
        [StringLength(5)]
        public string IdState { get; set; }

        public string StateName { get; set; }

        
        public virtual ICollection<Ethnic> Ethnics { get; set; }

        
        public virtual ICollection<Province> Provinces { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
