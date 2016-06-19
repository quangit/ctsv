using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bean
{
    [Table("StudentShipCLC")]
    public class StudentShipCLC
    {
        [Key]
        public string idStudentShipCLC { get; set; }
        public double Percent { get; set; }
    }
}