namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using PagedList;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageDocumentController : Controller
    {
        private readonly DocumentBO documentBO;

        private readonly DSAContext dsaContext;

        public ManageDocumentController()
        {
            this.dsaContext = new DSAContext();
            this.documentBO = new DocumentBO(this.dsaContext);
        }

        public ActionResult AddDocument()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddDocument(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageDocument") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    string type = col["type"];
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string documentNumber = col["documentNumber"];
                    string note = col["note"];
                    string attachedDocuments = col["attachedDocuments"] == null ? string.Empty : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = this.documentBO.AddDocument(
                        type,
                        documentNumber,
                        note,
                        title,
                        description,
                        contentHtml,
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

                    return this.Redirect("/QuanLy/QuanLyVanBan");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult DeleteDocument(int? id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageDocument") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = this.documentBO.DeleteDocument(id);
                    if (isSuccess)
                    {
                        this.TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Xóa dữ liệu thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLyVanBan");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult Document(int? page)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageDocument") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                int pageview = page ?? 1;
                IPagedList<Document> Document = this.documentBO.GetListDocument(page, null);
                return this.View(Document);
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult UpdateDocument(int? id)
        {
            return this.View(this.documentBO.GetDocument(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateDocument(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageDocument") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string idDocument = col["idDocument"];
                    string type = col["type"];
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string documentNumber = col["documentNumber"];
                    string note = col["note"];
                    string attachedDocuments = col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = this.documentBO.UpdateDocument(
                        idDocument,
                        type,
                        documentNumber,
                        note,
                        title,
                        description,
                        contentHtml,
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

                    return this.Redirect("/QuanLy/QuanLyVanBan");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }
    }
}