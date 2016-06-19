namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConductEvent")]
    public partial class ConductEvent
    {
        public ConductEvent()
        {
            ConductResults = new HashSet<ConductResult>();
            ConductSchedules = new HashSet<ConductSchedule>();
        }

        public ConductEvent(int idSemester, int idConductForm)
        {
            // TODO: Complete member initialization
            this.IdSemester = idSemester;
            this.IdConductForm = idConductForm;
        }

        [Key]
        public int IdConductEvent { get; set; }

        public int IdSemester { get; set; }

        public int IdConductForm { get; set; }

        public virtual ConductForm ConductForm { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual ICollection<ConductResult> ConductResults { get; set; }

        public virtual ICollection<ConductSchedule> ConductSchedules { get; set; }
    }
}
