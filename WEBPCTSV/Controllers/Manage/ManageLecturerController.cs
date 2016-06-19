using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Helpers.Common;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;
using Excel = Microsoft.Office.Interop.Excel;

namespace WEBPCTSV.Controllers.Manage
{
    public class ManageLecturerController : Controller
    {
        DSAContext dsaContext;
        LecturerBO lecturerBO;

        public ManageLecturerController()
        {
            dsaContext = new DSAContext();
            lecturerBO = new LecturerBO(dsaContext);
        }

        public ActionResult ListLecturer(int? page)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageLecturer") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                ViewBag.faculties = dsaContext.Faculties.ToList();
                ViewBag.semesters = dsaContext.Semesters.OrderByDescending(s => s.EndTime).ToList();
                int pageview = page ?? 1;
                return View(lecturerBO.GetListLecturerAll());
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }

        #region add lecturer
        public ActionResult AddLecturer()
        {
            ViewBag.faculty = dsaContext.Faculties.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddLecturer(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageLecturer") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
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
                    int idFaculty = Int32.Parse(col["Faculty"]);
                    string email = col["Email"];
                    string phoneNumber = col["PhoneNumber"];
                    string address = col["Address"];

                    if (String.IsNullOrWhiteSpace(lecturerNumber)
                        || String.IsNullOrWhiteSpace(lastName)
                        || String.IsNullOrWhiteSpace(firstName)
                        )
                    {
                        return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." }, JsonRequestBehavior.AllowGet);
                    }

                    int idLecturer = lecturerBO.AddLecturer(lecturerNumber, type, firstName, lastName, degree, academicTitle, position, idFaculty, email, phoneNumber, address);
                    if (idLecturer == -1)
                    {
                        return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = true, idLecturer = idLecturer, responseText = "Thêm giảng viên thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion

        #region update lecturer
        [HttpPost]
        public ActionResult AddClassOwnerAjax(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageLecturer") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                try
                {
                    int idLecturer = Int32.Parse(col["idLecturer"]);
                    int idSemester = Int32.Parse(col["idSemester"]);
                    int idClass = Int32.Parse(col["idClass"]);
                    string endDate = col["endDate"];
                    string scheduleFirst = col["scheduleFirst"];
                    string scheduleSecond = col["scheduleSecond"];
                    string roomFirst = col["roomFirst"];
                    string roomSecond = col["roomSecond"];

                    LecturerClassBO lecturerClassBO = new LecturerClassBO(dsaContext);
                    bool isSuccess = lecturerClassBO.AddClassOwner(idLecturer, idSemester, idClass, endDate, scheduleFirst, roomFirst, scheduleSecond, roomSecond);
                    if (isSuccess)
                    {
                        return Json(new { success = true, idLecturer = idLecturer, responseText = "Thêm lớp chủ nhiệm thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }

        }

        [HttpPost]
        public ActionResult DeleteClassOwnerAjax(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageLecturer") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                try
                {
                    int idLecturerClass = Int32.Parse(col["idLecturerClass"]);
                    int idLecturer = Int32.Parse(col["idLecturer"]);
                    LecturerClassBO lecturerClassBO = new LecturerClassBO(dsaContext);
                    bool isSuccess = lecturerClassBO.RemoveClassOwner(idLecturerClass, idLecturer);
                    if (isSuccess)
                    {
                        return Json(new { success = true, idLecturer = idLecturer, responseText = "Xóa lớp chủ nhiệm thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Xóa lớp chủ nhiệm thất bại! Kiểm tra lại thông tin." }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return Json(new { success = false, responseText = "Xóa lớp chủ nhiệm thất bại! Kiểm tra lại thông tin." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }

        }

        [HttpPost]
        public ActionResult UpdateClassOwnerAjax(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageLecturer") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                try
                {
                    int idLecturerClass = Int32.Parse(col["idLecturerClass"]);
                    int idSemester = Int32.Parse(col["idSemester"]);
                    int idClass = Int32.Parse(col["idClass"]);
                    DateTime endDate = DateTime.Parse(col["endDate"]);
                    string scheduleFirst = col["scheduleFirst"].Equals("") ? null : col["scheduleFirst"];
                    string scheduleSecond = col["scheduleSecond"].Equals("") ? null : col["scheduleSecond"];
                    string roomFirst = col["roomFirst"].Equals("") ? null : col["roomFirst"];
                    string roomSecond = col["roomSecond"].Equals("") ? null : col["roomSecond"];
                    LecturerClassBO lecturerClassBO = new LecturerClassBO(dsaContext);
                    bool isSuccess = lecturerClassBO.UpdateClassOwner(idLecturerClass, idSemester, idClass, endDate, scheduleFirst, scheduleSecond, roomFirst, roomSecond, col);
                    if (isSuccess)
                    {
                        return Json(new { success = true, responseText = "Cập nhật lớp chủ nhiệm thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Cập nhật dữ liệu thất bại! Kiểm tra lại thông tin." }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }

        }

        [HttpPost]
        public ActionResult DetailClassOwnerAjax(FormCollection col)
        {
            try
            {
                int idLecturerClass = Int32.Parse(col["idLecturerClass"]);
                LecturerClassBO lecturerClassBO = new LecturerClassBO(dsaContext);
                var jsonObject = lecturerClassBO.GetDetailClassOwner(idLecturerClass);
                if (jsonObject != null)
                {
                    return Json(new { success = true, lecturerClass = jsonObject, responseText = "Lấy thông tin thành công!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Không tìm thấy thông tin!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, responseText = "Không tìm thấy thông tin!" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetLecturerDocumentSemesterList(FormCollection col)
        {
            try
            {
                int idSemester = Int32.Parse(col["idSemester"]);
                LecturerDocumentSemesterBO lecturerDocumentSemesterBO = new LecturerDocumentSemesterBO(dsaContext);
                var listDocument = lecturerDocumentSemesterBO.GetLecturerDocumentListBySemester(idSemester);
                if (listDocument != null)
                {
                    return Json(new { success = true, listDocument = listDocument }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = "Không tìm thấy thông tin!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateLecturer(int idLecturer)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageLecturer") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                Lecturer lecturer = lecturerBO.GetLecturerById(idLecturer);
                if (lecturer == null)
                    ViewBag.error = "Không tìm thấy giảng viên chủ nhiệm!";
                else
                {
                    ViewBag.faculties = dsaContext.Faculties.ToList();
                    ViewBag.semesters = dsaContext.Semesters.ToList();
                    ViewBag.classes = dsaContext.Classes.ToList();
                    LecturerClassBO lecturerClassBO = new LecturerClassBO(dsaContext);
                    ViewBag.classowner = lecturerClassBO.GetLecturerClasses(idLecturer);
                }
                return View(lecturer);
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        [HttpPost]
        public ActionResult UpdateLecturer(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageLecturer") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                try
                {
                    int idLecturer = Int32.Parse(col["IdLecturer"]);
                    string lecturerNumber = col["LecturerNumber"];
                    string lastName = col["LastName"];
                    string firstName = col["FirstName"];
                    string degree = col["Degree"];
                    string type = col["Type"];
                    string academicTitle = col["AcademicTitle"];
                    string position = col["Position"];
                    int idFaculty = Int32.Parse(col["Faculty"]);
                    string email = col["Email"];
                    string phoneNumber = col["PhoneNumber"];
                    string address = col["Address"];

                    if (String.IsNullOrWhiteSpace(lecturerNumber)
                        || String.IsNullOrWhiteSpace(lastName)
                        || String.IsNullOrWhiteSpace(firstName))
                    {
                        return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Lecturer lecturer = lecturerBO.GetLecturerById(idLecturer);
                        if (lecturer == null)
                        {
                            return Json(new { success = false, responseText = "Không tìm thấy giảng viên. Kiểm tra lại thông tin!" }, JsonRequestBehavior.AllowGet);
                        }
                        else if (!lecturerNumber.Equals(lecturer.LecturerNumber))
                        {
                            Lecturer lecturerTmp = lecturerBO.GetLecturerByNumber(lecturerNumber);
                            if (lecturerTmp != null)
                            {
                                return Json(new { success = false, responseText = "Mã giảng viên không hợp lệ. Kiểm tra lại thông tin!" }, JsonRequestBehavior.DenyGet);
                            }
                        }
                        bool isSuccess = lecturerBO.UpdateLecturer(idLecturer, lecturerNumber, lastName, firstName, degree, type, academicTitle, position, idFaculty, email, phoneNumber, address);
                        if (isSuccess)
                        {
                            return Json(new { success = true, idLecturer = lecturer.IdLecturer, responseText = "Cập nhật giảng viên thành công!" }, JsonRequestBehavior.DenyGet);
                        }
                        else
                        {
                            return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin!" }, JsonRequestBehavior.DenyGet);
                        }
                    }
                }
                catch
                {
                    return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin!" }, JsonRequestBehavior.DenyGet);
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        [HttpPost]
        public ActionResult DeleteLecturerAjax(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageLecturer") != -1);
                if (!isGranted)
                {
                    return View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                try
                {
                    int idLecturer = Int32.Parse(col["IdLecturer"]);
                    bool isSuccess = lecturerBO.RemoveLecturer(idLecturer);
                    if (isSuccess)
                    {
                        return Json(new { success = true, responseText = "Xóa giảng viên thành công!" }, JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin!" }, JsonRequestBehavior.AllowGet);

                    }
                }
                catch
                {
                    return Json(new { success = false, responseText = "Nhập sai dữ liệu! Kiểm tra lại thông tin!" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Redirect("/QuanLy");
            }
        }
        #endregion

        #region import lecturer from excel
        // Import list of lecturer from file excel
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ImportExcel(FormCollection formCollection)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = (accountSession.Functions.IndexOf("ManageLecturer") != -1);
                if (!isGranted)
                {
                    return Content("false;Bạn không đủ quyền thực hiện chức năng này!");
                }
                string classOwner = "";
                string fullName = "";
                string degree = "";
                string phoneNumber = "";
                string email = "";
                string address = "";
                string type = "";
                string academicTitle = "";
                string position = "";
                try
                {
                    if (Request != null)
                    {
                        // Get data from form
                        int idFaculty = Int32.Parse(Request.Form["IdFaculty"]);
                        int idSemester = Int32.Parse(Request.Form["IdSemester"]);
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
                                        string errorClass = "";
                                        for (int rowIterator = 4; rowIterator <= noOfRow; rowIterator++)
                                        {
                                            try
                                            {
                                                // Get data from colunm excel
                                                classOwner = workSheet.Cells[rowIterator, 2].Value.ToString();
                                                fullName = workSheet.Cells[rowIterator, 3].Value.ToString();
                                                academicTitle = workSheet.Cells[rowIterator, 4].Value != null ? workSheet.Cells[rowIterator, 4].Value.ToString() : "";
                                                degree = workSheet.Cells[rowIterator, 5].Value != null ? workSheet.Cells[rowIterator, 5].Value.ToString() : "";
                                                type = workSheet.Cells[rowIterator, 6].Value != null ? workSheet.Cells[rowIterator, 6].Value.ToString() : "";
                                                position = workSheet.Cells[rowIterator, 7].Value != null ? workSheet.Cells[rowIterator, 7].Value.ToString() : "";
                                                phoneNumber = workSheet.Cells[rowIterator, 8].Value != null ? workSheet.Cells[rowIterator, 8].Value.ToString() : "";
                                                email = workSheet.Cells[rowIterator, 9].Value != null ? workSheet.Cells[rowIterator, 9].Value.ToString() : "";
                                                address = workSheet.Cells[rowIterator, 10].Value != null ? workSheet.Cells[rowIterator, 10].Value.ToString() : "";

                                                //Generate username of lecturer by faculty
                                                string[] arrName = StringExtension.GetPartOfFullName(fullName);
                                                string firstName = arrName[1];
                                                string lastName = arrName[0];
                                                Faculty faculty = dsaContext.Faculties.Find(idFaculty);
                                                string lecturerNumber = faculty.NumberFaculty + StringExtension.GenerateUsernameByName(firstName, lastName);
                                                //Check lecturer isExist
                                                var lecturer = dsaContext.Lecturers.SingleOrDefault(lec => (lec.LecturerNumber.Equals(lecturerNumber)) || (!lec.PhoneNumber.Equals("") && lec.PhoneNumber.Equals(phoneNumber)));
                                                int idLecturer;
                                                if (lecturer != null)
                                                {
                                                    lecturerBO.UpdateLecturer(lecturer.IdLecturer, lecturerNumber, lastName, firstName, degree, type, academicTitle, position, idFaculty, email, phoneNumber, address);
                                                    idLecturer = lecturer.IdLecturer;
                                                }
                                                else
                                                {
                                                    // Lecturer not exist
                                                    idLecturer = lecturerBO.AddLecturer(lecturerNumber, type, arrName[1], arrName[0], degree, academicTitle, position, idFaculty, email, phoneNumber, address);
                                                }

                                                // Insert class owner
                                                Class classExist = dsaContext.Classes.SingleOrDefault(classQuery => classQuery.ClassName.Equals(classOwner));
                                                Semester semester = dsaContext.Semesters.SingleOrDefault(se => se.IdSemester.Equals(idSemester));
                                                if (classExist != null)
                                                {
                                                    // Insert lecturer class owner
                                                    string room = workSheet.Cells[rowIterator, 14].Value != null ? workSheet.Cells[rowIterator, 14].Value.ToString() : "";
                                                    room += " Tiết " + (workSheet.Cells[rowIterator, 11].Value != null ? workSheet.Cells[rowIterator, 11].Value.ToString() : "");
                                                    string scheduleFirst = workSheet.Cells[rowIterator, 12].Value != null ? workSheet.Cells[rowIterator, 12].Value.ToString() : null;
                                                    string scheduleSecond = workSheet.Cells[rowIterator, 13].Value != null ? workSheet.Cells[rowIterator, 13].Value.ToString() : null;
                                                    LecturerClass lecClass = new LecturerClass(idLecturer, idSemester, classExist.IdClass, semester.EndTime.ToString(), scheduleFirst, room, scheduleSecond, room);
                                                    dsaContext.LecturerClasses.Add(lecClass);
                                                    dsaContext.SaveChanges();

                                                    // Insert document to require
                                                    var docs = dsaContext.LecturerDocumentSemesters.Where(lecDoc => lecDoc.IdSemester == idSemester).ToList();
                                                    foreach (LecturerDocumentSemester doc in docs)
                                                    {
                                                        LecturerClassDocument lecClassDoc;
                                                        lecClassDoc = new LecturerClassDocument(lecClass.IdLecturerClass, doc.IdDocumentSemester, false);
                                                        dsaContext.LecturerClassDocuments.Add(lecClassDoc);
                                                    }
                                                    dsaContext.SaveChanges();
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
                                        return Content("true;Nhập từ file excel thành công!" + (string.IsNullOrEmpty(errorClass) ? "" : " Các lớp bị lỗi: " + errorClass));
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
                    return Content("false;Nhập từ file excel thất bại!" + (!string.IsNullOrEmpty(fullName) ? " Lỗi cập nhật giảng viên " + fullName : ""));
                }
                catch
                {
                    // Return text to avoid auto download file json in old browser.
                    return Content("false;Nhập từ file excel thất bại!" + (!string.IsNullOrEmpty(fullName) ? " Lỗi cập nhật giảng viên " + fullName : ""));
                }
            }
            else
            {
                return Content("false;Chưa đăng nhập hệ thống!");
            }
        }
        #endregion

        #region export excel
        public ActionResult ExportExcel(int idFaculty, int idSemester)
        {
            AccountSession accountSession = (AccountSession)Session["AccountSession"];
            if (accountSession != null)
            {
                // Check permission
                if (accountSession.Functions.IndexOf("ManageLecturer") != -1)
                {
                    FacultyBO facultyBO = new FacultyBO(dsaContext);
                    SemesterBO semesterBO = new SemesterBO(dsaContext);
                    LecturerBO lecturerBO = new LecturerBO(dsaContext);
                    Semester semester = semesterBO.GetSemesterById(idSemester);
                    Faculty faculty = facultyBO.GetFaculty(idFaculty);
                    List<Lecturer> listLecturer = lecturerBO.GetListLecturerByFaculty(idFaculty);

                    if (semester == null)
                    {
                        return Json(new
                        {
                            success = false,
                            responseText = "Không tìm kỳ cần thống kê đánh giá!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Excel.Application application = new Excel.Application();
                        Excel.Workbook workbook;
                        if (semester.SemesterYear.IdSemesterYear.Equals("1"))
                        {
                            // Open file document template
                            workbook = application.Workbooks.Open(Server.MapPath("~/Files/Lecturer/ThongKeGiangVien_HKI_yyyy_Khoa_ddMMyyyy.xls"), 0, false, 5, "", "", false,
                                Excel.XlPlatform.xlWindows,
                                "", true, true, 0, true, false, false);
                        }
                        else
                        {
                            // Open file document template
                            workbook = application.Workbooks.Open(Server.MapPath("~/Files/Lecturer/ThongKeGiangVien_HKII_yyyy_Khoa_ddMMyyyy.xls"), 0, false, 5, "", "", false,
                                Excel.XlPlatform.xlWindows,
                                "", true, true, 0, true, false, false);
                        }

                        // Get sheet
                        Excel.Worksheet workSheet = workbook.Worksheets[1];

                        // Set values for sheet name
                        workSheet.Name = faculty.Acronym.Trim();
                        // Set field of report
                        workSheet.get_Range("A5", "G5").Value2 = "Khoa: " + faculty.FacultyName + "   Học kỳ: " + semester.SemesterYear.SemesterYearName + "    Năm học: " + semester.Year.YearName;
                        int row = 9;
                        for (int i = listLecturer.Count() - 1; i >= 0; i--)
                        {
                            try
                            {
                                List<LecturerClass> listLecturerClass = listLecturer[i].LecturerClasses.Where(c => c.IdSemester == idSemester).ToList();
                                foreach (LecturerClass lecClass in listLecturerClass)
                                {
                                    try
                                    {
                                        row++;
                                        // Set field index
                                        workSheet.Rows[9].Insert();
                                        workSheet.get_Range("A9", "A9").Value2 = i + 1;
                                        // Set field name
                                        workSheet.get_Range("B9", "B9").Value2 = listLecturer[i].LastName + " " + listLecturer[i].FirstName;
                                        workSheet.get_Range("C9", "C9").Value2 = lecClass.Class.ClassName;
                                        List<LecturerClassDocument> listDoc = dsaContext.LecturerClassDocuments.Where(l => l.IdLecturerClass == lecClass.IdLecturerClass).ToList();
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
                        workSheet.get_Range("D" + row, "D" + row).Value2 = "Đà Nẵng, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        // Set name of lecturer
                        workSheet.Rows[8].Delete();

                        String nameFile = "ThongKeGiangVien_HK" + semester.SemesterYear.SemesterYearName + "_" + semester.Year.YearName + "_" + faculty.Acronym.Trim() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".xls";

                        if (System.IO.File.Exists(Server.MapPath("~/Files/Lecturer/" + nameFile)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/Files/Lecturer/" + nameFile));
                        }

                        workbook.SaveAs(Server.MapPath("~/Files/Lecturer/" + nameFile));
                        workbook.Close();
                        Marshal.ReleaseComObject(workbook);
                        application.Quit();
                        Marshal.FinalReleaseComObject(application);

                        Response.ContentType = "application/octet-stream";
                        Response.AppendHeader("content-disposition", "attachment;filename=" + nameFile);
                        Response.TransmitFile(Server.MapPath("~/Files/Lecturer/" + nameFile));
                        Response.Flush();
                        Response.End();

                        System.IO.File.Delete(Server.MapPath("~/Files/Lecturer/" + nameFile));

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
        #endregion
    }
}
