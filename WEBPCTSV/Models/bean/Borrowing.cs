namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Borrowing")]
    public partial class Borrowing
    {
        [Key]
        public int IdBorrowing { get; set; }

        public int IdSemester { get; set; }

        public int IdStudent { get; set; }

        public bool? IsBorrowing { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual Student Student { get; set; }
    }
}