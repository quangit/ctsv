namespace WEBPCTSV.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class ManageClassController : Controller
    {
        public ActionResult AddClass(
            int idFaculty,
            int idCourse,
            string ClassName,
            int NumberMonthSchool,
            string MoneyOfMonth)
        {
            new ClassBO().Insert(idFaculty, idCourse, ClassName, NumberMonthSchool, MoneyOfMonth);
            return this.Content("Class", "text/plain");
        }

        public ActionResult AddCourse(FormCollection form)
        {
            new CourseBo().Insert(form);
            return this.RedirectToAction("Course");
        }

        public ActionResult AddFaculty(FormCollection form)
        {
            new FacultyBO().Insert(form);
            return this.RedirectToAction("Faculty");
        }

        public ActionResult Class()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlylop")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.Class = new ClassBO().Get();
            this.ViewBag.Faculty = new FacultyBO().GetListFaculty();
            this.ViewBag.Course = new CourseBo().GetListCourse();
            return this.View("Class");
        }

        public ActionResult Course()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlykhoas")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.Course = new CourseBo().GetListCourse();
            return this.View("Course");
        }

        public ActionResult DeleteClass(int idClass)
        {
            new ClassBO().Delete(idClass);
            return this.RedirectToAction("Class");
        }

        public ActionResult DeleteCourse(int idCourse)
        {
            new CourseBo().Delete(idCourse);
            return this.RedirectToAction("Course");
        }

        public ActionResult DeleteFaculty(int idFaculty)
        {
            new FacultyBO().Delete(idFaculty);
            return this.RedirectToAction("Faculty");
        }

        public ActionResult Faculty()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlykhoa")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.Faculty = new FacultyBO().GetListFaculty();
            return this.View("Faculty");
        }

        public PartialViewResult GetListClass(int idFaculty, int idCourse)
        {
            List<Class> listClass = new ClassBO().Get(idFaculty, idCourse);
            return this.PartialView("_ListClass", listClass);
        }

        public ActionResult GetNameClass(int idCourse)
        {
            string nameClass = new ClassBO().GetNameClass(idCourse);
            return this.Content(nameClass, "text/plain");
        }

        // GET: ManageClass
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult UpdateClass(int idClass, FormCollection form)
        {
            new ClassBO().Update(idClass, form);
            return this.RedirectToAction("Class");
        }

        public ActionResult UpdateCourse(int idCourse, FormCollection form)
        {
            new CourseBo().Update(idCourse, form);
            return this.RedirectToAction("Course");
        }

        public ActionResult UpdateFaculty(int idFaculty, FormCollection form)
        {
            new FacultyBO().Update(idFaculty, form);
            return this.RedirectToAction("Faculty");
        }
    }
}