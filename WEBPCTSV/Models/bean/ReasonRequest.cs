namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ReasonRequest")]
    public partial class ReasonRequest
    {
        public ReasonRequest()
        {
            this.Requestpapers = new HashSet<RequestPaper>();
        }

        public int IdPaper { get; set; }

        [Key]
        public int IdReasonRequest { get; set; }

        public string Note { get; set; }

        public virtual Paper Paper { get; set; }

        public string Reason { get; set; }

        public virtual ICollection<RequestPaper> Requestpapers { get; set; }
    }
}