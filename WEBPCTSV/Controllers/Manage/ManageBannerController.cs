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
    public class ManageBannerController : Controller
    {
        private DSAContext dsaContext;
        private NewsBO newsBO;

        public ManageBannerController()
        {
            dsaContext = new DSAContext();
            newsBO = new NewsBO(dsaContext);
        }

        #region View list banner
        public ActionResult Banner(int? page)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageBanner") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                int pageview = page ?? 1;
                IPagedList<News> banner = newsBO.GetListNews(page, "Banner");
                return View(banner);
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Add banner
        public ActionResult AddBanner()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddBanner(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageBanner") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");

                }
                else
                {
                    // Granted
                    string title = col["title"];
                    string description = col["description"];
                    string image = col["image"];
                    string attachedDocuments = col["attachedDocuments"] == null ? "" : col["attachedDocuments"];
                    string type = "Banner";
                    string contentHtml = "Banner";
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = newsBO.AddNews(type, title, description, contentHtml, image, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Thêm dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Thêm dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyBanner");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Update banner
        public ActionResult UpdateBanner(int? id)
        {
            return View(newsBO.GetNews(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateBanner(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageBanner") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
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
                    bool isSuccess = newsBO.UpdateNews(idNews, type, title, description, contentHtml, image, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Cập nhật dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Cập nhật dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyBanner");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Delete news
        public ActionResult DeleteBanner(int? id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageBanner") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = newsBO.DeleteNews(id);
                    if (isSuccess)
                    {
                        TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Xóa dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyBanner");
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
