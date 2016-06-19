using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Models.bo
{
    public class StudentshipApplicationBo
    {
        DSAContext context = new DSAContext();
        public void Add(int idSemester, int idStudent)
        {
            StudentshipApplication studentshipApplication = new StudentshipApplication();
            studentshipApplication.IdSemester = idSemester;
            studentshipApplication.IdStudent = idStudent;
            studentshipApplication.IsConsidered = false;
            context.StudentshipApplications.Add(studentshipApplication);
            context.SaveChanges();
        }

        public void Update(int idStudentshipApplication)
        {
            StudentshipApplication studentshipApplication = context.StudentshipApplications.Single(s => s.IdStudentshipApplication == idStudentshipApplication);
            studentshipApplication.IsConsidered = true;
            context.SaveChanges();
        }

        public List<StudentshipApplication> Get(int idSemester)
        {
            return context.StudentshipApplications.Where(s => s.IdSemester == idSemester).ToList();
        }
    }
}