namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Paper")]
    public partial class Paper
    {
        public Paper()
        {
            ReasonRequests = new HashSet<ReasonRequest>();
        }

        [Key]
        public int IdPaper { get; set; }

        public string PaperName { get; set; }

        public string ContentPaper { get; set; }

        public string Note { get; set; }

        public string PriceUrgency { get; set; }

        public string PriceNormal { get; set; }

        public int WaittingPeriodUrgency { get; set; }

        public int WaittingPeriodNormal { get; set; }

        public virtual ICollection<ReasonRequest> ReasonRequests { get; set; }
    }
}
