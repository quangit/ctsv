namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class AcademicAchievementBo
    {
        readonly DSAContext context = new DSAContext();

        public void Delete(int idAcademicAchievement)
        {
            AcademicAchievement academicAchievement =
                this.context.AcademicAchievements.Single(a => a.IdAcademicAchievement == idAcademicAchievement);
            this.context.AcademicAchievements.Remove(academicAchievement);
            this.context.SaveChanges();
        }

        public List<AcademicAchievement> Get(int idStudent)
        {
            return this.context.AcademicAchievements.Where(a => a.IdStudent == idStudent).ToList();
        }

        public void Insert(int idStudent, FormCollection form)
        {
            AcademicAchievement academicAchievement = new AcademicAchievement();
            academicAchievement.IdStudent = idStudent;
            if (form["selectSemester"] != string.Empty) academicAchievement.IdSemester = Convert.ToInt32(form["selectSemester"]);
            academicAchievement.NameContest = form["name"];
            if (form["selectOrganizationLevel"] != string.Empty) academicAchievement.IdOrganizationLevel = Convert.ToInt32(form["selectOrganizationLevel"]);
            academicAchievement.Reward = form["reward"];
            this.context.AcademicAchievements.Add(academicAchievement);
            this.context.SaveChanges();
        }

        public void Update(int idAcademicAchievement, FormCollection form)
        {
            AcademicAchievement academicAchievement =
                this.context.AcademicAchievements.Single(a => a.IdAcademicAchievement == idAcademicAchievement);
            if (form["selectSemester"] != string.Empty) academicAchievement.IdSemester = Convert.ToInt32(form["selectSemester"]);
            academicAchievement.NameContest = form["name"];
            if (form["selectOrganizationLevel"] != string.Empty) academicAchievement.IdOrganizationLevel = Convert.ToInt32(form["selectOrganizationLevel"]);
            academicAchievement.Reward = form["reward"];
            this.context.SaveChanges();
        }
    }
}