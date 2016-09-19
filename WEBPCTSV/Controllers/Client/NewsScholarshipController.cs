namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class NewsScholarshipController : Controller
    {
        readonly DSAContext dsaContext;

        readonly NewsScholarshipBO newsScholarshipBO;

        public NewsScholarshipController()
        {
            this.dsaContext = new DSAContext();
            this.newsScholarshipBO = new NewsScholarshipBO(this.dsaContext);
        }

        public ActionResult DetailNewsScholarship(int id)
        {
            return this.View(this.newsScholarshipBO.GetNewsScholarship(id));
        }

        public ActionResult ListNewsScholarship(int? page, string type = null)
        {
            if (type == null || string.IsNullOrEmpty(type))
            {
                type = "HocBong";
            }

            this.ViewBag.type = type;
            return this.View(this.newsScholarshipBO.GetListNewsScholarship(page, type));
        }

        public ActionResult TopNewsScholarship(int count)
        {
            this.ViewBag.topNewsScholarship = this.newsScholarshipBO.GetTopNewsScholarship(count);
            return this.View();
        }
    }
}