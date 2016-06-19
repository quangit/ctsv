using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class StudentSocialActivityBo
    {
        DSAContext context = new DSAContext();
        int rowInPage = new Configuration().rowInPage;
        public void Delete(int idStudentSocialActivity)
        {
            StudentSocialActivity studentSocialActivity = context.StudentSocialActivities.Single(s => s.IdStudentSocialActivities == idStudentSocialActivity);
            context.StudentSocialActivities.Remove(studentSocialActivity);
            context.SaveChanges();
        }

        public List<SocialActivity> GetListSocialActivityNotStudent(int page, int idStudent)
        {
            List<StudentSocialActivity> listStudentSocialActivity = context.StudentSocialActivities.Where(s => s.IdStudent == idStudent).ToList();
            List<int> listIdSocialActivity = new List<int>();
            foreach (StudentSocialActivity s in listStudentSocialActivity) listIdSocialActivity.Add(s.IdSocialActivity);
            return context.SocialActivities.Where(s => !listIdSocialActivity.Contains(s.IdSocialActivity)).OrderByDescending(st => st.IdSocialActivity).Skip((page - 1) * rowInPage).Take(rowInPage).ToList();
        }

        

        public void Add(int idStudent, int idSocialActivity)
        {
            StudentSocialActivity studentSocialActivity = new StudentSocialActivity();
            studentSocialActivity.IdStudent = idStudent;
            studentSocialActivity.IdSocialActivity = idSocialActivity;
            context.StudentSocialActivities.Add(studentSocialActivity);
            context.SaveChanges();
        }
    }
}