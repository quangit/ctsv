namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class YearBo
    {
        readonly DSAContext context = new DSAContext();

        public bool Delete(string id)
        {
            try
            {
                Year year = this.context.Years.Single(s => s.IdYear.Equals(id));
                this.context.Years.Remove(year);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public List<Year> Get()
        {
            return this.context.Years.ToList();
        }

        public bool Insert(FormCollection form)
        {
            try
            {
                Year year = new Year();
                year.YearName = form["YearName"];
                year.IdYear = Convert.ToInt32(form["IdYear"]);
                this.context.Years.Add(year);
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
                Year year = this.context.Years.Single(s => s.IdYear.Equals(id));
                year.YearName = form["YearName"];
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