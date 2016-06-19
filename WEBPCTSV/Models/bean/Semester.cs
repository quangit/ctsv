namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Semester")]
    public partial class Semester
    {
        public Semester()
        {
            AcademicAchievements = new HashSet<AcademicAchievement>();
            Borrowings = new HashSet<Borrowing>();
            ConductEvents = new HashSet<ConductEvent>();
            LearningOutComes = new HashSet<LearningOutCome>();
            LecturerClasses = new HashSet<LecturerClass>();
            LecturerDocumentSemesters = new HashSet<LecturerDocumentSemester>();
            RewardDiscipLines = new HashSet<RewardDiscipLine>();
            StudentShipCompanies = new HashSet<StudentShipCompany>();
            StudentShipSchools = new HashSet<StudentShipSchool>();
            StudyingAboads = new HashSet<StudyingAboad>();
            StudentshipApplications = new HashSet<StudentshipApplication>();
        }

        [Key]
        public int IdSemester { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BeginTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndTime { get; set; }

        public int IdYear { get; set; }

        [Required]
        [StringLength(128)]
        public string IdSemesterYear { get; set; }

        public virtual ICollection<AcademicAchievement> AcademicAchievements { get; set; }

        public virtual ICollection<Borrowing> Borrowings { get; set; }

        public virtual ICollection<ConductEvent> ConductEvents { get; set; }

        public virtual ICollection<LearningOutCome> LearningOutComes { get; set; }
        public virtual ICollection<StudentshipApplication> StudentshipApplications { get; set; }

        public virtual ICollection<LecturerClass> LecturerClasses { get; set; }

        public virtual ICollection<LecturerDocumentSemester> LecturerDocumentSemesters { get; set; }

        public virtual ICollection<RewardDiscipLine> RewardDiscipLines { get; set; }

        public virtual SemesterYear SemesterYear { get; set; }

        public virtual Year Year { get; set; }

        public virtual ICollection<StudentShipCompany> StudentShipCompanies { get; set; }

        public virtual ICollection<StudentShipSchool> StudentShipSchools { get; set; }

        public virtual ICollection<StudyingAboad> StudyingAboads { get; set; }
    }
}
