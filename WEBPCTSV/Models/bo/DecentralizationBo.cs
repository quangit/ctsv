namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class DecentralizationBo
    {
        readonly DSAContext context = new DSAContext();

        public int AddDecentralization(int idGroup, string idFuntion)
        {
            Decentralization decentralization = new Decentralization();
            decentralization.IdDecentralizationGroup = idGroup;
            decentralization.IdFunction = idFuntion;
            decentralization.IsAccept = true;
            
            this.context.Decentralizations.Add(decentralization);
            this.context.SaveChanges();
            return decentralization.IdDecentralization;
        }

        public List<Decentralization> listDecentralization()
        {
            return this.context.Decentralizations.ToList();
        }

        public List<Decentralization> listDecentralizationHaveGroup(int idGroup)
        {
            List<Decentralization> list =
                this.context.Decentralizations.Where(d => d.IdDecentralizationGroup == idGroup).ToList();
            return list;
        }

        public void UpdateIsAccept(int idDecentralization)
        {
            Decentralization decentralization =
                this.context.Decentralizations.Single(d => d.IdDecentralization == idDecentralization);
            decentralization.IsAccept = !decentralization.IsAccept;
            this.context.SaveChanges();
        }
    }
}