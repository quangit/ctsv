namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class FuntionBo
    {
        readonly DSAContext context = new DSAContext();

        public void AddFuntion(string idFuntion, string funtionName)
        {
            Function funtion = new Function();
            funtion.IdFunction = idFuntion;
            funtion.FunctionName = funtionName;
            this.context.Functions.Add(funtion);
            this.context.SaveChanges();
        }

        public List<Function> GetListFuntion()
        {
            return this.context.Functions.ToList();
        }
    }
}