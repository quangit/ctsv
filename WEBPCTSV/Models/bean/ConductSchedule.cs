namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConductSchedule")]
    public partial class ConductSchedule
    {
        public ConductSchedule()
        {
            ConductResults = new HashSet<ConductResult>();
        }

        public ConductSchedule(int idConductEvent, int idDecentralizationGroup, DateTime begin, DateTime end)
        {
            // TODO: Complete member initialization
            this.IdConductEvent = idConductEvent;
            this.IdDecenTralizationGroup = idDecentralizationGroup;
            this.BeginDateEvaluate = begin;
            this.EndDateEvaluate = end;
        }

        [Key]
        public int IdConductSchedule { get; set; }

        public int IdConductEvent { get; set; }

        public int IdDecenTralizationGroup { get; set; }

        public DateTime BeginDateEvaluate { get; set; }

        public DateTime EndDateEvaluate { get; set; }

        public virtual ConductEvent ConductEvent { get; set; }

        public virtual ICollection<ConductResult> ConductResults { get; set; }

        public virtual DecentralizationGroup DecentralizationGroup { get; set; }
    }
}
