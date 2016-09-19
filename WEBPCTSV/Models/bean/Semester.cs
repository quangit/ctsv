namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Semester")]
    public partial class Semester
    {
        public Semester()
        {
            this.AcademicAchievements = new HashSet<AcademicAchievement>();
            this.Borrowings = new HashSet<Borrowing>();
            this.ConductEvents = new HashSet<ConductEvent>();
            this.LearningOutComes = new HashSet<LearningOutCome>();
            this.LecturerClasses = new HashSet<LecturerClass>();
            this.LecturerDocumentSemesters = new HashSet<LecturerDocumentSemester>();
            this.RewardDiscipLines = new HashSet<RewardDiscipLine>();
            this.StudentShipCompanies = new HashSet<StudentShipCompany>();
            this.StudentShipSchools = new HashSet<StudentShipSchool>();
            this.StudyingAboads = new HashSet<StudyingAboad>();
            this.StudentshipApplications = new HashSet<StudentshipApplication>();
        }

        public virtual ICollection<AcademicAchievement> AcademicAchievements { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BeginTime { get; set; }

        public virtual ICollection<Borrowing> Borrowings { get; set; }

        public virtual ICollection<ConductEvent> ConductEvents { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndTime { get; set; }

        [Key]
        public int IdSemester { get; set; }

        [Required]
        [StringLength(128)]
        public string IdSemesterYear { get; set; }

        public int IdYear { get; set; }

        public virtual ICollection<LearningOutCome> LearningOutComes { get; set; }

        public virtual ICollection<LecturerClass> LecturerClasses { get; set; }

        public virtual ICollection<LecturerDocumentSemester> LecturerDocumentSemesters { get; set; }

        public virtual ICollection<RewardDiscipLine> RewardDiscipLines { get; set; }

        public virtual SemesterYear SemesterYear { get; set; }

        public virtual ICollection<StudentshipApplication> StudentshipApplications { get; set; }

        public virtual ICollection<StudentShipCompany> StudentShipCompanies { get; set; }

        public virtual ICollection<StudentShipSchool> StudentShipSchools { get; set; }

        public virtual ICollection<StudyingAboad> StudyingAboads { get; set; }

        public virtual Year Year { get; set; }
    }
}