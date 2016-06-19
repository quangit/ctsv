using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Models.bo
{
    public class ResourceBO
    {
        private DSAContext dsaContext;

        public ResourceBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public string getResourceByAcronym(string resourceAcronym)
        {
            try { return (dsaContext.Resources.SingleOrDefault(r => r.ResourceAcronym.Equals(resourceAcronym))).ResourceContent; }
            catch { }
            return "";
            
        }
        public Resource getResourceObjectByAcronym(string resourceAcronym)
        {
            return (dsaContext.Resources.SingleOrDefault(r => r.ResourceAcronym.Equals(resourceAcronym)));
        }

        public bool UpdateResource(int idResource, string contentResource)
        {
            try
            {
                Resource rs = dsaContext.Resources.Find(idResource);
                rs.ResourceContent = contentResource;
                dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}