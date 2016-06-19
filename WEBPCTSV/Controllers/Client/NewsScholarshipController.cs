using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class NewsScholarshipController : Controller
    {
        DSAContext dsaContext;
        NewsScholarshipBO newsScholarshipBO;

        public NewsScholarshipController()
        {
            dsaContext = new DSAContext();
            newsScholarshipBO = new NewsScholarshipBO(dsaContext);
        }
        public ActionResult ListNewsScholarship(int? page, string type = null)
        {
            if (type == null || string.IsNullOrEmpty(type))
            {
                type = "HocBong";
            }
            ViewBag.type = type;
            return View(newsScholarshipBO.GetListNewsScholarship(page, type));
        }
        public ActionResult DetailNewsScholarship(int id)
        {
            return View(newsScholarshipBO.GetNewsScholarship(id));
        }
        public ActionResult TopNewsScholarship(int count)
        {
            ViewBag.topNewsScholarship = newsScholarshipBO.GetTopNewsScholarship(count);
            return View();
        }
    }
}
