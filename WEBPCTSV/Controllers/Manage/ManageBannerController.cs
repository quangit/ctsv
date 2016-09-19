namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using PagedList;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageBannerController : Controller
    {
        private readonly DSAContext dsaContext;

        private readonly NewsBO newsBO;

        public ManageBannerController()
        {
            this.dsaContext = new DSAContext();
            this.newsBO = new NewsBO(this.dsaContext);
        }

        public ActionResult AddBanner()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddBanner(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageBanner") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string title = col["title"];
                    string description = col["description"];
                    string image = col["image"];
                    string attachedDocuments = col["attachedDocuments"] == null ? string.Empty : col["attachedDocuments"];
                    string type = "Banner";
                    string contentHtml = "Banner";
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

                    return this.Redirect("/QuanLy/QuanLyBanner");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult Banner(int? page)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageBanner") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                int pageview = page ?? 1;
                IPagedList<News> banner = this.newsBO.GetListNews(page, "Banner");
                return this.View(banner);
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult DeleteBanner(int? id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageBanner") != -1;
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

                    return this.Redirect("/QuanLy/QuanLyBanner");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult UpdateBanner(int? id)
        {
            return this.View(this.newsBO.GetNews(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateBanner(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageBanner") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string idNews = col["idNews"];
                    string title = col["title"];
                    string description = col["description"];
                    string image = col["image"];
                    string attachedDocuments = col["attachedDocuments"];
                    string type = "Banner";
                    string contentHtml = "Banner";
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

                    return this.Redirect("/QuanLy/QuanLyBanner");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }
    }
}