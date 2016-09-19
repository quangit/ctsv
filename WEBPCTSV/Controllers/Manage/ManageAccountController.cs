namespace WEBPCTSV.Controllers
{
    using System;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class ManageAccountController : Controller
    {
        readonly AccountBO accountBo = new AccountBO();

        public ActionResult AccountInformation()
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                try
                {
                    this.ViewBag.account = this.accountBo.GetAccount(Convert.ToInt32(this.Session["idAccount"]));
                    return this.View("AccountInformation");
                }
                catch
                {
                    return this.View("~/Views/Shared/Error.cshtml");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult ChangeDecentralizationGroupAccount(int idAccount, int idGroup)
        {
            this.accountBo.ChangeDecentralizationGroup(idAccount, idGroup);
            return this.Content("1", "text/plaint");
        }

        public ActionResult SaveEmail(string email)
        {
            this.accountBo.SaveAccount(Convert.ToInt32(this.Session["idAccount"]),email);
            return this.Content("1", "text/plaint");
        }

        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            string ischangePassword = this.accountBo.ChangePassword(
                Convert.ToInt32(this.Session["idAccount"]),
                oldPassword,
                newPassword);
            return this.Content(ischangePassword, "text/plain");
        }

        public ActionResult GetListAccount(int page, FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlytaikhoan")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            PageNumber pageNumber = new PageNumber();
            string search = form["search"];
            if (search == null)
            {
                if (this.Session["searchaccount"] != null) search = this.Session["searchaccount"].ToString();
            }
            else
            {
                this.Session["searchaccount"] = search;
            }

            DecentralizationGroupBo decentralizationGroupBo = new DecentralizationGroupBo();
            this.ViewBag.listAccount = this.accountBo.GetListAccount(page, search);
            pageNumber.PageNumberTotal = this.accountBo.TotalPage(search);
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/ManageAccount/GetListAccount?page=";
            this.ViewBag.pageNumber = pageNumber;
            this.ViewBag.listGroup = decentralizationGroupBo.getListGroup();
            return this.View("ManageAccount");
        }

        // GET: ManageAccount
        public ActionResult Index()
        {
            this.ViewBag.listAccount = this.accountBo.GetListAccount();
            return this.View("ManageAccount");
        }

        public ActionResult ResetPassword(int idAccount)
        {
            this.accountBo.ResetPassword(idAccount);
            return this.Content("1", "text/plain");
        }

        public ActionResult UpdateAccount(FormCollection form)
        {
            this.accountBo.UpdateAccount(form);
            return this.RedirectToAction("GetListAccount", new { page = 1 });
        }
    }
}