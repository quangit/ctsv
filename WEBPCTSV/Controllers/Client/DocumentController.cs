namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class DocumentController : Controller
    {
        readonly DocumentBO documentBO;

        readonly DSAContext dsaContext;

        public DocumentController()
        {
            this.dsaContext = new DSAContext();
            this.documentBO = new DocumentBO(this.dsaContext);
        }

        public ActionResult DetailDocument(int id)
        {
            return this.View(this.documentBO.GetDocument(id));
        }

        public ActionResult ListDocument(int? page, string type = null)
        {
            this.ViewBag.type = type;
            return this.View(this.documentBO.GetListDocument(page, type));
        }

        public ActionResult TopDocument(int count)
        {
            this.ViewBag.topDocument = this.documentBO.GetTopDocument(count);
            return this.View();
        }
    }
}