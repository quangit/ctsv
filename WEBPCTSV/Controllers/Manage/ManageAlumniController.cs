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
    public class ManageAlumniController : Controller
    {
        private DSAContext dsaContext;
        private NewsBO newsBO;

        public ManageAlumniController()
        {
            dsaContext = new DSAContext();
            newsBO = new NewsBO(dsaContext);
        }

        #region View list news alumni
        public ActionResult NewsAlumni(int? page)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageAlumni") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                int pageview = page ?? 1;
                IPagedList<News> news = newsBO.GetListNews(page, "Alumni");
                return View(news);
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Add news alumni
        public ActionResult AddNewsAlumni()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewsAlumni(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageAlumni") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string attachedDocuments = col["attachedDocuments"] == null ? "" : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    string type = "Alumni";
                    bool isSuccess = newsBO.AddNews(type, title, description, contentHtml, image, attachedDocuments, accountSession.FullName, isPinned);
                    if (isSuccess)
                    {
                        TempData["success"] = "Thêm dữ liệu thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Thêm dữ liệu thất bại!";
                    }
                    return Redirect("/QuanLy/QuanLyCuuSinhVien");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Update news alumni
        public ActionResult UpdateNewsAlumni(int? id)
        {
            return View(newsBO.GetNews(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNewsAlumni(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageAlumni") != -1);
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
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string attachedDocuments = col["attachedDocuments"];
                    string type = "Alumni";
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
                    return Redirect("/QuanLy/QuanLyCuuSinhVien");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region Delete news alumni
        public ActionResult DeleteNewsAlumni(int? id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageAlumni") != -1);
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
                    return Redirect("/QuanLy/QuanLyCuuSinhVien");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion
        #region infomation of alumni
        public ActionResult AlumniInfo()
        {
            ResourceBO resourceBO = new ResourceBO(dsaContext);
            ViewBag.alumniIntro = resourceBO.getResourceObjectByAcronym("GTCSV");
            ViewBag.alumniStar = resourceBO.getResourceObjectByAcronym("GSCSV");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AlumniInfo(FormCollection col)
        {
                AccountSession accountSession = (AccountSession)Session["AccountSession"];
                if (accountSession != null)
                {
                    bool isGranted = (accountSession.Functions.IndexOf("ManageAlumni") != -1);
                    if (!isGranted)
                    {
                        return View("~/Views/Shared/AdminDenyFunction.cshtml");
                    }
                    else
                    {
                        // Granted
                        ResourceBO resourceBO = new ResourceBO(dsaContext);
                        Dictionary<String, String> conductTtemsParameter = col.AllKeys.ToDictionary(k => k, v => col[v]);
                        foreach (KeyValuePair<string, String> itemDic in conductTtemsParameter)
                        {
                            int id;
                            try
                            {
                                String idResource = (itemDic.Key.Split('_'))[1]; ;
                                id = Int32.Parse(idResource);
                                resourceBO.UpdateResource(id, itemDic.Value);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        return Redirect("ThongTinCSV");
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
