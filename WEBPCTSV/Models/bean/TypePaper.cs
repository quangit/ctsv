namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypePaper")]
    public partial class TypePaper
    {
        
        public TypePaper()
        {
            Typepaperstudents = new HashSet<TypePaperStudent>();
        }

        [Key]
        public int IdTypePaper { get; set; }

        public string TypePaperName { get; set; }

        public string Note { get; set; }

        public string Link { get; set; }

        
        public virtual ICollection<TypePaperStudent> Typepaperstudents { get; set; }
    }
}
