using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class NewsEventController : Controller
    {
        DSAContext dsaContext;
        NewsEventBO newsEventBO;
        public NewsEventController()
        {
            dsaContext = new DSAContext();
            newsEventBO = new NewsEventBO(dsaContext);
        }

        public ActionResult ListNewsEvent(int? page)
        {
            return View(newsEventBO.GetListNewsEvent(page));
        }
        public ActionResult DetailNewsEvent(int id)
        {
            return View(newsEventBO.GetNewsEvent(id));
        }

        public ActionResult TopNewsEvent(int count)
        {
            ViewBag.topNewsEvent = newsEventBO.GetTopNewsEvent(count);
            return View();
        }

    }
}
