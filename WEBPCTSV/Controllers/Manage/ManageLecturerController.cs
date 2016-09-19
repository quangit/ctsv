namespace WEBPCTSV.Controllers.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.Office.Interop.Excel;

    using OfficeOpenXml;

    using WEBPCTSV.Helpers.Common;
    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageLecturerController : Controller
    {
        readonly DSAContext dsaContext;

        readonly LecturerBO lecturerBO;

        public ManageLecturerController()
        {
            this.dsaContext = new DSAContext();
            this.lecturerBO = new LecturerBO(this.dsaContext);
        }

        [HttpPost]
        public ActionResult AddClassOwnerAjax(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageLecturer") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                try
                {
                    int idLecturer = int.Parse(col["idLecturer"]);
                    int idSemester = int.Parse(col["idSemester"]);
                    int idClass = int.Parse(col["idClass"]);
                    string endDate = col["endDate"];
                    string scheduleFirst = col["scheduleFirst"];
                    string scheduleSecond = col["scheduleSecond"];
                    string roomFirst = col["roomFirst"];
                    string roomSecond = col["roomSecond"];

                    LecturerClassBO lecturerClassBO = new LecturerClassBO(this.dsaContext);
                    bool isSuccess = lecturerClassBO.AddClassOwner(
                        idLecturer,
                        idSemester,
                        idClass,
                        endDate,
                        scheduleFirst,
                        roomFirst,
                        scheduleSecond,
                        roomSecond);
                    if (isSuccess)
                    {
                        return
                            this.Json(
                                new
                                    {
                                        success = true,
                                        idLecturer = idLecturer,
                                        responseText = "Thêm lớp chủ nhiệm thành công!"
                                    },
                                JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return
                            this.Json(
                                new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." },
                                JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return this.Json(
                        new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." },
                        JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult AddLecturer()
        {
            this.ViewBag.faculty = this.dsaContext.Faculties.ToList();
            return this.View();
        }

        [HttpPost]
        public ActionResult AddLecturer(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageLecturer") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                try
                {
                    string lecturerNumber = col["LecturerNumber"];
                    string lastName = col["LastName"];
                    string firstName = col["FirstName"];
                    string type = col["Type"];
                    string degree = col["Degree"];
                    string academicTitle = col["AcademicTitle"];
                    string position = col["Position"];
                    int idFaculty = int.Parse(col["Faculty"]);
                    string email = col["Email"];
                    string phoneNumber = col["PhoneNumber"];
                    string address = col["Address"];

                    if (string.IsNullOrWhiteSpace(lecturerNumber) || string.IsNullOrWhiteSpace(lastName)
                        || string.IsNullOrWhiteSpace(firstName))
                    {
                        return
                            this.Json(
                                new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." },
                                JsonRequestBehavior.AllowGet);
                    }

                    int idLecturer = this.lecturerBO.AddLecturer(
                        lecturerNumber,
                        type,
                        firstName,
                        lastName,
                        degree,
                        academicTitle,
                        position,
                        idFaculty,
                        email,
                        phoneNumber,
                        address);
                    if (idLecturer == -1)
                    {
                        return
                            this.Json(
                                new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." },
                                JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return
                            this.Json(
                                new
                                    {
                                        success = true,
                                        idLecturer = idLecturer,
                                        responseText = "Thêm giảng viên thành công!"
                                    },
                                JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return this.Json(
                        new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." },
                        JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult DeleteClassOwnerAjax(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageLecturer") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                try
                {
                    int idLecturerClass = int.Parse(col["idLecturerClass"]);
                    int idLecturer = int.Parse(col["idLecturer"]);
                    LecturerClassBO lecturerClassBO = new LecturerClassBO(this.dsaContext);
                    bool isSuccess = lecturerClassBO.RemoveClassOwner(idLecturerClass, idLecturer);
                    if (isSuccess)
                    {
                        return
                            this.Json(
                                new
                                    {
                                        success = true,
                                        idLecturer = idLecturer,
                                        responseText = "Xóa lớp chủ nhiệm thành công!"
                                    },
                                JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return
                            this.Json(
                                new
                                    {
                                        success = false,
                                        responseText = "Xóa lớp chủ nhiệm thất bại! Kiểm tra lại thông tin."
                                    },
                                JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return
                        this.Json(
                            new
                                {
                                    success = false,
                                    responseText = "Xóa lớp chủ nhiệm thất bại! Kiểm tra lại thông tin."
                                },
                            JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult DeleteLecturerAjax(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageLecturer") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                try
                {
                    int idLecturer = int.Parse(col["IdLecturer"]);
                    bool isSuccess = this.lecturerBO.RemoveLecturer(idLecturer);
                    if (isSuccess)
                    {
                        return this.Json(
                            new { success = true, responseText = "Xóa giảng viên thành công!" },
                            JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return
                            this.Json(
                                new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin!" },
                                JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return this.Json(
                        new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin!" },
                        JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult DetailClassOwnerAjax(FormCollection col)
        {
            try
            {
                int idLecturerClass = int.Parse(col["idLecturerClass"]);
                LecturerClassBO lecturerClassBO = new LecturerClassBO(this.dsaContext);
                var jsonObject = lecturerClassBO.GetDetailClassOwner(idLecturerClass);
                if (jsonObject != null)
                {
                    return
                        this.Json(
                            new
                                {
                                    success = true,
                                    lecturerClass = jsonObject,
                                    responseText = "Lấy thông tin thành công!"
                                },
                            JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return this.Json(
                        new { success = false, responseText = "Không tìm thấy thông tin!" },
                        JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return this.Json(
                    new { success = false, responseText = "Không tìm thấy thông tin!" },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExportExcel(int idFaculty, int idSemester)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission
                if (accountSession.Functions.IndexOf("ManageLecturer") != -1)
                {
                    FacultyBO facultyBO = new FacultyBO(this.dsaContext);
                    SemesterBO semesterBO = new SemesterBO(this.dsaContext);
                    LecturerBO lecturerBO = new LecturerBO(this.dsaContext);
                    Semester semester = semesterBO.GetSemesterById(idSemester);
                    Faculty faculty = facultyBO.GetFaculty(idFaculty);
                    List<Lecturer> listLecturer = lecturerBO.GetListLecturerByFaculty(idFaculty);

                    if (semester == null)
                    {
                        return this.Json(
                            new { success = false, responseText = "Không tìm kỳ cần thống kê đánh giá!" },
                            JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Application application = new Microsoft.Office.Interop.Excel.Application();
                        Workbook workbook;
                        if (semester.SemesterYear.IdSemesterYear.Equals("1"))
                        {
                            // Open file document template
                            workbook =
                                application.Workbooks.Open(
                                    this.Server.MapPath("~/Files/Lecturer/ThongKeGiangVien_HKI_yyyy_Khoa_ddMMyyyy.xls"),
                                    0,
                                    false,
                                    5,
                                    string.Empty,
                                    string.Empty,
                                    false,
                                    Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                    string.Empty,
                                    true,
                                    true,
                                    0,
                                    true,
                                    false,
                                    false);
                        }
                        else
                        {
                            // Open file document template
                            workbook =
                                application.Workbooks.Open(
                                    this.Server.MapPath("~/Files/Lecturer/ThongKeGiangVien_HKII_yyyy_Khoa_ddMMyyyy.xls"),
                                    0,
                                    false,
                                    5,
                                    string.Empty,
                                    string.Empty,
                                    false,
                                    Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                    string.Empty,
                                    true,
                                    true,
                                    0,
                                    true,
                                    false,
                                    false);
                        }

                        // Get sheet
                        Worksheet workSheet = workbook.Worksheets[1];

                        // Set values for sheet name
                        workSheet.Name = faculty.Acronym.Trim();

                        // Set field of report
                        workSheet.get_Range("A5", "G5").Value2 = "Khoa: " + faculty.FacultyName + "   Học kỳ: "
                                                                 + semester.SemesterYear.SemesterYearName
                                                                 + "    Năm học: " + semester.Year.YearName;
                        int row = 9;
                        for (int i = listLecturer.Count() - 1; i >= 0; i--)
                        {
                            try
                            {
                                List<LecturerClass> listLecturerClass =
                                    listLecturer[i].LecturerClasses.Where(c => c.IdSemester == idSemester).ToList();
                                foreach (LecturerClass lecClass in listLecturerClass)
                                {
                                    try
                                    {
                                        row++;

                                        // Set field index
                                        workSheet.Rows[9].Insert();
                                        workSheet.get_Range("A9", "A9").Value2 = i + 1;

                                        // Set field name
                                        workSheet.get_Range("B9", "B9").Value2 = listLecturer[i].LastName + " "
                                                                                 + listLecturer[i].FirstName;
                                        workSheet.get_Range("C9", "C9").Value2 = lecClass.Class.ClassName;
                                        List<LecturerClassDocument> listDoc =
                                            this.dsaContext.LecturerClassDocuments.Where(
                                                l => l.IdLecturerClass == lecClass.IdLecturerClass).ToList();
                                        workSheet.get_Range("D9", "D9").Value2 = listDoc[0].IsApproved ? "+" : "-";
                                        workSheet.get_Range("E9", "E9").Value2 = listDoc[1].IsApproved ? "+" : "-";
                                        workSheet.get_Range("F9", "F9").Value2 = listDoc[2].IsApproved ? "+" : "-";
                                        workSheet.get_Range("G9", "G9").Value2 = listDoc[3].IsApproved ? "+" : "-";
                                        workSheet.get_Range("H9", "H9").Value2 = listDoc[4].IsApproved ? "+" : "-";
                                        if (idSemester == 1)
                                        {
                                            workSheet.get_Range("I9", "I9").Value2 = listDoc[5].IsApproved ? "+" : "-";
                                        }
                                    }
                                    catch
                                    {
                                        row--;
                                        workSheet.Rows[9].Delete();
                                    }
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        row += 2;

                        // Set date sign
                        workSheet.get_Range("D" + row, "D" + row).Value2 = "Đà Nẵng, ngày " + DateTime.Now.Day
                                                                           + " tháng " + DateTime.Now.Month + " năm "
                                                                           + DateTime.Now.Year;

                        // Set name of lecturer
                        workSheet.Rows[8].Delete();

                        string nameFile = "ThongKeGiangVien_HK" + semester.SemesterYear.SemesterYearName + "_"
                                          + semester.Year.YearName + "_" + faculty.Acronym.Trim() + "_"
                                          + DateTime.Now.ToString("ddMMyyyy") + ".xls";

                        if (System.IO.File.Exists(this.Server.MapPath("~/Files/Lecturer/" + nameFile)))
                        {
                            System.IO.File.Delete(this.Server.MapPath("~/Files/Lecturer/" + nameFile));
                        }

                        workbook.SaveAs(this.Server.MapPath("~/Files/Lecturer/" + nameFile));
                        workbook.Close();
                        Marshal.ReleaseComObject(workbook);
                        application.Quit();
                        Marshal.FinalReleaseComObject(application);

                        this.Response.ContentType = "application/octet-stream";
                        this.Response.AppendHeader("content-disposition", "attachment;filename=" + nameFile);
                        this.Response.TransmitFile(this.Server.MapPath("~/Files/Lecturer/" + nameFile));
                        this.Response.Flush();
                        this.Response.End();

                        System.IO.File.Delete(this.Server.MapPath("~/Files/Lecturer/" + nameFile));

                        return this.Json(
                            new { success = false, responseText = "Export file excel thành công!" },
                            JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return this.Json(
                        new { success = false, responseText = "Bạn không có quyền sử dụng chức năng này!" },
                        JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return this.Json(
                    new { success = false, responseText = "Chưa đăng nhập hệ thống!" },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetLecturerDocumentSemesterList(FormCollection col)
        {
            try
            {
                int idSemester = int.Parse(col["idSemester"]);
                LecturerDocumentSemesterBO lecturerDocumentSemesterBO = new LecturerDocumentSemesterBO(this.dsaContext);
                var listDocument = lecturerDocumentSemesterBO.GetLecturerDocumentListBySemester(idSemester);
                if (listDocument != null)
                {
                    return this.Json(new { success = true, listDocument = listDocument }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return this.Json(
                    new { success = false, responseText = "Không tìm thấy thông tin!" },
                    JsonRequestBehavior.AllowGet);
            }
        }

        // Import list of lecturer from file excel
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ImportExcel(FormCollection formCollection)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageLecturer") != -1;
                if (!isGranted)
                {
                    return this.Content("false;Bạn không đủ quyền thực hiện chức năng này!");
                }

                string classOwner = string.Empty;
                string fullName = string.Empty;
                string degree = string.Empty;
                string phoneNumber = string.Empty;
                string email = string.Empty;
                string address = string.Empty;
                string type = string.Empty;
                string academicTitle = string.Empty;
                string position = string.Empty;
                try
                {
                    if (this.Request != null)
                    {
                        // Get data from form
                        int idFaculty = int.Parse(this.Request.Form["IdFaculty"]);
                        int idSemester = int.Parse(this.Request.Form["IdSemester"]);
                        HttpPostedFileBase file = this.Request.Files["UploadedFile"];
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
                                        string errorClass = string.Empty;
                                        for (int rowIterator = 4; rowIterator <= noOfRow; rowIterator++)
                                        {
                                            try
                                            {
                                                // Get data from colunm excel
                                                classOwner = workSheet.Cells[rowIterator, 2].Value.ToString();
                                                fullName = workSheet.Cells[rowIterator, 3].Value.ToString();
                                                academicTitle = workSheet.Cells[rowIterator, 4].Value != null
                                                                    ? workSheet.Cells[rowIterator, 4].Value.ToString()
                                                                    : string.Empty;
                                                degree = workSheet.Cells[rowIterator, 5].Value != null
                                                             ? workSheet.Cells[rowIterator, 5].Value.ToString()
                                                             : string.Empty;
                                                type = workSheet.Cells[rowIterator, 6].Value != null
                                                           ? workSheet.Cells[rowIterator, 6].Value.ToString()
                                                           : string.Empty;
                                                position = workSheet.Cells[rowIterator, 7].Value != null
                                                               ? workSheet.Cells[rowIterator, 7].Value.ToString()
                                                               : string.Empty;
                                                phoneNumber = workSheet.Cells[rowIterator, 8].Value != null
                                                                  ? workSheet.Cells[rowIterator, 8].Value.ToString()
                                                                  : string.Empty;
                                                email = workSheet.Cells[rowIterator, 9].Value != null
                                                            ? workSheet.Cells[rowIterator, 9].Value.ToString()
                                                            : string.Empty;
                                                address = workSheet.Cells[rowIterator, 10].Value != null
                                                              ? workSheet.Cells[rowIterator, 10].Value.ToString()
                                                              : string.Empty;

                                                // Generate username of lecturer by faculty
                                                string[] arrName = StringExtension.GetPartOfFullName(fullName);
                                                string firstName = arrName[1];
                                                string lastName = arrName[0];
                                                Faculty faculty = this.dsaContext.Faculties.Find(idFaculty);
                                                string lecturerNumber = faculty.NumberFaculty
                                                                        + StringExtension.GenerateUsernameByName(
                                                                            firstName,
                                                                            lastName);

                                                // Check lecturer isExist
                                                var lecturer =
                                                    this.dsaContext.Lecturers.SingleOrDefault(
                                                        lec =>
                                                        lec.LecturerNumber.Equals(lecturerNumber)
                                                        || (!lec.PhoneNumber.Equals(string.Empty)
                                                            && lec.PhoneNumber.Equals(phoneNumber)));
                                                int idLecturer;
                                                if (lecturer != null)
                                                {
                                                    this.lecturerBO.UpdateLecturer(
                                                        lecturer.IdLecturer,
                                                        lecturerNumber,
                                                        lastName,
                                                        firstName,
                                                        degree,
                                                        type,
                                                        academicTitle,
                                                        position,
                                                        idFaculty,
                                                        email,
                                                        phoneNumber,
                                                        address);
                                                    idLecturer = lecturer.IdLecturer;
                                                }
                                                else
                                                {
                                                    // Lecturer not exist
                                                    idLecturer = this.lecturerBO.AddLecturer(
                                                        lecturerNumber,
                                                        type,
                                                        arrName[1],
                                                        arrName[0],
                                                        degree,
                                                        academicTitle,
                                                        position,
                                                        idFaculty,
                                                        email,
                                                        phoneNumber,
                                                        address);
                                                }

                                                // Insert class owner
                                                Class classExist =
                                                    this.dsaContext.Classes.SingleOrDefault(
                                                        classQuery => classQuery.ClassName.Equals(classOwner));
                                                Semester semester =
                                                    this.dsaContext.Semesters.SingleOrDefault(
                                                        se => se.IdSemester.Equals(idSemester));
                                                if (classExist != null)
                                                {
                                                    // Insert lecturer class owner
                                                    string room = workSheet.Cells[rowIterator, 14].Value != null
                                                                      ? workSheet.Cells[rowIterator, 14].Value.ToString(
                                                                          )
                                                                      : string.Empty;
                                                    room += " Tiết "
                                                            + (workSheet.Cells[rowIterator, 11].Value != null
                                                                   ? workSheet.Cells[rowIterator, 11].Value.ToString()
                                                                   : string.Empty);
                                                    string scheduleFirst = workSheet.Cells[rowIterator, 12].Value
                                                                           != null
                                                                               ? workSheet.Cells[rowIterator, 12].Value
                                                                                     .ToString()
                                                                               : null;
                                                    string scheduleSecond = workSheet.Cells[rowIterator, 13].Value
                                                                            != null
                                                                                ? workSheet.Cells[rowIterator, 13].Value
                                                                                      .ToString()
                                                                                : null;
                                                    LecturerClass lecClass = new LecturerClass(
                                                        idLecturer,
                                                        idSemester,
                                                        classExist.IdClass,
                                                        semester.EndTime.ToString(),
                                                        scheduleFirst,
                                                        room,
                                                        scheduleSecond,
                                                        room);
                                                    this.dsaContext.LecturerClasses.Add(lecClass);
                                                    this.dsaContext.SaveChanges();

                                                    // Insert document to require
                                                    var docs =
                                                        this.dsaContext.LecturerDocumentSemesters.Where(
                                                            lecDoc => lecDoc.IdSemester == idSemester).ToList();
                                                    foreach (LecturerDocumentSemester doc in docs)
                                                    {
                                                        LecturerClassDocument lecClassDoc;
                                                        lecClassDoc = new LecturerClassDocument(
                                                            lecClass.IdLecturerClass,
                                                            doc.IdDocumentSemester,
                                                            false);
                                                        this.dsaContext.LecturerClassDocuments.Add(lecClassDoc);
                                                    }

                                                    this.dsaContext.SaveChanges();
                                                }
                                                else
                                                {
                                                    errorClass += classOwner + " ";
                                                }
                                            }
                                            catch
                                            {
                                            }
                                        }

                                        context.Commit();
                                        return
                                            this.Content(
                                                "true;Nhập từ file excel thành công!"
                                                + (string.IsNullOrEmpty(errorClass)
                                                       ? string.Empty
                                                       : " Các lớp bị lỗi: " + errorClass));
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

                    return
                        this.Content(
                            "false;Nhập từ file excel thất bại!"
                            + (!string.IsNullOrEmpty(fullName) ? " Lỗi cập nhật giảng viên " + fullName : string.Empty));
                }
                catch
                {
                    // Return text to avoid auto download file json in old browser.
                    return
                        this.Content(
                            "false;Nhập từ file excel thất bại!"
                            + (!string.IsNullOrEmpty(fullName) ? " Lỗi cập nhật giảng viên " + fullName : string.Empty));
                }
            }
            else
            {
                return this.Content("false;Chưa đăng nhập hệ thống!");
            }
        }

        public ActionResult ListLecturer(int? page)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageLecturer") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                this.ViewBag.faculties = this.dsaContext.Faculties.ToList();
                this.ViewBag.semesters = this.dsaContext.Semesters.OrderByDescending(s => s.EndTime).ToList();
                int pageview = page ?? 1;
                return this.View(this.lecturerBO.GetListLecturerAll());
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult UpdateClassOwnerAjax(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageLecturer") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                try
                {
                    int idLecturerClass = int.Parse(col["idLecturerClass"]);
                    int idSemester = int.Parse(col["idSemester"]);
                    int idClass = int.Parse(col["idClass"]);
                    DateTime endDate = DateTime.Parse(col["endDate"]);
                    string scheduleFirst = col["scheduleFirst"].Equals(string.Empty) ? null : col["scheduleFirst"];
                    string scheduleSecond = col["scheduleSecond"].Equals(string.Empty) ? null : col["scheduleSecond"];
                    string roomFirst = col["roomFirst"].Equals(string.Empty) ? null : col["roomFirst"];
                    string roomSecond = col["roomSecond"].Equals(string.Empty) ? null : col["roomSecond"];
                    LecturerClassBO lecturerClassBO = new LecturerClassBO(this.dsaContext);
                    bool isSuccess = lecturerClassBO.UpdateClassOwner(
                        idLecturerClass,
                        idSemester,
                        idClass,
                        endDate,
                        scheduleFirst,
                        scheduleSecond,
                        roomFirst,
                        roomSecond,
                        col);
                    if (isSuccess)
                    {
                        return this.Json(
                            new { success = true, responseText = "Cập nhật lớp chủ nhiệm thành công!" },
                            JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return
                            this.Json(
                                new
                                    {
                                        success = false,
                                        responseText = "Cập nhật dữ liệu thất bại! Kiểm tra lại thông tin."
                                    },
                                JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return this.Json(
                        new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." },
                        JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        public ActionResult UpdateLecturer(int idLecturer)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageLecturer") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                Lecturer lecturer = this.lecturerBO.GetLecturerById(idLecturer);
                if (lecturer == null) this.ViewBag.error = "Không tìm thấy giảng viên chủ nhiệm!";
                else
                {
                    this.ViewBag.faculties = this.dsaContext.Faculties.ToList();
                    this.ViewBag.semesters = this.dsaContext.Semesters.ToList();
                    this.ViewBag.classes = this.dsaContext.Classes.ToList();
                    LecturerClassBO lecturerClassBO = new LecturerClassBO(this.dsaContext);
                    this.ViewBag.classowner = lecturerClassBO.GetLecturerClasses(idLecturer);
                }

                return this.View(lecturer);
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }

        [HttpPost]
        public ActionResult UpdateLecturer(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageLecturer") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }

                try
                {
                    int idLecturer = int.Parse(col["IdLecturer"]);
                    string lecturerNumber = col["LecturerNumber"];
                    string lastName = col["LastName"];
                    string firstName = col["FirstName"];
                    string degree = col["Degree"];
                    string type = col["Type"];
                    string academicTitle = col["AcademicTitle"];
                    string position = col["Position"];
                    int idFaculty = int.Parse(col["Faculty"]);
                    string email = col["Email"];
                    string phoneNumber = col["PhoneNumber"];
                    string address = col["Address"];

                    if (string.IsNullOrWhiteSpace(lecturerNumber) || string.IsNullOrWhiteSpace(lastName)
                        || string.IsNullOrWhiteSpace(firstName))
                    {
                        return
                            this.Json(
                                new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." },
                                JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Lecturer lecturer = this.lecturerBO.GetLecturerById(idLecturer);
                        if (lecturer == null)
                        {
                            return
                                this.Json(
                                    new
                                        {
                                            success = false,
                                            responseText = "Không tìm thấy giảng viên. Kiểm tra lại thông tin!"
                                        },
                                    JsonRequestBehavior.AllowGet);
                        }
                        else if (!lecturerNumber.Equals(lecturer.LecturerNumber))
                        {
                            Lecturer lecturerTmp = this.lecturerBO.GetLecturerByNumber(lecturerNumber);
                            if (lecturerTmp != null)
                            {
                                return
                                    this.Json(
                                        new
                                            {
                                                success = false,
                                                responseText = "Mã giảng viên không hợp lệ. Kiểm tra lại thông tin!"
                                            },
                                        JsonRequestBehavior.DenyGet);
                            }
                        }

                        bool isSuccess = this.lecturerBO.UpdateLecturer(
                            idLecturer,
                            lecturerNumber,
                            lastName,
                            firstName,
                            degree,
                            type,
                            academicTitle,
                            position,
                            idFaculty,
                            email,
                            phoneNumber,
                            address);
                        if (isSuccess)
                        {
                            return
                                this.Json(
                                    new
                                        {
                                            success = true,
                                            idLecturer = lecturer.IdLecturer,
                                            responseText = "Cập nhật giảng viên thành công!"
                                        },
                                    JsonRequestBehavior.DenyGet);
                        }
                        else
                        {
                            return
                                this.Json(
                                    new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin!" },
                                    JsonRequestBehavior.DenyGet);
                        }
                    }
                }
                catch
                {
                    return this.Json(
                        new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin!" },
                        JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }
    }
}