namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Course")]
    public partial class Course
    {
        public Course()
        {
            this.Classes = new HashSet<Class>();
        }

        public int AdmissionYear { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        [StringLength(45)]
        public string CouseName { get; set; }

        [Key]
        public int IdCourse { get; set; }
    }
}