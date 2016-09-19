namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RegencyStudent")]
    public partial class RegencyStudent
    {
        [MaxLength(10)]
        public string IdRegency { get; set; }

        [Key]
        public int IdRegencyStudent { get; set; }

        public int IdStudent { get; set; }

        public virtual Regency Regency { get; set; }

        public virtual Student Student { get; set; }
    }
}