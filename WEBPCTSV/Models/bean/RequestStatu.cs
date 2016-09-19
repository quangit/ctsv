namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class RequestStatu
    {
        public RequestStatu()
        {
            this.RequestPapers = new HashSet<RequestPaper>();
        }

        [Key]
        public int IdRequestStatus { get; set; }

        public virtual ICollection<RequestPaper> RequestPapers { get; set; }

        public string StatusName { get; set; }
    }
}