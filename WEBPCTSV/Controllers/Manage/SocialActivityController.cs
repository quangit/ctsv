namespace WEBPCTSV.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class SocialActivityController : Controller
    {
        readonly SocialActivityBo socialActivityBo = new SocialActivityBo();

        // GET: SocialActivity
        readonly StudentSocialActivityBo studentSocialActivityBo = new StudentSocialActivityBo();

        public ActionResult AddStudentSocialActivity(int idStudent, int idSocialActivity)
        {
            this.studentSocialActivityBo.Add(idStudent, idSocialActivity);
            return this.Content(string.Empty, "text/plain");
        }

        public ActionResult DeleteSocialActivity(int idSocialActivity)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "quanlyhoatdongxahoi")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.socialActivityBo.Delete(idSocialActivity);
            return this.RedirectToAction("ManageSocialActivity", new { page = 1 });
        }

        public ActionResult DeleteStudentSocialActivity(int idStudentSocialActivity)
        {
            this.studentSocialActivityBo.Delete(idStudentSocialActivity);
            return this.Content(string.Empty, "text/plain");
        }

        public PartialViewResult GetListSocialActivityNotStudent(int page, int idStudent)
        {
            List<SocialActivity> listSocialActivity = this.studentSocialActivityBo.GetListSocialActivityNotStudent(
                page,
                idStudent);
            return this.PartialView("~/Views/ManageStudent/_ListSocialActivity.cshtml", listSocialActivity);
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult InsertSocialActivity(FormCollection form)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "quanlyhoatdongxahoi")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.socialActivityBo.Insert(form);
            return this.RedirectToAction("ManageSocialActivity", new { page = 1 });
        }

        public ActionResult ManageSocialActivity(int page)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "quanlyhoatdongxahoi")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.SocialActivity = this.socialActivityBo.GetListSocialActivity(page);
            this.ViewBag.pageNumber = this.socialActivityBo.TotalPage(page);
            return this.View("ManageSocialActivity");
        }

        public ActionResult SearchSocialActivity(string search, int page)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "quanlyhoatdongxahoi")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.SocialActivity = this.socialActivityBo.GetListSocialActivity(page, search);
            this.ViewBag.pageNumber = this.socialActivityBo.TotalPage(page, search);
            return this.View("ManageSocialActivity");
        }

        public ActionResult UpdateSocialActivity(int idSocialActivity, FormCollection form)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "quanlyhoatdongxahoi")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.socialActivityBo.Update(idSocialActivity, form);
            return this.RedirectToAction("ManageSocialActivity", new { page = 1 });
        }
    }
}