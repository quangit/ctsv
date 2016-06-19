namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [Key]
        public int IdStaff { get; set; }

        public int? IdAccount { get; set; }

        public decimal? IdentityCardNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(11)]
        public string PhoneNumber { get; set; }

        public string Image { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public string Field { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public virtual Account Account { get; set; }
    }
}
