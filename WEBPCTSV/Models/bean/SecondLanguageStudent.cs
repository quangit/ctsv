namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SecondLanguageStudent")]
    public partial class SecondLanguageStudent
    {
        [Required]
        [StringLength(128)]
        public string IdSecondLanguage { get; set; }

        [Key]
        public int IdSecondLanguageStudent { get; set; }

        public int IdStudent { get; set; }

        public virtual SecondLanguage SecondLanguage { get; set; }

        public virtual Student Student { get; set; }
    }
}