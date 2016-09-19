namespace WEBPCTSV.Controllers
{
    using System;
    using System.Web.Mvc;

    using PagedList;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageQuestionController : Controller
    {
        private readonly DSAContext dsaContext;

        private readonly QuestionBO questionBO;

        public ManageQuestionController()
        {
            this.dsaContext = new DSAContext();
            this.questionBO = new QuestionBO(this.dsaContext);
        }

        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCommonQuestion(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageQuestion") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
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
                    int idQuestion = this.questionBO.AddQuestion(
                        typeRequest,
                        information,
                        name,
                        email,
                        field,
                        title,
                        contentHtml);
                    if (idQuestion != -1)
                    {
                        bool isSuccess = this.questionBO.UpdateQuestion(
                            idQuestion,
                            typeRequest,
                            information,
                            name,
                            email,
                            field,
                            title,
                            contentHtml,
                            reply,
                            accountSession.FullName,
                            false);
                        if (isSuccess)
                        {
                            this.TempData["success"] = "Thêm câu hỏi thành công!";
                        }
                        else
                        {
                            this.TempData["error"] = "Thêm câu hỏi thất bại!";
                        }
                    }
                    else
                    {
                        this.TempData["error"] = "Thêm câu hỏi thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLyCauHoi");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult AnswerQuestion(int? id)
        {
            return this.View(this.questionBO.GetQuestion(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AnswerQuestion(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageQuestion") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
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
                    bool isSuccess = this.questionBO.UpdateQuestion(
                        int.Parse(idQuestion),
                        typeRequest,
                        information,
                        name,
                        email,
                        field,
                        title,
                        contentHtml,
                        reply,
                        accountSession.FullName,
                        isEdit);
                    if (isSuccess)
                    {
                        this.TempData["success"] = "Cập nhật câu hỏi thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Cập nhật câu hỏi thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLyCauHoi");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult DeleteQuestion(int? id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageQuestion") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = this.questionBO.DeleteQuestion(id);
                    if (isSuccess)
                    {
                        this.TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Xóa dữ liệu thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLyCauHoi");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult Question(int? page)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageQuestion") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                int pageview = page ?? 1;
                IPagedList<Question> question = this.questionBO.GetListNewQuestion(page);
                return this.View(question);
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }
    }
}