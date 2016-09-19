namespace WEBPCTSV.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class ManagePaperController : Controller
    {
        readonly PaperBo paperBo = new PaperBo();

        [ValidateInput(false)]
        public ActionResult AddPaper(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "themgiayto")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.paperBo.AddPaper(form);
            return this.RedirectToAction("ListPaper");
        }

        public ActionResult AddReasonRequest(int idPaper, string reason)
        {
            ReasonPaperBo reasonPaper = new ReasonPaperBo();
            Paper paper = reasonPaper.AddReasonRequest(idPaper, reason);

            return this.PartialView("_AddReasonRequest", paper);
        }

        public ActionResult AddRequestPaper(FormCollection form)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(this.Session["idAccount"]);
            requestPaperBo.AddRequestPaper(idAccount, form);
            return this.RedirectToAction("ListSendRequestPaper", "ManageRequest", new { page = 1 });
        }

        public ActionResult DeletePaper(int idPaper)
        {
            this.paperBo.DeletePaper(idPaper);
            return this.RedirectToAction("ListPaper");
        }

        public void DeleteReasonRequest(int idReason)
        {
            ReasonPaperBo reasonPaper = new ReasonPaperBo();
            reasonPaper.DeleteReasonRequest(idReason);
        }

        public ActionResult DetailPaper(int id)
        {
            int idAccount = Convert.ToInt32(this.Session["idAccount"]);
            Paper paper = this.paperBo.ReplacePaper(id, idAccount);
            this.ViewBag.paper = paper;
            return this.View("DetailPaper");
        }

        public ActionResult DetailPrintPaper(int idPaper, int idAccountRequest, int numberPaper)
        {
            string printPaper = this.paperBo.DetailPrintPaper(idPaper, idAccountRequest, numberPaper);
            return this.Json(new { Result = printPaper });
        }

        public ActionResult EditPaper(int id)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "chinhsuagiayto")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            Paper paper = this.paperBo.GetPaper(id);
            this.ViewBag.paper = paper;
            return this.View("EditPaper");
        }

        public ActionResult GetStringInfoReasonRequest(int idReason)
        {
            ReasonPaperBo reasonPaper = new ReasonPaperBo();
            string info = string.Empty; // reasonPaper.GetStringInfoReason(idReason);
            return this.Content(info, "text/plain");
        }

        // GET: ManagerPaper
        public ActionResult Index()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "themgiayto")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            return this.View("CreatePaper");
        }

        public ActionResult ListPaper()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "chinhsuagiayto")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            List<Paper> listPaper = this.paperBo.GetListPaper();
            this.ViewBag.listPaper = listPaper;
            return this.View("ListPaper");
        }

        public ActionResult PrintListPaper(List<int> listIdRequest)
        {
            if (listIdRequest != null)
            {
                string content = this.paperBo.GetContentListPaper(listIdRequest);
                return this.Json(new { Result = content });
            }

            return null;
        }

        public ActionResult PrintListPaperByClass(int idReason, int idClass)
        {
            string content = this.paperBo.GetContentListPaperByClass(idReason, idClass);
            return this.Json(new { Result = content });
        }

        [ValidateInput(false)]
        public ActionResult UpdateContentPaper(string content, int idPaper)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "chinhsuagiayto")) return this.RedirectToAction("NotAccess", "ManageDecentralization");

            if (this.paperBo.UpdatePaper(content, idPaper))
            {
                return this.Content("1", "text/plain");
            }
            else
            {
                return this.Content("0", "text/plain");
            }
        }

        public ActionResult UpdatePaper(int idPaper, FormCollection form)
        {
            new PaperBo().UpdatePaper(idPaper, form);
            return this.RedirectToAction("EditPaper", new { id = idPaper });
        }
    }
}