namespace WEBPCTSV.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class ManageDecentralizationController : Controller
    {
        readonly DecentralizationBo decentralizationBo = new DecentralizationBo();

        readonly DecentralizationGroupBo decentralizationGroupBo = new DecentralizationGroupBo();

        readonly FuntionBo funtionBo = new FuntionBo();

        public ActionResult AddDecentralization(int idGroup, string idFuntion)
        {
            int idDecentralization = this.decentralizationBo.AddDecentralization(idGroup, idFuntion);
            return this.Content(idDecentralization + string.Empty, "text/plain");
        }

        public ActionResult AddGroup(string nameGroup)
        {
            this.decentralizationGroupBo.AddGroup(nameGroup);
            return this.Content("test", "text/plain");
        }

        public ActionResult DecentralizationGroup(int idgroup, string nameGroup)
        {
            List<Function> listFuntion = this.funtionBo.GetListFuntion();
            this.ViewBag.listFuntion = listFuntion;
            List<Decentralization> listDecentralization = this.decentralizationBo.listDecentralizationHaveGroup(idgroup);
            this.ViewBag.listDecentralization = listDecentralization;
            this.ViewBag.idgroup = idgroup;
            this.ViewBag.namegroup = nameGroup;
            return this.View("DecentralizationManage");
        }

        public ActionResult Index()
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "quanlyphanquyen")) return this.RedirectToAction("NotAccess");
            this.ViewBag.listGroup = this.decentralizationGroupBo.getListGroup();
            return this.View("Group");
        }

        public ActionResult NotAccess()
        {
            return this.View("NotAccess");
        }

        public ActionResult UpdateIsAccept(int idDecentralization)
        {
            this.decentralizationBo.UpdateIsAccept(idDecentralization);
            return this.Content("1", "text/plain");
        }
    }
}