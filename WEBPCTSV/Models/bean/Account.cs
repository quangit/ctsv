namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public Account()
        {
            Lecturers = new HashSet<Lecturer>();
            Messages = new HashSet<Message>();
            Messages1 = new HashSet<Message>();
            RequestPapers = new HashSet<RequestPaper>();
            RequestPapers1 = new HashSet<RequestPaper>();
            Staffs = new HashSet<Staff>();
            StaffDepartmentOfStudentAffairs = new HashSet<StaffDepartmentOfStudentAffair>();
            Students = new HashSet<Student>();
        }

        public Account(string typeAccount, string userName, string password, string idFacebook, string avatar, string email, string timeLock, bool isDelete, int idDecentralizationGroup)
        {
            // TODO: Complete member initialization
            this.TypeAccount = typeAccount;
            this.UserName = userName;
            this.Password = password;
            this.IdFacebook = idFacebook;
            this.Avatar = avatar;
            this.Email = email;
            try
            {
                this.TimeLock = DateTime.Parse(timeLock);
            }
            catch { }
            this.IsDelete = isDelete;
            this.IdDecentralizationGroup = idDecentralizationGroup;
        }

        [Key]
        public int IdAccount { get; set; }

        [StringLength(4)]
        public string TypeAccount { get; set; }

        [StringLength(128)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string IdFacebook { get; set; }

        public string Avatar { get; set; }

        public string Email { get; set; }

        public DateTime? TimeLock { get; set; }

        public bool? IsDelete { get; set; }

        public int IdDecentralizationGroup { get; set; }

        public virtual DecentralizationGroup DecentralizationGroup { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Message> Messages1 { get; set; }

        public virtual ICollection<RequestPaper> RequestPapers { get; set; }

        public virtual ICollection<RequestPaper> RequestPapers1 { get; set; }

        public virtual ICollection<Staff> Staffs { get; set; }

        public virtual ICollection<StaffDepartmentOfStudentAffair> StaffDepartmentOfStudentAffairs { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
    public class AccountSession
    {
        public AccountSession()
        {
        }
        public AccountSession(int idAccount, string username, string typeAccount, string avatar, int idDecentralizationGroup, string fullName, List<string> functions)
        {
            // TODO: Complete member initialization
            this.IdAccount = idAccount;
            this.TypeAccount = typeAccount;
            this.Avatar = avatar;
            this.IdDecentralizationGroup = idDecentralizationGroup;
            this.Functions = functions;
            this.FullName = fullName;
            this.UserName = username;
        }
        [Key]
        public int IdAccount { get; set; }

        public string FullName { get; set; }

        [StringLength(4)]
        public string TypeAccount { get; set; }

        [StringLength(128)]
        public string UserName { get; set; }

        public string Avatar { get; set; }

        public int IdDecentralizationGroup { get; set; }

        public List<string> Functions { get; set; }

    }
}
