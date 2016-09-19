namespace WEBPCTSV.Controllers.Client
{
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class SearchController : Controller
    {
        private readonly DSAContext dsaContext;

        public SearchController()
        {
            this.dsaContext = new DSAContext();
        }

        public ActionResult SearchResult(string txtFind = "")
        {
            this.ViewBag.searchString = txtFind;
            this.ViewBag.resultAnnoucements =
                (new NewsBO(this.dsaContext)).FindNews(txtFind, "ThongBao").Take(15).ToList();
            this.ViewBag.resultNewsScholarships =
                (new NewsScholarshipBO(this.dsaContext)).FindNewsScholarship(txtFind).Take(15).ToList();
            this.ViewBag.resultNewsEvents = (new NewsEventBO(this.dsaContext)).FindNewsEvent(txtFind).Take(15).ToList();
            this.ViewBag.resultNewsJobs = (new NewsJobBO(this.dsaContext)).FindNewsJob(txtFind).Take(15).ToList();
            this.ViewBag.documents = (new DocumentBO(this.dsaContext)).FindDocument(txtFind).Take(15).ToList();
            this.ViewBag.questions = (new QuestionBO(this.dsaContext)).FindQuestion(txtFind).Take(15).ToList();
            return this.View();
        }
    }
}