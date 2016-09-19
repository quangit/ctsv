namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Relative")]
    public partial class Relative
    {
        public string AddtionalRelativesAddress { get; set; }

        public decimal? CellphoneNumberRelatives { get; set; }

        [Key]
        public int IdRelatives { get; set; }

        public int IdStudent { get; set; }

        [StringLength(5)]
        public string IdWard { get; set; }

        public decimal? LandlineNumberRelatives { get; set; }

        public string RelativeFirstName { get; set; }

        public string relativeName { get; set; }

        public string RelativesLastName { get; set; }

        public virtual Student Student { get; set; }

        public virtual Ward Ward { get; set; }
    }
}