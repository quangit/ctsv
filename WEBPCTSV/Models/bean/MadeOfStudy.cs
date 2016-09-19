namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MadeOfStudy")]
    public partial class MadeOfStudy
    {
        public MadeOfStudy()
        {
            this.Students = new HashSet<Student>();
        }

        [Key]
        public int idMadeOfStudy { get; set; }

        public string MadeOfStudyName { get; set; }

        public string MadeOfStudyNameEN { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}