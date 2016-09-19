namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentshipApplication")]
    public class StudentshipApplication
    {
        public StudentshipApplication()
        {
        }

        public int IdSemester { get; set; }

        public int IdStudent { get; set; }

        [Key]
        public int IdStudentshipApplication { get; set; }

        public bool IsConsidered { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual Student Student { get; set; }
    }
}