using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class DecentralizationBo
    {
        DSAContext context = new DSAContext();
        public List<Decentralization> listDecentralization()
        {
            return context.Decentralizations.ToList();
        }

        public List<Decentralization> listDecentralizationHaveGroup(int idGroup)
        {
            List<Decentralization> list = context.Decentralizations.Where(d=>d.IdDecentralizationGroup==idGroup).ToList();
            return list;
        }

        public int AddDecentralization(int idGroup,string idFuntion)
        {
            Decentralization decentralization = new Decentralization();
            decentralization.IdDecentralizationGroup = idGroup;
            decentralization.IdFunction = idFuntion;
            decentralization.IsAccept = true; ;
            context.Decentralizations.Add(decentralization);
            context.SaveChanges();
            return decentralization.IdDecentralization;
        }

        public void UpdateIsAccept(int idDecentralization)
        {
            Decentralization decentralization = context.Decentralizations.Single(d => d.IdDecentralization == idDecentralization);
            decentralization.IsAccept = !decentralization.IsAccept;
            context.SaveChanges();
        }

        

        
    }
}