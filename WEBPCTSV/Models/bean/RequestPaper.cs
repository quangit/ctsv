namespace WEBPCTSV.Models.bean
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RequestPaper")]
    public partial class RequestPaper
    {
        public virtual Account AccountProcess { get; set; }

        public virtual Account AccountRequest { get; set; }

        public string ContentReason { get; set; }

        public int? IdAccountProcess { get; set; }

        public int IdAccountRequest { get; set; }

        public int? IdReasonRequest { get; set; }

        [Key]
        public int IdRequestPaper { get; set; }

        public int IdRequestStatus { get; set; }

        public bool isImportant { get; set; }

        public bool? IsPrint { get; set; }

        public int NumberPaper { get; set; }

        public virtual ReasonRequest Reasonrequest { get; set; }

        public virtual RequestStatus Requeststatus { get; set; }

        public DateTime? TimeReceivePaper { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TimeRequest { get; set; }
    }
}