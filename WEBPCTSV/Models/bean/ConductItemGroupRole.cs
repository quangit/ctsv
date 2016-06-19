namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConductItemGroupRole")]
    public partial class ConductItemGroupRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdConductItemGroupRole { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdDecentralizationGroup { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdConductItemGroup { get; set; }

        public virtual ConductItemGroup ConductItemGroup { get; set; }

        public virtual DecentralizationGroup DecentralizationGroup { get; set; }
    }
}
