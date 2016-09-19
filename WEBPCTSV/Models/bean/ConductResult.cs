namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ConductResult")]
    public partial class ConductResult
    {
        public ConductResult()
        {
        }

        public ConductResult(
            int idStudent,
            int idConductSchedule,
            int idConductEvent,
            bool isApproved,
            bool isSaved,
            string partialPoint)
        {
            this.IdStudent = idStudent;
            this.IdConductSchedule = idConductSchedule;
            this.IdConductEvent = idConductEvent;
            this.IsApproved = isApproved;
            this.IsSaved = isSaved;
            this.PartialPoint = partialPoint;
        }

        public virtual ConductEvent ConductEvent { get; set; }

        public virtual ConductSchedule ConductSchedule { get; set; }

        public int IdConductEvent { get; set; }

        [Key]
        public int IdConductResult { get; set; }

        public int IdConductSchedule { get; set; }

        public int IdStudent { get; set; }

        public bool IsApproved { get; set; }

        public bool? IsDeleted { get; set; }

        public bool IsSaved { get; set; }

        [Required]
        [StringLength(128)]
        public string PartialPoint { get; set; }

        public virtual Student Student { get; set; }
    }

    public class ConductResultSemester
    {
        public ConductResultSemester(int idSemester, string semesterName, string semesterYear, int point)
        {
            this.SemesterName = semesterName;
            this.SemesterYear = semesterYear;
            this.Point = point;
            this.IdSemester = idSemester;
        }

        public int IdSemester { get; set; }

        public int Point { get; set; }

        public string SemesterName { get; set; }

        public string SemesterYear { get; set; }
    }
}