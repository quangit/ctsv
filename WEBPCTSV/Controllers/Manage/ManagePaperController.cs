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
    public class ManagePaperController : Controller
    {
        PaperBo paperBo = new PaperBo();
        // GET: ManagerPaper
        public ActionResult Index()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "themgiayto")) return RedirectToAction("NotAccess", "ManageDecentralization");
            return View("CreatePaper");
        }

        [ValidateInput(false)]
        public ActionResult AddPaper(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "themgiayto")) return RedirectToAction("NotAccess", "ManageDecentralization");
            paperBo.AddPaper(form);
            return RedirectToAction("ListPaper");
        }


        public ActionResult ListPaper()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "chinhsuagiayto")) return RedirectToAction("NotAccess", "ManageDecentralization");
            List<Paper> listPaper = paperBo.GetListPaper();
            ViewBag.listPaper = listPaper;
            return View("ListPaper");
        }

        public ActionResult DetailPaper(int id)
        {
            int idAccount = Convert.ToInt32(Session["idAccount"]);
            Paper paper = paperBo.ReplacePaper(id, idAccount);
            ViewBag.paper = paper;
            return View("DetailPaper");
        }

        public ActionResult DetailPrintPaper(int idPaper, int idAccountRequest,int numberPaper)
        {
            Paper paper = paperBo.ReplacePaper(idPaper, idAccountRequest);
            string printPaper = "";
            for (int i = 0; i < numberPaper;i++ )
            {
                printPaper += paper.ContentPaper;
            }
                return Content(printPaper, "text/plain");
        }

        public ActionResult PrintListPaper(List<int> listIdRequest)
        {
            if(listIdRequest!=null)
            {
                string content = paperBo.GetContentListPaper(listIdRequest);
                return Json(new { Result = content });
            }
            return null;
        }
        public ActionResult PrintListPaperByClass(int idReason,int idClass)
        {
            string content = paperBo.GetContentListPaperByClass(idReason,idClass);
            return Json(new { Result = content });
        }

        public ActionResult EditPaper(int id)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "chinhsuagiayto")) return RedirectToAction("NotAccess", "ManageDecentralization");
            Paper paper = paperBo.GetPaper(id);
            ViewBag.paper = paper;
            return View("EditPaper");
        }

        public ActionResult DeletePaper(int idPaper)
        {
            paperBo.DeletePaper(idPaper);
            return RedirectToAction("ListPaper");
        }
        public ActionResult AddReasonRequest(int idPaper, string reason)
        {
            ReasonPaperBo reasonPaper = new ReasonPaperBo();
            Paper paper = reasonPaper.AddReasonRequest(idPaper, reason);

            return PartialView("_AddReasonRequest", paper);
        }


        public void DeleteReasonRequest(int idReason)
        {
            ReasonPaperBo reasonPaper = new ReasonPaperBo();
            reasonPaper.DeleteReasonRequest(idReason);
        }

        [ValidateInput(false)]
        public ActionResult UpdateContentPaper(string content, int idPaper)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "chinhsuagiayto")) return RedirectToAction("NotAccess", "ManageDecentralization");

            if (paperBo.UpdatePaper(content, idPaper))
            {
                return Content("1", "text/plain");
            }
            else
            {
                return Content("0", "text/plain");
            }
        }

        public ActionResult UpdatePaper(int idPaper,FormCollection form)
        {
            new PaperBo().UpdatePaper(idPaper, form);
            return RedirectToAction("EditPaper", new { id = idPaper });
        }

        public ActionResult AddRequestPaper(FormCollection form)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(Session["idAccount"]);
            requestPaperBo.AddRequestPaper(idAccount,form);
            return RedirectToAction("ListSendRequestPaper","ManageRequest", new { page = 1 });
        }

        public ActionResult GetStringInfoReasonRequest(int idReason)
        {
            ReasonPaperBo reasonPaper = new ReasonPaperBo();
            string info = ""; //reasonPaper.GetStringInfoReason(idReason);
            return Content(info, "text/plain");
        }
    }
}