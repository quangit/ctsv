namespace WEBPCTSV.Models.bean
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using WEBPCTSV.Helpers.Common;

    [Table("NewsEvent")]
    public partial class NewsEvent
    {
        public NewsEvent()
        {
        }

        public NewsEvent(string eventTime, string eventVenue, string requirement, string beginDate, string endDate)
        {
            if (!StringExtension.IsNullOrWhiteSpace(eventTime))
            {
                this.EventTime = eventTime;
            }

            if (!StringExtension.IsNullOrWhiteSpace(eventVenue))
            {
                this.EventVenue = eventVenue;
            }

            if (!StringExtension.IsNullOrWhiteSpace(requirement))
            {
                this.Requirement = requirement;
            }

            try
            {
                this.BeginDate = DateTime.Parse(beginDate);
            }
            catch (Exception)
            {
            }

            try
            {
                this.EndDate = DateTime.Parse(endDate);
            }
            catch (Exception)
            {
            }
        }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string EventTime { get; set; }

        public string EventVenue { get; set; }

        public int IdNews { get; set; }

        [Key]
        public int IdNewsEvent { get; set; }

        public virtual News News { get; set; }

        public string Requirement { get; set; }
    }
}