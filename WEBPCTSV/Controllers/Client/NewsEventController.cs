namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class NewsEventController : Controller
    {
        readonly DSAContext dsaContext;

        readonly NewsEventBO newsEventBO;

        public NewsEventController()
        {
            this.dsaContext = new DSAContext();
            this.newsEventBO = new NewsEventBO(this.dsaContext);
        }

        public ActionResult DetailNewsEvent(int id)
        {
            return this.View(this.newsEventBO.GetNewsEvent(id));
        }

        public ActionResult ListNewsEvent(int? page)
        {
            return this.View(this.newsEventBO.GetListNewsEvent(page));
        }

        public ActionResult TopNewsEvent(int count)
        {
            this.ViewBag.topNewsEvent = this.newsEventBO.GetTopNewsEvent(count);
            return this.View();
        }
    }
}