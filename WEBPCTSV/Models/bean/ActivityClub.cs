namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActivityClub")]
    public partial class ActivityClub
    {
        [Key]
        public int IdActivityClub { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Place { get; set; }

        public string Note { get; set; }

        public int IdClub { get; set; }

        public virtual Club Club { get; set; }
    }
}
