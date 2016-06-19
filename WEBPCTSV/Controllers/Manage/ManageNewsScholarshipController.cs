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
    public class ManageNewsScholarshipController : Controller
    {
        private DSAContext dsaContext;
        private NewsScholarshipBO newsScholarshipBO;

        public ManageNewsScholarshipController()
        {
            dsaContext = new DSAContext();
            newsScholarshipBO = new NewsScholarshipBO(dsaContext);
        }

        #region View list news Scholarship
        public ActionResult NewsScholarship(int? page)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsScholarship") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");

                }
                int pageview = page ?? 1;
                IPagedList<NewsScholarship> newsScholarShip = newsScholarshipBO.GetListNewsScholarship(page, "HocBong");
                return View(newsScholarShip);
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Add news scholarship
        public ActionResult AddNewsScholarship()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewsScholarship(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsScholarship") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
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
                    string attachedDocuments = col["attachedDocuments"] == null ? "" : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = newsScholarshipBO.AddNewsScholarship(type, sponsor, requirement, title, description, contentHtml, image, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Thêm dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Thêm dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyHocBong");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Update news scholarship
        public ActionResult UpdateNewsScholarship(int? id)
        {
            return View(newsScholarshipBO.GetNewsScholarship(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNewsScholarship(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsScholarship") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");

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
                    bool isSuccess = newsScholarshipBO.UpdateNewsScholarship(idNewsScholarship, type, sponsor, requirement, title, description, contentHtml, image, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Cập nhật dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Cập nhật dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyHocBong");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Delete news scholar ship
        public ActionResult DeleteNewsScholarship(int? id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsScholarship") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = newsScholarshipBO.DeleteNewsScholarship(id);
                    if (isSuccess)
                    {
                        TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Xóa dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyHocBong");
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
