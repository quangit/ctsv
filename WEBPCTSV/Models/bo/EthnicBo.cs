namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class EthnicBo
    {
        readonly DSAContext context = new DSAContext();

        public List<Ethnic> GetOptionEthnic(string idState)
        {
            return this.context.Ethnics.Where(e => e.IdState.Equals(idState)).ToList();
        }
    }
}