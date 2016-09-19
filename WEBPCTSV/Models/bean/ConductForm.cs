namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ConductForm")]
    public partial class ConductForm
    {
        public ConductForm()
        {
            this.ConductEvents = new HashSet<ConductEvent>();
            this.ConductItems = new HashSet<ConductItem>();
        }

        public ConductForm(string name, string note)
        {
            this.Name = name;
            this.Note = note;
        }

        public virtual ICollection<ConductEvent> ConductEvents { get; set; }

        public virtual ICollection<ConductItem> ConductItems { get; set; }

        [Key]
        public int IdConductForm { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Note { get; set; }
    }
}