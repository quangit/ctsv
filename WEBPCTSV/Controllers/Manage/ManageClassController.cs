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
    public class ManageClassController : Controller
    {
        // GET: ManageClass
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Class()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlylop")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.Class = new ClassBO().Get();
            ViewBag.Faculty = new FacultyBO().GetListFaculty();
            ViewBag.Course = new CourseBo().GetListCourse();
            return View("Class");
        }
        public PartialViewResult GetListClass(int idFaculty, int idCourse)
        {
            List<Class> listClass = new ClassBO().Get(idFaculty, idCourse);
            return PartialView("_ListClass", listClass);
        }
        public ActionResult GetNameClass(int idCourse)
        {
            string nameClass = new ClassBO().GetNameClass(idCourse);
            return Content(nameClass, "text/plain");
        }
        public ActionResult AddClass(int idFaculty, int idCourse, string ClassName, int NumberMonthSchool, string MoneyOfMonth)
        {
            new ClassBO().Insert(idFaculty, idCourse, ClassName,NumberMonthSchool,MoneyOfMonth);
            return Content("Class", "text/plain");
        }
        public ActionResult UpdateClass(int idClass, FormCollection form)
        {
            new ClassBO().Update(idClass, form);
            return RedirectToAction("Class");
        }
        public ActionResult DeleteClass(int idClass)
        {
            new ClassBO().Delete(idClass);
            return RedirectToAction("Class");
        }

        public ActionResult Faculty()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlykhoa")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.Faculty = new FacultyBO().GetListFaculty();
            return View("Faculty");
        }
   

        public ActionResult AddFaculty(FormCollection form)
        {
            new FacultyBO().Insert(form);
            return RedirectToAction("Faculty");
        }
        public ActionResult UpdateFaculty(int idFaculty, FormCollection form)
        {
            new FacultyBO().Update(idFaculty, form);
            return RedirectToAction("Faculty");
        }
        public ActionResult DeleteFaculty(int idFaculty)
        {
            new FacultyBO().Delete(idFaculty);
            return RedirectToAction("Faculty");
        }

        public ActionResult Course()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlykhoas")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.Course = new CourseBo().GetListCourse();
            return View("Course");
        }

        public ActionResult AddCourse(FormCollection form)
        {
            new CourseBo().Insert(form);
            return RedirectToAction("Course");
        }
        public ActionResult UpdateCourse(int idCourse, FormCollection form)
        {
            new CourseBo().Update(idCourse, form);
            return RedirectToAction("Course");
        }
        public ActionResult DeleteCourse(int idCourse)
        {
            new CourseBo().Delete(idCourse);
            return RedirectToAction("Course");
        }
    }
}