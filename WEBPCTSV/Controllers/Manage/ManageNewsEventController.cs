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
    public class ManageNewsEventController : Controller
    {
        private DSAContext dsaContext;
        private NewsEventBO newsEventBO;

        public ManageNewsEventController()
        {
            dsaContext = new DSAContext();
            newsEventBO = new NewsEventBO(dsaContext);
        }

        #region View list news event
        public ActionResult NewsEvent(int? page)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsEvent") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                int pageview = page ?? 1;
                IPagedList<NewsEvent> newsEvent = newsEventBO.GetListNewsEvent(page);
                return View(newsEvent);
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Add news event
        public ActionResult AddNewsEvent()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewsEvent(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsEvent") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    string eventTime = col["eventTime"];
                    string eventVenue = col["eventVenue"];
                    string beginDate = col["beginDate"];
                    string endDate = col["endDate"];
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string requirement = col["requirement"];
                    string attachedDocuments = col["attachedDocuments"] == null ? "" : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = newsEventBO.AddNewsEvent(eventTime, eventVenue, requirement, beginDate, endDate, title, description, contentHtml, image, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Thêm dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Thêm dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLySuKien");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Update news event
        public ActionResult UpdateNewsEvent(int? id)
        {
            return View(newsEventBO.GetNewsEvent(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNewsEvent(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsEvent") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");

                }
                else
                {
                    // Granted
                    string idNewsEvent = col["idNewsEvent"];
                    string eventTime = col["eventTime"];
                    string eventVenue = col["eventVenue"];
                    string beginDate = col["beginDate"];
                    string endDate = col["endDate"];
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string requirement = col["requirement"];
                    string attachedDocuments = col["attachedDocuments"] == null ? "" : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    bool isSuccess = newsEventBO.UpdateNewsEvent(idNewsEvent, eventTime, eventVenue, beginDate, endDate, requirement, title, description, contentHtml, image, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Cập nhật dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Cập nhật dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLySuKien");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Delete news event
        public ActionResult DeleteNewsEvent(int? id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageNewsEvent") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = newsEventBO.DeleteNewsEvent(id);
                    if (isSuccess)
                    {
                        TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Xóa dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLySuKien");
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
