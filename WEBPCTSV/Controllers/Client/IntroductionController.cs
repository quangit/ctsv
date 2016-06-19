using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class IntroductionController : Controller
    {
        DSAContext dsaContext;
        public IntroductionController()
        {
            dsaContext = new DSAContext();
        }
        public ActionResult FunctionAndDuty()
        {
            return View();
        }

        public ActionResult HumanResource()
        {
            StaffBO staffBO = new StaffBO(dsaContext);
            return View(staffBO.getListStaff());
        }
    }
}
