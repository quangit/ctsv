using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class FamilyCompositionBo
    {
        DSAContext context = new DSAContext();
        public List<FamilyComposition> GetListFamilyComposition()
        {
            return context.FamilyCompositions.ToList();
        }

    }
}