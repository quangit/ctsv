namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmailSubscription")]
    public partial class EmailSubscription
    {
        [Key]
        [StringLength(50)]
        public string Email { get; set; }

        public DateTime? SubscribedDate { get; set; }
    }
}
