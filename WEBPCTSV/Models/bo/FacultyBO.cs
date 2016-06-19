using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class FacultyBO
    {
        DSAContext context = new DSAContext();
        public FacultyBO()
        {}
        private DSAContext dsaContext;
        public FacultyBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public Faculty GetFaculty(int idFaculty)
        {
            try
            {
                return dsaContext.Faculties.SingleOrDefault(c => c.IdFaculty == idFaculty);
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        public List<Faculty> GetListFaculty()
        {

            return context.Faculties.ToList();
        }

        public Faculty Get(string facultyNumber)
        {
            try {
                return context.Faculties.Single(f => f.NumberFaculty.Equals(facultyNumber));
            }
            catch { }
            return null;
            
        }


        public void Insert(FormCollection form)
        {
            Faculty faculty = new Faculty();
            faculty.NumberFaculty = form["NumberFaculty"];
            faculty.FacultyName = form["FacultyName"];
            context.Faculties.Add(faculty);
            context.SaveChanges();
        }

        public void Update(int idFaculty, FormCollection form)
        {
            Faculty faculty = context.Faculties.Single(f => f.IdFaculty == idFaculty);
            faculty.NumberFaculty = form["NumberFaculty"];
            faculty.FacultyName = form["FacultyName"];
            context.SaveChanges();
        }

        public void Delete(int idFaculty)
        {
            Faculty faculty = context.Faculties.Single(f => f.IdFaculty == idFaculty);
            context.Faculties.Remove(faculty);
            context.SaveChanges();
        }
    }
}