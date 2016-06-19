namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecondLanguage")]
    public partial class SecondLanguage
    {
        public SecondLanguage()
        {
            SecondLanguageStudents = new HashSet<SecondLanguageStudent>();
        }

        [Key]
        public string IdSecondLanguage { get; set; }

        [Column("SecondLanguage")]
        public string SecondLanguageName { get; set; }
        public virtual ICollection<SecondLanguageStudent> SecondLanguageStudents { get; set; }
    }
}
