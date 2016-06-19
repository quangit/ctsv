namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("District")]
    public partial class District
    {
        public District()
        {
            Students = new HashSet<Student>();
            Wards = new HashSet<Ward>();
        }
        public District(string idDistrict, string districtName, string idProvince)
        {
            this.IdDistrict = idDistrict;
            this.DistrictName = districtName;
            this.IdProvince = idProvince;
        }

        [Key]
        [StringLength(5)]
        public string IdDistrict { get; set; }

        public string DistrictName { get; set; }

        [Required]
        [StringLength(5)]
        public string IdProvince { get; set; }

        public virtual Province Province { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Ward> Wards { get; set; }
    }
}
