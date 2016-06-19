using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class AcademicAchievementBo
    {
        DSAContext context = new DSAContext();
        public List<AcademicAchievement> Get(int idStudent)
        {
            return context.AcademicAchievements.Where(a => a.IdStudent == idStudent).ToList();
        }

        public void Insert(int idStudent , FormCollection form)
        {
            AcademicAchievement academicAchievement = new AcademicAchievement();
            academicAchievement.IdStudent = idStudent;
            if (form["selectSemester"]!="") academicAchievement.IdSemester = Convert.ToInt32(form["selectSemester"]);
            academicAchievement.NameContest = form["name"];
            if (form["selectOrganizationLevel"] != "") academicAchievement.IdOrganizationLevel = Convert.ToInt32(form["selectOrganizationLevel"]);
            academicAchievement.Reward = form["reward"];
            context.AcademicAchievements.Add(academicAchievement);
            context.SaveChanges();
        }
        public void Update(int idAcademicAchievement, FormCollection form)
        {
            AcademicAchievement academicAchievement = context.AcademicAchievements.Single(a => a.IdAcademicAchievement == idAcademicAchievement);
            if (form["selectSemester"] != "") academicAchievement.IdSemester = Convert.ToInt32(form["selectSemester"]);
            academicAchievement.NameContest = form["name"];
            if (form["selectOrganizationLevel"] != "") academicAchievement.IdOrganizationLevel = Convert.ToInt32(form["selectOrganizationLevel"]);
            academicAchievement.Reward = form["reward"];
            context.SaveChanges();
        }

        public void Delete(int idAcademicAchievement)
        {
            AcademicAchievement academicAchievement = context.AcademicAchievements.Single(a => a.IdAcademicAchievement == idAcademicAchievement);
            context.AcademicAchievements.Remove(academicAchievement);
            context.SaveChanges();
        }

    }
}