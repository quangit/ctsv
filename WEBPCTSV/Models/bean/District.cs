namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("District")]
    public partial class District
    {
        public District()
        {
            this.Students = new HashSet<Student>();
            this.Wards = new HashSet<Ward>();
        }

        public District(string idDistrict, string districtName, string idProvince)
        {
            this.IdDistrict = idDistrict;
            this.DistrictName = districtName;
            this.IdProvince = idProvince;
        }

        public string DistrictName { get; set; }

        [Key]
        [StringLength(5)]
        public string IdDistrict { get; set; }

        [Required]
        [StringLength(5)]
        public string IdProvince { get; set; }

        public virtual Province Province { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Ward> Wards { get; set; }
    }
}