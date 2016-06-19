namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewsJob")]
    public partial class NewsJob
    {
        public NewsJob() { }
        public NewsJob(string company, string requirement)
        {
            this.Company = company;
            this.Requirement = requirement;
        }

        [Key]
        public int IdNewsJob { get; set; }

        public int IdNews { get; set; }

        public string Company { get; set; }

        public string Requirement { get; set; }

        public virtual News News { get; set; }
    }
}
