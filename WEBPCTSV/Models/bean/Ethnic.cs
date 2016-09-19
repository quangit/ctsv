namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Ethnic")]
    public partial class Ethnic
    {
        public Ethnic()
        {
            this.Students = new HashSet<Student>();
        }

        public Ethnic(string ethnicName, string idState)
        {
            this.EthnicName = ethnicName;
            this.IdState = idState;
        }

        public string EthnicName { get; set; }

        [Key]
        public int IdEthnic { get; set; }

        [Required]
        [StringLength(5)]
        public string IdState { get; set; }

        public virtual State State { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}