namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class SemesterYearBo
    {
        readonly DSAContext context = new DSAContext();

        public bool Delete(string id)
        {
            try
            {
                SemesterYear semesterYear = this.context.SemesterYears.Single(s => s.IdSemesterYear.Equals(id));
                this.context.SemesterYears.Remove(semesterYear);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public List<SemesterYear> Get()
        {
            return this.context.SemesterYears.ToList();
        }

        public bool Insert(FormCollection form)
        {
            try
            {
                SemesterYear semesterYear = new SemesterYear();
                semesterYear.SemesterYearName = form["Semester"];
                semesterYear.IdSemesterYear = form["Semester"];
                this.context.SemesterYears.Add(semesterYear);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public bool Update(string id, FormCollection form)
        {
            try
            {
                SemesterYear semesterYear = this.context.SemesterYears.Single(s => s.IdSemesterYear.Equals(id));
                semesterYear.SemesterYearName = form["Semester"];
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }
    }
}