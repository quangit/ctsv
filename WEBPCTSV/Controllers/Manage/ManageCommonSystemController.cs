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
    public class ManageCommonSystemController : Controller
    {
        // GET: ManageCommonSystem
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SecondLanguage()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyngoaingu")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.SecondLanguage = new SecondLanguageBo().GetListSecondLanguage();
            return View("SecondLanguage");
        }
        public ActionResult ImportRegency()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlychucvu")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.SecondLanguage = new SecondLanguageBo().GetListSecondLanguage();
            return View("ImportRegency");
        }
        public ActionResult AddSecondLanguage(FormCollection form)
        {
            new SecondLanguageBo().Insert(form);
            return RedirectToAction("SecondLanguage");
        }
        public ActionResult UpdateSecondLanguage(string idSecondLanguage, FormCollection form)
        {
            new SecondLanguageBo().Update(idSecondLanguage, form);
            return RedirectToAction("SecondLanguage");
        }
        public ActionResult DeleteSecondLanguage(string idSecondLanguage)
        {
            new SecondLanguageBo().Delete(idSecondLanguage);
            return RedirectToAction("SecondLanguage");
        }


        public ActionResult Regency()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlychucvu")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.Regency = new RegencyBo().GetListRegency();
            return View("Regency");
        }
        public ActionResult AddRegency(FormCollection form)
        {
            new RegencyBo().Insert(form);
            return RedirectToAction("Regency");
        }
        public ActionResult UpdateRegency(string idRegency, FormCollection form)
        {
            new RegencyBo().Update(idRegency, form);
            return RedirectToAction("Regency");
        }
        public ActionResult ResetProgressImportRegency()
        {
            new RegencyStudentBo().ResetProgress();
            return Content("1", "text/plain");
        }
        public ActionResult DeleteRegency(string idRegency)
        {
            new RegencyBo().Delete(idRegency);
            return RedirectToAction("Regency");
        }


        public ActionResult Semester()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocky")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.Semester = new SemesterBO().Get();
            return View("Semester");
        }
        public ActionResult AddSemester()
        {
            new SemesterBO().Insert();
            return RedirectToAction("Semester");
        }
        public ActionResult UpdateSemester(int idSemester, FormCollection form)
        {
            new SemesterBO().Update(idSemester, form);
            return RedirectToAction("Semester");
        }
        public ActionResult DeleteSemester(int idSemester)
        {
            new SemesterBO().Delete(idSemester);
            return RedirectToAction("Semester");
        }

    }
}