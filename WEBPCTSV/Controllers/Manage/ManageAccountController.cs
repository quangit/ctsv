using WEBPCTSV.Models.bo;
using WEBPCTSV.Models.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Controllers
{
    public class ManageAccountController : Controller
    {
        AccountBO accountBo = new AccountBO();
        // GET: ManageAccount
        public ActionResult Index()
        {
            ViewBag.listAccount = accountBo.GetListAccount();
            return View("ManageAccount");
        }

        public ActionResult UpdateAccount(FormCollection form)
        {
            accountBo.UpdateAccount(form);
            return RedirectToAction("GetListAccount", new { page =1});
        }

        public ActionResult AccountInformation()
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                try
                {
                    ViewBag.account = accountBo.GetAccount(Convert.ToInt32(Session["idAccount"]));
                    return View("AccountInformation");
                }
                catch
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }

        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            string ischangePassword = accountBo.ChangePassword(Convert.ToInt32(Session["idAccount"]), oldPassword, newPassword);
            return Content(ischangePassword, "text/plain");
        }

        public ActionResult GetListAccount(int page,FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlytaikhoan")) return RedirectToAction("NotAccess", "ManageDecentralization");
            PageNumber pageNumber = new PageNumber();
            string search = form["search"];
            if (search == null)
            {
                if (Session["searchaccount"] != null) search = Session["searchaccount"].ToString();
            }
            else
            {
                Session["searchaccount"] = search;
            }
            DecentralizationGroupBo decentralizationGroupBo = new DecentralizationGroupBo();
            ViewBag.listAccount = accountBo.GetListAccount(page,search);
            pageNumber.PageNumberTotal = accountBo.TotalPage(search);
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/ManageAccount/GetListAccount?page=";
            ViewBag.pageNumber = pageNumber;
            ViewBag.listGroup = decentralizationGroupBo.getListGroup();
            return View("ManageAccount");
        }

        public ActionResult ChangeDecentralizationGroupAccount(int idAccount,int idGroup)
        {
            accountBo.ChangeDecentralizationGroup(idAccount, idGroup);
            return Content("1", "text/plaint");
        }

        

        public ActionResult ResetPassword(int idAccount)
        {
            accountBo.ResetPassword(idAccount);
            return Content("1", "text/plain");
        }
   
    }
}
