namespace WEBPCTSV.Controllers
{
    using System;
    using System.Web.Mvc;

    using PagedList;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageNewsController : Controller
    {
        private readonly DSAContext dsaContext;

        private readonly NewsBO newsBO;

        public ManageNewsController()
        {
            this.dsaContext = new DSAContext();
            this.newsBO = new NewsBO(this.dsaContext);
        }

        public ActionResult AddNews()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNews(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNews") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string title = col["title"];
                    string type = col["type"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string attachedDocuments = col["attachedDocuments"] == null ? string.Empty : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = this.newsBO.AddNews(
                        type,
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

                    return this.Redirect("/QuanLy/QuanLyTinTuc");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult DeleteNews(int? id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNews") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = this.newsBO.DeleteNews(id);
                    if (isSuccess)
                    {
                        this.TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Xóa dữ liệu thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLyTinTuc");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult News(int? page)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNews") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                int pageview = page ?? 1;
                IPagedList<News> news = this.newsBO.GetListNews(page, null);
                return this.View(news);
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PinToTop(FormCollection col)
        {
            try
            {
                int idNews = int.Parse(col["idNews"]);
                bool isSuccess = this.newsBO.PinToTop(idNews);
                if (isSuccess)
                {
                    return this.Content("Thay đổi ghim tin thành công!");
                }
                else
                {
                    return this.Content("Thay đổi ghim thất bại!");
                }
            }
            catch (Exception)
            {
                return this.Content("Thay đổi ghim thất bại!");
            }
        }

        public ActionResult UpdateNews(int? id)
        {
            return this.View(this.newsBO.GetNews(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNews(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNews") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string idNews = col["idNews"];
                    string type = col["type"];
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string attachedDocuments = col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = this.newsBO.UpdateNews(
                        idNews,
                        type,
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

                    return this.Redirect("/QuanLy/QuanLyTinTuc");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }
    }
}