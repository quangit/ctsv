namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using PagedList;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageNewsEventController : Controller
    {
        private readonly DSAContext dsaContext;

        private readonly NewsEventBO newsEventBO;

        public ManageNewsEventController()
        {
            this.dsaContext = new DSAContext();
            this.newsEventBO = new NewsEventBO(this.dsaContext);
        }

        public ActionResult AddNewsEvent()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewsEvent(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsEvent") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    string eventTime = col["eventTime"];
                    string eventVenue = col["eventVenue"];
                    string beginDate = col["beginDate"];
                    string endDate = col["endDate"];
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string requirement = col["requirement"];
                    string attachedDocuments = col["attachedDocuments"] == null ? string.Empty : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = this.newsEventBO.AddNewsEvent(
                        eventTime,
                        eventVenue,
                        requirement,
                        beginDate,
                        endDate,
                        title,
                        description,
                        contentHtml,
                        image,
                        attachedDocuments,
                        accountSession.FullName,
                        isPinned);
                    if (isSuccess)
                    {
                        this.TempData["success"] = "Thêm dữ liệu thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Thêm dữ liệu thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLySuKien");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult DeleteNewsEvent(int? id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsEvent") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = this.newsEventBO.DeleteNewsEvent(id);
                    if (isSuccess)
                    {
                        this.TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Xóa dữ liệu thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLySuKien");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult NewsEvent(int? page)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsEvent") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                int pageview = page ?? 1;
                IPagedList<NewsEvent> newsEvent = this.newsEventBO.GetListNewsEvent(page);
                return this.View(newsEvent);
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult UpdateNewsEvent(int? id)
        {
            return this.View(this.newsEventBO.GetNewsEvent(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNewsEvent(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsEvent") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string idNewsEvent = col["idNewsEvent"];
                    string eventTime = col["eventTime"];
                    string eventVenue = col["eventVenue"];
                    string beginDate = col["beginDate"];
                    string endDate = col["endDate"];
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string requirement = col["requirement"];
                    string attachedDocuments = col["attachedDocuments"] == null ? string.Empty : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = this.newsEventBO.UpdateNewsEvent(
                        idNewsEvent,
                        eventTime,
                        eventVenue,
                        beginDate,
                        endDate,
                        requirement,
                        title,
                        description,
                        contentHtml,
                        image,
                        attachedDocuments,
                        accountSession.FullName,
                        isPinned);
                    if (isSuccess)
                    {
                        this.TempData["success"] = "Cập nhật dữ liệu thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Cập nhật dữ liệu thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLySuKien");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }
    }
}