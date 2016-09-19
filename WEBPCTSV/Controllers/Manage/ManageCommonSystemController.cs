namespace WEBPCTSV.Controllers
{
    using System;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class ManageCommonSystemController : Controller
    {
        public ActionResult AddRegency(FormCollection form)
        {
            new RegencyBo().Insert(form);
            return this.RedirectToAction("Regency");
        }

        public ActionResult AddSecondLanguage(FormCollection form)
        {
            new SecondLanguageBo().Insert(form);
            return this.RedirectToAction("SecondLanguage");
        }

        public ActionResult AddSemester()
        {
            new SemesterBO().Insert();
            return this.RedirectToAction("Semester");
        }

        public ActionResult DeleteRegency(string idRegency)
        {
            new RegencyBo().Delete(idRegency);
            return this.RedirectToAction("Regency");
        }

        public ActionResult DeleteSecondLanguage(string idSecondLanguage)
        {
            new SecondLanguageBo().Delete(idSecondLanguage);
            return this.RedirectToAction("SecondLanguage");
        }

        public ActionResult DeleteSemester(int idSemester)
        {
            new SemesterBO().Delete(idSemester);
            return this.RedirectToAction("Semester");
        }

        public ActionResult ImportRegency()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlychucvu")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.SecondLanguage = new SecondLanguageBo().GetListSecondLanguage();
            return this.View("ImportRegency");
        }

        // GET: ManageCommonSystem
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Regency()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlychucvu")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.Regency = new RegencyBo().GetListRegency();
            return this.View("Regency");
        }

        public ActionResult ResetProgressImportRegency()
        {
            new RegencyStudentBo().ResetProgress();
            return this.Content("1", "text/plain");
        }

        public ActionResult SecondLanguage()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyngoaingu")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.SecondLanguage = new SecondLanguageBo().GetListSecondLanguage();
            return this.View("SecondLanguage");
        }

        public ActionResult Semester()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocky")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.Semester = new SemesterBO().Get();
            return this.View("Semester");
        }

        public ActionResult test()
        {
            return this.View("test");
        }

        public ActionResult UpdateRegency(string idRegency, FormCollection form)
        {
            new RegencyBo().Update(idRegency, form);
            return this.RedirectToAction("Regency");
        }

        public ActionResult UpdateSecondLanguage(string idSecondLanguage, FormCollection form)
        {
            new SecondLanguageBo().Update(idSecondLanguage, form);
            return this.RedirectToAction("SecondLanguage");
        }

        public ActionResult UpdateSemester(int idSemester, FormCollection form)
        {
            new SemesterBO().Update(idSemester, form);
            return this.RedirectToAction("Semester");
        }
    }
}