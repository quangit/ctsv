namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DecentralizationGroup")]
    public partial class DecentralizationGroup
    {
        public DecentralizationGroup()
        {
            Accounts = new HashSet<Account>();
            ConductItemGroupRoles = new HashSet<ConductItemGroupRole>();
            ConductSchedules = new HashSet<ConductSchedule>();
            Decentralizations = new HashSet<Decentralization>();
        }
        public DecentralizationGroup(string nameGroup)
        {
            this.DecentralizationGroupName = nameGroup;
        }

        [Key]
        public int IdDecentralizationGroup { get; set; }

        public string DecentralizationGroupName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<ConductItemGroupRole> ConductItemGroupRoles { get; set; }

        public virtual ICollection<ConductSchedule> ConductSchedules { get; set; }

        public virtual ICollection<Decentralization> Decentralizations { get; set; }
    }
}
