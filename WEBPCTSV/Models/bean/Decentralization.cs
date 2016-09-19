namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Decentralization")]
    public partial class Decentralization
    {
        public Decentralization()
        {
        }

        public Decentralization(string idFuntion, int idGroup, bool isAccept)
        {
            this.IdFunction = idFuntion;
            this.IdDecentralizationGroup = idGroup;
            this.IsAccept = isAccept;
        }

        public virtual DecentralizationGroup DecentralizationGroup { get; set; }

        public virtual Function Function { get; set; }

        [Key]
        public int IdDecentralization { get; set; }

        public int IdDecentralizationGroup { get; set; }

        [Required]
        [StringLength(128)]
        public string IdFunction { get; set; }

        public bool? IsAccept { get; set; }
    }
}