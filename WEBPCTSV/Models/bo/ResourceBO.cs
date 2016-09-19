namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class ResourceBO
    {
        private readonly DSAContext dsaContext;

        public ResourceBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public string getResourceByAcronym(string resourceAcronym)
        {
            try
            {
                return
                    this.dsaContext.Resources.SingleOrDefault(r => r.ResourceAcronym.Equals(resourceAcronym))
                        .ResourceContent;
            }
            catch
            {
            }

            return string.Empty;
        }

        public Resource getResourceObjectByAcronym(string resourceAcronym)
        {
            return this.dsaContext.Resources.SingleOrDefault(r => r.ResourceAcronym.Equals(resourceAcronym));
        }

        public bool UpdateResource(int idResource, string contentResource)
        {
            try
            {
                Resource rs = this.dsaContext.Resources.Find(idResource);
                rs.ResourceContent = contentResource;
                this.dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}