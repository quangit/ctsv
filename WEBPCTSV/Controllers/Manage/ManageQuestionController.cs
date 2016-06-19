using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class ManageQuestionController : Controller
    {
        private DSAContext dsaContext;
        private QuestionBO questionBO;

        public ManageQuestionController()
        {
            dsaContext = new DSAContext();
            questionBO = new QuestionBO(dsaContext);
        }

        #region View list question
        public ActionResult Question(int? page)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageQuestion") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                int pageview = page ?? 1;
                IPagedList<Question> question = questionBO.GetListNewQuestion(page);
                return View(question);
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Add question
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCommonQuestion(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageQuestion") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string typeRequest = col["TypeRequest"];
                    string field = col["Field"];
                    string title = col["Title"];
                    string contentHtml = col["ContentHtml"];
                    string reply = col["Reply"];
                    string information = "admin";
                    string name = "admin";
                    string email = "admin";
                    int idQuestion = questionBO.AddQuestion(typeRequest, information, name, email, field, title, contentHtml);
                    if (idQuestion != -1)
                    {
                        bool isSuccess = questionBO.UpdateQuestion(idQuestion, typeRequest, information, name, email, field, title, contentHtml, reply, accountSession.FullName, false);
                        if (isSuccess)
                        {
                            TempData["success"] = "Thêm câu hỏi thành công!";
                        }
                        else
                        {
                            TempData["error"] = "Thêm câu hỏi thất bại!";
                        }
                    }
                    else
                    {
                        TempData["error"] = "Thêm câu hỏi thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyCauHoi");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Update question
        public ActionResult AnswerQuestion(int? id)
        {
            return View(questionBO.GetQuestion(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AnswerQuestion(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageQuestion") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string idQuestion = col["IdQuestion"];
                    string title = col["Title"];
                    string typeRequest = col["TypeRequest"];
                    string field = col["Field"];
                    string information = col["Information"];
                    string email = col["Email"];
                    string name = col["Name"];
                    string contentHtml = col["ContentHtml"];
                    string reply = col["Reply"];
                    bool isEdit = (!string.IsNullOrWhiteSpace(col["IsEdit"])) ? true : false;
                    bool isSuccess = questionBO.UpdateQuestion(Int32.Parse(idQuestion), typeRequest, information, name, email, field, title, contentHtml, reply, accountSession.FullName, isEdit);
                    if (isSuccess)
                    {
                        TempData["success"] = "Cập nhật câu hỏi thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Cập nhật câu hỏi thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyCauHoi");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Delete news
        public ActionResult DeleteQuestion(int? id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageQuestion") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = questionBO.DeleteQuestion(id);
                    if (isSuccess)
                    {
                        TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Xóa dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyCauHoi");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
    }
}
