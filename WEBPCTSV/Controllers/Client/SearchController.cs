using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers.Client
{
    public class SearchController : Controller
    {

        private DSAContext dsaContext;

        public SearchController()
        {
            dsaContext = new DSAContext();
        }
        public ActionResult SearchResult(string txtFind = "")
        {
            ViewBag.searchString = txtFind;
            ViewBag.resultAnnoucements = (new NewsBO(dsaContext)).FindNews(txtFind, "ThongBao").Take(15).ToList();
            ViewBag.resultNewsScholarships = (new NewsScholarshipBO(dsaContext)).FindNewsScholarship(txtFind).Take(15).ToList();
            ViewBag.resultNewsEvents = (new NewsEventBO(dsaContext)).FindNewsEvent(txtFind).Take(15).ToList();
            ViewBag.resultNewsJobs = (new NewsJobBO(dsaContext)).FindNewsJob(txtFind).Take(15).ToList();
            ViewBag.documents = (new DocumentBO(dsaContext)).FindDocument(txtFind).Take(15).ToList();
            ViewBag.questions = (new QuestionBO(dsaContext)).FindQuestion(txtFind).Take(15).ToList();
            return View();
        }

    }
}
