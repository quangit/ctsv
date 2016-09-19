namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class SemesterBO
    {
        private readonly DSAContext context = new DSAContext();

        public SemesterBO()
        {
        }

        public SemesterBO(DSAContext dsaContext)
        {
            this.context = dsaContext;
        }

        public bool Delete(int id)
        {
            try
            {
                Semester semester = this.context.Semesters.Single(s => s.IdSemester == id);
                this.context.Semesters.Remove(semester);
                this.context.SaveChanges();
            }
            catch
            {
            }

            return false;
        }

        public List<Semester> Get()
        {
            try
            {
                List<Semester> listSemester = this.context.Semesters.OrderByDescending(s => s.IdSemester).ToList();
                return listSemester;
            }
            catch
            {
            }

            return null;
        }

        public Semester GetBeforeSemester()
        {
            return null;
        }

        public Semester GetSemesterById(int idSemester)
        {
            return this.context.Semesters.Find(idSemester);
        }

        public List<Semester> GetSemesterByYear(int admissionYear)
        {
            return
                this.context.Semesters.Where(
                    s => s.IdYear >= admissionYear && s.BeginTime <= DateTime.Now && !s.IdSemesterYear.Equals("he"))
                    .ToList();
        }

        public Semester GetSemesterCurrent()
        {
            return this.context.Semesters.SingleOrDefault(s => s.BeginTime <= DateTime.Now && s.EndTime >= DateTime.Now);
        }

        public List<Semester> GetYearConduct()
        {
            List<Semester> listSemester =
                this.context.Semesters.Where(
                    s => s.SemesterYear.SemesterYearName.Equals("I") || s.SemesterYear.SemesterYearName.Equals("II"))
                    .OrderByDescending(s => s.IdSemester)
                    .ToList();
            return listSemester;
        }

        public bool Insert()
        {
            try
            {
                List<Semester> listSemester = this.context.Semesters.ToList();
                Semester semester = listSemester.Last();
                Semester newSemester = new Semester();
                if (semester.IdSemesterYear.Equals("1"))
                {
                    newSemester.IdSemesterYear = "2";
                    newSemester.IdYear = semester.IdYear;
                    SemesterYear semesterYear = this.context.SemesterYears.Single(s => s.IdSemesterYear.Equals("2"));
                    newSemester.BeginTime = Convert.ToDateTime((semester.Year.IdYear + 1) + semesterYear.BeginTime);
                    newSemester.EndTime = Convert.ToDateTime((semester.Year.IdYear + 1) + semesterYear.EndTime);
                }

                if (semester.IdSemesterYear.Equals("2"))
                {
                    newSemester.IdSemesterYear = "he";
                    newSemester.IdYear = semester.IdYear;
                    SemesterYear semesterYear = this.context.SemesterYears.Single(s => s.IdSemesterYear.Equals("he"));
                    newSemester.BeginTime = Convert.ToDateTime((semester.Year.IdYear + 1) + semesterYear.BeginTime);
                    newSemester.EndTime = Convert.ToDateTime((semester.Year.IdYear + 1) + semesterYear.EndTime);
                }

                if (semester.IdSemesterYear.Equals("he"))
                {
                    newSemester.IdSemesterYear = "1";
                    int idyear = Convert.ToInt16(semester.IdYear);
                    newSemester.IdYear = idyear + 1;
                    SemesterYear semesterYear = this.context.SemesterYears.Single(s => s.IdSemesterYear.Equals("1"));
                    newSemester.BeginTime = Convert.ToDateTime((semester.Year.IdYear + 1) + semesterYear.BeginTime);
                    newSemester.EndTime = Convert.ToDateTime((semester.Year.IdYear + 2) + semesterYear.EndTime);
                }

                this.context.Semesters.Add(newSemester);
                this.context.SaveChanges();
            }
            catch
            {
            }

            return false;
        }

        public bool Update(int id, FormCollection form)
        {
            try
            {
                Semester semester = this.context.Semesters.Single(s => s.IdSemester == id);
                semester.BeginTime = Convert.ToDateTime(form["begintime"]);
                semester.EndTime = Convert.ToDateTime(form["endtime"]);
                this.context.SaveChanges();
            }
            catch
            {
            }

            return false;
        }
    }
}