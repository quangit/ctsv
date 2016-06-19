using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class HomeController : Controller
    {
        DSAContext dsaContext;

        public HomeController()
        {
            dsaContext = new DSAContext();
        }
        public ActionResult Index()
        {
            NewsBO newsBO = new NewsBO(dsaContext);
            NewsEventBO newsEventBO = new NewsEventBO(dsaContext);
            NewsScholarshipBO newsScholarshipBO = new NewsScholarshipBO(dsaContext);
            NewsJobBO newsJobBO = new NewsJobBO(dsaContext);
                        DocumentBO documentBO = new DocumentBO(dsaContext);
                        QuestionBO questionBO = new QuestionBO(dsaContext);

            ViewBag.news = newsBO.GetTopNews(5, "TinTuc");
            ViewBag.announcementSidebar = newsBO.GetTopNews(10, null);
            ViewBag.eventSidebar = newsEventBO.GetTopNewsEvent(2);
            ViewBag.newsScholarship = newsScholarshipBO.GetTopNewsScholarship(6);
            ViewBag.newsJob = newsJobBO.GetTopNewsJob(6);
            ViewBag.documents = documentBO.GetTopDocument(5);
            ViewBag.banner = newsBO.GetBanner(5);
            ViewBag.questions = questionBO.GetRandomQuestion(5);
            return View();
        }

    }
}
