namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DecentralizationGroup")]
    public partial class DecentralizationGroup
    {
        public DecentralizationGroup()
        {
            this.Accounts = new HashSet<Account>();
            this.ConductItemGroupRoles = new HashSet<ConductItemGroupRole>();
            this.ConductSchedules = new HashSet<ConductSchedule>();
            this.Decentralizations = new HashSet<Decentralization>();
        }

        public DecentralizationGroup(string nameGroup)
        {
            this.DecentralizationGroupName = nameGroup;
        }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<ConductItemGroupRole> ConductItemGroupRoles { get; set; }

        public virtual ICollection<ConductSchedule> ConductSchedules { get; set; }

        public string DecentralizationGroupName { get; set; }

        public virtual ICollection<Decentralization> Decentralizations { get; set; }

        [Key]
        public int IdDecentralizationGroup { get; set; }
    }
}