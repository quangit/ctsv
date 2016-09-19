namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using PagedList;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageNewsJobController : Controller
    {
        private readonly DSAContext dsaContext;

        private readonly NewsJobBO newsJobBO;

        public ManageNewsJobController()
        {
            this.dsaContext = new DSAContext();
            this.newsJobBO = new NewsJobBO(this.dsaContext);
        }

        public ActionResult AddNewsJob()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewsJob(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsJob") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string company = col["company"];
                    string requirement = col["requirement"];
                    string attachedDocuments = col["attachedDocuments"] == null ? string.Empty : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = this.newsJobBO.AddNewsJob(
                        company,
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
                        this.TempData["success"] = "Thêm dữ liệu thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Thêm dữ liệu thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLyTuyenDung");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult DeleteNewsJob(int? id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsJob") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = this.newsJobBO.DeleteNewsJob(id);
                    if (isSuccess)
                    {
                        this.TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Xóa dữ liệu thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLyTuyenDung");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult NewsJob(int? page)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsJob") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                int pageview = page ?? 1;
                IPagedList<NewsJob> NewsJob = this.newsJobBO.GetListNewsJob(page);
                return this.View(NewsJob);
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult UpdateNewsJob(int? id)
        {
            return this.View(this.newsJobBO.GetNewsJob(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNewsJob(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsJob") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string idNewsJob = col["idNewsJob"];
                    string type = col["type"];
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string company = col["company"];
                    string requirement = col["requirement"];
                    string attachedDocuments = col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = this.newsJobBO.UpdateNewsJob(
                        idNewsJob,
                        company,
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

                    return this.Redirect("/QuanLy/QuanLyTuyenDung");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }
    }
}