using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class ResourceController : Controller
    {
        private DSAContext dsaContext;
        ResourceBO resourceBO;

        public ResourceController()
        {
            dsaContext = new DSAContext();
            resourceBO = new ResourceBO(dsaContext);
        }


        public ActionResult Resource(string acronym)
        {
            ViewBag.content = resourceBO.getResourceByAcronym(acronym);
            return View();
        }

    }
}
