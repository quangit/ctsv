namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BloodGroup")]
    public partial class BloodGroup
    {
        public BloodGroup()
        {
            this.Students = new HashSet<Student>();
        }

        public string BloodGroupName { get; set; }

        [Key]
        public int IdBloodGroup { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}