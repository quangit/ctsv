
using WEBPCTSV.Models.bo;
using WEBPCTSV.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Controllers
{
    public class ManageRequestController : Controller
    {
        // GET: ManagerRequest
        public ActionResult Index()
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(Session["idAccount"]);
            ViewBag.listRequest = requestPaperBo.ListRequestPaper(idAccount, 1);
            return View("ListSendRequestPaper");
        }

        public ActionResult ListSendRequestPaper(int page)
        {
            if (Session["idAccount"] == null) return RedirectToAction("Index", "Home");

            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(Session["idAccount"]);
            Account account = new AccountBO().GetAccount(idAccount);
            if (!account.TypeAccount.Equals("SV")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.listRequest = requestPaperBo.ListRequestPaper(idAccount, page);
            PageNumber pageNumber = requestPaperBo.GetSendPageNumber(idAccount, page);
            pageNumber.Link = "/ManageRequest/ListSendRequestPaper?page=";
            ViewBag.pageNumber = pageNumber;
            ViewBag.listPaper = new PaperBo().GetListPaper();
            return View("ListSendRequestPaper");
        }


        public ActionResult ListAllRequestPaper(int value, int page, FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyyeucaugiayto")) return RedirectToAction("NotAccess", "ManageDecentralization");
            string search = form["inputSearchRequestPaper"];
            if (search == null)
            {
                if (Session["searchrequest"] != null) search = Session["searchrequest"].ToString();
            }
            else
            {
                Session["searchrequest"] = search;
            }
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            ViewBag.listRequest = requestPaperBo.ListAllRequestPaper(value, page, search);
            PageNumber pageNumber = requestPaperBo.GetPageNumber(value, page, search);
            pageNumber.Link = "/ManageRequest/ListAllRequestPaper?value=" + value + "&&page=";
            ViewBag.pageNumber = pageNumber;
            ViewBag.idRequestStatus = value;
            ViewBag.listStatus = new RequestStatusBo().GetRequestStatus();
            return View("ListAllRequestPaper");
        }


        public ActionResult DeleteRequestPaper(int idRequestPaper)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            requestPaperBo.DeleteRequestPaper(idRequestPaper);
            return Content("1", "text/plain");
        }

        public ActionResult UpdateStatusProcessRequest(int idRequestPaper)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(Session["idAccount"]);
            requestPaperBo.UpdateStatusProcessRequestPaper(idRequestPaper, idAccount);
            return Content("1", "text/plain");
        }



        public ActionResult UpdateStatusCancelRequest(int idRequestPaper)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(Session["idAccount"]);
            requestPaperBo.UpdateStatusCancelRequestPaper(idRequestPaper, idAccount);
            return Content("1", "text/plain");
        }

        public ActionResult ChoosePaper()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyyeucaugiayto")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.Papers = new PaperBo().GetListPaper();
            return View("ChoosePapers");
        }

        public ActionResult ChooseClass(int idReason, int idFaculty)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyyeucaugiayto")) return RedirectToAction("NotAccess", "ManageDecentralization");
            List<Faculty> listFaculty = new FacultyBO().GetListFaculty();
            ViewBag.Faculty = listFaculty;
            ViewBag.idFaculty = idFaculty;
            List<Class> listClass = new List<Class>();
            if (idFaculty == 1)
            {
                listClass = new ClassBO().GetClassByFaculty(listFaculty.FirstOrDefault().IdFaculty);
            }
            else
            {
                listClass = new ClassBO().GetClassByFaculty(idFaculty);
            }
            ViewBag.listClass = listClass;
            ViewBag.idReason = idReason;
            return View("ChooseClass");
        }
        public PartialViewResult ListClassByFaculty(int idFaculty)
        {
            List<Class> listClass = new ClassBO().GetClassByFaculty(idFaculty);
            return PartialView("_ListClass", listClass);
        }

        public PartialViewResult ListStudentByClass(int idClass)
        {
            List<Student> listStudent = new StudentBO().GetListStudentByClass(idClass);
            return PartialView("_ListStudent", listStudent);
        }

        public ActionResult ListStudent(int idReason, int idClass)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyyeucaugiayto")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.student = new StudentBO().GetListStudentByClass(idClass);
            ViewBag.request = new RequestPaperBo().GetRequestByClass(idReason,idClass);
            ViewBag.idReason = idReason;
            ReasonRequest reason = new ReasonPaperBo().Get(idReason);
            ViewBag.PaperName = reason.Paper.PaperName;
            ViewBag.idClass = idClass;
            return View("ListStudentByClass");
        }

        public ActionResult AddRequest(int idStudent, int idReason, int numberPaper)
        {
            RequestPaper request = new RequestPaperBo().AddRequest(idStudent, idReason, numberPaper);
            ReasonRequest reason = new ReasonPaperBo().GetReasonByIdRequest(request.IdRequestPaper);
            String content = "<div class='input-group' style='width:100px;float:left'>"
                            + "<input id='updatenumberPaper_" + idStudent + "' type='number' min='0' max='20' step='1' value='" + request.NumberPaper + "' class='form-control' />"
                            + "<div class='input-group-btn'>"
                            + "<button class='btn btn-info' onclick=\"UpdateRequest('" + request.IdRequestPaper + "','updatenumberPaper_" + idStudent + "')\"><span class='glyphicon glyphicon-pencil '></span></button>"
                            + "</div>"
                            + "</div>";
            if (request.IdRequestStatus != 3)
            {
                content += "<button type='button' class='btn btn-success' style='margin-left:5px'><span class='glyphicon glyphicon-print' data-toggle='tooltip' title='In đơn !' onclick='PrintHtml("+reason.IdPaper+","+request.IdAccountRequest+","+request.NumberPaper+")'></span></button>";
            }

            if (request.IdRequestStatus == 1 || request.IdRequestStatus == 2)
            {
                content += "<label class='btn btn-danger' style='margin-left:5px'><a class='glyphicon glyphicon-remove' style='color:white' onclick='UpdateStatusCancelRequest('" + request.IdRequestPaper + "')' data-toggle='tooltip' title='Hủy yêu cầu!'></a></label>";
            }
            return Content(content, "text/plain");
        }
        public ActionResult UpdateRequest(int idRequest, int numberPaper)
        {
            new RequestPaperBo().UpdateRequest(idRequest, numberPaper);
            return Content("1", "text/plain");
        }

    }
}