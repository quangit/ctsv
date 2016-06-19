
using WEBPCTSV.Models.bo;
using WEBPCTSV.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Controllers
{
    public class ManageRequestController : Controller
    {
        // GET: ManagerRequest
        public ActionResult Index()
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount  = Convert.ToInt32(Session["idAccount"]);
            ViewBag.listRequest = requestPaperBo.ListRequestPaper(idAccount,1);
            return View("ListSendRequestPaper");
        }

        public ActionResult ListSendRequestPaper(int page)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(Session["idAccount"]);
            ViewBag.listRequest = requestPaperBo.ListRequestPaper(idAccount,page);
            PageNumber pageNumber = requestPaperBo.GetSendPageNumber(idAccount, page);
            pageNumber.Link = "/ManageRequest/ListSendRequestPaper?page=";
            ViewBag.pageNumber = pageNumber;
            ViewBag.listPaper = new PaperBo().GetListPaper();
            return View("ListSendRequestPaper");
        }

  
        public ActionResult ListAllRequestPaper(int value,int page,FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyyeucaugiayto")) return RedirectToAction("NotAccess", "ManageDecentralization");
            string search = form["inputSearchRequestPaper"];
            if (search == null)
            {
                if (Session["search"] != null) search = Session["search"].ToString();
            }
            else
            {
                Session["search"] = search;
            }
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            ViewBag.listRequest = requestPaperBo.ListAllRequestPaper(value,page,search);
            PageNumber pageNumber = requestPaperBo.GetPageNumber(value, page,search);
            pageNumber.Link = "/ManageRequest/ListAllRequestPaper?value="+value+"&&page=";
            ViewBag.pageNumber = pageNumber;
            ViewBag.idRequestStatus = value;
            ViewBag.listStatus = new RequestStatusBo().GetRequestStatus();
            return View("ListAllRequestPaper");
        }


        public ActionResult DeleteRequestPaper(int idRequestPaper)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            requestPaperBo.DeleteRequestPaper(idRequestPaper);
            return Content("1","text/plain");
        }

        public ActionResult UpdateStatusProcessRequest(int idRequestPaper)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(Session["idAccount"]);
            requestPaperBo.UpdateStatusProcessRequestPaper(idRequestPaper,idAccount);
            return Content("1", "text/plain");
        }

        

        public ActionResult UpdateStatusCancelRequest(int idRequestPaper)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(Session["idAccount"]);
            requestPaperBo.UpdateStatusCancelRequestPaper(idRequestPaper,idAccount);
            return Content("1", "text/plain");
        }
        
    }
}