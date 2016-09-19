namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ConductEvent")]
    public partial class ConductEvent
    {
        public ConductEvent()
        {
            this.ConductResults = new HashSet<ConductResult>();
            this.ConductSchedules = new HashSet<ConductSchedule>();
        }

        public ConductEvent(int idSemester, int idConductForm)
        {
            // TODO: Complete member initialization
            this.IdSemester = idSemester;
            this.IdConductForm = idConductForm;
        }

        public virtual ConductForm ConductForm { get; set; }

        public virtual ICollection<ConductResult> ConductResults { get; set; }

        public virtual ICollection<ConductSchedule> ConductSchedules { get; set; }

        [Key]
        public int IdConductEvent { get; set; }

        public int IdConductForm { get; set; }

        public int IdSemester { get; set; }

        public virtual Semester Semester { get; set; }
    }
}