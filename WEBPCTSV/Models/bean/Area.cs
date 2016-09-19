namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Area")]
    public partial class Area
    {
        public Area()
        {
            this.Students = new HashSet<Student>();
        }

        public string AreaName { get; set; }

        [Key]
        [StringLength(6)]
        public string IdArea { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}