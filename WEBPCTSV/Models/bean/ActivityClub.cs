namespace WEBPCTSV.Models.bean
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ActivityClub")]
    public partial class ActivityClub
    {
        public DateTime? BeginTime { get; set; }

        public virtual Club Club { get; set; }

        public DateTime? EndTime { get; set; }

        [Key]
        public int IdActivityClub { get; set; }

        public int IdClub { get; set; }

        public string Note { get; set; }

        public string Place { get; set; }
    }
}