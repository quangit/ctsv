using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class YearBo
    {
        DSAContext context = new DSAContext();
        public List<Year> Get()
        {
            return context.Years.ToList();
        }

        public bool Insert( FormCollection form)
        {
            try {
                Year year = new Year();
                year.YearName = form["YearName"];
                year.IdYear =Convert.ToInt32(form["IdYear"]);
                context.Years.Add(year);
                return true;
            }
            catch { }
            return false;
        }

        public bool Update(string id, FormCollection form)
        {
            try {
                Year year = context.Years.Single(s => s.IdYear.Equals(id));
                year.YearName = form["YearName"];
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public bool Delete( string id)
        {
            try {
                Year year = context.Years.Single(s => s.IdYear.Equals(id));
                context.Years.Remove(year);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }
    }
}