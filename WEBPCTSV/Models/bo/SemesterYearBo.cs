using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class SemesterYearBo
    {
        DSAContext context = new DSAContext();
        public List<SemesterYear> Get()
        {
            return context.SemesterYears.ToList();
        }

        public bool Insert(FormCollection form)
        {
            try {
                SemesterYear semesterYear = new SemesterYear();
                semesterYear.SemesterYearName = form["Semester"];
                semesterYear.IdSemesterYear = form["Semester"];
                context.SemesterYears.Add(semesterYear);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public bool Update(string id, FormCollection form)
        {
            try {
                SemesterYear semesterYear = context.SemesterYears.Single(s => s.IdSemesterYear.Equals(id));
                semesterYear.SemesterYearName = form["Semester"];
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public bool Delete(string id)
        {
            try {
                SemesterYear semesterYear = context.SemesterYears.Single(s => s.IdSemesterYear.Equals(id));
                context.SemesterYears.Remove(semesterYear);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }
    }
}