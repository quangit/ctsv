namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RequestStatu
    {
        public RequestStatu()
        {
            RequestPapers = new HashSet<RequestPaper>();
        }

        [Key]
        public int IdRequestStatus { get; set; }

        public string StatusName { get; set; }

        public virtual ICollection<RequestPaper> RequestPapers { get; set; }
    }
}
