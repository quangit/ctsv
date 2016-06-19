using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.Support
{
    public class CheckDecentralization
    {
        static public bool Check(int idDecenTralizationGroup, string idFuntion)
        {
            DSAContext context = new DSAContext();
            try
            {

                Decentralization decentralization = context.Decentralizations.Single(d => d.IdFunction.Equals(idFuntion) && d.IdDecentralizationGroup == idDecenTralizationGroup);
                if (decentralization.IsAccept == true) return true;
            }
            catch { }
            return false;
        }

        public static bool CheckTypeAccount(int idAccount, string Type)
        {
            string typeAccount = new AccountBO().GetTypeAccount(idAccount);
            if (typeAccount.Equals(Type)) return true;
            return false;
        }

        public static bool CheckPersonal(int idAccount, int idStudent)
        {
            try {
                DSAContext context = new DSAContext();
                Account account = new AccountBO(context).GetAccount(idAccount);
                if (account.Students.First().IdStudent == idStudent) return true;
            }
            catch { }
            return false;
            
        }
    }
}