using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class ProvinceBo
    {
        DSAContext context = new DSAContext();
        public List<Province> GetListCourse(string idState)
        {
            return context.Provinces.Where(p=>p.IdState.Equals(idState)).ToList();
        }
    }
}