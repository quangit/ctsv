namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Borrowing")]
    public partial class Borrowing
    {
        [Key]
        public int IdBorrowing { get; set; }

        public bool? IsBorrowing { get; set; }

        public int IdStudent { get; set; }

        public int IdSemester { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual Student Student { get; set; }
    }
}
