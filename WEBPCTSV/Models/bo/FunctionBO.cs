namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class FunctionBO
    {
        DSAContext context = new DSAContext();

        private readonly DSAContext dsaContext;

        public FunctionBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public void AddFuntion(string idFuntion, string funtionName)
        {
            Function funtion = new Function();
            funtion.IdFunction = idFuntion;
            funtion.FunctionName = funtionName;
            this.dsaContext.Functions.Add(funtion);
            this.dsaContext.SaveChanges();
        }

        public List<string> getListFunctionByGroup(int idDecentralizationGroup)
        {
            var decentralizations =
                this.dsaContext.Decentralizations.Where(d => d.IdDecentralizationGroup.Equals(idDecentralizationGroup));
            List<string> listFunction = new List<string>();
            foreach (var function in decentralizations)
            {
                listFunction.Add(function.IdFunction);
            }

            return listFunction;
        }

        public List<Function> GetListFuntion()
        {
            return this.dsaContext.Functions.ToList();
        }
    }
}