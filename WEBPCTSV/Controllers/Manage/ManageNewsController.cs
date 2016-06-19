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
    public class ManageNewsController : Controller
    {
        private DSAContext dsaContext;
        private NewsBO newsBO;

        public ManageNewsController()
        {
            dsaContext = new DSAContext();
            newsBO = new NewsBO(dsaContext);
        }

        #region View list news 
        public ActionResult News(int? page)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNews") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                int pageview = page ?? 1;
                IPagedList<News> news = newsBO.GetListNews(page, null);
                return View(news);
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Add news 
        public ActionResult AddNews()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNews(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNews") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");

                }
                else
                {
                    // Granted
                    string title = col["title"];
                    string type = col["type"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string attachedDocuments = col["attachedDocuments"] == null ? "" : col["attachedDocuments"];
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
                    return Redirect("/QuanLy/QuanLyTinTuc");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Update news 
        public ActionResult UpdateNews(int? id)
        {
            return View(newsBO.GetNews(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNews(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNews") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
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
                    bool isSuccess = newsBO.UpdateNews(idNews, type, title, description, contentHtml, image, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Cập nhật dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Cập nhật dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyTinTuc");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Delete news
        public ActionResult DeleteNews(int? id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNews") != -1);
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
                    return Redirect("/QuanLy/QuanLyTinTuc");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PinToTop(FormCollection col)
        {
            try
            {
                int idNews = Int32.Parse(col["idNews"]);
                bool isSuccess = newsBO.PinToTop(idNews);
                if (isSuccess)
                {
                    return Content("Thay đổi ghim tin thành công!");
                }
                else
                {
                    return Content("Thay đổi ghim thất bại!");
                }
            }
            catch (Exception)
            {
                return Content("Thay đổi ghim thất bại!");
            }
        }
        #region pin news into TOP of list
        
        #endregion
    }
}
