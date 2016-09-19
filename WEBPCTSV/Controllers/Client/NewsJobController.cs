namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class NewsJobController : Controller
    {
        readonly DSAContext dsaContext;

        readonly NewsJobBO newsJobBO;

        public NewsJobController()
        {
            this.dsaContext = new DSAContext();
            this.newsJobBO = new NewsJobBO(this.dsaContext);
        }

        public ActionResult DetailNewsJob(int id)
        {
            return this.View(this.newsJobBO.GetNewsJob(id));
        }

        public ActionResult ListNewsJob(int? page)
        {
            return this.View(this.newsJobBO.GetListNewsJob(page));
        }

        public ActionResult TopNewsJob(int count)
        {
            this.ViewBag.topNewsJob = this.newsJobBO.GetTopNewsJob(count);
            return this.View();
        }
    }
}