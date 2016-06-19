using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class TypeInputBo
    {
        DSAContext context = new DSAContext();
        public List<TypeInput> GetListTypeInput()
        {
            return context.TypeInputs.ToList();
        }
    }
}