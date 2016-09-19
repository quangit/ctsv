namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ResourceController : Controller
    {
        private readonly DSAContext dsaContext;

        readonly ResourceBO resourceBO;

        public ResourceController()
        {
            this.dsaContext = new DSAContext();
            this.resourceBO = new ResourceBO(this.dsaContext);
        }

        public ActionResult Resource(string acronym)
        {
            this.ViewBag.content = this.resourceBO.getResourceByAcronym(acronym);
            return this.View();
        }
    }
}