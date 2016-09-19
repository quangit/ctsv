namespace WEBPCTSV.Controllers
{
    using System;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class ManageStudentController : Controller
    {
        private readonly StudentBO studentBo = new StudentBO();

        public ActionResult AddAcademicAchievement(int idStudent, FormCollection form)
        {
            new AcademicAchievementBo().Insert(idStudent, form);
            return this.RedirectToAction("EditStudentAcademicAchievement", new { idStudent });
        }

        public ActionResult AddNewStudent(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "themsinhvien")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.studentBo.AddStudent(form);
            return this.RedirectToAction("ListStudent", new { page = 1 });
        }

        public ActionResult AddStudent()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "themsinhvien")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            var facultyBo = new FacultyBO();
            var courseBo = new CourseBo();
            var stateBo = new StateBo();
            var provinceBo = new ProvinceBo();

            this.ViewBag.listFaculty = facultyBo.GetListFaculty();
            this.ViewBag.listCourse = courseBo.GetListCourse();
            this.ViewBag.listState = stateBo.GetListState();
            this.ViewBag.listProvince = provinceBo.GetListCourse("1");
            return this.View();
        }

        public ActionResult DeleteAcademicAchievement(int idStudent, int idAcademicAchievement)
        {
            new AcademicAchievementBo().Delete(idAcademicAchievement);
            return this.RedirectToAction("EditStudentAcademicAchievement", new { idStudent });
        }

        public ActionResult DetailStudent(int idStudent)
        {
            this.ViewBag.student = this.studentBo.GetStudent(idStudent);
            return this.View("DetailStudent");
        }

        public ActionResult EditStudent(int idStudent)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "chinhsuathongtinsinhvien")
                && !CheckDecentralization.CheckPersonal(Convert.ToInt32(this.Session["idAccount"]), idStudent)) return this.RedirectToAction("NotAccess", "ManageDecentralization");

            var facultyBo = new FacultyBO();
            var courseBo = new CourseBo();
            var stateBo = new StateBo();
            this.ViewBag.listFaculty = facultyBo.GetListFaculty();
            this.ViewBag.listCourse = courseBo.GetListCourse();
            this.ViewBag.listState = stateBo.GetListState();
            this.ViewBag.student = this.studentBo.GetStudent(idStudent);

            return this.View("EditStudent");
        }

        public ActionResult EditStudent2(int idStudent)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "chinhsuathongtinsinhvien")
                && !CheckDecentralization.CheckPersonal(Convert.ToInt32(this.Session["idAccount"]), idStudent)) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            var stateBo = new StateBo();
            var provinceBo = new ProvinceBo();
            var religionBo = new ReligionBo();
            var ethnicBo = new EthnicBo();
            this.ViewBag.listState = stateBo.GetListState();
            this.ViewBag.listProvince = provinceBo.GetListCourse("1");
            var student = this.studentBo.GetStudent(idStudent);
            this.ViewBag.student = student;
            this.ViewBag.religion = religionBo.GetReligion();
            this.ViewBag.listEthnic = ethnicBo.GetOptionEthnic(student.idState);
            this.ViewBag.listArea = new AreaBo().GetListArea();

            return this.View("EditStudent2");
        }

        public ActionResult EditStudent3(int idStudent)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "chinhsuathongtinsinhvien")
                && !CheckDecentralization.CheckPersonal(Convert.ToInt32(this.Session["idAccount"]), idStudent)) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            var bloodgroupBo = new BloodGroupBo();

            var student = this.studentBo.GetStudent(idStudent);
            this.ViewBag.student = student;
            this.ViewBag.bloodgroup = bloodgroupBo.GetBloodGroup();

            return this.View("EditStudent3");
        }

        public ActionResult EditStudent4(int idStudent)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "chinhsuathongtinsinhvien")
                && !CheckDecentralization.CheckPersonal(Convert.ToInt32(this.Session["idAccount"]), idStudent)) return this.RedirectToAction("NotAccess", "ManageDecentralization");

            var bloodgroupBo = new BloodGroupBo();
            var student = this.studentBo.GetStudent(idStudent);
            this.ViewBag.student = student;
            this.ViewBag.madeofstudy = new MadeOfStudyBo().GetListMadeOfStudy();
            this.ViewBag.typeInput = new TypeInputBo().GetListTypeInput();
            this.ViewBag.preferentialPolicyBeneficiary =
                new PreferentialPolicyBeneficiaryBo().GetListPreferentialPolicyBeneficiary();
            this.ViewBag.SecondLanguage = new SecondLanguageBo().GetListSecondLanguage();
            this.ViewBag.SocialPolicyBeneficiary = new SocialPolicyBeneficiaryBo().GetListSocialPolicyBeneficiary();
            this.ViewBag.typepaper = new TypePaperBo().GetListPaper();
            return this.View("EditStudent4");
        }

        public ActionResult EditStudentAcademicAchievement(int idStudent)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "chinhsuathongtinsinhvien")
                && !CheckDecentralization.CheckPersonal(Convert.ToInt32(this.Session["idAccount"]), idStudent)) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            var student = this.studentBo.GetStudent(idStudent);
            this.ViewBag.student = student;
            this.ViewBag.academicAchievement = new AcademicAchievementBo().Get(idStudent);
            this.ViewBag.OrganizationLevel = new OrganizationLevelBo().Get();
            this.ViewBag.Semester = new SemesterBO().Get();
            return this.View("EditStudentAcademicAchievement");
        }

        public ActionResult EditStudentRelative(int idStudent)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "chinhsuathongtinsinhvien")
                && !CheckDecentralization.CheckPersonal(Convert.ToInt32(this.Session["idAccount"]), idStudent)) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            var student = this.studentBo.GetStudent(idStudent);
            this.ViewBag.student = student;
            this.ViewBag.FamilyComposition = new FamilyCompositionBo().GetListFamilyComposition();
            this.ViewBag.listProvince = new ProvinceBo().GetListCourse("1");
            return this.View("EditStudentRelative");
        }

        public ActionResult EditStudentSocialActivity(int idStudent)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "chinhsuathongtinsinhvien")
                && !CheckDecentralization.CheckPersonal(Convert.ToInt32(this.Session["idAccount"]), idStudent)) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            var student = this.studentBo.GetStudent(idStudent);
            this.ViewBag.student = student;
            this.ViewBag.regency = new RegencyBo().GetListRegency();
            return this.View("EditStudentSocialActivity");
        }

        public ActionResult GetDistrict(string idProvince)
        {
            var districtBo = new DistrictBo();
            var stringOption = districtBo.GetOptionDistrict(idProvince);
            return this.Content(stringOption, "text/plain");
        }

        public ActionResult GetListClass(int idFaculty, int idCourse)
        {
            var classBo = new ClassBO();
            var stringSelectClass = classBo.GetStringSelectListCourse(idFaculty, idCourse);
            return this.Content(stringSelectClass, "text/plain");
        }

        public ActionResult GetWard(string idDistrict)
        {
            var ward = new WardBo();
            var stringOption = ward.GetOptionWard(idDistrict);
            return this.Content(stringOption, "text/plain");
        }

        public ActionResult ImportStudent()
        {
            // if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "importexcel")) return RedirectToAction("NotAccess", "ManageDecentralization");
            return this.View("ImportStudent");
        }

        // GET: ManageStudent
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetListErrorImportScote()
        {
            return this.PartialView(
                "~/Views/PartialShared/_ListErrorImportExcel.cshtml",
                this.studentBo.GetListErrorImportExcel());
        }

        public ActionResult GetCountListErrorImportScote()
        {
            return this.Content("" + this.studentBo.GetCountListErrorImportExcel(), "text/plain");
        }

        public ActionResult ListStudent(int page, FormCollection form)
        {
            if (
                !CheckDecentralization.Check(
                    Convert.ToInt32(this.Session["idDecenTralizationGroup"]),
                    "xemdanhsachsinhvien")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            var pageNumber = new PageNumber();
            var search = form["search"];
            if (search == null)
            {
                if (this.Session["searchstudent"] != null) search = this.Session["searchstudent"].ToString();
            }
            else
            {
                this.Session["searchstudent"] = search;
            }

            this.ViewBag.listStudent = this.studentBo.GetListStudent(page, search);
            pageNumber.PageNumberTotal = this.studentBo.TotalPage(search);
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/ManageStudent/ListStudent?page=";
            this.ViewBag.pageNumber = pageNumber;
            return this.View("ListStudent");
        }

        public ActionResult PersonalInformation()
        {
            try
            {
                var facultyBo = new FacultyBO();
                var courseBo = new CourseBo();
                var stateBo = new StateBo();
                this.ViewBag.listFaculty = facultyBo.GetListFaculty();
                this.ViewBag.listCourse = courseBo.GetListCourse();
                this.ViewBag.listState = stateBo.GetListState();
                this.ViewBag.student = this.studentBo.GetStudentByIdAccount(Convert.ToInt32(this.Session["idAccount"]));

                return this.View("EditStudent");
            }
            catch
            {
                return this.View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        public ActionResult ProcessEditStudent(int idStudent, FormCollection form)
        {
            this.studentBo.EditStudent(idStudent, form);
            return this.RedirectToAction("EditStudent", new { idStudent });
        }

        [HttpPost]
        public ActionResult ProcessEditStudent2(int idStudent, FormCollection form)
        {
            this.studentBo.EditStudent2(idStudent, form);
            return this.RedirectToAction("EditStudent2", new { idStudent });
        }

        public ActionResult ProcessEditStudent3(int idStudent, FormCollection form)
        {
            this.studentBo.EditStudent3(idStudent, form);
            return this.RedirectToAction("EditStudent3", new { idStudent });
        }

        public ActionResult ProcessEditStudent4(int idStudent, FormCollection form)
        {
            this.studentBo.EditStudent4(idStudent, form);
            return this.RedirectToAction("EditStudent4", new { idStudent });
        }

        public ActionResult ProcessEditStudentRelative(int idStudent, FormCollection form)
        {
            this.studentBo.ProcessEditStudentRelative(idStudent, form);
            new RelativeBo().AddRelative(idStudent, form);
            return this.RedirectToAction("EditStudentRelative", new { idStudent });
        }

        public ActionResult ResetPercentProgress()
        {
            StudentBO.percentProgress = 0;
            return this.Content(string.Empty, "text/plain");
        }

        public ActionResult UpdateAcademicAchievement(int idStudent, int idAcademicAchievement, FormCollection form)
        {
            new AcademicAchievementBo().Update(idAcademicAchievement, form);
            return this.RedirectToAction("EditStudentAcademicAchievement", new { idStudent });
        }

        public ActionResult UpdatePoliticalParty(int idStudent)
        {
            this.studentBo.UpdatePoliticalParty(idStudent);
            return this.Content(string.Empty, "text/plain");
        }

        public ActionResult UpdateRegency(string idRegency, int idStudent)
        {
            new RegencyBo().UpdateRegencyStudent(idRegency, idStudent);
            return this.Content(string.Empty, "text/plain");
        }

        public ActionResult UpdateSecondLanguage(string idLanguage, int idStudent)
        {
            new SecondLanguageBo().UpdateSecondLanguage(idLanguage, idStudent);
            return this.Content(string.Empty, "text/plain");
        }

        public ActionResult UpdateSocialPolicyBeneficiaryStudent(int idSocialPolicyBeneficiary, int idStudent)
        {
            new SocialPolicyBeneficiaryBo().UpdateSocialPolicyBeneficiaryStudent(idSocialPolicyBeneficiary, idStudent);
            return this.Content(string.Empty, "text/plain");
        }

        public ActionResult UpdateTypePaperStudent(int idTypePaper, int idStudent)
        {
            new TypePaperBo().UpdateTypePaperStudent(idTypePaper, idStudent);
            return this.Content(string.Empty, "text/plain");
        }

        public ActionResult UpdateYouthUnion(int idStudent)
        {
            this.studentBo.UpdateYouthUnion(idStudent);
            return this.Content(string.Empty, "text/plain");
        }
    }
}