namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class NewsController : Controller
    {
        readonly DSAContext dsaContext;

        readonly NewsBO newsBO;

        public NewsController()
        {
            this.dsaContext = new DSAContext();
            this.newsBO = new NewsBO(this.dsaContext);
        }

        public ActionResult AlumniIntro()
        {
            return this.View();
        }

        public ActionResult AlumniStar()
        {
            return this.View();
        }

        public ActionResult DetailNews(int id)
        {
            return this.View(this.newsBO.GetNews(id));
        }

        public ActionResult DetailNewsAlumni(int id)
        {
            return this.View(this.newsBO.GetNews(id));
        }

        public ActionResult DetailNewsLegalEducation(int id)
        {
            return this.View(this.newsBO.GetNews(id));
        }

        public ActionResult DetailNewsMedicalStudent(int id)
        {
            return this.View(this.newsBO.GetNews(id));
        }

        public ActionResult DetailNewsRelationCompany(int id)
        {
            return this.View(this.newsBO.GetNews(id));
        }

        public ActionResult ListNews(int? page)
        {
            return this.View(this.newsBO.GetListNews(page, "ThongBao"));
        }

        public ActionResult ListNewsAlumni(int? page)
        {
            return this.View(this.newsBO.GetListNews(page, "Alumni"));
        }

        public ActionResult ListNewsLegalEducation(int? page)
        {
            return this.View(this.newsBO.GetListNews(page, "LegalEducation"));
        }

        public ActionResult ListNewsMedicalStudent(int? page)
        {
            return this.View(this.newsBO.GetListNews(page, "MedicalStudent"));
        }

        public ActionResult ListNewsRelationCompany(int? page)
        {
            return this.View(this.newsBO.GetListNews(page, "RelationCompany"));
        }

        public ActionResult TopNews(int count)
        {
            this.ViewBag.topNews = this.newsBO.GetTopNews(count, "ThongBao");
            return this.View();
        }

        public ActionResult TopNewsAlumni(int count)
        {
            this.ViewBag.topNews = this.newsBO.GetTopNews(count, "Alumni");
            return this.View();
        }
    }
}