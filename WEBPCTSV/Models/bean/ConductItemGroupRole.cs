namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ConductItemGroupRole")]
    public partial class ConductItemGroupRole
    {
        public virtual ConductItemGroup ConductItemGroup { get; set; }

        public virtual DecentralizationGroup DecentralizationGroup { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdConductItemGroup { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdConductItemGroupRole { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdDecentralizationGroup { get; set; }
    }
}