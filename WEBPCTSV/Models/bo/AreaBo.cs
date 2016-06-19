using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class AreaBo
    {
        DSAContext context = new DSAContext();
        public List<Area> GetListArea()
        {
            return context.Areas.ToList();
        }
    }
}