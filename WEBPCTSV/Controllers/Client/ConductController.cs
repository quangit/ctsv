using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;
using WEBPCTSV.Helpers.Common;
using WEBPCTSV.Helpers;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using OfficeOpenXml;

namespace WEBPCTSV.Controllers
{
    public class ConductController : Controller
    {
        DSAContext dsaContext;
        public ConductController()
        {
            dsaContext = new DSAContext();
        }
        public ActionResult Home()
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                if (currentEvent != null)
                {
                    List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                    ViewBag.currentSchedule = currentSchedule;
                    ViewBag.currentSemester = currentSemester;
                }
                else
                {
                    ViewBag.error = "Không tìm thấy đợt xét rèn luyện cho kỳ này!";
                }
                return View("Home");
            }
            else
            {
                return Redirect("/Login");
            }
        }

        #region show conduct detail of student
        public ActionResult ConductEvaluate()
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null && accountSession.TypeAccount.Equals("SV"))
            {
                // Check schedule to evaluate
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                if (accountConductSchedule != null && (DateTime.Now >= accountConductSchedule.BeginDateEvaluate && DateTime.Now <= accountConductSchedule.EndDateEvaluate))
                {
                    StudentBO studentBO = new StudentBO(dsaContext);
                    Student student = studentBO.GetStudentByStudentNumber(accountSession.UserName);
                    if (student.IsReserved == true || student.IsGraduated == true || student.IsExpelled == true)
                    {
                        ViewBag.error = "Bạn không được phép đánh giá kết quả rèn luyện!";
                        return View();
                    }
                    ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                    ConductResult conductResult = conductResultBO.GetConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule);
                    if (conductResult != null)
                    {
                        // Evaluated
                        if (conductResult.IsApproved)
                        {
                            ViewBag.error = "Bạn đã hoàn thành đánh giá kết quả rèn luyện";
                            return View();
                        }
                        if (conductResult.IsSaved)
                        {
                            ViewBag.conductResult = conductResult;
                        }
                    }
                    // Not evaluated or saved
                    ViewBag.student = student;
                    ViewBag.currentSemester = currentSemester;
                    List<ConductItem> itemsByForm = (new ConductItemsBO(dsaContext)).GetListConductItems(currentEvent.IdConductForm);
                    ViewBag.itemsByForm = itemsByForm;
                }
                else
                {
                    ViewBag.error = "Hiện tại không phải giai đoạn đánh giá điểm rèn luyện của bạn! "
                        + (accountConductSchedule != null ? "Thời gian đánh giá điểm của bạn từ "
                        + accountConductSchedule.BeginDateEvaluate.ToString("HH:mm dd/MM/yyy")
                        + " đến " + accountConductSchedule.EndDateEvaluate.ToString("HH:mm dd/MM/yyy") : "");
                }
                return View();
            }
            else
            {
                return Redirect("/Login");
            }
        }
        #endregion

        #region Update conduct detail post method
        [HttpPost]
        public ActionResult ConductEvaluate(FormCollection collection)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null && accountSession.TypeAccount.Equals("SV"))
            {
                // Check schedule to evaluate
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                if (accountConductSchedule != null && (DateTime.Now >= accountConductSchedule.BeginDateEvaluate && DateTime.Now <= accountConductSchedule.EndDateEvaluate))
                {
                    // Checking type submit
                    bool isApproved;
                    bool isSaved;
                    if (accountSession.IdDecentralizationGroup == Define.Role.Prefect)
                    {
                        // Prefect only save conduct
                        isApproved = false;
                        isSaved = !isApproved;
                    }
                    else
                    {
                        // Get parameter
                        isApproved = StringExtension.IsNullOrWhiteSpace(collection["isApproved"]) ? false : bool.Parse(collection["isApproved"]);
                        isSaved = !isApproved;
                    }

                    // Init partial point
                    List<ConductItem> itemsByForm = (new ConductItemsBO(dsaContext)).GetListConductItems(currentEvent.IdConductForm);
                    string partialPoint = "";
                    foreach (ConductItem item in itemsByForm)
                    {
                        if (!StringExtension.IsNullOrWhiteSpace(collection["P" + item.IdConductItems]) && Int32.Parse(collection["P" + item.IdConductItems]) <= item.MaxPoints)
                            partialPoint += item.IdConductItems + ":" + collection["P" + item.IdConductItems] + ";";
                    }
                    partialPoint = (!partialPoint.Equals("")) ? partialPoint.Substring(0, partialPoint.Length - 1) : "";

                    // Check isExist student in ConductResult
                    StudentBO studentBO = new StudentBO(dsaContext);
                    Student student = studentBO.GetStudentByStudentNumber(accountSession.UserName);

                    if (student.IsReserved == true || student.IsGraduated == true || student.IsExpelled == true)
                    {
                        return Json(new { success = false, responseText = "Bạn không được phép đánh giá kết quả rèn luyện sinh viên này!" }, JsonRequestBehavior.DenyGet);
                    }

                    ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                    ConductResult conductResult = conductResultBO.GetConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule);
                    if (conductResult != null)
                    {
                        if (conductResult.IsApproved)
                        {
                            return Json(new { success = false, responseText = "Bạn đã hoàn thành đánh giá kết quả rèn luyện!" }, JsonRequestBehavior.DenyGet);
                        }
                        else
                        {
                            bool isSuccess = conductResultBO.UpdateConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule, currentEvent.IdConductEvent, isApproved, isSaved, partialPoint);
                            if (isSuccess)
                            {
                                if (isSaved)
                                {
                                    return Json(new { success = true, isApproved = isApproved, responseText = "Lưu kết quả rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                                }
                                return Json(new { success = true, isApproved = isApproved, responseText = "Đánh giá kết quả rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                            }
                            else
                            {
                                return Json(new { success = false, responseText = "Cập nhật kết quả rèn luyện thất bại! Kiểm tra lại thông tin." }, JsonRequestBehavior.DenyGet);
                            }
                        }
                    }
                    else
                    {
                        //Not evaluated
                        bool isSuccess = conductResultBO.AddConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule, currentEvent.IdConductEvent, isApproved, isSaved, partialPoint);
                        if (isSuccess)
                        {
                            if (isSaved)
                            {
                                return Json(new { success = true, isApproved = isApproved, responseText = "Lưu kết quả rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                            }
                            return Json(new { success = true, isApproved = isApproved, responseText = "Đánh giá kết quả rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                        }
                        else
                        {
                            return Json(new { success = false, responseText = "Cập nhật kết quả rèn luyện thất bại! Kiểm tra lại thông tin." }, JsonRequestBehavior.DenyGet);
                        }
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        responseText = "Hiện tại không phải giai đoạn đánh giá điểm rèn luyện của bạn! "
                            + (accountConductSchedule != null ? "Thời gian đánh giá điểm của bạn từ "
                            + accountConductSchedule.BeginDateEvaluate.ToString("HH:mm dd/MM/yyy")
                            + " đến " + accountConductSchedule.EndDateEvaluate.ToString("HH:mm dd/MM/yyy") : "")
                    }, JsonRequestBehavior.DenyGet);
                }

            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseText = "Bạn không có quyền thực hiện chức năng này!"
                }, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion

        #region Show list student of class
        public ActionResult ClassEvaluate(int? idClass)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list student of class
                if (accountSession.Functions.IndexOf("ConductListStudent") != -1)
                {
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                    ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                    List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                    ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                    // Check valid evaluate time
                    if (accountConductSchedule != null && (DateTime.Now >= accountConductSchedule.BeginDateEvaluate && DateTime.Now <= accountConductSchedule.EndDateEvaluate))
                    {
                        StudentBO studentBO = new StudentBO(dsaContext);
                        ClassBO classBO = new ClassBO(dsaContext);
                        int idClassEvaluate = idClass ?? 0;
                        // Check permission
                        if (accountSession.IdDecentralizationGroup == Define.Role.Prefect)
                        {
                            idClassEvaluate = studentBO.GetStudentByStudentNumber(accountSession.UserName).Class.IdClass;
                        }
                        ConductSchedule previewConductSchedule = currentSchedule.Where(s => s.EndDateEvaluate < accountConductSchedule.EndDateEvaluate).OrderByDescending(c => c.EndDateEvaluate).FirstOrDefault();
                        List<Student> listStudent = studentBO.GetListStudentByClass(idClassEvaluate);
                        Class classEvaluate = classBO.GetClass(idClassEvaluate);
                        if (classEvaluate == null)
                        {
                            ViewBag.error = "Không tìm thấy lớp đánh giá!";
                            return View();
                        }
                        else
                        {
                            ViewBag.listStudent = listStudent;
                            ViewBag.currentEvent = currentEvent;
                            ViewBag.className = classEvaluate.ClassName;
                            ViewBag.classId = classEvaluate.IdClass;
                            ViewBag.accountConductSchedule = accountConductSchedule;
                            ViewBag.previewConductSchedule = previewConductSchedule;
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.error = "Hiện tại không phải giai đoạn đánh giá điểm rèn luyện của bạn! "
                            + (accountConductSchedule != null ? "Thời gian đánh giá điểm của bạn từ "
                            + accountConductSchedule.BeginDateEvaluate.ToString("HH:mm dd/MM/yyy")
                            + " đến " + accountConductSchedule.EndDateEvaluate.ToString("HH:mm dd/MM/yyy") : "");
                    }
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/ClientDenyFunction.cshtml");
                }
            }
            else
            {
                return Redirect("/Login");
            }
        }
        #endregion

        #region Update conduct detail by idStudent
        public ActionResult ConductEvaluateStudent(int idStudent)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check schedule to evaluate
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                if (accountConductSchedule != null && (DateTime.Now >= accountConductSchedule.BeginDateEvaluate && DateTime.Now <= accountConductSchedule.EndDateEvaluate))
                {
                    ConductSchedule previewConductSchedule = currentSchedule.Where(s => s.EndDateEvaluate < accountConductSchedule.EndDateEvaluate).OrderByDescending(c => c.EndDateEvaluate).FirstOrDefault();
                    StudentBO studentBO = new StudentBO(dsaContext);
                    Student student = studentBO.GetStudent(idStudent);
                    if (student == null)
                    {
                        ViewBag.error = "Không tìm thấy sinh viên!";
                        return View();
                    }
                    if (student.IsReserved == true || student.IsGraduated == true || student.IsExpelled == true)
                    {
                        return Json(new { success = false, responseText = "Bạn không được phép đánh giá kết quả rèn luyện sinh viên này!" }, JsonRequestBehavior.DenyGet);
                    }
                    ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                    ConductResult conductResultEvaluated = conductResultBO.GetConductResult(student.IdStudent, previewConductSchedule.IdConductSchedule);
                    ConductResult conductResult = conductResultBO.GetConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule);
                    if (conductResult != null)
                    {
                        // Evaluated
                        if (conductResult.IsApproved)
                        {
                            ViewBag.error = "Điểm rèn luyện đã được duyệt!";
                            return View();
                        }
                        if (conductResult.IsSaved)
                        {
                            ViewBag.conductResult = conductResult;
                        }
                    }
                    // Not evaluated or saved
                    ViewBag.conductResultEvaluated = conductResultEvaluated;
                    ViewBag.student = student;
                    ViewBag.currentSemester = currentSemester;
                    List<ConductItem> itemsByForm = (new ConductItemsBO(dsaContext)).GetListConductItems(currentEvent.IdConductForm);
                    ViewBag.itemsByForm = itemsByForm;
                }
                else
                {
                    ViewBag.error = "Hiện tại không phải giai đoạn đánh giá điểm rèn luyện của bạn! "
                        + (accountConductSchedule != null ? "Thời gian đánh giá điểm của bạn từ "
                        + accountConductSchedule.BeginDateEvaluate.ToString("HH:mm dd/MM/yyy")
                        + " đến " + accountConductSchedule.EndDateEvaluate.ToString("HH:mm dd/MM/yyy") : "");
                }
                return View();
            }
            else
            {
                return Redirect("/Login");
            }
        }

        [HttpPost]
        public ActionResult ConductEvaluateStudent(FormCollection collection)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                if (accountSession.Functions.IndexOf("ConductEvaluateStudents") != -1)
                {
                    int idStudent = Int32.Parse(collection["idStudent"]);
                    // Check permission to evaluate
                    // Lecturer granted to evaluate student in class 
                    if (accountSession.IdDecentralizationGroup == Define.Role.Lecturer)
                    {
                        ClassBO classBO = new ClassBO(dsaContext);
                        List<Class> classes = classBO.GetClassesByLecturerNumber(accountSession.UserName);
                        bool isExist = false;
                        foreach (Class perClass in classes)
                        {
                            if (classBO.IsStudentInClass(perClass.IdClass, idStudent))
                            {
                                isExist = true;
                                break;
                            }
                        }
                        if (!isExist)
                        {
                            return Json(new { success = false, responseText = "Bạn không được phép đánh giá sinh viên này!" }, JsonRequestBehavior.DenyGet);
                        }
                    }
                    else if (accountSession.IdDecentralizationGroup == Define.Role.Prefect)
                    {
                        // Prefect granted to evaluate student in same class
                        ClassBO classBO = new ClassBO(dsaContext);
                        Student student = (new StudentBO(dsaContext)).GetStudentByStudentNumber(accountSession.UserName);
                        if (!classBO.IsStudentsInSameClass(student.IdStudent, idStudent))
                        {
                            return Json(new { success = false, responseText = "Bạn không được phép đánh giá sinh viên này!" }, JsonRequestBehavior.DenyGet);
                        }
                    }

                    // Check schedule to evaluate
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                    ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                    List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                    ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                    if (accountConductSchedule != null && (DateTime.Now >= accountConductSchedule.BeginDateEvaluate && DateTime.Now <= accountConductSchedule.EndDateEvaluate))
                    {
                        // Get parameter
                        bool isApproved = StringExtension.IsNullOrWhiteSpace(collection["isApproved"]) ? false : bool.Parse(collection["isApproved"]);
                        bool isSaved = !isApproved;

                        // Init partial point
                        List<ConductItem> itemsByForm = (new ConductItemsBO(dsaContext)).GetListConductItems(currentEvent.IdConductForm);
                        Dictionary<int, int> pointNew = new Dictionary<int, int>();
                        Dictionary<int, int> pointOld = new Dictionary<int, int>();
                        foreach (ConductItem item in itemsByForm)
                        {
                            if (!StringExtension.IsNullOrWhiteSpace(collection["P" + item.IdConductItems]) && Int32.Parse(collection["P" + item.IdConductItems]) <= item.MaxPoints)
                                pointNew.Add(item.IdConductItems, Int32.Parse(collection["P" + item.IdConductItems]));
                        }

                        //Get old value
                        string old = collection["old"];
                        if (!StringExtension.IsNullOrWhiteSpace(old))
                        {
                            string[] arrPairValue = old.Split(';');
                            {
                                foreach (string pair in arrPairValue)
                                {
                                    try
                                    {
                                        string[] arrValue = pair.Split(':');
                                        pointOld.Add(Int32.Parse(arrValue[0]), Int32.Parse(arrValue[1]));
                                    }
                                    catch (Exception)
                                    {

                                    }

                                }
                            }
                        }

                        // Merge point
                        string partialPoint = "";
                        //partialPoint = (!partialPoint.Equals("")) ? partialPoint.Substring(0, partialPoint.Length - 1) : "";
                        foreach (ConductItem item in itemsByForm)
                        {
                            if (pointNew.ContainsKey(item.IdConductItems) && pointNew[item.IdConductItems] <= item.MaxPoints)
                                partialPoint += item.IdConductItems + ":" + pointNew[item.IdConductItems] + ";";
                            else if (pointOld.ContainsKey(item.IdConductItems) && pointOld[item.IdConductItems] <= item.MaxPoints)
                            {
                                partialPoint += item.IdConductItems + ":" + pointNew[item.IdConductItems] + ";";
                            }
                        }
                        partialPoint = (!partialPoint.Equals("")) ? partialPoint.Substring(0, partialPoint.Length - 1) : "";

                        // Check isExist student in ConductResult
                        ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                        ConductResult conductResult = conductResultBO.GetConductResult(idStudent, accountConductSchedule.IdConductSchedule);
                        if (conductResult != null)
                        {
                            if (conductResult.IsApproved)
                            {
                                return Json(new { success = false, responseText = "Bạn đã hoàn thành đánh giá kết quả rèn luyện!" }, JsonRequestBehavior.DenyGet);
                            }
                            else
                            {
                                bool isSuccess = conductResultBO.UpdateConductResult(idStudent, accountConductSchedule.IdConductSchedule, currentEvent.IdConductEvent, isApproved, isSaved, partialPoint);
                                if (isSuccess)
                                {
                                    if (isSaved)
                                    {
                                        return Json(new { success = true, isApproved = isApproved, responseText = "Lưu kết quả rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                                    }
                                    return Json(new { success = true, isApproved = isApproved, responseText = "Đánh giá kết quả rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                                }
                                else
                                {
                                    return Json(new { success = false, responseText = "Cập nhật kết quả rèn luyện thất bại! Kiểm tra lại thông tin." }, JsonRequestBehavior.DenyGet);
                                }
                            }
                        }
                        else
                        {
                            //Not evaluated
                            bool isSuccess = conductResultBO.AddConductResult(idStudent, accountConductSchedule.IdConductSchedule, currentEvent.IdConductEvent, isApproved, isSaved, partialPoint);
                            if (isSuccess)
                            {
                                if (isSaved)
                                {
                                    return Json(new { success = true, isApproved = isApproved, responseText = "Lưu kết quả rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                                }
                                return Json(new { success = true, isApproved = isApproved, responseText = "Đánh giá kết quả rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                            }
                            else
                            {
                                return Json(new { success = false, responseText = "Cập nhật kết quả rèn luyện thất bại! Kiểm tra lại thông tin." }, JsonRequestBehavior.DenyGet);
                            }
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            responseText = "Hiện tại không phải giai đoạn đánh giá điểm rèn luyện của bạn! "
                                + (accountConductSchedule != null ? "Thời gian đánh giá điểm của bạn từ "
                                + accountConductSchedule.BeginDateEvaluate.ToString("HH:mm dd/MM/yyy")
                                + " đến " + accountConductSchedule.EndDateEvaluate.ToString("HH:mm dd/MM/yyy") : "")
                        }, JsonRequestBehavior.DenyGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        responseText = "Bạn không đủ quyền để đánh giá sinh viên này!"
                    }, JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseText = "Chưa đăng nhập hệ thống!"
                }, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion

        #region Show list class of lecturer or faculty
        public ActionResult ClassesEvaluate(int? idFaculty)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ConductListClass") != -1)
                {
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                    ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                    List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                    ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                    // Check valid evaluate time
                    if (accountConductSchedule != null && (DateTime.Now >= accountConductSchedule.BeginDateEvaluate && DateTime.Now <= accountConductSchedule.EndDateEvaluate))
                    {
                        ClassBO classBO = new ClassBO(dsaContext);
                        List<Class> classes = new List<Class>();
                        if (idFaculty != null)
                        {
                            FacultyBO facultyBO = new FacultyBO(dsaContext);
                            Faculty faculty = facultyBO.GetFaculty(idFaculty.Value);
                            classes = (classBO.GetClassesByIdFaculty(faculty.IdFaculty));
                            ViewBag.faculty = faculty;
                        }
                        else
                        {
                            classes = classBO.GetClassesByLecturerNumber(accountSession.UserName);
                        }

                        ViewBag.classes = classes;
                        ViewBag.currentEvent = currentEvent;
                        ViewBag.accountConductSchedule = accountConductSchedule;
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Hiện tại không phải giai đoạn đánh giá điểm rèn luyện của bạn! "
                            + (accountConductSchedule != null ? "Thời gian đánh giá điểm của bạn từ "
                            + accountConductSchedule.BeginDateEvaluate.ToString("HH:mm dd/MM/yyy")
                            + " đến " + accountConductSchedule.EndDateEvaluate.ToString("HH:mm dd/MM/yyy") : "");
                    }
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/ClientDenyFunction.cshtml");
                }
            }
            else
            {
                return Redirect("/Login");
            }
        }

        #endregion

        public ActionResult FacultyEvaluate()
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ConductListFaculty") != -1)
                {
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                    ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                    List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                    ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                    // Check valid evaluate time
                    if (accountConductSchedule != null && (DateTime.Now >= accountConductSchedule.BeginDateEvaluate && DateTime.Now <= accountConductSchedule.EndDateEvaluate))
                    {
                        FacultyBO facultyBO = new FacultyBO(dsaContext);
                        List<Faculty> faculties = facultyBO.GetListFaculty();
                        ViewBag.faculties = faculties;
                        ViewBag.listItem = new ConductItemsBO(dsaContext).GetListConductItems(currentEvent.IdConductForm).Where(i => i.ItemIndex.Length == 4).OrderBy(i => i.ItemIndex);
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Hiện tại không phải giai đoạn đánh giá điểm rèn luyện của bạn! "
                            + (accountConductSchedule != null ? "Thời gian đánh giá điểm của bạn từ "
                            + accountConductSchedule.BeginDateEvaluate.ToString("HH:mm dd/MM/yyy")
                            + " đến " + accountConductSchedule.EndDateEvaluate.ToString("HH:mm dd/MM/yyy") : "");
                    }
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/ClientDenyFunction.cshtml");
                }
            }
            else
            {
                return Redirect("/Login");
            }
        }


        [HttpPost]
        public ActionResult ConductApproveStudent(int idStudent)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission
                if (accountSession.Functions.IndexOf("ConductApproveStudent") != -1)
                {
                    if (accountSession.IdDecentralizationGroup == Define.Role.Lecturer)
                    {
                        ClassBO classBO = new ClassBO(dsaContext);
                        List<Class> classes = classBO.GetClassesByLecturerNumber(accountSession.UserName);
                        bool isExist = false;
                        foreach (Class perClass in classes)
                        {
                            if (classBO.IsStudentInClass(perClass.IdClass, idStudent))
                            {
                                isExist = true;
                                break;
                            }
                        }
                        if (!isExist)
                        {
                            return Json(new { success = false, responseText = "Bạn không được phép đánh giá sinh viên này!" }, JsonRequestBehavior.DenyGet);
                        }
                    }
                    else if (accountSession.IdDecentralizationGroup == Define.Role.Prefect)
                    {
                        // Prefect granted to evaluate student in same class
                        ClassBO classBO = new ClassBO(dsaContext);
                        Student student = (new StudentBO(dsaContext)).GetStudentByStudentNumber(accountSession.UserName);
                        if (!classBO.IsStudentsInSameClass(student.IdStudent, idStudent))
                        {
                            return Json(new { success = false, responseText = "Bạn không được phép đánh giá sinh viên này!" }, JsonRequestBehavior.DenyGet);
                        }
                    }


                    // Check schedule to evaluate
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                    ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                    List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                    ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                    if (accountConductSchedule != null && (DateTime.Now >= accountConductSchedule.BeginDateEvaluate && DateTime.Now <= accountConductSchedule.EndDateEvaluate))
                    {
                        ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                        // Get conduct result saved
                        ConductResult conductResultEvaluated = conductResultBO.GetConductResult(idStudent, accountConductSchedule.IdConductSchedule);
                        if (conductResultEvaluated != null)
                        {
                            if (conductResultBO.UpdateConductResult(idStudent, accountConductSchedule.IdConductSchedule, currentEvent.IdConductEvent, true, false, conductResultEvaluated.PartialPoint))
                            {
                                return Json(new { success = true, responseText = "Duyệt điểm rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                            }
                            else
                            {
                                return Json(new { success = true, responseText = "Xảy ra lỗi trong quá trình duyệt điểm! Vui lòng thử lại hoặc chọn đánh giá thủ công." }, JsonRequestBehavior.DenyGet);
                            }
                        }
                        else
                        {
                            bool isReserved = false;
                            bool isGraduated = false;
                            bool isExpelled = false;
                            try
                            {
                                StudentBO studentBO = new StudentBO(dsaContext);
                                Student student = studentBO.GetStudent(idStudent);
                                isReserved = student.IsReserved.Value;
                                isGraduated = student.IsGraduated.Value;
                                isExpelled = student.IsExpelled.Value;
                            }
                            catch
                            {
                            }
                            if (isGraduated || isReserved || isExpelled)
                            {
                                return Json(new { success = false, responseText = "Không được phép duyệt điểm rèn luyện sinh viên!" }, JsonRequestBehavior.DenyGet);
                            }
                            // Not saved 
                            ConductSchedule previewConductSchedule = currentSchedule.Where(s => s.EndDateEvaluate < accountConductSchedule.EndDateEvaluate).OrderByDescending(c => c.EndDateEvaluate).FirstOrDefault();
                            conductResultEvaluated = conductResultBO.GetConductResult(idStudent, previewConductSchedule.IdConductSchedule);
                            string partialPoint = "0";
                            if (conductResultEvaluated != null)
                            {
                                partialPoint = conductResultEvaluated.PartialPoint;
                            }
                            if (conductResultBO.AddConductResult(idStudent, accountConductSchedule.IdConductSchedule, currentEvent.IdConductEvent, true, false, partialPoint))
                            {
                                return Json(new { success = true, responseText = "Duyệt điểm rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                            }
                            else
                            {
                                return Json(new { success = false, responseText = "Xảy ra lỗi trong quá trình duyệt điểm! Vui lòng thử lại hoặc chọn đánh giá thủ công." }, JsonRequestBehavior.DenyGet);
                            }
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            responseText = "Hiện tại không phải giai đoạn đánh giá điểm rèn luyện của bạn! "
                                + (accountConductSchedule != null ? "Thời gian đánh giá điểm của bạn từ "
                                + accountConductSchedule.BeginDateEvaluate.ToString("HH:mm dd/MM/yyy")
                                + " đến " + accountConductSchedule.EndDateEvaluate.ToString("HH:mm dd/MM/yyy") : "")
                        }, JsonRequestBehavior.DenyGet);
                    }

                }
                else
                {
                    return View("~/Views/Shared/ClientDenyFunction.cshtml");
                }
            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseText = "Chưa đăng nhập hệ thống!"
                }, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult ConductApproveClass(int idClass)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission
                if (accountSession.Functions.IndexOf("ConductApproveClass") != -1)
                {
                    // Check permission to evaluate class
                    if (accountSession.IdDecentralizationGroup == Define.Role.Lecturer)
                    {
                        ClassBO classBO = new ClassBO(dsaContext);
                        List<Class> classes = classBO.GetClassesByLecturerNumber(accountSession.UserName);
                        bool isGranted = false;
                        foreach (Class perClass in classes)
                        {
                            if (idClass == perClass.IdClass)
                            {
                                isGranted = true;
                                break;
                            }
                        }
                        if (!isGranted)
                        {
                            return Json(new { success = false, responseText = "Bạn không được phép duyệt điểm lớp này!" }, JsonRequestBehavior.DenyGet);
                        }
                    }
                    else if (accountSession.IdDecentralizationGroup == Define.Role.Prefect)
                    {
                        // Prefect granted to evaluate student in same class
                        ClassBO classBO = new ClassBO(dsaContext);
                        Student student = (new StudentBO(dsaContext)).GetStudentByStudentNumber(accountSession.UserName);
                        if (!(student.Class.IdClass == idClass))
                        {
                            return Json(new { success = false, responseText = "Bạn không được phép duyệt điểm lớp này!" }, JsonRequestBehavior.DenyGet);
                        }
                    }

                    // Check schedule to evaluate
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                    ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                    List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                    ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                    if (accountConductSchedule != null && (DateTime.Now >= accountConductSchedule.BeginDateEvaluate && DateTime.Now <= accountConductSchedule.EndDateEvaluate))
                    {
                        ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                        // Get conduct result saved
                        ConductResult conductResultEvaluated;
                        StudentBO studentBO = new StudentBO(dsaContext);
                        List<Student> students = studentBO.GetListStudentByClass(idClass);
                        bool isSuccess = true;
                        foreach (Student student in students)
                        {
                            bool isReserved = false;
                            bool isGraduated = false;
                            bool isExpelled = false;
                            try
                            {
                                isReserved = student.IsReserved.Value;
                                isGraduated = student.IsGraduated.Value;
                                isExpelled = student.IsExpelled.Value;
                            }
                            catch
                            {
                            }
                            if (isGraduated || isReserved || isExpelled)
                            {
                                continue;
                            }

                            conductResultEvaluated = conductResultBO.GetConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule);
                            if (conductResultEvaluated != null && conductResultEvaluated.IsApproved == false)
                            {
                                if (!conductResultBO.UpdateConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule, currentEvent.IdConductEvent, true, false, conductResultEvaluated.PartialPoint))
                                {
                                    isSuccess = false;
                                }
                            }
                            else if (conductResultEvaluated == null)
                            {
                                // Not saved 
                                ConductSchedule previewConductSchedule = currentSchedule.Where(s => s.EndDateEvaluate < accountConductSchedule.EndDateEvaluate).OrderByDescending(c => c.EndDateEvaluate).FirstOrDefault();
                                conductResultEvaluated = conductResultBO.GetConductResult(student.IdStudent, previewConductSchedule.IdConductSchedule);
                                string partialPoint = "0";
                                if (conductResultEvaluated != null)
                                {
                                    partialPoint = conductResultEvaluated.PartialPoint;
                                }
                                if (!conductResultBO.AddConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule, currentEvent.IdConductEvent, true, false, partialPoint))
                                {
                                    isSuccess = false;
                                }
                            }
                        }
                        if (isSuccess)
                        {
                            return Json(new { success = true, responseText = "Duyệt điểm rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                        }
                        else
                        {
                            return Json(new { success = true, responseText = "Xảy ra lỗi trong quá trình duyệt điểm! Vui lòng kiểm tra lại hoặc chọn đánh giá thủ công." }, JsonRequestBehavior.DenyGet);
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            responseText = "Hiện tại không phải giai đoạn đánh giá điểm rèn luyện của bạn! "
                                + (accountConductSchedule != null ? "Thời gian đánh giá điểm của bạn từ "
                                + accountConductSchedule.BeginDateEvaluate.ToString("HH:mm dd/MM/yyy")
                                + " đến " + accountConductSchedule.EndDateEvaluate.ToString("HH:mm dd/MM/yyy") : "")
                        }, JsonRequestBehavior.DenyGet);
                    }

                }
                else
                {
                    return View("~/Views/Shared/ClientDenyFunction.cshtml");
                }
            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseText = "Chưa đăng nhập hệ thống!"
                }, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult ConductApproveFaculty(int idFaculty)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission
                if (accountSession.Functions.IndexOf("ConductApproveFaculty") != -1)
                {
                    // Check schedule to evaluate
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                    ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                    List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                    ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                    if (accountConductSchedule != null && (DateTime.Now >= accountConductSchedule.BeginDateEvaluate && DateTime.Now <= accountConductSchedule.EndDateEvaluate))
                    {
                        ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                        // Get conduct result saved
                        ConductResult conductResultEvaluated;
                        StudentBO studentBO = new StudentBO(dsaContext);
                        List<Student> students = studentBO.GetListStudentByFaculty(idFaculty);
                        bool isSuccess = true;
                        foreach (Student student in students)
                        {
                            conductResultEvaluated = conductResultBO.GetConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule);
                            if (conductResultEvaluated != null && conductResultEvaluated.IsApproved == false)
                            {
                                if (!conductResultBO.UpdateConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule, currentEvent.IdConductEvent, true, false, conductResultEvaluated.PartialPoint))
                                {
                                    isSuccess = false;
                                }
                            }
                            else if (conductResultEvaluated == null)
                            {
                                // Not saved 
                                ConductSchedule previewConductSchedule = currentSchedule.Where(s => s.EndDateEvaluate < accountConductSchedule.EndDateEvaluate).OrderByDescending(c => c.EndDateEvaluate).FirstOrDefault();
                                conductResultEvaluated = conductResultBO.GetConductResult(student.IdStudent, previewConductSchedule.IdConductSchedule);
                                string partialPoint = "0";
                                if (conductResultEvaluated != null)
                                {
                                    partialPoint = conductResultEvaluated.PartialPoint;
                                }
                                if (!conductResultBO.AddConductResult(student.IdStudent, accountConductSchedule.IdConductSchedule, currentEvent.IdConductEvent, true, false, partialPoint))
                                {
                                    isSuccess = false;
                                }
                            }
                        }
                        if (isSuccess)
                        {
                            return Json(new { success = true, responseText = "Duyệt điểm rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                        }
                        else
                        {
                            return Json(new { success = true, responseText = "Xảy ra lỗi trong quá trình duyệt điểm! Vui lòng kiểm tra lại hoặc chọn đánh giá thủ công." }, JsonRequestBehavior.DenyGet);
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            responseText = "Hiện tại không phải giai đoạn đánh giá điểm rèn luyện của bạn! "
                                + (accountConductSchedule != null ? "Thời gian đánh giá điểm của bạn từ "
                                + accountConductSchedule.BeginDateEvaluate.ToString("HH:mm dd/MM/yyy")
                                + " đến " + accountConductSchedule.EndDateEvaluate.ToString("HH:mm dd/MM/yyy") : "")
                        }, JsonRequestBehavior.DenyGet);
                    }

                }
                else
                {
                    return View("~/Views/Shared/ClientDenyFunction.cshtml");
                }
            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseText = "Chưa đăng nhập hệ thống!"
                }, JsonRequestBehavior.DenyGet);
            }
        }

        #region Conduct Information
        public ActionResult ConductSummary(int? idStudent)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                SemesterBO semesterBO = new SemesterBO(dsaContext);
                Semester currentSemester = semesterBO.GetSemesterCurrent();
                StudentBO studentBO = new StudentBO(dsaContext);
                Student student;
                if (idStudent != null)
                {
                    student = studentBO.GetStudent(idStudent.Value);
                }
                else
                {
                    student = studentBO.GetStudentByStudentNumber(accountSession.UserName);
                }
                // Get list semester
                List<Semester> listSemester = semesterBO.GetSemesterByYear(student.Class.Course.AdmissionYear);
                List<ConductResultSemester> listResult = new List<ConductResultSemester>();
                List<ConductResult> listConductResult = student.ConductResults.OrderBy(r => r.ConductEvent.Semester.BeginTime).ToList();
                foreach (Semester semester in listSemester)
                {
                    ConductResult conductResult = null;
                    try
                    {
                        conductResult = student.ConductResults.Where(c => c.ConductEvent.IdSemester == semester.IdSemester && c.IsApproved == true).OrderByDescending(c => c.IdConductSchedule).FirstOrDefault();
                    }
                    catch
                    {
                        conductResult = null;
                    }
                    int total = 0;
                    if (conductResult != null)
                    {
                        string[] arrPairValue = conductResult.PartialPoint.Split(';');
                        foreach (string pair in arrPairValue)
                        {
                            try
                            {
                                total += Int32.Parse(pair.Split(':')[1]);
                            }
                            catch { }
                        }
                        ConductResultSemester conductResultSemester = new ConductResultSemester(conductResult.ConductEvent.IdSemester, conductResult.ConductEvent.Semester.SemesterYear.SemesterYearName, conductResult.ConductEvent.Semester.Year.YearName, total);
                        listResult.Add(conductResultSemester);
                    }
                }
                ViewBag.listResult = listResult;
                return View();
            }
            else
            {
                return Redirect("/Login");
            }
        }
        public ActionResult ConductDetail(int idSemester, int? idStudent)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                try
                {
                    // Check schedule to evaluate
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(idSemester);
                    List<ConductSchedule> listSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                    ConductSchedule studentConductSchedule = listSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == Define.Role.Prefect);
                    ConductSchedule lecturerConductSchedule = listSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == Define.Role.Lecturer);
                    ConductSchedule dsaConductSchedule = listSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == Define.Role.DSAStaff);

                    StudentBO studentBO = new StudentBO(dsaContext);
                    Student student;
                    if (idStudent != null)
                    {
                        student = studentBO.GetStudent(idStudent.Value);
                    }
                    else
                    {
                        student = studentBO.GetStudentByStudentNumber(accountSession.UserName);
                    }
                    ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                    try
                    {
                        ConductResult studentConductResult = studentConductSchedule != null ? conductResultBO.GetConductResult(student.IdStudent, studentConductSchedule.IdConductSchedule) : null;
                        ConductResult lecturerConductResult = lecturerConductSchedule != null ? conductResultBO.GetConductResult(student.IdStudent, lecturerConductSchedule.IdConductSchedule) : null;
                        ConductResult dsaConductResult = dsaConductSchedule != null ? conductResultBO.GetConductResult(student.IdStudent, dsaConductSchedule.IdConductSchedule) : null;
                        Dictionary<string, string> pointStudent = new Dictionary<string, string>();
                        Dictionary<string, string> pointLecturer = new Dictionary<string, string>();
                        Dictionary<string, string> pointDSA = new Dictionary<string, string>();
                        if (studentConductResult != null)
                        {
                            string[] arrPairValue = studentConductResult.PartialPoint.Split(';');
                            foreach (string pair in arrPairValue)
                            {
                                string[] arrValue = pair.Split(':');
                                pointStudent.Add(arrValue[0], arrValue[1]);
                            }
                        }
                        else
                        {
                            studentConductSchedule = listSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == Define.Role.Student);
                            studentConductResult = studentConductSchedule != null ? conductResultBO.GetConductResult(student.IdStudent, studentConductSchedule.IdConductSchedule) : null;
                            if (studentConductResult != null)
                            {
                                string[] arrPairValue = studentConductResult.PartialPoint.Split(';');
                                foreach (string pair in arrPairValue)
                                {
                                    string[] arrValue = pair.Split(':');
                                    pointStudent.Add(arrValue[0], arrValue[1]);
                                }
                            }
                        }
                        if (lecturerConductResult != null)
                        {
                            string[] arrPairValue = lecturerConductResult.PartialPoint.Split(';');
                            foreach (string pair in arrPairValue)
                            {
                                string[] arrValue = pair.Split(':');
                                pointLecturer.Add(arrValue[0], arrValue[1]);
                            }
                        }
                        if (dsaConductResult != null)
                        {
                            string[] arrPairValue = dsaConductResult.PartialPoint.Split(';');
                            foreach (string pair in arrPairValue)
                            {
                                string[] arrValue = pair.Split(':');
                                pointDSA.Add(arrValue[0], arrValue[1]);
                            }
                        }
                        ViewBag.pointStudent = pointStudent;
                        ViewBag.pointLecturer = pointLecturer;
                        ViewBag.pointDSA = pointDSA;
                    }
                    catch
                    {
                    }
                    // Not evaluated or saved
                    ViewBag.student = student;
                    ViewBag.currentSemester = new SemesterBO(dsaContext).GetSemesterById(idSemester);
                    List<ConductItem> itemsByForm = (new ConductItemsBO(dsaContext)).GetListConductItems(currentEvent.IdConductForm);
                    ViewBag.itemsByForm = itemsByForm;
                    return View();
                }
                catch
                {
                    ViewBag.error = "Xảy ra lỗi trong quá trình lấy thông tin chi tiết!";
                    return View();
                }
            }
            else
            {
                return Redirect("/Login");
            }
        }
        #endregion
        public ActionResult ImportListStudent(FormCollection formCollection)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ImportConductItemStudent") != -1);
                if (!isGranted)
                {
                    return Content("false;Bạn không có quyền sử dụng chức năng này!");
                }
                string studentNumber = "";
                int point = 0;
                try
                {
                    if (Request != null)
                    {
                        // Get data from form
                        int idItem = Int32.Parse(Request.Form["IdItem"]);
                        HttpPostedFileBase file = Request.Files["UploadedFile"];
                        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                        {
                            string fileName = file.FileName;
                            string fileContentType = file.ContentType;
                            byte[] fileBytes = new byte[file.ContentLength];
                            var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                            using (var package = new ExcelPackage(file.InputStream))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                using (var context = dsaContext.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        string errorStudent = "";
                                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                        {
                                            try
                                            {
                                                // Get data from colunm excel
                                                studentNumber = workSheet.Cells[rowIterator, 2].Value.ToString();
                                                point = Int32.Parse(workSheet.Cells[rowIterator, 4].Value.ToString().Trim());
                                                // Get student
                                                StudentBO studentBO = new StudentBO(dsaContext);
                                                Student student = studentBO.GetStudentByStudentNumber(studentNumber);
                                                // Get conduct event
                                                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                                                ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                                                Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                                                ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                                                ConductSchedule currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent).Where(c => c.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup).FirstOrDefault();
                                                ConductSchedule previewSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent).OrderByDescending(s => s.EndDateEvaluate).Where(s => s.IdConductSchedule != currentSchedule.IdConductSchedule).FirstOrDefault();
                                                // Get result of student
                                                ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                                                ConductResult conductResult = student.ConductResults.Where(r => r.IdConductEvent == currentEvent.IdConductEvent && r.ConductSchedule.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup).FirstOrDefault();
                                                Dictionary<int, int> points = new Dictionary<int, int>();
                                                if (conductResult != null)
                                                {
                                                    // Parse original point
                                                    string partialPoint = conductResult.PartialPoint;
                                                    if (!StringExtension.IsNullOrWhiteSpace(partialPoint))
                                                    {
                                                        string[] arrPairValue = partialPoint.Split(';');
                                                        {
                                                            foreach (string pair in arrPairValue)
                                                            {
                                                                try
                                                                {
                                                                    string[] arrValue = pair.Split(':');
                                                                    points.Add(Int32.Parse(arrValue[0]), Int32.Parse(arrValue[1]));
                                                                }
                                                                catch (Exception)
                                                                {

                                                                }
                                                            }
                                                        }
                                                        //Check point of item
                                                        if (points.ContainsKey(idItem))
                                                            points[idItem] = point;
                                                        else
                                                        {
                                                            points.Add(idItem, point);
                                                        }
                                                        // Get partial point string
                                                        string resultPoint = "";
                                                        List<ConductItem> itemByForm = conductResult.ConductEvent.ConductForm.ConductItems.ToList();
                                                        foreach (ConductItem item in itemByForm)
                                                        {
                                                            if (points.ContainsKey(item.IdConductItems) && points[item.IdConductItems] <= item.MaxPoints)
                                                                resultPoint += item.IdConductItems + ":" + points[item.IdConductItems] + ";";
                                                        }
                                                        resultPoint = (!resultPoint.Equals("")) ? resultPoint.Substring(0, resultPoint.Length - 1) : "";
                                                        conductResultBO.UpdateConductResult(conductResult.IdStudent, conductResult.IdConductSchedule, conductResult.IdConductEvent, conductResult.IsApproved, conductResult.IsSaved, resultPoint);
                                                    }
                                                    else
                                                    {
                                                        conductResultBO.UpdateConductResult(conductResult.IdStudent, conductResult.IdConductSchedule, conductResult.IdConductEvent, conductResult.IsApproved, conductResult.IsSaved, idItem + ":" + point);
                                                    }
                                                }
                                                else
                                                {
                                                    string newPartial = idItem + ":" + point;
                                                    string oldPartial = "";
                                                    ConductResult previewConductResult = conductResultBO.GetConductResult(student.IdStudent, previewSchedule.IdConductSchedule);
                                                    if (previewConductResult != null)
                                                    {
                                                        oldPartial = previewConductResult.PartialPoint;
                                                    }
                                                    // Init partial point
                                                    List<ConductItem> itemsByForm = (new ConductItemsBO(dsaContext)).GetListConductItems(currentEvent.IdConductForm);
                                                    Dictionary<int, int> pointNew = new Dictionary<int, int>();
                                                    Dictionary<int, int> pointOld = new Dictionary<int, int>();
                                                    pointNew.Add(idItem, point);

                                                    //Get old value
                                                    if (!StringExtension.IsNullOrWhiteSpace(oldPartial))
                                                    {
                                                        string[] arrPairValue = oldPartial.Split(';');
                                                        {
                                                            foreach (string pair in arrPairValue)
                                                            {
                                                                try
                                                                {
                                                                    string[] arrValue = pair.Split(':');
                                                                    pointOld.Add(Int32.Parse(arrValue[0]), Int32.Parse(arrValue[1]));
                                                                }
                                                                catch (Exception)
                                                                {

                                                                }

                                                            }
                                                        }
                                                    }

                                                    // Merge point
                                                    string partialPoint = "";
                                                    foreach (ConductItem item in itemsByForm)
                                                    {
                                                        if (pointNew.ContainsKey(item.IdConductItems) && pointNew[item.IdConductItems] <= item.MaxPoints)
                                                            partialPoint += item.IdConductItems + ":" + pointNew[item.IdConductItems] + ";";
                                                        else if (pointOld.ContainsKey(item.IdConductItems) && pointOld[item.IdConductItems] <= item.MaxPoints)
                                                        {
                                                            partialPoint += item.IdConductItems + ":" + pointNew[item.IdConductItems] + ";";
                                                        }
                                                    }
                                                    partialPoint = (!partialPoint.Equals("")) ? partialPoint.Substring(0, partialPoint.Length - 1) : "";
                                                    conductResultBO.AddConductResult(student.IdStudent, currentSchedule.IdConductSchedule, currentEvent.IdConductEvent, false, true, partialPoint);
                                                }
                                            }
                                            catch
                                            {
                                                errorStudent += studentNumber + " ";
                                            }
                                        }
                                        context.Commit();
                                        return Content("true;Nhập từ file excel hoàn tất!" + (string.IsNullOrEmpty(errorStudent) ? "" : " Các sinh viên bị lỗi: " + errorStudent));
                                    }
                                    catch (Exception e)
                                    {
                                        context.Rollback();
                                        return Content("false;Nhập từ file excel thất bại!" + (e.Message != null ? e.Message : ""));
                                    }
                                }
                            }
                        }
                    }
                    return Content("false;Nhập từ file excel thất bại!");
                }
                catch
                {
                    // Return text to avoid auto download file json in old browser.
                    return Content("false;Nhập từ file excel thất bại!");
                }
            }
            else
            {
                return Content("false;Chưa đăng nhập vào hệ thống!");
            }
        }

        #region export file excel

        public ActionResult ExportListStudent(int idClass)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list student of class
                if (accountSession.Functions.IndexOf("ExportClassList") != -1)
                {
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                    ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                    List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent);
                    ConductSchedule accountConductSchedule = currentSchedule.SingleOrDefault(s => s.IdDecenTralizationGroup == accountSession.IdDecentralizationGroup);
                    // Check valid evaluate time
                    StudentBO studentBO = new StudentBO(dsaContext);
                    ClassBO classBO = new ClassBO(dsaContext);
                    // Check permission
                    List<Student> listStudent = studentBO.GetListStudentByClass(idClass).Where(s => s.IsExpelled == false && s.IsGraduated == false && s.IsReserved == false).ToList();
                    Class classExport = classBO.GetClass(idClass);
                    if (classExport == null)
                    {
                        return Json(new
                        {
                            success = false,
                            responseText = "Không tìm thấy lớp đánh giá!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ViewBag.listStudent = listStudent;
                        ViewBag.accountConductSchedule = accountConductSchedule;
                        Excel.Application application = new Excel.Application();
                        // Open file document template
                        Excel.Workbook workbook = application.Workbooks.Open(Server.MapPath("~/Files/Conduct/BangDiemRenLuyen_Class_ddMMyyyy.xls"), 0, false, 5, "", "", false,
                            Excel.XlPlatform.xlWindows,
                            "", true, true, 0, true, false, false);
                        // Get sheet
                        Excel.Worksheet workSheet = workbook.Worksheets[1];

                        // Set values for sheet name
                        workSheet.Name = classExport.ClassName;
                        // Set field conduct event
                        workSheet.get_Range("A5", "G5").Value2 = "Lớp: " + classExport.ClassName + " Học kỳ: " + currentEvent.Semester.SemesterYear.SemesterYearName + " Năm học: " + currentEvent.Semester.Year.YearName;
                        int row = 10;
                        for (int i = listStudent.Count() - 1; i >= 0; i--)
                        {
                            // Get total point of student
                            ConductResult conductResult = listStudent[i].ConductResults.SingleOrDefault(c => c.IdConductEvent == currentEvent.IdConductEvent && c.IdConductSchedule == accountConductSchedule.IdConductSchedule);
                            int total = 0;
                            if (conductResult != null)
                            {
                                string[] arrPairValue = conductResult.PartialPoint.Split(';');
                                foreach (string pair in arrPairValue)
                                {
                                    try
                                    {
                                        total += Int32.Parse(pair.Split(':')[1]);
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                            // Set field index
                            workSheet.Rows[10].Insert();
                            workSheet.get_Range("A10", "A10").Value2 = i + 1;
                            // Set student number
                            workSheet.get_Range("B10", "B10").Value2 = listStudent[i].StudentNumber;
                            // Set field student name
                            workSheet.get_Range("C10", "C10").Value2 = listStudent[i].LastNameStudent + " " + listStudent[i].FirstNameStudent;
                            // Set field point student
                            workSheet.get_Range("D10", "D10").Value2 = listStudent[i].Class.ClassName;
                            // Set field point 
                            workSheet.get_Range("E10", "E10").Value2 = total;
                            // Set Grade Evaluated
                            workSheet.get_Range("F10", "F10").Value2 = DataExtension.GetGradeEvaluated(total);
                            row++;
                        }
                        workSheet.Rows[row].Delete();
                        row += 1;
                        // Set date sign
                        workSheet.get_Range("D" + row, "D" + row).Value2 = "Đà Nẵng, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        // Set name of lecturer
                        row += 4;
                        LecturerClass lecturerClass = classExport.LecturerClasses.Where(l => l.IdSemester == currentEvent.IdSemester).OrderByDescending(l => l.EndDate).FirstOrDefault();
                        workSheet.get_Range("D" + row, "D" + row).Value2 = lecturerClass != null ? lecturerClass.Lecturer.LastName + " " + lecturerClass.Lecturer.FirstName : "";
                        workSheet.Rows[9].Delete();

                        String nameFile = "BangDiemRenLuyen_" + classExport.ClassName + "_" + DateTime.Now.ToString("ddMMyyyy") + ".xls";

                        if (System.IO.File.Exists(Server.MapPath("~/Files/Conduct/" + nameFile)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/Files/Conduct/" + nameFile));
                        }

                        workbook.SaveAs(Server.MapPath("~/Files/Conduct/" + nameFile));
                        workbook.Close();
                        Marshal.ReleaseComObject(workbook);
                        application.Quit();
                        Marshal.FinalReleaseComObject(application);

                        Response.ContentType = "application/octet-stream";
                        Response.AppendHeader("content-disposition", "attachment;filename=" + nameFile);
                        Response.TransmitFile(Server.MapPath("~/Files/Conduct/" + nameFile));
                        Response.Flush();
                        Response.End();

                        System.IO.File.Delete(Server.MapPath("~/Files/Conduct/" + nameFile));

                        return Json(new
                        {
                            success = false,
                            responseText = "Export file excel thành công!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        responseText = "Bạn không có quyền sử dụng chức năng này!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseText = "Chưa đăng nhập hệ thống!"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExportListClass(int idFaculty)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission export list class of faculty
                if (accountSession.Functions.IndexOf("ExportFacultyList") != -1)
                {
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    Semester currentSemester = (new SemesterBO(dsaContext)).GetSemesterCurrent();
                    ConductEvent currentEvent = conductEventBO.GetConductEventBySemester(currentSemester.IdSemester);
                    List<ConductSchedule> currentSchedule = conductScheduleBO.GetListScheduleByEvent(currentEvent.IdConductEvent).OrderByDescending(s => s.EndDateEvaluate).ToList();
                    ConductSchedule lastConductSchedule = currentSchedule.FirstOrDefault();
                    StudentBO studentBO = new StudentBO(dsaContext);
                    ClassBO classBO = new ClassBO(dsaContext);
                    FacultyBO facultyBO = new FacultyBO(dsaContext);
                    Faculty faculty = facultyBO.GetFaculty(idFaculty);
                    if (faculty == null)
                    {
                        return Json(new
                        {
                            success = false,
                            responseText = "Không tìm thấy khoa đánh giá!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    List<Class> listClass = classBO.GetClassByFaculty(idFaculty);
                    string errorClass = "";
                    Excel.Application application = new Excel.Application();
                    // Open file document template
                    Excel.Workbook workbook = application.Workbooks.Open(Server.MapPath("~/Files/Conduct/BangDiemRenLuyen_Faculty_ddMMyyyy.xls"), 0, false, 5, "", "", false,
                        Excel.XlPlatform.xlWindows,
                        "", true, true, 0, true, false, false);
                    foreach (Class classExport in listClass)
                    {
                        try
                        {
                            List<Student> listStudent = studentBO.GetListStudentByClass(classExport.IdClass).Where(s => s.IsExpelled == false && s.IsGraduated == false && s.IsReserved == false).ToList(); ;
                            if (listStudent == null)
                            {
                                continue;
                            }
                            else
                            {
                                Excel.Worksheet workSheet = workbook.Worksheets[1];
                                workSheet.Copy(Type.Missing, workbook.Worksheets[workbook.Sheets.Count]); // copy
                                workbook.Worksheets[workbook.Worksheets.Count].Name = classExport.ClassName;      // rename
                                workSheet = workbook.Worksheets[workbook.Worksheets.Count];
                                // Set field conduct event
                                string facultyStr = "";
                                if (classExport.ClassName.Contains("PFIEV"))
                                {
                                    facultyStr = "        Chương trình Đào tạo kỹ sư chất lượng cao Việt-Pháp (PFIEV)";
                                }
                                else if (classExport.ClassName.Contains("ECE") || classExport.ClassName.Contains("ES"))
                                {
                                    facultyStr = "        Trung tâm suất sắc";
                                }
                                workSheet.get_Range("A5", "G5").Value2 = "Lớp sinh hoạt: " + classExport.ClassName + (facultyStr.Equals("")?"          Khoa: " + faculty.FacultyName: facultyStr);
                                workSheet.get_Range("A6", "G6").Value2 = "Học kỳ: " + currentEvent.Semester.SemesterYear.SemesterYearName + "      Năm học: " + currentEvent.Semester.Year.YearName;
                                int row = 11;
                                for (int i = listStudent.Count() - 1; i >= 0; i--)
                                {
                                    // Get total point of student
                                    ConductResult conductResult = listStudent[i].ConductResults.SingleOrDefault(c => c.IdConductEvent == currentEvent.IdConductEvent && c.IdConductSchedule == lastConductSchedule.IdConductSchedule);
                                    int total = 0;
                                    if (conductResult != null)
                                    {
                                        string[] arrPairValue = conductResult.PartialPoint.Split(';');
                                        foreach (string pair in arrPairValue)
                                        {
                                            try
                                            {
                                                total += Int32.Parse(pair.Split(':')[1]);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                    }
                                    // Set field index
                                    workSheet.Rows[11].Insert();
                                    workSheet.get_Range("A11", "A11").Value2 = i + 1;
                                    // Set student number
                                    workSheet.get_Range("B11", "B11").Value2 = listStudent[i].StudentNumber;
                                    // Set field student name
                                    workSheet.get_Range("C11", "C11").Value2 = listStudent[i].LastNameStudent + " " + listStudent[i].FirstNameStudent;
                                    // Set class student
                                    workSheet.get_Range("D11", "D11").Value2 = listStudent[i].Class.ClassName;
                                    // Set field point
                                    workSheet.get_Range("E11", "E11").Value2 = total;
                                    // Set Grade Evaluated
                                    workSheet.get_Range("F11", "F11").Value2 = DataExtension.GetGradeEvaluated(total);
                                    row++;
                                }
                                workSheet.Rows[row].Delete();
                                row += 1;
                                // Set date sign
                                workSheet.get_Range("D" + row, "D" + row).Value2 = "Đà Nẵng, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                // Set name of lecturer
                                row += 3;
                                //Remove first row of content
                                workSheet.Rows[10].Delete();
                            }
                        }
                        catch
                        {
                            errorClass += classExport.ClassName + "; ";
                        }
                    }
                    String nameFile = "BangDiemRenLuyen_" + faculty.Acronym.Trim() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".xls";

                    if (System.IO.File.Exists(Server.MapPath("~/Files/Conduct/" + nameFile)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Files/Conduct/" + nameFile));
                    }
                    //Turn off alert delete sheet
                    application.DisplayAlerts = false;
                    workbook.Sheets[1].Delete();
                    application.DisplayAlerts = true;
                    workbook.SaveAs(Server.MapPath("~/Files/Conduct/" + nameFile));
                    workbook.Close();
                    Marshal.ReleaseComObject(workbook);
                    application.Quit();
                    Marshal.FinalReleaseComObject(application);

                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("content-disposition", "attachment;filename=" + nameFile);
                    Response.TransmitFile(Server.MapPath("~/Files/Conduct/" + nameFile));
                    Response.Flush();
                    Response.End();

                    System.IO.File.Delete(Server.MapPath("~/Files/Conduct/" + nameFile));
                    return Json(new
                    {
                        success = false,
                        responseText = "Export file excel thành công!"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        responseText = "Bạn không có quyền sử dụng chức năng này!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseText = "Chưa đăng nhập hệ thống!"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
