namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReasonRequest")]
    public partial class ReasonRequest
    {
        public ReasonRequest()
        {
            Requestpapers = new HashSet<RequestPaper>();
        }

        [Key]
        public int IdReasonRequest { get; set; }

        public string Reason { get; set; }

        public string Note { get; set; }

        public int IdPaper { get; set; }

        public virtual Paper Paper { get; set; }

        public virtual ICollection<RequestPaper> Requestpapers { get; set; }
    }
}
