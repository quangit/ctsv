namespace WEBPCTSV.Controllers
{
    using System;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class StudentShipController : Controller
    {
        readonly LearningOutComeBo learningOutComeBo = new LearningOutComeBo();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public ActionResult AddStudentShip(FormCollection form)
        {
            int idSemester = Convert.ToInt32(form["selectSemester"]);
            new StudentShipSchoolBo().Insert(form);
            return this.RedirectToAction("StudentShipFaculty", new { idSemester = idSemester });
        }

        public ActionResult AddStudentShipApplication(int selectSemester)
        {
            Student student = new StudentBO().GetStudentByIdAccount(Convert.ToInt32(this.Session["idAccount"]));
            new StudentshipApplicationBo().Add(selectSemester, student.IdStudent);
            return this.RedirectToAction("SendStudentShipApplication");
        }

        public ActionResult ChangeConditionStudentShip(int idStudentShipSchoolFaculty, FormCollection form)
        {
            new ConditionStudentShipSchoolBo().Update(idStudentShipSchoolFaculty, form);
            return this.RedirectToAction(
                "ListStudentShipSchoolFaculty",
                new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty, page = 1 });
        }

        public ActionResult ChangeMoneyRankingAcademic(int idStudentShipSchoolFaculty, FormCollection form)
        {
            new RankingAcademicBo().Update(form);
            return this.RedirectToAction(
                "ListStudentShipSchoolFaculty",
                new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty, page = 1 });
        }

        public ActionResult GetMoneyByCountStudentShip(int idStudentShipSchoolFaculty, int countStudentShip)
        {
            string totalMoney = this.learningOutComeBo.GetMoneyByCount(idStudentShipSchoolFaculty, countStudentShip);
            return this.Content(totalMoney, "text/Plain");
        }

        public ActionResult GetMoneyStudentShipSchool(int idSemester)
        {
            string money = new StudentShipSchoolBo().GetMoney(idSemester);
            return this.Content(money, "text/plain");
        }

        public ActionResult GetProgressExportStudentShip()
        {
            int progress = StudentShipSchoolBo.percentProgress;
            return this.Content(progress + string.Empty, "text/plain");
        }

        public ActionResult GetProgressSaveStudentShipSchoolStudent()
        {
            int progress = StudentShipSchoolStudentBo.percentProgress;
            return this.Content(progress + string.Empty, "text/plain");
        }

        public ActionResult ImportScoteStudent()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocbong")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.listSemester = new SemesterBO().Get();
            return this.View("ImportScoteStudent");
        }


        public ActionResult GetListErrorImportScote()
        {
            return this.PartialView(
                "~/Views/PartialShared/_ListErrorImportExcel.cshtml",
                new LearningOutComeBo().GetListErrorImportExcel());
        }

        public ActionResult GetCountListErrorImportScote()
        {
            return this.Content("" + new LearningOutComeBo().GetCountListErrorImportExcel(), "text/plain");
        }

        // GET: StudentShip
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ListScoteStudent(int idSemester, int page, FormCollection form)
        {
            string search = form["search"];
            if (search == null)
            {
                if (this.Session["searchship"] != null) search = this.Session["searchship"].ToString();
            }
            else
            {
                this.Session["searchship"] = search;
            }

            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocbong")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.listSemester = new SemesterBO().Get();
            this.ViewBag.Semester = idSemester;
            this.ViewBag.listLearningOutCome = this.learningOutComeBo.Get(idSemester, page, search);
            PageNumber pagenumber = this.learningOutComeBo.TotalPage(idSemester, page, search, string.Empty);
            this.ViewBag.pageNumber = pagenumber;
            return this.View("ListScoteStudent");
        }

        public ActionResult ListStudentShip(int page, FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocbong")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.listSemester = new SemesterBO().Get();
            this.ViewBag.listLearningOutCome = this.learningOutComeBo.Get(page, form);
            this.ViewBag.pageNumber = this.learningOutComeBo.TotalPage(page, form);
            return this.View("ListConsiderStudentShip");
        }

        public ActionResult ListStudentShipSchoolFaculty(int idStudentShipSchoolFaculty, int page, FormCollection form)
        {
            string search = form["search"];
            if (search == null)
            {
                if (this.Session["search"] != null) search = this.Session["search"].ToString();
            }
            else
            {
                this.Session["search"] = search;
            }

            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocbong")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.StudentShipSchoolFaculty = new StudentShipSchoolFacultyBo().GetById(idStudentShipSchoolFaculty);
            this.ViewBag.listLearningOutCome = this.learningOutComeBo.GetByIdFaculty(
                page,
                idStudentShipSchoolFaculty,
                search);
            this.ViewBag.pageNumber = this.learningOutComeBo.TotalPage(page, idStudentShipSchoolFaculty, search);
            this.ViewBag.ConditionStudentShip = new ConditionStudentShipSchoolBo().Get(idStudentShipSchoolFaculty);
            this.ViewBag.RankingAcademic = new RankingAcademicBo().Get();
            this.ViewBag.SURPLUSMoney = this.learningOutComeBo.GetSURPLUSMoney(idStudentShipSchoolFaculty);
            this.ViewBag.CountStudentShip = this.learningOutComeBo.GetCountStudentShip(idStudentShipSchoolFaculty);
            return this.View("ListConsiderStudentShip");
        }

        public ActionResult ListStudentShipSchoolFacultyCLC(
            int idStudentShipSchoolFaculty,
            int page,
            FormCollection form)
        {
            string search = form["search"];
            if (search == null)
            {
                if (this.Session["search"] != null) search = this.Session["search"].ToString();
            }
            else
            {
                this.Session["search"] = search;
            }

            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocbong")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.StudentShipSchoolFaculty = new StudentShipSchoolFacultyBo().GetById(idStudentShipSchoolFaculty);
            this.ViewBag.listLearningOutCome = this.learningOutComeBo.GetByIdFacultyCLC(
                page,
                idStudentShipSchoolFaculty,
                search);
            this.ViewBag.pageNumber = this.learningOutComeBo.TotalPage(page, idStudentShipSchoolFaculty, search);
            this.ViewBag.ConditionStudentShip = new ConditionStudentShipSchoolBo().Get(idStudentShipSchoolFaculty);
            this.ViewBag.SURPLUSMoney = this.learningOutComeBo.GetSURPLUSMoney(idStudentShipSchoolFaculty);
            this.ViewBag.CountStudentShip = this.learningOutComeBo.GetCountStudentShipCLC(idStudentShipSchoolFaculty);
            this.ViewBag.top1 = new StudentShipCLCBo().GetTop1();
            this.ViewBag.top2 = new StudentShipCLCBo().GetTop2();
            return this.View("ListConsiderStudentShipCLC");
        }

        public ActionResult ListStudentShipSchoolStudent(int idStudentShipSchoolFaculty)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocbong")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.StudentShipSchoolFaculty = new StudentShipSchoolFacultyBo().GetById(idStudentShipSchoolFaculty);
            this.ViewBag.listStudentShipSchoolStudent =
                new StudentShipSchoolStudentBo().GetList(idStudentShipSchoolFaculty);
            return this.View("ListStudentShipSchoolStudent");
        }

        public ActionResult MoveMoneyStudentShip(string money, int idStudentShipSchoolFaculty)
        {
            new StudentShipSchoolFacultyBo().MoveMoneyStudentShip(money, idStudentShipSchoolFaculty);
            return this.RedirectToAction(
                "ListStudentShipSchoolFacultyCLC",
                new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty, page = 1 });
        }

        public ActionResult ResetPercentProgress()
        {
            LearningOutComeBo.percentProgress = 0;
            return this.Content(string.Empty, "text/plain");
        }

        public ActionResult SaveStudentShipSchoolStudent(int idStudentShipSchoolFaculty)
        {
            this.learningOutComeBo.SaveStudentShipSchoolStudent(idStudentShipSchoolFaculty);
            return this.RedirectToAction(
                "ListStudentShipSchoolStudent",
                new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty });
        }

        public ActionResult SaveStudentShipSchoolStudentCLC(int idStudentShipSchoolFaculty)
        {
            this.learningOutComeBo.SaveStudentShipSchoolStudentCLC(idStudentShipSchoolFaculty);
            return this.RedirectToAction(
                "ListStudentShipSchoolStudent",
                new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty });
        }

        public ActionResult SelectSemester()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocbong")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.listSemester = new SemesterBO().Get();
            return this.View("SelectSemester");
        }

        public ActionResult SelectSemesterScoteStudent()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocbong")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.listSemester = new SemesterBO().Get();
            return this.View("SelectSemesterScoteStudent");
        }

        public ActionResult SendStudentShipApplication()
        {
            this.ViewBag.listSemester = new SemesterBO().Get();
            return this.View("SendStudenshipApplication");
        }

        public ActionResult StudentShipFaculty(int idSemester)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocbong")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            StudentShipSchool studentShipSchool = new StudentShipSchoolBo().Get(idSemester);
            this.ViewBag.studentShipSchool = studentShipSchool;
            this.ViewBag.Faculty = new FacultyBO().GetListFaculty();
            this.ViewBag.listStudentShipSchoolFaculty =
                new StudentShipSchoolFacultyBo().Get(studentShipSchool.IdStudentShipSchool);
            this.ViewBag.surplus = new StudentShipSchoolFacultyBo().GetSURPLUS(studentShipSchool.IdStudentShipSchool);
            return this.View("StudentShipFaculty");
        }

        public ActionResult StudentShipFacultyCLC(int idSemester)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlyhocbong")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            StudentShipSchool studentShipSchool = new StudentShipSchoolBo().Get(idSemester);
            this.ViewBag.studentShipSchool = studentShipSchool;
            this.ViewBag.ClassCLC = new ClassBO().GetClassCLC();
            this.ViewBag.listStudentShipSchoolFaculty =
                new StudentShipSchoolFacultyBo().Get(studentShipSchool.IdStudentShipSchool);
            this.ViewBag.surplus = new StudentShipSchoolFacultyBo().GetSURPLUS(studentShipSchool.IdStudentShipSchool);
            return this.View("StudentShipFacultyCLC");
        }

        public ActionResult UpdateIsAcceptConsider(int idLearningOutCome)
        {
            this.learningOutComeBo.UpdateIsAcceptConsider(idLearningOutCome);
            return this.Content("1", "text/plain");
        }

        public ActionResult UpdateIsDisEnableConsider(int idLearningOutCome)
        {
            this.learningOutComeBo.UpdateIsDisEnableConsider(idLearningOutCome);
            return this.Content("1", "text/plain");
        }

        public ActionResult UpdateMoneyStudentShipFaculty(int idStudentShipSchoolFaculty, FormCollection form)
        {
            new StudentShipSchoolFacultyBo().Update(idStudentShipSchoolFaculty, form);
            return this.RedirectToAction(
                "ListStudentShipSchoolFaculty",
                new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty, page = 1 });
        }

        public ActionResult UpdateMoneyStudentShipFacultyCLC(int idStudentShipSchoolFaculty, FormCollection form)
        {
            new StudentShipSchoolFacultyBo().Update(idStudentShipSchoolFaculty, form);
            return this.RedirectToAction(
                "ListStudentShipSchoolFacultyCLC",
                new { idStudentShipSchoolFaculty = idStudentShipSchoolFaculty, page = 1 });
        }

        public ActionResult UpdateMoneyStudentShipShoolFaculty(
            int idFaculty,
            int idStudentShipSchool,
            string totalMoney)
        {
            new StudentShipSchoolFacultyBo().InsertStudentShipFaculty(
                idStudentShipSchool,
                idFaculty,
                null,
                ConvertObject.ConvertCurrencyToString(totalMoney),
                null);
            new StudentShipSchoolBo().UpdateMoney(idStudentShipSchool);
            return this.Content("1", "text/plain");
        }

        public ActionResult UpdateMoneyStudentShipShoolFacultyCLC(
            int idClass,
            int idStudentShipSchool,
            string totalMoney,
            string tuitionFee)
        {
            new StudentShipSchoolFacultyBo().InsertStudentShipFaculty(
                idStudentShipSchool,
                null,
                idClass,
                ConvertObject.ConvertCurrencyToString(totalMoney),
                ConvertObject.ConvertCurrencyToString(tuitionFee));
            new StudentShipSchoolBo().UpdateMoney(idStudentShipSchool);
            return this.Content("1", "text/plain");
        }
    }
}