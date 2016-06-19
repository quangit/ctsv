using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Models.bo
{
    public class FunctionBO
    {
        private DSAContext dsaContext;
        DSAContext context = new DSAContext();

        public FunctionBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public List<string> getListFunctionByGroup(int idDecentralizationGroup)
        {
            var decentralizations = dsaContext.Decentralizations.Where(d => d.IdDecentralizationGroup.Equals(idDecentralizationGroup));
            List<string> listFunction = new List<string>();
            foreach (var function in decentralizations)
            {
                listFunction.Add(function.IdFunction);
            }
            return listFunction;
        }
        public List<Function> GetListFuntion()
        {
            return dsaContext.Functions.ToList();
        }

        public void AddFuntion(string idFuntion, string funtionName)
        {
            Function funtion = new Function();
            funtion.IdFunction = idFuntion;
            funtion.FunctionName = funtionName;
            dsaContext.Functions.Add(funtion);
            dsaContext.SaveChanges();
        }
    }
}