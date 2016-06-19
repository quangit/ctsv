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
    public class ManageNewsJobController : Controller
    {
        private DSAContext dsaContext;
        private NewsJobBO newsJobBO;

        public ManageNewsJobController()
        {
            dsaContext = new DSAContext();
            newsJobBO = new NewsJobBO(dsaContext);
        }

        #region View list news job
        public ActionResult NewsJob(int? page)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsJob") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");

                }
                int pageview = page ?? 1;
                IPagedList<NewsJob> NewsJob = newsJobBO.GetListNewsJob(page);
                return View(NewsJob);
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Add news job
        public ActionResult AddNewsJob()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewsJob(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsJob") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string company = col["company"];
                    string requirement = col["requirement"];
                    string attachedDocuments = col["attachedDocuments"] == null ? "" : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = newsJobBO.AddNewsJob(company, requirement, title, description, contentHtml, image, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Thêm dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Thêm dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyTuyenDung");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Update news job
        public ActionResult UpdateNewsJob(int? id)
        {
            return View(newsJobBO.GetNewsJob(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNewsJob(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsJob") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");

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
                    bool isSuccess = newsJobBO.UpdateNewsJob(idNewsJob, company, requirement, title, description, contentHtml, image, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Cập nhật dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Cập nhật dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyTuyenDung");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Delete news job
        public ActionResult DeleteNewsJob(int? id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsJob") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = newsJobBO.DeleteNewsJob(id);
                    if (isSuccess)
                    {
                        TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Xóa dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyTuyenDung");
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
