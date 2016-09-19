namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RequestStatus")]
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            this.RequestPapers = new HashSet<RequestPaper>();
        }

        public RequestStatus(string statusName)
        {
            this.StatusName = statusName;
        }

        [Key]
        public int IdRequestStatus { get; set; }

        public virtual ICollection<RequestPaper> RequestPapers { get; set; }

        public string StatusName { get; set; }
    }
}