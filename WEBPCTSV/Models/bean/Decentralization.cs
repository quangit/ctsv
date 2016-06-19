namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Decentralization")]
    public partial class Decentralization
    {
        public Decentralization() { }
        public Decentralization(string idFuntion, int idGroup, bool isAccept)
        {
            this.IdFunction = idFuntion;
            this.IdDecentralizationGroup = idGroup;
            this.IsAccept = isAccept;
        }
        [Key]
        public int IdDecentralization { get; set; }

        public bool? IsAccept { get; set; }

        [Required]
        [StringLength(128)]
        public string IdFunction { get; set; }

        public int IdDecentralizationGroup { get; set; }

        public virtual DecentralizationGroup DecentralizationGroup { get; set; }

        public virtual Function Function { get; set; }
    }
}
