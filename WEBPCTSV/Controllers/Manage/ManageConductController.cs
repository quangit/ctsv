namespace WEBPCTSV.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using OfficeOpenXml;

    using WEBPCTSV.Helpers;
    using WEBPCTSV.Helpers.Common;
    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageConductController : Controller
    {
        readonly DSAContext dsaContext;

        public ManageConductController()
        {
            this.dsaContext = new DSAContext();
        }

        public ActionResult AddConductEvent()
        {
            SemesterBO semesterBO = new SemesterBO(this.dsaContext);
            ConductFormBO conductFormBO = new ConductFormBO(this.dsaContext);
            this.ViewBag.conductForm = conductFormBO.GetListConductForm();
            this.ViewBag.semesters = semesterBO.GetYearConduct();
            return this.View();
        }

        [HttpPost]
        public ActionResult AddConductEvent(FormCollection formCollection)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    try
                    {
                        int idSemester = int.Parse(formCollection["idSemester"]);
                        int idConductForm = int.Parse(formCollection["idConductForm"]);
                        ConductEventBO conductEventBO = new ConductEventBO(this.dsaContext);
                        try
                        {
                            int idConductEvent = conductEventBO.AddConductEvent(idSemester, idConductForm);
                            return
                                this.Json(
                                    new
                                        {
                                            success = "true",
                                            responseText = "Thêm dữ liệu thành công!",
                                            idConductEvent = idConductEvent
                                        },
                                    JsonRequestBehavior.DenyGet);
                        }
                        catch (MyException e)
                        {
                            return this.Json(
                                new { success = false, responseText = e.Message },
                                JsonRequestBehavior.DenyGet);
                        }
                        catch (Exception)
                        {
                            return
                                this.Json(
                                    new { success = "false", responseText = "Lỗi trong quá trình cập nhật dữ liệu!" },
                                    JsonRequestBehavior.DenyGet);
                        }
                    }
                    catch
                    {
                        return
                            this.Json(
                                new { success = "false", responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." },
                                JsonRequestBehavior.DenyGet);
                    }
                }
                else
                {
                    return
                        this.Json(
                            new { success = "false", responseText = "Bạn không có quyền thực hiện chức năng này!" },
                            JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return this.Json(
                    new { success = "false", responseText = "Chưa đăng nhập hệ thống!" },
                    JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult AddConductEventSchedule(FormCollection formCollection)
        {
            try
            {
                int idConductEvent = int.Parse(formCollection["idConductEvent"]);
                int idDecentralizationGroup = int.Parse(formCollection["idDecentralizationGroup"]);
                string beginDate = formCollection["beginDate"] + " " + formCollection["beginTime"];
                string endDate = formCollection["endDate"] + " " + formCollection["endTime"];
                DateTime begin = DateTime.Parse(beginDate);
                DateTime end = DateTime.Parse(endDate);
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(this.dsaContext);
                if (conductScheduleBO.AddSchedule(idConductEvent, idDecentralizationGroup, begin, end))
                {
                    return this.Json(
                        new { success = true, responseText = "Thêm lịch đánh giá thành công!" },
                        JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return this.Json(
                        new { success = false, responseText = "Thêm lịch đánh giá thất bại!" },
                        JsonRequestBehavior.DenyGet);
                }
            }
            catch (MyException e)
            {
                return this.Json(new { success = false, responseText = e.Message }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return this.Json(
                    new { success = false, responseText = "Lỗi trong quá trình cập nhật dữ liệu!" },
                    JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult AddConductForm()
        {
            this.ViewBag.groupEvaluate = this.dsaContext.ConductItemGroups.ToList();
            return this.View();
        }

        [HttpPost]
        public ActionResult AddConductForm(FormCollection formCollection)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    string error = "Xảy ra lỗi trong quá trình thêm mục đánh giá! Kiểm tra lại biểu mẫu xét rèn luyện.";
                    string success = "Thêm mới dữ liệu thành công!";
                    bool isSuccess = true;
                    int itemCount = int.Parse(formCollection["itemCount"]);
                    string name = formCollection["name"];
                    string note = formCollection["note"];
                    ConductFormBO conductFormBO = new ConductFormBO(this.dsaContext);
                    int idConductForm = conductFormBO.Add(name, note);
                    if (idConductForm != -1)
                    {
                        ConductItemsBO conductItemsBO = new ConductItemsBO(this.dsaContext);
                        for (int i = 1; i <= itemCount; i++)
                        {
                            try
                            {
                                string itemIndex = formCollection["Index" + i];
                                string itemName = formCollection["Title" + itemIndex];
                                int maxPoints = int.Parse(formCollection["Point" + itemIndex]);
                                int idConductItemGroup = int.Parse(formCollection["Group" + itemIndex]);
                                int idConductItems = conductItemsBO.Add(
                                    idConductForm,
                                    itemIndex,
                                    itemName,
                                    maxPoints,
                                    idConductItemGroup);
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
                            this.TempData["success"] = success;
                        }
                        else
                        {
                            this.TempData["error"] = error;
                        }
                    }
                    else
                    {
                        this.TempData["error"] = error;
                    }

                    return this.Redirect("/QuanLy/QuanLyRenLuyen/MauXetRenLuyen");
                }
                else
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult ConductClassList(int idClass, int? idSemester)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    SemesterBO semesterBO = new SemesterBO(this.dsaContext);
                    Semester semester;
                    if (idSemester == null)
                    {
                        semester = semesterBO.GetSemesterCurrent();
                    }
                    else
                    {
                        semester = semesterBO.GetSemesterById(idSemester.Value);
                    }

                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(this.dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(this.dsaContext);
                    StudentBO studentBO = new StudentBO(this.dsaContext);
                    ClassBO classBO = new ClassBO(this.dsaContext);
                    ConductEvent conductEvent = conductEventBO.GetConductEventBySemester(semester.IdSemester);
                    List<ConductSchedule> listSchedule =
                        conductScheduleBO.GetListScheduleByEvent(conductEvent.IdConductEvent);
                    ConductSchedule lastConductSchedule =
                        listSchedule.OrderByDescending(s => s.EndDateEvaluate).FirstOrDefault();
                    List<Student> listStudent = studentBO.GetListStudentByClass(idClass);
                    Class classEvaluate = classBO.GetClass(idClass);
                    if (classEvaluate == null)
                    {
                        this.ViewBag.error = "Không tìm thấy lớp đánh giá!";
                        return this.View();
                    }

                    this.ViewBag.listStudent = listStudent;
                    this.ViewBag.currentEvent = conductEvent;
                    this.ViewBag.className = classEvaluate.ClassName;
                    this.ViewBag.lastConductSchedule = lastConductSchedule;
                    return this.View();
                }
                else
                {
                    this.ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                    return this.View();
                }
            }
            else
            {
                this.ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                return this.View();
            }
        }

        public ActionResult ConductEvaluate(int idStudent, int idSemester)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    Semester semester = (new SemesterBO(this.dsaContext)).GetSemesterById(idSemester);
                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(this.dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(this.dsaContext);
                    ConductEvent conductEvent = conductEventBO.GetConductEventBySemester(idSemester);
                    List<ConductSchedule> conductSchedule =
                        conductScheduleBO.GetListScheduleByEvent(conductEvent.IdConductEvent);
                    ConductSchedule lastConductSchedule =
                        conductSchedule.OrderByDescending(s => s.EndDateEvaluate).FirstOrDefault();
                    StudentBO studentBO = new StudentBO(this.dsaContext);
                    Student student = studentBO.GetStudent(idStudent);
                    if (student == null)
                    {
                        this.ViewBag.error = "Không tìm thấy sinh viên!";
                        return this.View();
                    }

                    ConductResultBO conductResultBO = new ConductResultBO(this.dsaContext);
                    ConductResult conductResult = conductResultBO.GetConductResult(
                        student.IdStudent,
                        lastConductSchedule.IdConductSchedule);

                    // Not evaluated or saved
                    this.ViewBag.conductResult = conductResult;
                    this.ViewBag.student = student;
                    this.ViewBag.semester = semester;
                    List<ConductItem> itemsByForm =
                        (new ConductItemsBO(this.dsaContext)).GetListConductItems(conductEvent.IdConductForm);
                    this.ViewBag.itemsByForm = itemsByForm;
                    return this.View();
                }
                else
                {
                    this.ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                    return this.View();
                }
            }
            else
            {
                this.ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                return this.View();
            }
        }

        public ActionResult ConductEvent()
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    ConductEventBO conductEventBO = new ConductEventBO(this.dsaContext);
                    this.ViewBag.conductEvents = conductEventBO.GetListConductEvent();
                    return this.View();
                }
                else
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult ConductFaculty(int idFaculty, int? idSemester)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    SemesterBO semesterBO = new SemesterBO(this.dsaContext);
                    Semester semester;
                    if (idSemester == null)
                    {
                        semester = semesterBO.GetSemesterCurrent();
                    }
                    else
                    {
                        semester = semesterBO.GetSemesterById(idSemester.Value);
                    }

                    ConductScheduleBO conductScheduleBO = new ConductScheduleBO(this.dsaContext);
                    ConductEventBO conductEventBO = new ConductEventBO(this.dsaContext);
                    ConductEvent conductEvent = conductEventBO.GetConductEventBySemester(semester.IdSemester);
                    List<ConductSchedule> listSchedule =
                        conductScheduleBO.GetListScheduleByEvent(conductEvent.IdConductEvent);
                    ConductSchedule lastConductSchedule =
                        listSchedule.OrderByDescending(s => s.EndDateEvaluate).FirstOrDefault();
                    ClassBO classBO = new ClassBO(this.dsaContext);
                    List<Class> classes = new List<Class>();
                    FacultyBO facultyBO = new FacultyBO(this.dsaContext);
                    Faculty faculty = facultyBO.GetFaculty(idFaculty);
                    classes = classBO.GetClassesByIdFaculty(faculty.IdFaculty);
                    this.ViewBag.faculty = faculty;
                    this.ViewBag.classes = classes;
                    this.ViewBag.conductEvent = conductEvent;
                    this.ViewBag.lastConductSchedule = lastConductSchedule;
                    return this.View();
                }
                else
                {
                    this.ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                    return this.View();
                }
            }
            else
            {
                this.ViewBag.error = "Bạn không đủ quyền để truy cập chức năng này!";
                return this.View();
            }
        }

        public ActionResult ConductForm()
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    ConductFormBO conductFormBO = new ConductFormBO(this.dsaContext);
                    this.ViewBag.conductForms = conductFormBO.GetListConductForm();
                    return this.View();
                }
                else
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult ConductStudentSubmit(FormCollection collection)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];

            // Check permission show list
            if (accountSession != null && accountSession.Functions.IndexOf("ManageConduct") != -1)
            {
                int idSemester;
                int idStudent;
                try
                {
                    idSemester = int.Parse(collection["idSemester"]);
                    idStudent = int.Parse(collection["idStudent"]);
                }
                catch
                {
                    return
                        this.Json(
                            new
                                {
                                    success = false,
                                    responseText = "Cập nhật kết quả rèn luyện thất bại! Kiểm tra lại thông tin."
                                },
                            JsonRequestBehavior.DenyGet);
                }

                // Check schedule to evaluate
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(this.dsaContext);
                ConductEventBO conductEventBO = new ConductEventBO(this.dsaContext);
                ConductEvent conductEvent = conductEventBO.GetConductEventBySemester(idSemester);
                List<ConductSchedule> conductSchedule =
                    conductScheduleBO.GetListScheduleByEvent(conductEvent.IdConductEvent);
                ConductSchedule lastConductSchedule =
                    conductSchedule.OrderByDescending(s => s.EndDateEvaluate).FirstOrDefault();

                // Init partial point
                List<ConductItem> itemsByForm =
                    (new ConductItemsBO(this.dsaContext)).GetListConductItems(conductEvent.IdConductForm);
                string partialPoint = string.Empty;
                foreach (ConductItem item in itemsByForm)
                {
                    if (!StringExtension.IsNullOrWhiteSpace(collection["P" + item.IdConductItems])
                        && int.Parse(collection["P" + item.IdConductItems]) <= item.MaxPoints) partialPoint += item.IdConductItems + ":" + collection["P" + item.IdConductItems] + ";";
                }

                partialPoint = (!partialPoint.Equals(string.Empty))
                                   ? partialPoint.Substring(0, partialPoint.Length - 1)
                                   : string.Empty;

                // Check isExist student in ConductResult
                ConductResultBO conductResultBO = new ConductResultBO(this.dsaContext);
                ConductResult conductResult = conductResultBO.GetConductResult(
                    idStudent,
                    lastConductSchedule.IdConductSchedule);
                if (conductResult != null)
                {
                    bool isSuccess = conductResultBO.UpdateConductResult(
                        idStudent,
                        lastConductSchedule.IdConductSchedule,
                        conductEvent.IdConductEvent,
                        true,
                        false,
                        partialPoint);
                    if (isSuccess)
                    {
                        return this.Json(
                            new { success = true, responseText = "Lưu kết quả rèn luyện thành công!" },
                            JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return
                            this.Json(
                                new
                                    {
                                        success = false,
                                        responseText = "Cập nhật kết quả rèn luyện thất bại! Kiểm tra lại thông tin."
                                    },
                                JsonRequestBehavior.DenyGet);
                    }
                }
                else
                {
                    // Not evaluated
                    bool isSuccess = conductResultBO.AddConductResult(
                        idStudent,
                        lastConductSchedule.IdConductSchedule,
                        conductEvent.IdConductEvent,
                        true,
                        false,
                        partialPoint);
                    if (isSuccess)
                    {
                        return this.Json(
                            new { success = true, responseText = "Lưu kết quả rèn luyện thành công!" },
                            JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return
                            this.Json(
                                new
                                    {
                                        success = false,
                                        responseText = "Cập nhật kết quả rèn luyện thất bại! Kiểm tra lại thông tin."
                                    },
                                JsonRequestBehavior.DenyGet);
                    }
                }
            }
            else
            {
                return this.Json(
                    new { success = false, responseText = "Bạn không đủ quyền để truy cập chức năng này!" },
                    JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteConductEvent(FormCollection formCollection)
        {
            try
            {
                int idConductEvent = int.Parse(formCollection["idConductEvent"]);
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(this.dsaContext);
                if (conductScheduleBO.DeleteConductEvent(idConductEvent))
                {
                    return this.Json(
                        new { success = true, responseText = "Xóa đợt đánh giá rèn luyện thành công!" },
                        JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return this.Json(
                        new { success = false, responseText = "Xóa đợt đánh giá rèn luyện thất bại!" },
                        JsonRequestBehavior.DenyGet);
                }
            }
            catch (Exception)
            {
                return this.Json(
                    new { success = false, responseText = "Lỗi trong quá trình cập nhật dữ liệu!" },
                    JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteConductEventSchedule(FormCollection formCollection)
        {
            try
            {
                int idConductSchedule = int.Parse(formCollection["idConductSchedule"]);
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(this.dsaContext);
                if (conductScheduleBO.DeleteSchedule(idConductSchedule))
                {
                    return this.Json(
                        new { success = true, responseText = "Xóa lịch đánh giá thành công!" },
                        JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return this.Json(
                        new { success = false, responseText = "Xóa lịch đánh giá thất bại!" },
                        JsonRequestBehavior.DenyGet);
                }
            }
            catch (MyException e)
            {
                return this.Json(new { success = false, responseText = e.Message }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return this.Json(
                    new { success = false, responseText = "Lỗi trong quá trình xóa dữ liệu!" },
                    JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult DeleteConductForm(int id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    ConductFormBO conductFormBO = new ConductFormBO(this.dsaContext);
                    if (conductFormBO.DeleteConductForm(id))
                    {
                        this.TempData["success"] = "Xóa biểu mẫu đánh giá rèn luyện thành công!";
                    }
                    else
                    {
                        this.TempData["error"] = "Xảy ra lỗi trong quá trình xóa!";
                    }

                    return this.Redirect("/QuanLy/QuanLyRenLuyen/MauXetRenLuyen");
                }
                else
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult DeleteConductItem(FormCollection formCollection)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    int id = -1;
                    try
                    {
                        id = int.Parse(formCollection["id"]);
                    }
                    catch
                    {
                        return this.Json(
                            new { success = false, responseText = "Mã danh mục xóa không hợp lệ!" },
                            JsonRequestBehavior.DenyGet);
                    }

                    ConductItemsBO conductItemsBO = new ConductItemsBO(this.dsaContext);
                    if (conductItemsBO.DeleteItem(id))
                    {
                        return this.Json(
                            new { success = true, responseText = "Xóa mục đánh giá rèn luyện thành công!" },
                            JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return this.Json(
                            new { success = false, responseText = "Xảy ra lỗi trong quá trình xóa!" },
                            JsonRequestBehavior.DenyGet);
                    }
                }
                else
                {
                    return
                        this.Json(
                            new { success = false, responseText = "Bạn không có quyền thực hiện chức năng này!" },
                            JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return this.Json(
                    new { success = false, responseText = "Chưa đăng nhập hệ thống!" },
                    JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult Home()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ImportConductResult(FormCollection collection)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageConduct") != -1;
                if (!isGranted)
                {
                    return this.Content("false;Bạn không đủ quyền thực hiện chức năng này!");
                }

                try
                {
                    if (this.Request != null)
                    {
                        // Get data from form
                        HttpPostedFileBase file = this.Request.Files["UploadedFile"];
                        int idSemester = int.Parse(this.Request.Form["IdSemester"]);
                        ConductEventBO conductEventBO = new ConductEventBO(this.dsaContext);
                        ConductEvent conductEvent = conductEventBO.GetConductEventBySemester(idSemester);
                        ConductResultBO conductResultBO = new ConductResultBO(this.dsaContext);
                        ConductScheduleBO conductScheduleBO = new ConductScheduleBO(this.dsaContext);
                        ConductSchedule conductSchedule;

                        // Check conduct event information
                        if (conductEvent == null)
                        {
                            return
                                this.Content(
                                    "false;Không tìm thấy đợt đánh giá kết quả rèn luyện cho kỳ học này! Thêm đợt đánh giá rèn luyện trước khi import dữ liệu!");
                        }
                        else
                        {
                            conductSchedule =
                                conductScheduleBO.GetListScheduleByEvent(conductEvent.IdConductEvent)
                                    .OrderByDescending(s => s.EndDateEvaluate)
                                    .FirstOrDefault();
                            if (conductSchedule == null)
                            {
                                return
                                    this.Content(
                                        "false;Không tìm lịch đánh giá rèn luyện của Phòng Công tác sinh viên! Thêm lịch đánh giá của phòng!");
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
                                        using (var context = this.dsaContext.Database.BeginTransaction())
                                        {
                                            try
                                            {
                                                string errorStudent = string.Empty;
                                                string studentName = string.Empty;
                                                string studentID = string.Empty;
                                                string studentClass = string.Empty;
                                                int point = 0;
                                                for (int rowIterator = 6; rowIterator <= noOfRow; rowIterator++)
                                                {
                                                    try
                                                    {
                                                        // Get data from colunm excel
                                                        studentID = workSheet.Cells[rowIterator, 2].Value != null
                                                                        ? workSheet.Cells[rowIterator, 2].Value.ToString
                                                                              ().Trim()
                                                                        : string.Empty;
                                                        if (StringExtension.IsNullOrWhiteSpace(studentID)) continue;
                                                        StudentBO studentBO = new StudentBO(this.dsaContext);
                                                        Student student = studentBO.GetStudentByStudentNumber(studentID);

                                                        studentName = workSheet.Cells[rowIterator, 3].Value.ToString();
                                                        studentClass = workSheet.Cells[rowIterator, 4].Value.ToString();
                                                        point =
                                                            int.Parse(
                                                                workSheet.Cells[rowIterator, 5].Value.ToString().Trim());

                                                        bool isSuccess = true;
                                                        if (student != null)
                                                        {
                                                            ConductItem conductItem =
                                                                conductEvent.ConductForm.ConductItems.Where(
                                                                    i => i.ItemIndex.Length == Define.ItemType.Item)
                                                                    .FirstOrDefault();
                                                            string partialPoint = conductItem.IdConductItems + ":"
                                                                                  + point;
                                                            ConductResult conductResult =
                                                                conductResultBO.GetConductResult(
                                                                    student.IdStudent,
                                                                    conductSchedule.IdConductSchedule);
                                                            if (conductResult != null)
                                                            {
                                                                isSuccess =
                                                                    conductResultBO.UpdateConductResult(
                                                                        student.IdStudent,
                                                                        conductSchedule.IdConductSchedule,
                                                                        conductEvent.IdConductEvent,
                                                                        true,
                                                                        false,
                                                                        partialPoint);
                                                            }
                                                            else
                                                            {
                                                                isSuccess =
                                                                    conductResultBO.AddConductResult(
                                                                        student.IdStudent,
                                                                        conductSchedule.IdConductSchedule,
                                                                        conductEvent.IdConductEvent,
                                                                        true,
                                                                        false,
                                                                        partialPoint);
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
                                                return
                                                    this.Content(
                                                        "true;Nhập từ file excel thành công!"
                                                        + (string.IsNullOrEmpty(errorStudent)
                                                               ? string.Empty
                                                               : " Các sinh viên bị lỗi: " + errorStudent));
                                            }
                                            catch (Exception e)
                                            {
                                                context.Rollback();
                                                return
                                                    this.Content(
                                                        "false;Nhập từ file excel thất bại!"
                                                        + (e.Message != null ? e.Message : string.Empty));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return this.Content("false;Nhập từ file excel thất bại!");
                }
                catch
                {
                    // Return text to avoid auto download file json in old browser.
                    return this.Content("false;Nhập từ file excel thất bại!");
                }
            }
            else
            {
                return this.Content("false;Chưa đăng nhập hệ thống!");
            }
        }

        public ActionResult Index()
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    FacultyBO facultyBO = new FacultyBO(this.dsaContext);
                    SemesterBO semesterBO = new SemesterBO(this.dsaContext);
                    List<Faculty> faculties = facultyBO.GetListFaculty();
                    List<Semester> semesters = semesterBO.GetYearConduct();
                    this.ViewBag.faculties = faculties;
                    this.ViewBag.semesters = semesters;
                    return this.View();
                }
                else
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult UpdateConductEvent(int id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    ConductEventBO conductEventBO = new ConductEventBO(this.dsaContext);
                    DecentralizationGroupBo decentralizationGroupBo = new DecentralizationGroupBo();
                    SemesterBO semesterBO = new SemesterBO(this.dsaContext);
                    ConductFormBO conductFormBO = new ConductFormBO(this.dsaContext);
                    ConductEvent conductEvent = conductEventBO.Get(id);
                    if (conductEvent != null)
                    {
                        this.ViewBag.semesters = semesterBO.GetYearConduct();
                        this.ViewBag.groups = decentralizationGroupBo.getListGroup();
                        this.ViewBag.conductForms = conductFormBO.GetListConductForm();
                        this.ViewBag.conductEvent = conductEvent;
                        this.ViewBag.schedules = conductEvent.ConductSchedules.ToList();
                    }
                    else
                    {
                        this.ViewBag.error = "Không tìm thấy dữ liệu!";
                    }

                    return this.View();
                }
                else
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult UpdateConductEvent(FormCollection formCollection)
        {
            try
            {
                int idConductEvent = int.Parse(formCollection["idConductEvent"]);
                int idSemester = int.Parse(formCollection["idSemester"]);
                int idConductForm = int.Parse(formCollection["idConductForm"]);
                ConductEventBO conductEventBO = new ConductEventBO(this.dsaContext);
                if (conductEventBO.UpdateConductEvent(idConductEvent, idSemester, idConductForm))
                {
                    return
                        this.Json(
                            new
                                {
                                    success = true,
                                    responseText = "Cập nhật thông tin đợt đánh giá rèn luyện thành công!"
                                },
                            JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return
                        this.Json(
                            new
                                {
                                    success = false,
                                    responseText = "Cập nhật thông tin đợt đánh giá rèn luyện thất bại!"
                                },
                            JsonRequestBehavior.DenyGet);
                }
            }
            catch (MyException e)
            {
                return this.Json(new { success = false, responseText = e.Message }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return this.Json(
                    new { success = false, responseText = "Lỗi trong quá trình cập nhật dữ liệu!" },
                    JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateConductEventSchedule(FormCollection formCollection)
        {
            try
            {
                int idConductSchedule = int.Parse(formCollection["idConductSchedule"]);
                int idDecentralizationGroup = int.Parse(formCollection["idDecentralizationGroup"]);
                string beginDate = formCollection["beginDate"] + " " + formCollection["beginTime"];
                string endDate = formCollection["endDate"] + " " + formCollection["endTime"];
                DateTime begin = DateTime.Parse(beginDate);
                DateTime end = DateTime.Parse(endDate);
                ConductScheduleBO conductScheduleBO = new ConductScheduleBO(this.dsaContext);
                if (conductScheduleBO.UpdateSchedule(idConductSchedule, idDecentralizationGroup, begin, end))
                {
                    return this.Json(
                        new { success = true, responseText = "Cập nhật lịch đánh giá thành công!" },
                        JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return this.Json(
                        new { success = false, responseText = "Cập nhật lịch đánh giá thất bại!" },
                        JsonRequestBehavior.DenyGet);
                }
            }
            catch (MyException e)
            {
                return this.Json(new { success = false, responseText = e.Message }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return this.Json(
                    new { success = false, responseText = "Lỗi trong quá trình cập nhật dữ liệu!" },
                    JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult UpdateConductForm(int id)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission show list
                if (accountSession.Functions.IndexOf("ManageConduct") != -1)
                {
                    ConductFormBO conductFormBO = new ConductFormBO(this.dsaContext);
                    this.ViewBag.conductForm = conductFormBO.GetConductForm(id);
                    this.ViewBag.conductItemGroups = this.dsaContext.ConductItemGroups.ToList();
                    return this.View();
                }
                else
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult UpdateConductForm(FormCollection formCollection)
        {
            string error = "Xảy ra lỗi trong quá trình cập nhật đánh giá! Kiểm tra lại biểu mẫu xét rèn luyện.";
            string success = "Cập nhật dữ liệu thành công!";
            try
            {
                int idConductForm = int.Parse(formCollection["idConductForm"]);
                string name = formCollection["Name"];
                string note = formCollection["Note"];
                ConductFormBO conductFormBO = new ConductFormBO(this.dsaContext);
                if (conductFormBO.UpdateConductForm(idConductForm, name, note))
                {
                    bool isSuccess = true;
                    int itemCount = int.Parse(formCollection["itemCount"]);
                    ConductItemsBO conductItemsBO = new ConductItemsBO(this.dsaContext);
                    for (int i = 1; i <= itemCount; i++)
                    {
                        try
                        {
                            string itemIndex = formCollection["Index" + i];
                            string itemName = formCollection["Title" + itemIndex];
                            int maxPoints = int.Parse(formCollection["Point" + itemIndex]);
                            int idConductItemGroup = int.Parse(formCollection["Group" + itemIndex]);
                            int idConductItems = -1;
                            try
                            {
                                idConductItems = int.Parse(formCollection["ID" + itemIndex]);
                            }
                            catch
                            {
                            }

                            if (idConductItems != -1)
                            {
                                if (
                                    !conductItemsBO.UpdateConductItem(
                                        idConductItems,
                                        idConductForm,
                                        itemIndex,
                                        itemName,
                                        maxPoints,
                                        idConductItemGroup))
                                {
                                    isSuccess = false;
                                }
                            }
                            else
                            {
                                if (conductItemsBO.Add(
                                    idConductForm,
                                    itemIndex,
                                    itemName,
                                    maxPoints,
                                    idConductItemGroup) == -1)
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
                        return this.Json(new { success = true, responseText = success }, JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return this.Json(new { success = false, responseText = error }, JsonRequestBehavior.DenyGet);
                    }
                }
                else
                {
                    return this.Json(new { success = false, responseText = error }, JsonRequestBehavior.DenyGet);
                }
            }
            catch
            {
                return this.Json(new { success = false, responseText = error }, JsonRequestBehavior.DenyGet);
            }
        }
    }
}