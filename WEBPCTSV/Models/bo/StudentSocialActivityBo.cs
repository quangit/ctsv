namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.Support;

    public class StudentSocialActivityBo
    {
        readonly DSAContext context = new DSAContext();

        readonly int rowInPage = new Configuration().rowInPage;

        public void Add(int idStudent, int idSocialActivity)
        {
            StudentSocialActivity studentSocialActivity = new StudentSocialActivity();
            studentSocialActivity.IdStudent = idStudent;
            studentSocialActivity.IdSocialActivity = idSocialActivity;
            this.context.StudentSocialActivities.Add(studentSocialActivity);
            this.context.SaveChanges();
        }

        public void Delete(int idStudentSocialActivity)
        {
            StudentSocialActivity studentSocialActivity =
                this.context.StudentSocialActivities.Single(s => s.IdStudentSocialActivities == idStudentSocialActivity);
            this.context.StudentSocialActivities.Remove(studentSocialActivity);
            this.context.SaveChanges();
        }

        public List<SocialActivity> GetListSocialActivityNotStudent(int page, int idStudent)
        {
            List<StudentSocialActivity> listStudentSocialActivity =
                this.context.StudentSocialActivities.Where(s => s.IdStudent == idStudent).ToList();
            List<int> listIdSocialActivity = new List<int>();
            foreach (StudentSocialActivity s in listStudentSocialActivity) listIdSocialActivity.Add(s.IdSocialActivity);
            return
                this.context.SocialActivities.Where(s => !listIdSocialActivity.Contains(s.IdSocialActivity))
                    .OrderByDescending(st => st.IdSocialActivity)
                    .Skip((page - 1) * this.rowInPage)
                    .Take(this.rowInPage)
                    .ToList();
        }
    }
}