namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SecondLanguage")]
    public partial class SecondLanguage
    {
        public SecondLanguage()
        {
            this.SecondLanguageStudents = new HashSet<SecondLanguageStudent>();
        }

        [Key]
        public string IdSecondLanguage { get; set; }

        [Column("SecondLanguage")]
        public string SecondLanguageName { get; set; }

        public virtual ICollection<SecondLanguageStudent> SecondLanguageStudents { get; set; }
    }
}