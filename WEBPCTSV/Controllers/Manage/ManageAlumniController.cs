namespace WEBPCTSV.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using PagedList;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageAlumniController : Controller
    {
        private readonly DSAContext dsaContext;

        private readonly NewsBO newsBO;

        public ManageAlumniController()
        {
            this.dsaContext = new DSAContext();
            this.newsBO = new NewsBO(this.dsaContext);
        }

        public ActionResult AddNewsAlumni()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewsAlumni(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageAlumni") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    string title = col["title"];
                    string description = col["description"];
                    string contentHtml = col["contentHtml"];
                    string image = col["image"];
                    string attachedDocuments = col["attachedDocuments"] == null ? string.Empty : col["attachedDocuments"];
                    bool isPinned = (!string.IsNullOrWhiteSpace(col["isPinned"])) ? true : false;
                    string type = "Alumni";
                    bool isSuccess = this.newsBO.AddNews(
                        type,
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

                    return this.Redirect("/QuanLy/QuanLyCuuSinhVien");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult AlumniInfo()
        {
            ResourceBO resourceBO = new ResourceBO(this.dsaContext);
            this.ViewBag.alumniIntro = resourceBO.getResourceObjectByAcronym("GTCSV");
            this.ViewBag.alumniStar = resourceBO.getResourceObjectByAcronym("GSCSV");
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AlumniInfo(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageAlumni") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    ResourceBO resourceBO = new ResourceBO(this.dsaContext);
                    Dictionary<string, string> conductTtemsParameter = col.AllKeys.ToDictionary(k => k, v => col[v]);
                    foreach (KeyValuePair<string, string> itemDic in conductTtemsParameter)
                    {
                        int id;
                        try
                        {
                            string idResource = itemDic.Key.Split('_')[1];
                            
                            id = int.Parse(idResource);
                            resourceBO.UpdateResource(id, itemDic.Value);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    return this.Redirect("ThongTinCSV");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult DeleteNewsAlumni(int? id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageAlumni") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    bool isSuccess = this.newsBO.DeleteNews(id);
                    if (isSuccess)
                    {
                        this.TempData["success"] = "Xóa dữ liệu thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Xóa dữ liệu thất bại!";
                    }

                    return this.Redirect("/QuanLy/QuanLyCuuSinhVien");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult NewsAlumni(int? page)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageAlumni") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                int pageview = page ?? 1;
                IPagedList<News> news = this.newsBO.GetListNews(page, "Alumni");
                return this.View(news);
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult UpdateNewsAlumni(int? id)
        {
            return this.View(this.newsBO.GetNews(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNewsAlumni(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageAlumni") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
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
                    bool isSuccess = this.newsBO.UpdateNews(
                        idNews,
                        type,
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

                    return this.Redirect("/QuanLy/QuanLyCuuSinhVien");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }
    }
}