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
    public class ManageDocumentController : Controller
    {
        private DSAContext dsaContext;
        private DocumentBO documentBO;

        public ManageDocumentController()
        {
            dsaContext = new DSAContext();
            documentBO = new DocumentBO(dsaContext);
        }

        #region View list document
        public ActionResult Document(int? page)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageDocument") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");

                }
                int pageview = page ?? 1;
                IPagedList<Document> Document = documentBO.GetListDocument(page, null);
                return View(Document);
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Add document
        public ActionResult AddDocument()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddDocument(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageDocument") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    string type = col["type"];
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string documentNumber = col["documentNumber"];
                    string note = col["note"];
                    string attachedDocuments = col["attachedDocuments"] == null ? "" : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = documentBO.AddDocument(type, documentNumber, note, title, description, contentHtml, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Thêm dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Thêm dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyVanBan");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Update document
        public ActionResult UpdateDocument(int? id)
        {
            return View(documentBO.GetDocument(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateDocument(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageDocument") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");

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
                    bool isSuccess = documentBO.UpdateDocument(idDocument, type, documentNumber, note, title, description, contentHtml, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Cập nhật dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Cập nhật dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyVanBan");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Delete document
        public ActionResult DeleteDocument(int? id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageDocument") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = documentBO.DeleteDocument(id);
                    if (isSuccess)
                    {
                        TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Xóa dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyVanBan");
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
