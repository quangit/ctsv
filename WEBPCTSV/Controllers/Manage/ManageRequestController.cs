namespace WEBPCTSV.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class ManageRequestController : Controller
    {
        public ActionResult AddRequest(int idStudent, int idReason, int numberPaper)
        {
            RequestPaper request = new RequestPaperBo().AddRequest(idStudent, idReason, numberPaper);
            ReasonRequest reason = new ReasonPaperBo().GetReasonByIdRequest(request.IdRequestPaper);
            string content = "<div class='input-group' style='width:100px;float:left'>"
                             + "<input id='updatenumberPaper_" + idStudent
                             + "' type='number' min='0' max='20' step='1' value='" + request.NumberPaper
                             + "' class='form-control' />" + "<div class='input-group-btn'>"
                             + "<button class='btn btn-info' onclick=\"UpdateRequest('" + request.IdRequestPaper
                             + "','updatenumberPaper_" + idStudent
                             + "')\"><span class='glyphicon glyphicon-pencil '></span></button>" + "</div>" + "</div>";
            if (request.IdRequestStatus != 3)
            {
                content +=
                    "<button type='button' class='btn btn-success' style='margin-left:5px'><span class='glyphicon glyphicon-print' data-toggle='tooltip' title='In đơn !' onclick='PrintHtml("
                    + reason.IdPaper + "," + request.IdAccountRequest + "," + request.NumberPaper
                    + ")'></span></button>";
            }

            if (request.IdRequestStatus == 1 || request.IdRequestStatus == 2)
            {
                content +=
                    "<label class='btn btn-danger' style='margin-left:5px'><a class='glyphicon glyphicon-remove' style='color:white' onclick='UpdateStatusCancelRequest('"
                    + request.IdRequestPaper + "')' data-toggle='tooltip' title='Hủy yêu cầu!'></a></label>";
            }

            return this.Content(content, "text/plain");
        }

        public ActionResult ChooseClass(int idReason, int idFaculty)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "quanlyyeucaugiayto")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            List<Faculty> listFaculty = new FacultyBO().GetListFaculty();
            this.ViewBag.Faculty = listFaculty;
            this.ViewBag.idFaculty = idFaculty;
            List<Class> listClass = new List<Class>();
            if (idFaculty == 1)
            {
                listClass = new ClassBO().GetClassByFaculty(listFaculty.FirstOrDefault().IdFaculty);
            }
            else
            {
                listClass = new ClassBO().GetClassByFaculty(idFaculty);
            }

            this.ViewBag.listClass = listClass;
            this.ViewBag.idReason = idReason;
            return this.View("ChooseClass");
        }

        public ActionResult ChoosePaper()
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "quanlyyeucaugiayto")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.Papers = new PaperBo().GetListPaper();
            return this.View("ChoosePapers");
        }

        public ActionResult DeleteRequestPaper(int idRequestPaper)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            requestPaperBo.DeleteRequestPaper(idRequestPaper);
            return this.Content("1", "text/plain");
        }

        // GET: ManagerRequest
        public ActionResult Index()
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(this.Session["idAccount"]);
            this.ViewBag.listRequest = requestPaperBo.ListRequestPaper(idAccount, 1);
            return this.View("ListSendRequestPaper");
        }

        public ActionResult ListAllRequestPaper(int value, int page, FormCollection form)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "quanlyyeucaugiayto")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            string search = form["inputSearchRequestPaper"];
            if (search == null)
            {
                if (this.Session["searchrequest"] != null) search = this.Session["searchrequest"].ToString();
            }
            else
            {
                this.Session["searchrequest"] = search;
            }

            RequestPaperBo requestPaperBo = new RequestPaperBo();
            this.ViewBag.listRequest = requestPaperBo.ListAllRequestPaper(value, page, search);
            PageNumber pageNumber = requestPaperBo.GetPageNumber(value, page, search);
            pageNumber.Link = "/ManageRequest/ListAllRequestPaper?value=" + value + "&&page=";
            this.ViewBag.pageNumber = pageNumber;
            this.ViewBag.idRequestStatus = value;
            this.ViewBag.listStatus = new RequestStatusBo().GetRequestStatus();
            this.ViewBag.inforFooter = requestPaperBo.GetInfoFooterPaper(Convert.ToInt32(this.Session["idAccount"]));
            return this.View("ListAllRequestPaper");
        }

        public PartialViewResult ListClassByFaculty(int idFaculty)
        {
            List<Class> listClass = new ClassBO().GetClassByFaculty(idFaculty);
            return this.PartialView("_ListClass", listClass);
        }

        public ActionResult ListSendRequestPaper(int page)
        {
            if (this.Session["idAccount"] == null) return this.RedirectToAction("Index", "Home");

            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(this.Session["idAccount"]);
            Account account = new AccountBO().GetAccount(idAccount);
            if (!account.TypeAccount.Equals("SV")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.listRequest = requestPaperBo.ListRequestPaper(idAccount, page);
            PageNumber pageNumber = requestPaperBo.GetSendPageNumber(idAccount, page);
            pageNumber.Link = "/ManageRequest/ListSendRequestPaper?page=";
            this.ViewBag.pageNumber = pageNumber;
            this.ViewBag.listPaper = new PaperBo().GetListPaper();
            return this.View("ListSendRequestPaper");
        }

        public ActionResult ListStudent(int idReason, int idClass)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "quanlyyeucaugiayto")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.student = new StudentBO().GetListStudentByClass(idClass);
            this.ViewBag.request = new RequestPaperBo().GetRequestByClass(idReason, idClass);
            this.ViewBag.idReason = idReason;
            ReasonRequest reason = new ReasonPaperBo().Get(idReason);
            this.ViewBag.PaperName = reason.Paper.PaperName;
            this.ViewBag.idClass = idClass;
            this.ViewBag.inforFooter =
                new RequestPaperBo().GetInfoFooterPaper(Convert.ToInt32(this.Session["idAccount"]));
            return this.View("ListStudentByClass");
        }

        public PartialViewResult ListStudentByClass(int idClass)
        {
            List<Student> listStudent = new StudentBO().GetListStudentByClass(idClass);
            return this.PartialView("_ListStudent", listStudent);
        }

        public ActionResult UpdateRequest(int idRequest, int numberPaper)
        {
            new RequestPaperBo().UpdateRequest(idRequest, numberPaper);
            return this.Content("1", "text/plain");
        }

        public ActionResult UpdateStatusCancelRequest(int idRequestPaper)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(this.Session["idAccount"]);
            requestPaperBo.UpdateStatusCancelRequestPaper(idRequestPaper, idAccount);
            return this.Content("1", "text/plain");
        }

        public ActionResult UpdateStatusProcessRequest(int idRequestPaper)
        {
            RequestPaperBo requestPaperBo = new RequestPaperBo();
            int idAccount = Convert.ToInt32(this.Session["idAccount"]);
            requestPaperBo.UpdateStatusProcessRequestPaper(idRequestPaper, idAccount);
            return this.Content("1", "text/plain");
        }
    }
}