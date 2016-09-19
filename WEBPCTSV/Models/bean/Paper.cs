namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Paper")]
    public partial class Paper
    {
        public Paper()
        {
            this.ReasonRequests = new HashSet<ReasonRequest>();
        }

        public string ContentPaper { get; set; }

        [Key]
        public int IdPaper { get; set; }

        public string Note { get; set; }

        public string PaperName { get; set; }

        public string PriceNormal { get; set; }

        public string PriceUrgency { get; set; }

        public virtual ICollection<ReasonRequest> ReasonRequests { get; set; }

        public int WaittingPeriodNormal { get; set; }

        public int WaittingPeriodUrgency { get; set; }
    }
}