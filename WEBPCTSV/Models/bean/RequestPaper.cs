namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestPaper")]
    public partial class RequestPaper
    {
        [Key]
        public int IdRequestPaper { get; set; }

        public string ContentReason { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TimeRequest { get; set; }
        public DateTime? TimeReceivePaper{ get; set; }

        public int NumberPaper { get; set; }

        public bool isImportant { get; set; }

        public int? IdReasonRequest { get; set; }

        public int IdRequestStatus { get; set; }

        public int IdAccountRequest { get; set; }

        public int? IdAccountProcess { get; set; }

        public bool? IsPrint { get;set;}

        public virtual Account AccountRequest { get; set; }

        public virtual Account AccountProcess { get; set; }

        public virtual ReasonRequest Reasonrequest { get; set; }

        public virtual RequestStatus Requeststatus { get; set; }

    }
}
