namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class HomeController : Controller
    {
        readonly DSAContext dsaContext;

        public HomeController()
        {
            this.dsaContext = new DSAContext();
        }

        public ActionResult Index()
        {
            NewsBO newsBO = new NewsBO(this.dsaContext);
            NewsEventBO newsEventBO = new NewsEventBO(this.dsaContext);
            NewsScholarshipBO newsScholarshipBO = new NewsScholarshipBO(this.dsaContext);
            NewsJobBO newsJobBO = new NewsJobBO(this.dsaContext);
            DocumentBO documentBO = new DocumentBO(this.dsaContext);
            QuestionBO questionBO = new QuestionBO(this.dsaContext);

            this.ViewBag.news = newsBO.GetTopNews(5, "TinTuc");
            this.ViewBag.announcementSidebar = newsBO.GetTopNews(10, null);
            this.ViewBag.eventSidebar = newsEventBO.GetTopNewsEvent(2);
            this.ViewBag.newsScholarship = newsScholarshipBO.GetTopNewsScholarship(6);
            this.ViewBag.newsJob = newsJobBO.GetTopNewsJob(6);
            this.ViewBag.documents = documentBO.GetTopDocument(5);
            this.ViewBag.banner = newsBO.GetBanner(5);
            this.ViewBag.questions = questionBO.GetRandomQuestion(5);
            return this.View();
        }
    }
}