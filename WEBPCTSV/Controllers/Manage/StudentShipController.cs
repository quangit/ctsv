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
    public class StudentShipController : Controller
    {
        LearningOutComeBo learningOutComeBo = new LearningOutComeBo();

        // GET: StudentShip
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImportScoteStudent()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocbong")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.listSemester = new SemesterBO().Get();
            return View("ImportScoteStudent");
        }

        public ActionResult ResetPercentProgress()
        {
            LearningOutComeBo.percentProgress = 0;
            return Content("", "text/plain");
        }
        public ActionResult ListStudentShip(int page, FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocbong")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.listSemester = new SemesterBO().Get();
            ViewBag.listLearningOutCome = learningOutComeBo.Get(page, form);
            ViewBag.pageNumber = learningOutComeBo.TotalPage(page, form);
            return View("ListConsiderStudentShip");
        }

        public ActionResult ListScoteStudent(int idSemester, int page, FormCollection form)
        {
            string search = form["search"];
            if (search == null)
            {
                if (Session["search"] != null) search = Session["search"].ToString();
            }
            else
            {
                Session["search"] = search;
            }
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocbong")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.listSemester = new SemesterBO().Get();
            ViewBag.Semester = idSemester; 
            ViewBag.listLearningOutCome = learningOutComeBo.Get(idSemester, page,search);
            PageNumber pagenumber = learningOutComeBo.TotalPage(idSemester, page,search,"");
            ViewBag.pageNumber = pagenumber;
            return View("ListScoteStudent");
        }

        public ActionResult ListStudentShipSchoolFaculty(int idStudentShipSchoolFaculty,int page,FormCollection form)
        {
            string search = form["search"];
            if(search==null)
            {
                if (Session["search"]!=null) search = Session["search"].ToString();
            }
            else {
                Session["search"] = search;
            }
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocbong")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.StudentShipSchoolFaculty = new StudentShipSchoolFacultyBo().GetById(idStudentShipSchoolFaculty);
            ViewBag.listLearningOutCome = learningOutComeBo.GetByIdFaculty(page, idStudentShipSchoolFaculty,search);
            ViewBag.pageNumber = learningOutComeBo.TotalPage(page, idStudentShipSchoolFaculty,search);
            ViewBag.ConditionStudentShip = new ConditionStudentShipSchoolBo().Get(idStudentShipSchoolFaculty);
            ViewBag.RankingAcademic = new RankingAcademicBo().Get();
            ViewBag.SURPLUSMoney = learningOutComeBo.GetSURPLUSMoney(idStudentShipSchoolFaculty);
            ViewBag.CountStudentShip = learningOutComeBo.GetCountStudentShip(idStudentShipSchoolFaculty);
            return View("ListConsiderStudentShip");
        }
        public ActionResult ListStudentShipSchoolFacultyCLC(int idStudentShipSchoolFaculty, int page,FormCollection form)
        {
            string search = form["search"];
            if (search == null)
            {
                if (Session["search"] != null) search = Session["search"].ToString();
            }
            else
            {
                Session["search"] = search;
            }
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocbong")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.StudentShipSchoolFaculty = new StudentShipSchoolFacultyBo().GetById(idStudentShipSchoolFaculty);
            ViewBag.listLearningOutCome = learningOutComeBo.GetByIdFacultyCLC(page, idStudentShipSchoolFaculty,search);
            ViewBag.pageNumber = learningOutComeBo.TotalPage(page, idStudentShipSchoolFaculty,search);
            ViewBag.ConditionStudentShip = new ConditionStudentShipSchoolBo().Get(idStudentShipSchoolFaculty);
            ViewBag.SURPLUSMoney = learningOutComeBo.GetSURPLUSMoney(idStudentShipSchoolFaculty);
            ViewBag.CountStudentShip = learningOutComeBo.GetCountStudentShipCLC(idStudentShipSchoolFaculty);
            ViewBag.top1 = new StudentShipCLCBo().GetTop1();
            ViewBag.top2 = new StudentShipCLCBo().GetTop2();          
            return View("ListConsiderStudentShipCLC");
        }

        public ActionResult GetMoneyByCountStudentShip(int idStudentShipSchoolFaculty,int countStudentShip)
        {
            string totalMoney = learningOutComeBo.GetMoneyByCount(idStudentShipSchoolFaculty, countStudentShip);
            return Content(totalMoney, "text/Plain");
        }

        public ActionResult ChangeConditionStudentShip(int idStudentShipSchoolFaculty, FormCollection form)
        {
            new ConditionStudentShipSchoolBo().Update(idStudentShipSchoolFaculty, form);
            return RedirectToAction("ListStudentShipSchoolFaculty", new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty, page = 1 });
        }

        public ActionResult ChangeMoneyRankingAcademic(int idStudentShipSchoolFaculty, FormCollection form)
        {
            new RankingAcademicBo().Update(form);
            return RedirectToAction("ListStudentShipSchoolFaculty", new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty, page = 1 });
        }

        public ActionResult UpdateMoneyStudentShipFaculty(int idStudentShipSchoolFaculty, FormCollection form)
        {
            new StudentShipSchoolFacultyBo().Update(idStudentShipSchoolFaculty, form);
            return RedirectToAction("ListStudentShipSchoolFaculty", new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty, page = 1 });

        }
        public ActionResult UpdateMoneyStudentShipFacultyCLC(int idStudentShipSchoolFaculty, FormCollection form)
        {
            new StudentShipSchoolFacultyBo().Update(idStudentShipSchoolFaculty, form);
            return RedirectToAction("ListStudentShipSchoolFacultyCLC", new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty, page = 1 });

        }


        public ActionResult StudentShipFaculty(int idSemester)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocbong")) return RedirectToAction("NotAccess", "ManageDecentralization");
            StudentShipSchool studentShipSchool = new StudentShipSchoolBo().Get(idSemester);
            ViewBag.studentShipSchool = studentShipSchool;
            ViewBag.Faculty = new FacultyBO().GetListFaculty();
            ViewBag.listStudentShipSchoolFaculty = new StudentShipSchoolFacultyBo().Get(studentShipSchool.IdStudentShipSchool);
            ViewBag.surplus = new StudentShipSchoolFacultyBo().GetSURPLUS(studentShipSchool.IdStudentShipSchool);
            return View("StudentShipFaculty");
        }
        public ActionResult StudentShipFacultyCLC(int idSemester)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocbong")) return RedirectToAction("NotAccess", "ManageDecentralization");
            StudentShipSchool studentShipSchool = new StudentShipSchoolBo().Get(idSemester);
            ViewBag.studentShipSchool = studentShipSchool;
            ViewBag.ClassCLC = new ClassBO().GetClassCLC();
            ViewBag.listStudentShipSchoolFaculty = new StudentShipSchoolFacultyBo().Get(studentShipSchool.IdStudentShipSchool);
            ViewBag.surplus = new StudentShipSchoolFacultyBo().GetSURPLUS(studentShipSchool.IdStudentShipSchool);
            return View("StudentShipFacultyCLC");
        }

        public ActionResult SelectSemester()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocbong")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.listSemester = new SemesterBO().Get();
            return View("SelectSemester");
        }
        public ActionResult SelectSemesterScoteStudent()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocbong")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.listSemester = new SemesterBO().Get();
            return View("SelectSemesterScoteStudent");
        }

        public ActionResult GetMoneyStudentShipSchool(int idSemester)
        {

            string money = new StudentShipSchoolBo().GetMoney(idSemester);
            return Content(money, "text/plain");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult AddStudentShip(FormCollection form)
        {
            int idSemester = Convert.ToInt32(form["selectSemester"]);
            new StudentShipSchoolBo().Insert(form);
            return RedirectToAction("StudentShipFaculty", new { idSemester = idSemester });
        }

        public ActionResult UpdateMoneyStudentShipShoolFaculty(int idFaculty, int idStudentShipSchool, string totalMoney)
        {
            new StudentShipSchoolFacultyBo().InsertStudentShipFaculty(idStudentShipSchool, idFaculty, null, ConvertObject.ConvertCurrencyToString(totalMoney), null);
            new StudentShipSchoolBo().UpdateMoney(idStudentShipSchool);
            return Content("1", "text/plain");
        }

        public ActionResult UpdateMoneyStudentShipShoolFacultyCLC(int idClass, int idStudentShipSchool, string totalMoney, string tuitionFee)
        {
            new StudentShipSchoolFacultyBo().InsertStudentShipFaculty(idStudentShipSchool, null, idClass, ConvertObject.ConvertCurrencyToString(totalMoney), ConvertObject.ConvertCurrencyToString(tuitionFee));
            new StudentShipSchoolBo().UpdateMoney(idStudentShipSchool);
            return Content("1", "text/plain");
        }

        public ActionResult ListStudentShipSchoolStudent(int idStudentShipSchoolFaculty)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlyhocbong")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.StudentShipSchoolFaculty = new StudentShipSchoolFacultyBo().GetById(idStudentShipSchoolFaculty);
            ViewBag.listStudentShipSchoolStudent = new StudentShipSchoolStudentBo().GetList(idStudentShipSchoolFaculty);
            return View("ListStudentShipSchoolStudent");
        }

        public ActionResult SaveStudentShipSchoolStudent(int idStudentShipSchoolFaculty)
        {
            learningOutComeBo.SaveStudentShipSchoolStudent(idStudentShipSchoolFaculty);
            return RedirectToAction("ListStudentShipSchoolStudent", new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty });
        }

        public ActionResult SaveStudentShipSchoolStudentCLC(int idStudentShipSchoolFaculty)
        {
            learningOutComeBo.SaveStudentShipSchoolStudentCLC(idStudentShipSchoolFaculty);
            return RedirectToAction("ListStudentShipSchoolStudent", new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty });
        }

        public ActionResult GetProgressSaveStudentShipSchoolStudent()
        {
            int progress = StudentShipSchoolStudentBo.percentProgress;
            return Content(progress + "", "text/plain");
        }

        public ActionResult GetProgressExportStudentShip()
        {
            int progress = StudentShipSchoolBo.percentProgress;
            return Content(progress + "", "text/plain");
        }


        public ActionResult UpdateIsAcceptConsider(int idLearningOutCome) {
            learningOutComeBo.UpdateIsAcceptConsider(idLearningOutCome);
            return Content("1", "text/plain");
        }

        public ActionResult UpdateIsDisEnableConsider(int idLearningOutCome)
        {
            learningOutComeBo.UpdateIsDisEnableConsider(idLearningOutCome);
            return Content("1", "text/plain");
        }

        public ActionResult MoveMoneyStudentShip(string money, int idStudentShipSchoolFaculty)
        {
            new StudentShipSchoolFacultyBo().MoveMoneyStudentShip(money, idStudentShipSchoolFaculty);
            return RedirectToAction("ListStudentShipSchoolFacultyCLC", new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty, page = 1 });
        }

        public ActionResult SendStudentShipApplication()
        {
            ViewBag.listSemester = new SemesterBO().Get();
            return View("SendStudentshipApplication");
        }

        public ActionResult AddStudentShipApplication(int selectSemester)
        {
            Student student = new StudentBO().GetStudentByIdAccount(Convert.ToInt32(Session["idAccount"]));
            new StudentshipApplicationBo().Add(selectSemester, student.IdStudent);
            return RedirectToAction("SendStudentShipApplication");
        }

    }
}