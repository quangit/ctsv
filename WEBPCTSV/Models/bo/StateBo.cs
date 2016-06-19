using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class StateBo
    {
        DSAContext context = new DSAContext();
        public List<State> GetListState()
        {
            return context.States.ToList();
        }
    }
}