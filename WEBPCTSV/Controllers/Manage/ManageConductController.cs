using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Helpers;
using WEBPCTSV.Helpers.Common;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class ManageConductController : Controller
    {
        DSAContext dsaContext;
        public ManageConductController()
        {
            dsaContext = new DSAContext();
        }
        public ActionResult Index()
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    FacultyBO facultyBO = new FacultyBO(dsaContext);
                    SemesterBO semesterBO = new SemesterBO(dsaContext);
                    List<Faculty> faculties = facultyBO.GetListFaculty();
                    List<Semester> semesters = semesterBO.GetYearConduct();
                    ViewBag.faculties = faculties;
                    ViewBag.semesters = semesters;
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        [HttpPost]
        public ActionResult ConductFaculty(int idFaculty, int? idSemester)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    SemesterBO semesterBO = new SemesterBO(dsaContext);
                    Semester semester;
                    if (idSemester == null)
                    {
                        semester = semesterBO.GetSemesterCurrent();
                    }
                    else
                    {
                        semester = semesterBO.GetSemesterById(idSemester.Value);
                    }
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    ConductEvent conductEvent = conductEventBO.GetConductEventBySemester(semester.IdSemester);
                    List<ConductSchedule> listSchedule = conductScheduleBO.GetListScheduleByEvent(conductEvent.IdConductEvent);
                    ConductSchedule lastConductSchedule = listSchedule.OrderByDescending(s => s.EndDateEvaluate).FirstOrDefault();
                    ClassBO classBO = new ClassBO(dsaContext);
                    List<Class> classes = new List<Class>();
                    FacultyBO facultyBO = new FacultyBO(dsaContext);
                    Faculty faculty = facultyBO.GetFaculty(idFaculty);
                    classes = (classBO.GetClassesByIdFaculty(faculty.IdFaculty));
                    ViewBag.faculty = faculty;
                    ViewBag.classes = classes;
                    ViewBag.conductEvent = conductEvent;
                    ViewBag.lastConductSchedule = lastConductSchedule;
                    return View();
                }
                else
                {
                    ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                    return View();
                }
            }
            else
            {
                ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult ConductClassList(int idClass, int? idSemester)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    SemesterBO semesterBO = new SemesterBO(dsaContext);
                    Semester semester;
                    if (idSemester == null)
                    {
                        semester = semesterBO.GetSemesterCurrent();
                    }
                    else
                    {
                        semester = semesterBO.GetSemesterById(idSemester.Value);
                    }
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    StudentBO studentBO = new StudentBO(dsaContext);
                    ClassBO classBO = new ClassBO(dsaContext);
                    ConductEvent conductEvent = conductEventBO.GetConductEventBySemester(semester.IdSemester);
                    List<ConductSchedule> listSchedule = conductScheduleBO.GetListScheduleByEvent(conductEvent.IdConductEvent);
                    ConductSchedule lastConductSchedule = listSchedule.OrderByDescending(s => s.EndDateEvaluate).FirstOrDefault();
                    List<Student> listStudent = studentBO.GetListStudentByClass(idClass);
                    Class classEvaluate = classBO.GetClass(idClass);
                    if (classEvaluate == null)
                    {
                        ViewBag.error = "Không tìm thấy lớp đánh giá!";
                        return View();
                    }
                    ViewBag.listStudent = listStudent;
                    ViewBag.currentEvent = conductEvent;
                    ViewBag.className = classEvaluate.ClassName;
                    ViewBag.lastConductSchedule = lastConductSchedule;
                    return View();
                }
                else
                {
                    ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                    return View();
                }
            }
            else
            {
                ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                return View();
            }
        }

        public ActionResult ConductEvaluate(int idStudent, int idSemester)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    Semester semester = (new SemesterBO(dsaContext)).GetSemesterById(idSemester);
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    ConductEvent conductEvent = conductEventBO.GetConductEventBySemester(idSemester);
                    List<ConductSchedule> conductSchedule = conductScheduleBO.GetListScheduleByEvent(conductEvent.IdConductEvent);
                    ConductSchedule lastConductSchedule = conductSchedule.OrderByDescending(s => s.EndDateEvaluate).FirstOrDefault();
                    StudentBO studentBO = new StudentBO(dsaContext);
                    Student student = studentBO.GetStudent(idStudent);
                    if (student == null)
                    {
                        ViewBag.error = "Không tìm thấy sinh viên!";
                        return View();
                    }
                    ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                    ConductResult conductResult = conductResultBO.GetConductResult(student.IdStudent, lastConductSchedule.IdConductSchedule);
                    // Not evaluated or saved
                    ViewBag.conductResult = conductResult;
                    ViewBag.student = student;
                    ViewBag.semester = semester;
                    List<ConductItem> itemsByForm = (new ConductItemsBO(dsaContext)).GetListConductItems(conductEvent.IdConductForm);
                    ViewBag.itemsByForm = itemsByForm;
                    return View();
                }
                else
                {
                    ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                    return View();
                }
            }
            else
            {
                ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult ConductStudentSubmit(FormCollection collection)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            // Check permission show list
            if (accountSession != null && accountSession.Functions.IndexOf("ManageConduct") != -1)
            {
                int idSemester;
                int idStudent;
                try
                {
                    idSemester = Int32.Parse(collection["idSemester"]);
                    idStudent = Int32.Parse(collection["idStudent"]);
                }
                catch
                {
                    return Json(new { success = false, responseText = "Cập nhật kết quả rèn luyện thất bại! Kiểm tra lại thông tin." }, JsonRequestBehavior.DenyGet);
                }
                // Check schedule to evaluate
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                ConductEvent conductEvent = conductEventBO.GetConductEventBySemester(idSemester);
                List<ConductSchedule> conductSchedule = conductScheduleBO.GetListScheduleByEvent(conductEvent.IdConductEvent);
                ConductSchedule lastConductSchedule = conductSchedule.OrderByDescending(s => s.EndDateEvaluate).FirstOrDefault();
                // Init partial point
                List<ConductItem> itemsByForm = (new ConductItemsBO(dsaContext)).GetListConductItems(conductEvent.IdConductForm);
                string partialPoint = "";
                foreach (ConductItem item in itemsByForm)
                {
                    if (!StringExtension.IsNullOrWhiteSpace(collection["P" + item.IdConductItems]) && Int32.Parse(collection["P" + item.IdConductItems]) <= item.MaxPoints)
                        partialPoint += item.IdConductItems + ":" + collection["P" + item.IdConductItems] + ";";
                }
                partialPoint = (!partialPoint.Equals("")) ? partialPoint.Substring(0, partialPoint.Length - 1) : "";

                // Check isExist student in ConductResult
                ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                ConductResult conductResult = conductResultBO.GetConductResult(idStudent, lastConductSchedule.IdConductSchedule);
                if (conductResult != null)
                {
                    bool isSuccess = conductResultBO.UpdateConductResult(idStudent, lastConductSchedule.IdConductSchedule, conductEvent.IdConductEvent, true, false, partialPoint);
                    if (isSuccess)
                    {
                        return Json(new { success = true, responseText = "Lưu kết quả rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Cập nhật kết quả rèn luyện thất bại! Kiểm tra lại thông tin." }, JsonRequestBehavior.DenyGet);
                    }
                }
                else
                {
                    //Not evaluated
                    bool isSuccess = conductResultBO.AddConductResult(idStudent, lastConductSchedule.IdConductSchedule, conductEvent.IdConductEvent, true, false, partialPoint);
                    if (isSuccess)
                    {
                        return Json(new { success = true, responseText = "Lưu kết quả rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Cập nhật kết quả rèn luyện thất bại! Kiểm tra lại thông tin." }, JsonRequestBehavior.DenyGet);
                    }
                }
            }
            else
            {
                return Json(new { success = false, responseText = "Bạn không đủ quyền để truy cập chức năng này!" }, JsonRequestBehavior.DenyGet);
            }
        }
        public ActionResult Home()
        {
            return View();
        }
        #region manage conduct event
        public ActionResult ConductEvent()
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    ViewBag.conductEvents = conductEventBO.GetListConductEvent();
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        public ActionResult AddConductEvent()
        {
            SemesterBO semesterBO = new SemesterBO(dsaContext);
            ConductFormBO conductFormBO = new ConductFormBO(dsaContext);
            ViewBag.conductForm = conductFormBO.GetListConductForm();
            ViewBag.semesters = semesterBO.GetYearConduct();
            return View();
        }
        [HttpPost]
        public ActionResult AddConductEvent(FormCollection formCollection)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    try
                    {
                        int idSemester = Int32.Parse(formCollection["idSemester"]);
                        int idConductForm = Int32.Parse(formCollection["idConductForm"]);
                        ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                        try
                        {
                            int idConductEvent = conductEventBO.AddConductEvent(idSemester, idConductForm);
                            return Json(new { success = "true", responseText = "Thêm dữ liệu thành công!", idConductEvent = idConductEvent }, JsonRequestBehavior.DenyGet);
                        }
                        catch (MyException e)
                        {
                            return Json(new { success = false, responseText = e.Message }, JsonRequestBehavior.DenyGet);
                        }
                        catch (Exception)
                        {
                            return Json(new { success = "false", responseText = "Lỗi trong quá trình cập nhật dữ liệu!" }, JsonRequestBehavior.DenyGet);
                        }
                    }
                    catch
                    {
                        return Json(new { success = "false", responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." }, JsonRequestBehavior.DenyGet);
                    }
                }
                else
                {
                    return Json(new { success = "false", responseText = "Bạn không có quyền thực hiện chức năng này!" }, JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return Json(new { success = "false", responseText = "Chưa đăng nhập hệ thống!" }, JsonRequestBehavior.DenyGet);
            }
        }
        public ActionResult UpdateConductEvent(int id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                    DecentralizationGroupBo decentralizationGroupBo = new DecentralizationGroupBo();
                    SemesterBO semesterBO = new SemesterBO(dsaContext);
                    ConductFormBO conductFormBO = new ConductFormBO(dsaContext);
                    ConductEvent conductEvent = conductEventBO.Get(id);
                    if (conductEvent != null)
                    {
                        ViewBag.semesters = semesterBO.GetYearConduct();
                        ViewBag.groups = decentralizationGroupBo.getListGroup();
                        ViewBag.conductForms = conductFormBO.GetListConductForm();
                        ViewBag.conductEvent = conductEvent;
                        ViewBag.schedules = conductEvent.ConductSchedules.ToList();
                    }
                    else
                    {
                        ViewBag.error = "Không tìm thấy dữ liệu!";
                    }
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult UpdateConductEvent(FormCollection formCollection)
        {
            try
            {
                int idConductEvent = Int32.Parse(formCollection["idConductEvent"]);
                int idSemester = Int32.Parse(formCollection["idSemester"]);
                int idConductForm = Int32.Parse(formCollection["idConductForm"]);
                ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                if (conductEventBO.UpdateConductEvent(idConductEvent, idSemester, idConductForm))
                {
                    return Json(new { success = true, responseText = "Cập nhật thông tin đợt đánh giá rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Cập nhật thông tin đợt đánh giá rèn luyện thất bại!" }, JsonRequestBehavior.DenyGet);
                }
            }
            catch (MyException e)
            {
                return Json(new { success = false, responseText = e.Message }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = "Lỗi trong quá trình cập nhật dữ liệu!" }, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteConductEvent(FormCollection formCollection)
        {
            try
            {
                int idConductEvent = Int32.Parse(formCollection["idConductEvent"]);
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                if (conductScheduleBO.DeleteConductEvent(idConductEvent))
                {
                    return Json(new { success = true, responseText = "Xóa đợt đánh giá rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Xóa đợt đánh giá rèn luyện thất bại!" }, JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = "Lỗi trong quá trình cập nhật dữ liệu!" }, JsonRequestBehavior.DenyGet);
            }
        }



        [HttpPost]
        public ActionResult UpdateConductEventSchedule(FormCollection formCollection)
        {
            try
            {
                int idConductSchedule = Int32.Parse(formCollection["idConductSchedule"]);
                int idDecentralizationGroup = Int32.Parse(formCollection["idDecentralizationGroup"]);
                string beginDate = formCollection["beginDate"] + " " + formCollection["beginTime"];
                string endDate = formCollection["endDate"] + " " + formCollection["endTime"];
                DateTime begin = DateTime.Parse(beginDate);
                DateTime end = DateTime.Parse(endDate);
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                if (conductScheduleBO.UpdateSchedule(idConductSchedule, idDecentralizationGroup, begin, end))
                {
                    return Json(new { success = true, responseText = "Cập nhật lịch đánh giá thành công!" }, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Cập nhật lịch đánh giá thất bại!" }, JsonRequestBehavior.DenyGet);
                }
            }
            catch (MyException e)
            {
                return Json(new { success = false, responseText = e.Message }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = "Lỗi trong quá trình cập nhật dữ liệu!" }, JsonRequestBehavior.DenyGet);
            }
        }
        [HttpPost]
        public ActionResult AddConductEventSchedule(FormCollection formCollection)
        {
            try
            {
                int idConductEvent = Int32.Parse(formCollection["idConductEvent"]);
                int idDecentralizationGroup = Int32.Parse(formCollection["idDecentralizationGroup"]);
                string beginDate = formCollection["beginDate"] + " " + formCollection["beginTime"];
                string endDate = formCollection["endDate"] + " " + formCollection["endTime"];
                DateTime begin = DateTime.Parse(beginDate);
                DateTime end = DateTime.Parse(endDate);
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                if (conductScheduleBO.AddSchedule(idConductEvent, idDecentralizationGroup, begin, end))
                {
                    return Json(new { success = true, responseText = "Thêm lịch đánh giá thành công!" }, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Thêm lịch đánh giá thất bại!" }, JsonRequestBehavior.DenyGet);
                }
            }
            catch (MyException e)
            {
                return Json(new { success = false, responseText = e.Message }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = "Lỗi trong quá trình cập nhật dữ liệu!" }, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteConductEventSchedule(FormCollection formCollection)
        {
            try
            {
                int idConductSchedule = Int32.Parse(formCollection["idConductSchedule"]);
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                if (conductScheduleBO.DeleteSchedule(idConductSchedule))
                {
                    return Json(new { success = true, responseText = "Xóa lịch đánh giá thành công!" }, JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Xóa lịch đánh giá thất bại!" }, JsonRequestBehavior.DenyGet);
                }
            }
            catch (MyException e)
            {
                return Json(new { success = false, responseText = e.Message }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = "Lỗi trong quá trình xóa dữ liệu!" }, JsonRequestBehavior.DenyGet);
            }
        }


        #endregion

        #region manage conduct form
        public ActionResult ConductForm()
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    ConductFormBO conductFormBO = new ConductFormBO(dsaContext);
                    ViewBag.conductForms = conductFormBO.GetListConductForm();
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        public ActionResult AddConductForm()
        {
            ViewBag.groupEvaluate = dsaContext.ConductItemGroups.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddConductForm(FormCollection formCollection)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    string error = "Xảy ra lỗi trong quá trình thêm mục đánh giá! Kiểm tra lại biểu mẫu xét rèn luyện.";
                    string success = "Thêm mới dữ liệu thành công!";
                    bool isSuccess = true;
                    int itemCount = Int32.Parse(formCollection["itemCount"]);
                    string name = formCollection["name"];
                    string note = formCollection["note"];
                    ConductFormBO conductFormBO = new ConductFormBO(dsaContext);
                    int idConductForm = conductFormBO.Add(name, note);
                    if (idConductForm != -1)
                    {
                        ConductItemsBO conductItemsBO = new ConductItemsBO(dsaContext);
                        for (int i = 1; i <= itemCount; i++)
                        {
                            try
                            {
                                string itemIndex = formCollection["Index" + i];
                                string itemName = formCollection["Title" + itemIndex];
                                int maxPoints = Int32.Parse(formCollection["Point" + itemIndex]);
                                int idConductItemGroup = Int32.Parse(formCollection["Group" + itemIndex]);
                                int idConductItems = conductItemsBO.Add(idConductForm, itemIndex, itemName, maxPoints, idConductItemGroup);
                                if (idConductItems == -1)
                                {
                                    isSuccess = false;
                                }
                            }
                            catch
                            {
                                isSuccess = false;
                            }
                        }
                        if (isSuccess)
                        {
                            TempData["success"] = success;
                        }
                        else
                        {
                            TempData["error"] = error;
                        }
                    }
                    else
                    {
                        TempData["error"] = error;
                    }
                    return Redirect("/QuanLy/QuanLyRenLuyen/MauXetRenLuyen");
                }
                else
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }

        public ActionResult UpdateConductForm(int id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    ConductFormBO conductFormBO = new ConductFormBO(dsaContext);
                    ViewBag.conductForm = conductFormBO.GetConductForm(id);
                    ViewBag.conductItemGroups = dsaContext.ConductItemGroups.ToList();
                    return View();
                }
                else
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult UpdateConductForm(FormCollection formCollection)
        {
            string error = "Xảy ra lỗi trong quá trình cập nhật đánh giá! Kiểm tra lại biểu mẫu xét rèn luyện.";
            string success = "Cập nhật dữ liệu thành công!";
            try
            {
                int idConductForm = Int32.Parse(formCollection["idConductForm"]);
                string name = formCollection["Name"];
                string note = formCollection["Note"];
                ConductFormBO conductFormBO = new ConductFormBO(dsaContext);
                if (conductFormBO.UpdateConductForm(idConductForm, name, note))
                {

                    bool isSuccess = true;
                    int itemCount = Int32.Parse(formCollection["itemCount"]);
                    ConductItemsBO conductItemsBO = new ConductItemsBO(dsaContext);
                    for (int i = 1; i <= itemCount; i++)
                    {
                        try
                        {
                            string itemIndex = formCollection["Index" + i];
                            string itemName = formCollection["Title" + itemIndex];
                            int maxPoints = Int32.Parse(formCollection["Point" + itemIndex]);
                            int idConductItemGroup = Int32.Parse(formCollection["Group" + itemIndex]);
                            int idConductItems = -1;
                            try
                            {
                                idConductItems = Int32.Parse(formCollection["ID" + itemIndex]);
                            }
                            catch { }
                            if (idConductItems != -1)
                            {
                                if (!conductItemsBO.UpdateConductItem(idConductItems, idConductForm, itemIndex, itemName, maxPoints, idConductItemGroup))
                                {
                                    isSuccess = false;
                                }
                            }
                            else
                            {
                                if (conductItemsBO.Add(idConductForm, itemIndex, itemName, maxPoints, idConductItemGroup) == -1)
                                {
                                    isSuccess = false;
                                }
                            }

                        }
                        catch
                        {
                            isSuccess = false;
                        }
                    }
                    if (isSuccess)
                    {
                        return Json(new { success = true, responseText = success }, JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = error }, JsonRequestBehavior.DenyGet);
                    }
                }
                else
                {
                    return Json(new { success = false, responseText = error }, JsonRequestBehavior.DenyGet);
                }
            }
            catch
            {
                return Json(new { success = false, responseText = error }, JsonRequestBehavior.DenyGet);
            }
        }
        public ActionResult DeleteConductForm(int id)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    ConductFormBO conductFormBO = new ConductFormBO(dsaContext);
                    if (conductFormBO.DeleteConductForm(id))
                    {
                        TempData["success"] = "Xóa biểu mẫu đánh giá rèn luyện thành công!";
                    }
                    else
                    {
                        TempData["error"] = "Xảy ra lỗi trong quá trình xóa!";
                    }
                    return Redirect("/QuanLy/QuanLyRenLuyen/MauXetRenLuyen");
                }
                else
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        [HttpPost]
        public ActionResult DeleteConductItem(FormCollection formCollection)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    int id = -1;
                    try
                    {
                        id = Int32.Parse(formCollection["id"]);
                    }
                    catch
                    {
                        return Json(new { success = false, responseText = "Mã danh mục xóa không hợp lệ!" }, JsonRequestBehavior.DenyGet);
                    }
                    ConductItemsBO conductItemsBO = new ConductItemsBO(dsaContext);
                    if (conductItemsBO.DeleteItem(id))
                    {
                        return Json(new { success = true, responseText = "Xóa mục đánh giá rèn luyện thành công!" }, JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Xảy ra lỗi trong quá trình xóa!" }, JsonRequestBehavior.DenyGet);
                    }
                }
                else
                {
                    return Json(new { success = false, responseText = "Bạn không có quyền thực hiện chức năng này!" }, JsonRequestBehavior.DenyGet);

                }
            }
            else
            {
                return Json(new { success = false, responseText = "Chưa đăng nhập hệ thống!" }, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion

        #region Import - Export File
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ImportConductResult(FormCollection collection)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageConduct") != -1);
                if (!isGranted)
                {
                    return Content("false;Bạn không đủ quyền thực hiện chức năng này!");
                }
                try
                {
                    if (Request != null)
                    {
                        // Get data from form
                        HttpPostedFileBase file = Request.Files["UploadedFile"];
                        int idSemester = Int32.Parse(Request.Form["IdSemester"]);
                        ConductEventBO conductEventBO = new ConductEventBO(dsaContext);
                        ConductEvent conductEvent = conductEventBO.GetConductEventBySemester(idSemester);
                        ConductResultBO conductResultBO = new ConductResultBO(dsaContext);
                        ConductScheduleBO conductScheduleBO = new ConductScheduleBO(dsaContext);
                        ConductSchedule conductSchedule;

                        //Check conduct event information
                        if (conductEvent == null)
                        {
                            return Content("false;Không tìm thấy đợt đánh giá kết quả rèn luyện cho kỳ học này! Thêm đợt đánh giá rèn luyện trước khi import dữ liệu!");
                        }
                        else
                        {
                            conductSchedule = conductScheduleBO.GetListScheduleByEvent(conductEvent.IdConductEvent).OrderByDescending(s => s.EndDateEvaluate).FirstOrDefault();
                            if (conductSchedule == null)
                            {
                                return Content("false;Không tìm lịch đánh giá rèn luyện của Phòng Công tác sinh viên! Thêm lịch đánh giá của phòng!");
                            }
                            else
                            {
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
                                                string studentName = "";
                                                string studentID = "";
                                                string studentClass = "";
                                                int point = 0;
                                                for (int rowIterator = 6; rowIterator <= noOfRow; rowIterator++)
                                                {
                                                    try
                                                    {
                                                        // Get data from colunm excel
                                                        studentID = workSheet.Cells[rowIterator, 2].Value != null ? workSheet.Cells[rowIterator, 2].Value.ToString().Trim() : "";
                                                        if (StringExtension.IsNullOrWhiteSpace(studentID)) continue;
                                                        StudentBO studentBO = new StudentBO(dsaContext);
                                                        Student student = studentBO.GetStudentByStudentNumber(studentID);

                                                        studentName = workSheet.Cells[rowIterator, 3].Value.ToString();
                                                        studentClass = workSheet.Cells[rowIterator, 4].Value.ToString();
                                                        point = Int32.Parse(workSheet.Cells[rowIterator, 5].Value.ToString().Trim());

                                                        bool isSuccess = true;
                                                        if (student != null)
                                                        {
                                                            ConductItem conductItem = conductEvent.ConductForm.ConductItems.Where(i => i.ItemIndex.Length == Define.ItemType.Item).FirstOrDefault();
                                                            string partialPoint = conductItem.IdConductItems + ":" + point;
                                                            ConductResult conductResult = conductResultBO.GetConductResult(student.IdStudent, conductSchedule.IdConductSchedule);
                                                            if (conductResult != null)
                                                            {
                                                                isSuccess = conductResultBO.UpdateConductResult(student.IdStudent, conductSchedule.IdConductSchedule, conductEvent.IdConductEvent, true, false, partialPoint);
                                                            }
                                                            else
                                                            {
                                                                isSuccess = conductResultBO.AddConductResult(student.IdStudent, conductSchedule.IdConductSchedule, conductEvent.IdConductEvent, true, false, partialPoint);
                                                            }
                                                            if (!isSuccess)
                                                            {
                                                                errorStudent += studentName + "(" + studentID + "), ";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            errorStudent += studentName + "(" + studentID + "), ";
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        errorStudent += studentName + "(" + studentID + "), ";
                                                    }
                                                }
                                                context.Commit();
                                                return Content("true;Nhập từ file excel thành công!" + (string.IsNullOrEmpty(errorStudent) ? "" : " Các sinh viên bị lỗi: " + errorStudent));
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
                return Content("false;Chưa đăng nhập hệ thống!");
            }
        }
        #endregion
    }
}
