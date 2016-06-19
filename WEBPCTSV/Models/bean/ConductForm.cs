namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConductForm")]
    public partial class ConductForm
    {
        public ConductForm()
        {
            ConductEvents = new HashSet<ConductEvent>();
            ConductItems = new HashSet<ConductItem>();
        }

        public ConductForm(string name, string note)
        {
            this.Name = name;
            this.Note = note;
        }

        [Key]
        public int IdConductForm { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Note { get; set; }

        public virtual ICollection<ConductEvent> ConductEvents { get; set; }

        public virtual ICollection<ConductItem> ConductItems { get; set; }
    }
}
