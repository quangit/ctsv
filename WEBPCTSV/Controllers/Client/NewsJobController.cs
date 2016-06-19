using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class NewsJobController : Controller
    {
        DSAContext dsaContext;
        NewsJobBO newsJobBO;
        public NewsJobController()
        {
            dsaContext = new DSAContext();
            newsJobBO = new NewsJobBO(dsaContext);
        }

        public ActionResult ListNewsJob(int? page)
        {
            return View(newsJobBO.GetListNewsJob(page));
        }
        public ActionResult DetailNewsJob(int id)
        {
            return View(newsJobBO.GetNewsJob(id));
        }

        public ActionResult TopNewsJob(int count)
        {
            ViewBag.topNewsJob = newsJobBO.GetTopNewsJob(count);
            return View();
        }

    }
}
