namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Province")]
    public partial class Province
    {
        public Province()
        {
            this.Districts = new HashSet<District>();
            this.Students = new HashSet<Student>();
        }

        public Province(string idProvince, string provinceName, string idState)
        {
            this.IdProvince = idProvince;
            this.ProvinceName = provinceName;
            this.IdState = idState;
        }

        public virtual ICollection<District> Districts { get; set; }

        [Key]
        [StringLength(5)]
        public string IdProvince { get; set; }

        [StringLength(5)]
        public string IdState { get; set; }

        public string ProvinceName { get; set; }

        public virtual State State { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}