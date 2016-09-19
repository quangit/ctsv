namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class StudentshipApplicationBo
    {
        readonly DSAContext context = new DSAContext();

        public void Add(int idSemester, int idStudent)
        {
            StudentshipApplication studentshipApplication = new StudentshipApplication();
            studentshipApplication.IdSemester = idSemester;
            studentshipApplication.IdStudent = idStudent;
            studentshipApplication.IsConsidered = false;
            this.context.StudentshipApplications.Add(studentshipApplication);
            this.context.SaveChanges();
        }

        public List<StudentshipApplication> Get(int idSemester)
        {
            return this.context.StudentshipApplications.Where(s => s.IdSemester == idSemester).ToList();
        }

        public void Update(int idStudentshipApplication)
        {
            StudentshipApplication studentshipApplication =
                this.context.StudentshipApplications.Single(s => s.IdStudentshipApplication == idStudentshipApplication);
            studentshipApplication.IsConsidered = true;
            this.context.SaveChanges();
        }
    }
}