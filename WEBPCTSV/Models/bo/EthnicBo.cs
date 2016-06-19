using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class EthnicBo
    {
        DSAContext context = new DSAContext();
        public List<Ethnic> GetOptionEthnic(string idState)
        {
            return context.Ethnics.Where(e => e.IdState.Equals(idState)).ToList();
        }
    }
}