namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("NewsJob")]
    public partial class NewsJob
    {
        public NewsJob()
        {
        }

        public NewsJob(string company, string requirement)
        {
            this.Company = company;
            this.Requirement = requirement;
        }

        public string Company { get; set; }

        public int IdNews { get; set; }

        [Key]
        public int IdNewsJob { get; set; }

        public virtual News News { get; set; }

        public string Requirement { get; set; }
    }
}