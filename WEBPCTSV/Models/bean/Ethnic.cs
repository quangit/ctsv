namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ethnic")]
    public partial class Ethnic
    {
        public Ethnic()
        {
            Students = new HashSet<Student>();
        }
        public Ethnic(string ethnicName, string idState)
        {
            this.EthnicName = ethnicName;
            this.IdState = idState;
        }

        [Key]
        public int IdEthnic { get; set; }

        public string EthnicName { get; set; }

        [Required]
        [StringLength(5)]
        public string IdState { get; set; }

        public virtual State State { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
