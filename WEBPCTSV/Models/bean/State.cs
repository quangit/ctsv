namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("State")]
    public partial class State
    {
        public State()
        {
            this.Ethnics = new HashSet<Ethnic>();
            this.Provinces = new HashSet<Province>();
        }

        public virtual ICollection<Ethnic> Ethnics { get; set; }

        [Key]
        [StringLength(5)]
        public string IdState { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }

        public string StateName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}