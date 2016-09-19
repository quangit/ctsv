namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Ward")]
    public partial class Ward
    {
        public Ward()
        {
            this.Relatives = new HashSet<Relative>();
            this.StudentsHaveSameBirthPlace = new HashSet<Student>();
            this.StudentsHaveSameNativeLand = new HashSet<Student>();
            this.StudentsHaveSamePermanentResidence = new HashSet<Student>();
            this.StudentsHaveSameExternAddress = new HashSet<Student>();
        }

        public Ward(string idWard, string ward, string idDistrict)
        {
            this.IdWard = idWard;
            this.WardName = ward;
            this.IdDistrict = idDistrict;
        }

        public virtual District District { get; set; }

        [StringLength(5)]
        public string IdDistrict { get; set; }

        [Key]
        [StringLength(5)]
        public string IdWard { get; set; }

        public virtual ICollection<Relative> Relatives { get; set; }

        public virtual ICollection<Student> StudentsHaveSameBirthPlace { get; set; }

        public virtual ICollection<Student> StudentsHaveSameExternAddress { get; set; }

        public virtual ICollection<Student> StudentsHaveSameNativeLand { get; set; }

        public virtual ICollection<Student> StudentsHaveSamePermanentResidence { get; set; }

        public string WardName { get; set; }
    }
}