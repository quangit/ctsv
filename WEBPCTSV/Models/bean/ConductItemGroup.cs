namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConductItemGroup")]
    public partial class ConductItemGroup
    {
        public ConductItemGroup()
        {
            ConductItemGroupRoles = new HashSet<ConductItemGroupRole>();
            ConductItems = new HashSet<ConductItem>();
        }

        [Key]
        public int IdConductItemGroup { get; set; }

        [Required]
        [StringLength(128)]
        public string ConductItemGroupName { get; set; }

        public virtual ICollection<ConductItemGroupRole> ConductItemGroupRoles { get; set; }

        public virtual ICollection<ConductItem> ConductItems { get; set; }
    }
}
