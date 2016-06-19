namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ward")]
    public partial class Ward
    {
        
        public Ward()
        {
            Relatives = new HashSet<Relative>();
            StudentsHaveSameBirthPlace = new HashSet<Student>();
            StudentsHaveSameNativeLand = new HashSet<Student>();
            StudentsHaveSamePermanentResidence = new HashSet<Student>();
            StudentsHaveSameExternAddress = new HashSet<Student>();
        }
        public Ward(string idWard,string ward,string idDistrict)
        {
            this.IdWard = idWard;
            this.WardName = ward;
            this.IdDistrict = idDistrict;
        }


        [Key]
        [StringLength(5)]
        public string IdWard { get; set; }
        
        public string WardName { get; set; }

        [StringLength(5)]
        public string IdDistrict { get; set; }



        public virtual District District { get; set; }

        
        public virtual ICollection<Relative> Relatives { get; set; }


        public virtual ICollection<Student> StudentsHaveSameBirthPlace { get; set; }

        
        public virtual ICollection<Student> StudentsHaveSameNativeLand { get; set; }


        public virtual ICollection<Student> StudentsHaveSamePermanentResidence { get; set; }

        
        public virtual ICollection<Student> StudentsHaveSameExternAddress { get; set; }
    }
}
