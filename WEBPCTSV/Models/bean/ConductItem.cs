namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ConductItem
    {
        public ConductItem()
        {
        }

        public ConductItem(int idConductForm, string itemIndex, string itemName, int maxPoints, int idConductItemGroup)
        {
            this.IdConductForm = idConductForm;
            this.ItemIndex = itemIndex;
            this.ItemName = itemName;
            this.MaxPoints = maxPoints;
            this.IdConductItemGroup = idConductItemGroup;
        }

        public virtual ConductForm ConductForm { get; set; }

        public virtual ConductItemGroup ConductItemGroup { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdConductForm { get; set; }

        public int IdConductItemGroup { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdConductItems { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ItemIndex { get; set; }

        [Required]
        public string ItemName { get; set; }

        public int MaxPoints { get; set; }
    }
}