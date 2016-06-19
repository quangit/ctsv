namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using WEBPCTSV.Helpers.Common;

    [Table("LecturerClass")]
    public partial class LecturerClass
    {
        public LecturerClass()
        {
        }
        public LecturerClass(int idLecturer, int idSemester, int idClass, string endDate, string scheduleFirst, string roomFirst, string scheduleSecond, string roomSecond)
        {
            // TODO: Complete member initialization
            this.IdLecturer = idLecturer;
            this.IdSemester = idSemester;
            this.IdClass = idClass;
            try
            {
                this.EndDate = DateTime.Parse(endDate);
            }
            catch { }
            try
            {
                this.ScheduleFirst = StringExtension.ConvertStringToDateTime(scheduleFirst, "yyyy-MM-dd");
            }
            catch { }
            try
            {
                this.ScheduleSecond = StringExtension.ConvertStringToDateTime(scheduleSecond, "yyyy-MM-dd");
            }
            catch { }

            if (!string.IsNullOrEmpty(roomFirst))
            {
                this.RoomFirst = roomFirst;
            }
            if (!string.IsNullOrEmpty(roomSecond))
            {
                this.RoomSecond = roomSecond;
            }
        }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSemester { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdClass { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime EndDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLecturerClass { get; set; }

        public int IdLecturer { get; set; }

        public bool? IsEvaluated { get; set; }

        public DateTime? ScheduleFirst { get; set; }

        [StringLength(50)]
        public string RoomFirst { get; set; }

        public DateTime? ScheduleSecond { get; set; }

        [StringLength(50)]
        public string RoomSecond { get; set; }

        public virtual Class Class { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        public virtual Semester Semester { get; set; }
    }
}
