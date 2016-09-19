namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using PagedList;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageNewsScholarshipController : Controller
    {
        private readonly DSAContext dsaContext;

        private readonly NewsScholarshipBO newsScholarshipBO;

        public ManageNewsScholarshipController()
        {
            this.dsaContext = new DSAContext();
            this.newsScholarshipBO = new NewsScholarshipBO(this.dsaContext);
        }

        public ActionResult AddNewsScholarship()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewsScholarship(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsScholarship") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    string title = col["title"];
                    string type = col["type"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string sponsor = col["sponsor"];
                    string requirement = col["requirement"];
                    string attachedDocuments = col["attachedDocuments"] == null ? string.Empty : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = this.newsScholarshipBO.AddNewsScholarship(
                        type,
                        sponsor,
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

                    return this.Redirect("/QuanLy/QuanLyHocBong");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult DeleteNewsScholarship(int? id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsScholarship") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = this.newsScholarshipBO.DeleteNewsScholarship(id);
                    if (isSuccess)
                    {
                        this.TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Xóa dữ liệu thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLyHocBong");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult NewsScholarship(int? page)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsScholarship") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                int pageview = page ?? 1;
                IPagedList<NewsScholarship> newsScholarShip = this.newsScholarshipBO.GetListNewsScholarship(
                    page,
                    "HocBong");
                return this.View(newsScholarShip);
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult UpdateNewsScholarship(int? id)
        {
            return this.View(this.newsScholarshipBO.GetNewsScholarship(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNewsScholarship(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageNewsScholarship") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string idNewsScholarship = col["idNewsScholarship"];
                    string type = col["type"];
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string sponsor = col["sponsor"];
                    string requirement = col["requirement"];
                    string attachedDocuments = col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = this.newsScholarshipBO.UpdateNewsScholarship(
                        idNewsScholarship,
                        type,
                        sponsor,
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

                    return this.Redirect("/QuanLy/QuanLyHocBong");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }
    }
}