namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class CourseBo
    {
        readonly DSAContext context = new DSAContext();

        public void Delete(int idCourse)
        {
            Course course = this.context.Courses.Single(c => c.IdCourse == idCourse);
            this.context.Courses.Remove(course);
            this.context.SaveChanges();
        }

        public Course Get(string name)
        {
            try
            {
                return this.context.Courses.Single(c => c.CouseName.Equals(name));
            }
            catch
            {
            }

            return null;
        }

        public List<Course> GetListCourse()
        {
            return this.context.Courses.ToList();
        }

        public void Insert(FormCollection form)
        {
            Course course = new Course();
            course.CouseName = form["CourseName"];
            course.AdmissionYear = Convert.ToInt32(form["AdmissionYear"]);
            this.context.Courses.Add(course);
            this.context.SaveChanges();
        }

        public void Update(int idCourse, FormCollection form)
        {
            Course course = this.context.Courses.Single(c => c.IdCourse == idCourse);
            course.CouseName = form["CourseName"];
            course.AdmissionYear = Convert.ToInt32(form["AdmissionYear"]);
            this.context.SaveChanges();
        }
    }
}