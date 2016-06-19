namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MadeOfStudy")]
    public partial class MadeOfStudy
    {
        
        public MadeOfStudy()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public int idMadeOfStudy { get; set; }

        public string MadeOfStudyName { get; set; }

        
        public virtual ICollection<Student> Students { get; set; }
    }
}
