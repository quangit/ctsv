namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestStatus")]
    public partial class RequestStatus
    {
       
        public RequestStatus()
        {
            RequestPapers = new HashSet<RequestPaper>();
        }

        public RequestStatus(string statusName)
        {
            this.StatusName = statusName;
        }

        [Key]
        public int IdRequestStatus { get; set; }

        public string StatusName { get; set; }

        
        public virtual ICollection<RequestPaper> RequestPapers { get; set; }
    }
}
