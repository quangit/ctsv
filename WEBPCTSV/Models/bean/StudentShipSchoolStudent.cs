namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentShipSchoolStudent")]
    public partial class StudentShipSchoolStudent
    {
        public int IdLearningOutComes { get; set; }

        public int IdStudentShipSchoolFaculty { get; set; }

        [Key]
        public int IdStudentshipSchoolStudent { get; set; }

        public virtual LearningOutCome LearningOutCome { get; set; }

        [StringLength(15)]
        public string Money { get; set; }

        public string Rank { get; set; }

        public virtual StudentShipSchoolFaculty StudentShipSchoolFaculty { get; set; }
    }
}