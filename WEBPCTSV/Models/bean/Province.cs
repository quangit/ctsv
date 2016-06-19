namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Province")]
    public partial class Province
    {
        public Province()
        {
            Districts = new HashSet<District>();
            Students = new HashSet<Student>();
        }
        public Province(string idProvince,string provinceName, string idState)
        {
            this.IdProvince = idProvince;
            this.ProvinceName = provinceName;
            this.IdState = idState;
        }

        [Key]
        [StringLength(5)]
        public string IdProvince { get; set; }

        public string ProvinceName { get; set; }

        [StringLength(5)]
        public string IdState { get; set; }

        public virtual ICollection<District> Districts { get; set; }

        public virtual State State { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
