using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;
using WEBPCTSV.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Controllers
{
    public class ManageDecentralizationController : Controller
    {
        DecentralizationBo decentralizationBo = new DecentralizationBo();
        DecentralizationGroupBo decentralizationGroupBo = new DecentralizationGroupBo();
        FuntionBo funtionBo = new FuntionBo();
        public ActionResult Index()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyphanquyen")) return RedirectToAction("NotAccess");
            ViewBag.listGroup = decentralizationGroupBo.getListGroup();
            return View("Group");
        }

        public ActionResult NotAccess()
        {
            return View("NotAccess");
        }
        public ActionResult AddGroup(string nameGroup)
        {
            decentralizationGroupBo.AddGroup(nameGroup);
            return Content("test", "text/plain");
        }

        public ActionResult DecentralizationGroup(int idgroup, string nameGroup)
        {
            List<Function> listFuntion = funtionBo.GetListFuntion();
            ViewBag.listFuntion = listFuntion;
            List<Decentralization> listDecentralization = decentralizationBo.listDecentralizationHaveGroup(idgroup);
            ViewBag.listDecentralization = listDecentralization;
            ViewBag.idgroup = idgroup;
            ViewBag.namegroup = nameGroup;
            return View("DecentralizationManage");
        }


        public ActionResult UpdateIsAccept(int idDecentralization)
        {
            decentralizationBo.UpdateIsAccept(idDecentralization);
            return Content("1", "text/plain");
        }


        public ActionResult AddDecentralization(int idGroup,string idFuntion)
        {
            int idDecentralization = decentralizationBo.AddDecentralization(idGroup, idFuntion);
            return Content(idDecentralization+"", "text/plain");
        }
    }
}