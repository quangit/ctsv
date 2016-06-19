using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;
using WEBPCTSV.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Controllers
{
    public class SocialActivityController : Controller
    {
        // GET: SocialActivity
        StudentSocialActivityBo studentSocialActivityBo = new StudentSocialActivityBo();
        SocialActivityBo socialActivityBo = new SocialActivityBo();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetListSocialActivityNotStudent(int page, int idStudent)
        {
            List<SocialActivity> listSocialActivity = studentSocialActivityBo.GetListSocialActivityNotStudent(page, idStudent);
            return PartialView("~/Views/ManageStudent/_ListSocialActivity.cshtml", listSocialActivity);
        }

        public ActionResult ManageSocialActivity(int page)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhoatdongxahoi")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.SocialActivity = socialActivityBo.GetListSocialActivity(page);
            ViewBag.pageNumber = socialActivityBo.TotalPage(page);
            return View("ManageSocialActivity");
        }

        public ActionResult SearchSocialActivity(string search,int page)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhoatdongxahoi")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.SocialActivity = socialActivityBo.GetListSocialActivity(page,search);
            ViewBag.pageNumber = socialActivityBo.TotalPage(page,search);
            return View("ManageSocialActivity");
        }


        public ActionResult AddStudentSocialActivity(int idStudent , int idSocialActivity)
        {
            studentSocialActivityBo.Add(idStudent, idSocialActivity);
            return Content("", "text/plain");
        }


        public ActionResult DeleteStudentSocialActivity(int idStudentSocialActivity)
        {
            studentSocialActivityBo.Delete(idStudentSocialActivity);
            return Content("", "text/plain");
        }

        public ActionResult UpdateSocialActivity(int idSocialActivity, FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhoatdongxahoi")) return RedirectToAction("NotAccess", "ManageDecentralization");
            socialActivityBo.Update(idSocialActivity, form);
            return RedirectToAction("ManageSocialActivity", new { page = 1 });
        }

        public ActionResult InsertSocialActivity(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhoatdongxahoi")) return RedirectToAction("NotAccess", "ManageDecentralization");
            socialActivityBo.Insert(form);
            return RedirectToAction("ManageSocialActivity", new { page = 1 });
        }

        public ActionResult DeleteSocialActivity(int idSocialActivity)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhoatdongxahoi")) return RedirectToAction("NotAccess", "ManageDecentralization");
            socialActivityBo.Delete(idSocialActivity);
            return RedirectToAction("ManageSocialActivity", new { page = 1 });
        }

    }
}