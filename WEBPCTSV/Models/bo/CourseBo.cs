using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class CourseBo
    {
        DSAContext context = new DSAContext();
        public List<Course> GetListCourse()
        {
            return context.Courses.ToList();
        }

        public Course Get(string name)
        {
            try {
                return context.Courses.Single(c => c.CouseName.Equals(name));
            }
            catch { }
            return null;
            
        }

        public void Insert(FormCollection form)
        {
            Course course = new Course();
            course.CouseName = form["CourseName"];
            course.AdmissionYear = Convert.ToInt32(form["AdmissionYear"]);
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void Update(int idCourse, FormCollection form)
        {
            Course course = context.Courses.Single(c => c.IdCourse == idCourse);
            course.CouseName = form["CourseName"];
            course.AdmissionYear = Convert.ToInt32(form["AdmissionYear"]);
            context.SaveChanges();
        }

        public void Delete(int idCourse)
        {
            Course course = context.Courses.Single(c => c.IdCourse == idCourse);
            context.Courses.Remove(course);
            context.SaveChanges();
        }
    }
}