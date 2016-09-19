namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class FacultyBO
    {
        readonly DSAContext context = new DSAContext();

        private readonly DSAContext dsaContext;

        public FacultyBO()
        {
        }

        public FacultyBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public void Delete(int idFaculty)
        {
            Faculty faculty = this.context.Faculties.Single(f => f.IdFaculty == idFaculty);
            this.context.Faculties.Remove(faculty);
            this.context.SaveChanges();
        }

        public Faculty Get(string facultyNumber)
        {
            try
            {
                return this.context.Faculties.Single(f => f.NumberFaculty.Equals(facultyNumber));
            }
            catch
            {
            }

            return null;
        }

        public Faculty GetFaculty(int idFaculty)
        {
            try
            {
                return this.dsaContext.Faculties.SingleOrDefault(c => c.IdFaculty == idFaculty);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Faculty> GetListFaculty()
        {
            return this.context.Faculties.ToList();
        }

        public void Insert(FormCollection form)
        {
            Faculty faculty = new Faculty();
            faculty.NumberFaculty = form["NumberFaculty"];
            faculty.FacultyName = form["FacultyName"];
            this.context.Faculties.Add(faculty);
            this.context.SaveChanges();
        }

        public void Update(int idFaculty, FormCollection form)
        {
            Faculty faculty = this.context.Faculties.Single(f => f.IdFaculty == idFaculty);
            faculty.NumberFaculty = form["NumberFaculty"];
            faculty.FacultyName = form["FacultyName"];
            this.context.SaveChanges();
        }
    }
}