namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentShipCLC")]
    public class StudentShipCLC
    {
        [Key]
        public string idStudentShipCLC { get; set; }

        public double Percent { get; set; }
    }
}