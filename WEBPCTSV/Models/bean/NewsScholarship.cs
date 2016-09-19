namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using WEBPCTSV.Helpers.Common;

    [Table("NewsScholarship")]
    public partial class NewsScholarship
    {
        public NewsScholarship()
        {
        }

        public NewsScholarship(string sponsor, string requirement)
        {
            if (!StringExtension.IsNullOrWhiteSpace(sponsor))
            {
                this.Sponsor = sponsor;
            }

            if (!StringExtension.IsNullOrWhiteSpace(requirement))
            {
                this.Requirement = requirement;
            }
        }

        public int IdNews { get; set; }

        [Key]
        public int IdNewsScholarship { get; set; }

        public virtual News News { get; set; }

        public string Requirement { get; set; }

        public string Sponsor { get; set; }
    }
}