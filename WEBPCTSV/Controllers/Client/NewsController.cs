using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class NewsController : Controller
    {
        DSAContext dsaContext;
        NewsBO newsBO;

        public NewsController()
        {
            dsaContext = new DSAContext();
            newsBO = new NewsBO(dsaContext);
        }
        #region Announcement News
        public ActionResult ListNews(int? page)
        {
            return View(newsBO.GetListNews(page, "ThongBao"));
        }
        public ActionResult DetailNews(int id)
        {
            return View(newsBO.GetNews(id));
        }

        public ActionResult TopNews(int count)
        {
            ViewBag.topNews = newsBO.GetTopNews(count, "ThongBao");
            return View();
        }
        #endregion

        #region Legal Education News
        public ActionResult ListNewsLegalEducation(int? page)
        {
            return View(newsBO.GetListNews(page, "LegalEducation"));
        }
        public ActionResult DetailNewsLegalEducation(int id)
        {
            return View(newsBO.GetNews(id));
        }
        #endregion

        #region Medical student news
        public ActionResult ListNewsMedicalStudent(int? page)
        {
            return View(newsBO.GetListNews(page, "MedicalStudent"));
        }
        public ActionResult DetailNewsMedicalStudent(int id)
        {
            return View(newsBO.GetNews(id));
        }
        #endregion

        #region Ralation Company news
        public ActionResult ListNewsRelationCompany(int? page)
        {
            return View(newsBO.GetListNews(page, "RelationCompany"));
        }
        public ActionResult DetailNewsRelationCompany(int id)
        {
            return View(newsBO.GetNews(id));
        }
        #endregion

        #region Alumni news
        public ActionResult AlumniIntro()
        {
            return View();
        }

        public ActionResult AlumniStar()
        {
            return View();
        }

        public ActionResult ListNewsAlumni(int? page)
        {
            return View(newsBO.GetListNews(page, "Alumni"));
        }

        public ActionResult DetailNewsAlumni(int id)
        {
            return View(newsBO.GetNews(id));
        }

        public ActionResult TopNewsAlumni(int count)
        {
            ViewBag.topNews = newsBO.GetTopNews(count, "Alumni");
            return View();
        }
        #endregion
    }
}
