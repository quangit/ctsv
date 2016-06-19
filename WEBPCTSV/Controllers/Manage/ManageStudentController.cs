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
    public class ManageStudentController : Controller
    {
        StudentBO studentBo = new StudentBO();
        // GET: ManageStudent
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddStudent()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "themsinhvien")) return RedirectToAction("NotAccess", "ManageDecentralization");
            FacultyBO facultyBo = new FacultyBO();
            CourseBo courseBo = new CourseBo();
            StateBo stateBo = new StateBo();
            ProvinceBo provinceBo = new ProvinceBo();

            ViewBag.listFaculty = facultyBo.GetListFaculty();
            ViewBag.listCourse = courseBo.GetListCourse();
            ViewBag.listState = stateBo.GetListState();
            ViewBag.listProvince = provinceBo.GetListCourse("1");
            return View();
        }

        public ActionResult AddNewStudent(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "themsinhvien")) return RedirectToAction("NotAccess", "ManageDecentralization");
            studentBo.AddStudent(form);
            return RedirectToAction("ListStudent", new { page = 1});
        }


        public ActionResult ListStudent(int page,FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "xemdanhsachsinhvien")) return RedirectToAction("NotAccess", "ManageDecentralization");
            PageNumber pageNumber = new PageNumber();
            string search = form["search"];
            if (search == null)
            {
                if (Session["search"] != null) search = Session["search"].ToString();
            }
            else
            {
                Session["search"] = search;
            }
            ViewBag.listStudent = studentBo.GetListStudent(page,search);
            pageNumber.PageNumberTotal = studentBo.TotalPage(search);
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/ManageStudent/ListStudent?page=";
            ViewBag.pageNumber = pageNumber;
            return View("ListStudent");
        }


        public ActionResult DetailStudent(int idStudent)
        {

            ViewBag.student = studentBo.GetStudent(idStudent);
            return View("DetailStudent");
        }

        public ActionResult EditStudent(int idStudent)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "chinhsuathongtinsinhvien")
                && (!CheckDecentralization.CheckPersonal(Convert.ToInt32(Session["idAccount"]),idStudent))) return RedirectToAction("NotAccess", "ManageDecentralization");

            FacultyBO facultyBo = new FacultyBO();
            CourseBo courseBo = new CourseBo();
            StateBo stateBo = new StateBo();
            ViewBag.listFaculty = facultyBo.GetListFaculty();
            ViewBag.listCourse = courseBo.GetListCourse();
            ViewBag.listState = stateBo.GetListState();
            ViewBag.student = studentBo.GetStudent(idStudent);

            return View("EditStudent");
        }

        public ActionResult PersonalInformation()
        {
            try
            {
                FacultyBO facultyBo = new FacultyBO();
                CourseBo courseBo = new CourseBo();
                StateBo stateBo = new StateBo();
                ViewBag.listFaculty = facultyBo.GetListFaculty();
                ViewBag.listCourse = courseBo.GetListCourse();
                ViewBag.listState = stateBo.GetListState();
                ViewBag.student = studentBo.GetStudentByIdAccount(Convert.ToInt32(Session["idAccount"]));

                return View("EditStudent");
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }        
        }

        [HttpPost]
        public ActionResult ProcessEditStudent(int idStudent, FormCollection form)
        {
            studentBo.EditStudent(idStudent, form);
            return RedirectToAction("EditStudent", new { idStudent = idStudent });
        }

        public ActionResult EditStudent2(int idStudent)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "chinhsuathongtinsinhvien")
                && (!CheckDecentralization.CheckPersonal(Convert.ToInt32(Session["idAccount"]), idStudent))) return RedirectToAction("NotAccess", "ManageDecentralization");
            StateBo stateBo = new StateBo();
            ProvinceBo provinceBo = new ProvinceBo();
            ReligionBo religionBo = new ReligionBo();
            EthnicBo ethnicBo = new EthnicBo();
            ViewBag.listState = stateBo.GetListState();
            ViewBag.listProvince = provinceBo.GetListCourse("1");
            Student student = studentBo.GetStudent(idStudent);
            ViewBag.student = student;
            ViewBag.religion = religionBo.GetReligion();
            ViewBag.listEthnic = ethnicBo.GetOptionEthnic(student.idState);
            ViewBag.listArea = new AreaBo().GetListArea();

            return View("EditStudent2");
        }

        [HttpPost]
        public ActionResult ProcessEditStudent2(int idStudent, FormCollection form)
        {
            studentBo.EditStudent2(idStudent, form);
            return RedirectToAction("EditStudent2", new { idStudent = idStudent });
        }

        public ActionResult EditStudent3(int idStudent)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "chinhsuathongtinsinhvien")
                && (!CheckDecentralization.CheckPersonal(Convert.ToInt32(Session["idAccount"]), idStudent))) return RedirectToAction("NotAccess", "ManageDecentralization");
            BloodGroupBo bloodgroupBo = new BloodGroupBo();

            Student student = studentBo.GetStudent(idStudent);
            ViewBag.student = student;
            ViewBag.bloodgroup = bloodgroupBo.GetBloodGroup();


            return View("EditStudent3");
        }

        public ActionResult ProcessEditStudent3(int idStudent, FormCollection form)
        {
            studentBo.EditStudent3(idStudent, form);
            return RedirectToAction("EditStudent3", new { idStudent = idStudent });
        }



        public ActionResult EditStudent4(int idStudent)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "chinhsuathongtinsinhvien")
                && (!CheckDecentralization.CheckPersonal(Convert.ToInt32(Session["idAccount"]), idStudent))) return RedirectToAction("NotAccess", "ManageDecentralization");

            BloodGroupBo bloodgroupBo = new BloodGroupBo();
            Student student = studentBo.GetStudent(idStudent);
            ViewBag.student = student;
            ViewBag.madeofstudy = new MadeOfStudyBo().GetListMadeOfStudy();
            ViewBag.typeInput = new TypeInputBo().GetListTypeInput();
            ViewBag.preferentialPolicyBeneficiary = new PreferentialPolicyBeneficiaryBo().GetListPreferentialPolicyBeneficiary();
            ViewBag.SecondLanguage = new SecondLanguageBo().GetListSecondLanguage();
            ViewBag.SocialPolicyBeneficiary = new SocialPolicyBeneficiaryBo().GetListSocialPolicyBeneficiary();
            ViewBag.typepaper = new TypePaperBo().GetListPaper();
            return View("EditStudent4");
        }

        public ActionResult ProcessEditStudent4(int idStudent, FormCollection form)
        {
            studentBo.EditStudent4(idStudent, form);
            return RedirectToAction("EditStudent4", new { idStudent = idStudent });
        }

        public ActionResult UpdateTypePaperStudent(int idTypePaper , int idStudent)
        {
            new TypePaperBo().UpdateTypePaperStudent(idTypePaper, idStudent);
            return Content("", "text/plain");
        }

        public ActionResult UpdateSecondLanguage(string idLanguage,int idStudent)
        {
            new SecondLanguageBo().UpdateSecondLanguage(idLanguage, idStudent);
            return Content("", "text/plain");
        }

        public ActionResult UpdateSocialPolicyBeneficiaryStudent(int idSocialPolicyBeneficiary,int idStudent)
        {
            new SocialPolicyBeneficiaryBo().UpdateSocialPolicyBeneficiaryStudent(idSocialPolicyBeneficiary, idStudent);
            return Content("", "text/plain");
        }



        public ActionResult EditStudentRelative(int idStudent)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "chinhsuathongtinsinhvien")
                && (!CheckDecentralization.CheckPersonal(Convert.ToInt32(Session["idAccount"]), idStudent))) return RedirectToAction("NotAccess", "ManageDecentralization");
            Student student = studentBo.GetStudent(idStudent);
            ViewBag.student = student;
            ViewBag.FamilyComposition = new FamilyCompositionBo().GetListFamilyComposition();
            ViewBag.listProvince = new ProvinceBo().GetListCourse("1");
            return View("EditStudentRelative");
        }

        public ActionResult ProcessEditStudentRelative(int idStudent,FormCollection form)
        {
            studentBo.ProcessEditStudentRelative(idStudent, form);
            new RelativeBo().AddRelative(idStudent, form);
            return RedirectToAction("EditStudentRelative", new { idStudent = idStudent });
        }

        public ActionResult EditStudentSocialActivity(int idStudent)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "chinhsuathongtinsinhvien")
                && (!CheckDecentralization.CheckPersonal(Convert.ToInt32(Session["idAccount"]), idStudent))) return RedirectToAction("NotAccess", "ManageDecentralization");
            Student student = studentBo.GetStudent(idStudent);
            ViewBag.student = student;
            ViewBag.regency = new RegencyBo().GetListRegency();
            return View("EditStudentSocialActivity");
        }


        public ActionResult EditStudentAcademicAchievement(int idStudent)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "chinhsuathongtinsinhvien")
                && (!CheckDecentralization.CheckPersonal(Convert.ToInt32(Session["idAccount"]), idStudent))) return RedirectToAction("NotAccess", "ManageDecentralization");
            Student student = studentBo.GetStudent(idStudent);
            ViewBag.student = student;
            ViewBag.academicAchievement = new AcademicAchievementBo().Get(idStudent);
            ViewBag.OrganizationLevel = new OrganizationLevelBo().Get();
            ViewBag.Semester = new SemesterBO().Get();
            return View("EditStudentAcademicAchievement");
        }

        public ActionResult AddAcademicAchievement(int idStudent,FormCollection form)
        {
            new AcademicAchievementBo().Insert(idStudent, form);
            return RedirectToAction("EditStudentAcademicAchievement", new { idStudent = idStudent });
        }
        public ActionResult UpdateAcademicAchievement(int idStudent, int idAcademicAchievement, FormCollection form)
        {
            new AcademicAchievementBo().Update(idAcademicAchievement, form);
            return RedirectToAction("EditStudentAcademicAchievement", new { idStudent = idStudent });
        }

        public ActionResult DeleteAcademicAchievement(int idStudent, int idAcademicAchievement)
        {
            new AcademicAchievementBo().Delete(idAcademicAchievement);
            return RedirectToAction("EditStudentAcademicAchievement", new { idStudent = idStudent });
        }


        public ActionResult ImportStudent()
        {
            //if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "importexcel")) return RedirectToAction("NotAccess", "ManageDecentralization");
            return View("ImportStudent");
        }

        public ActionResult UpdateRegency(string idRegency, int idStudent)
        {
            new RegencyBo().UpdateRegencyStudent(idRegency, idStudent);
            return Content("", "text/plain");
        }

        public ActionResult UpdateYouthUnion(int idStudent)
        {
            studentBo.UpdateYouthUnion(idStudent);
            return Content("", "text/plain");
        }

        public ActionResult UpdatePoliticalParty(int idStudent)
        {
            studentBo.UpdatePoliticalParty(idStudent);
            return Content("", "text/plain");
        }


        public ActionResult GetListClass(int idFaculty,int idCourse)
        {
            ClassBO classBo = new ClassBO();
            string stringSelectClass = classBo.GetStringSelectListCourse(idFaculty, idCourse);
            return Content(stringSelectClass, "text/plain");
        }

        public ActionResult GetDistrict(string idProvince)
        {
            DistrictBo districtBo = new DistrictBo();
            string stringOption  = districtBo.GetOptionDistrict(idProvince);
            return Content(stringOption, "text/plain");
        }

        public ActionResult GetWard(string idDistrict)
        {
            WardBo ward = new WardBo();
            string stringOption = ward.GetOptionWard(idDistrict);
            return Content(stringOption, "text/plain");
        }

        public ActionResult ResetPercentProgress()
        {
            StudentBO.percentProgress = 0;
            return Content("", "text/plain");
        }
    }
}