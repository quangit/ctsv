using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class FuntionBo
    {
        DSAContext context = new DSAContext();

        public List<Function> GetListFuntion()
        {
            return context.Functions.ToList();
        }

        public void AddFuntion(string idFuntion, string funtionName)
        {
            Function funtion = new Function();
            funtion.IdFunction = idFuntion;
            funtion.FunctionName = funtionName;
            context.Functions.Add(funtion);
            context.SaveChanges();
        }
    }
}