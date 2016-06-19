namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        public Course()
        {
            Classes = new HashSet<Class>();
        }

        [Key]
        public int IdCourse { get; set; }

        [StringLength(45)]
        public string CouseName { get; set; }

        public int AdmissionYear { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
