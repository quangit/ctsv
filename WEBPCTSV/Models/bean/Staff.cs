namespace WEBPCTSV.Models.bean
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Staff")]
    public partial class Staff
    {
        public virtual Account Account { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public string Field { get; set; }

        public int? IdAccount { get; set; }

        public decimal? IdentityCardNumber { get; set; }

        [Key]
        public int IdStaff { get; set; }

        public string Image { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }
    }
}