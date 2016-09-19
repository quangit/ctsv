namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ConductItemGroup")]
    public partial class ConductItemGroup
    {
        public ConductItemGroup()
        {
            this.ConductItemGroupRoles = new HashSet<ConductItemGroupRole>();
            this.ConductItems = new HashSet<ConductItem>();
        }

        [Required]
        [StringLength(128)]
        public string ConductItemGroupName { get; set; }

        public virtual ICollection<ConductItemGroupRole> ConductItemGroupRoles { get; set; }

        public virtual ICollection<ConductItem> ConductItems { get; set; }

        [Key]
        public int IdConductItemGroup { get; set; }
    }
}