namespace WEBPCTSV.Controllers
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;

    using OfficeOpenXml;

    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class FileController : Controller
    {
        static int percentProgress = 0;

        public FileResult DownloadExcel(string filename)
        {
            string dir = this.Server.MapPath("~/FileData/Sample/");
            string path = Path.Combine(dir, filename);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return this.File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        // public ActionResult ExportListStudentShip(int idStudentShipSchoolFaculty)
        // {
        // string dir = Server.MapPath("~/FileData/hinhanh/AvatarThumbnail/");
        // FileInfo newFile = new FileInfo(dir + @"\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "hocbong.xlsx");
        // ExcelPackage pck = new ExcelPackage(newFile);
        // //Add the Content sheet
        // pck = new StudentShipSchoolFacultyBo().ExportListStudentShipschoolFaculty(idStudentShipSchoolFaculty, pck);
        // //ws.View.ShowGridLines = false;
        // pck.Save();
        // //BinaryFormatter bf = new BinaryFormatter();
        // //using (MemoryStream ms = new MemoryStream())
        // //{
        // //    bf.Serialize(ms, pck);
        // //    return View();
        // //}
        // byte[] filedata = System.IO.File.ReadAllBytes(newFile.FullName);

        // string contentType = MimeMapping.GetMimeMapping(newFile.FullName);

        // var cd = new System.Net.Mime.ContentDisposition
        // {
        // FileName = newFile.Name,
        // Inline = true,
        // };

        // Response.AppendHeader("Content-Disposition", cd.ToString());

        // return File(filedata, contentType);
        // }
        public ActionResult ExportListStudentShip(int idStudentShipSchoolFaculty)
        {
            MemoryStream fs =
                new StudentShipSchoolFacultyBo().ExportListStudentShipschoolFaculty(idStudentShipSchoolFaculty);
            return this.File(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MyFileName.xlsx");
        }

        public ActionResult ExportStudentShip(int idStudentShipSchool)
        {
            MemoryStream fs = new StudentShipSchoolBo().ExportListStudentShips(idStudentShipSchool);
            return this.File(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MyFileName.xlsx");
        }

        public ActionResult GetInforProgress()
        {
            percentProgress = StudentBO.percentProgress;
            return this.Content(percentProgress + string.Empty, "text/plain");
        }

        public ActionResult GetInforProgressImportRegency()
        {
            percentProgress = RegencyStudentBo.percentProgress;
            return this.Content(percentProgress + string.Empty, "text/plain");
        }

        public ActionResult GetInforProgressImportScote()
        {
            percentProgress = LearningOutComeBo.percentProgress;
            return this.Content(percentProgress + string.Empty, "text/plain");
        }

        public ActionResult ImagePaper(string id)
        {
            string dir = this.Server.MapPath("~/FileData/hinhanh/ImagePaper/");
            string path = Path.Combine(dir, id);
            int length = id.Length;
            string duoi = id.Substring(length - 3);
            if (duoi.Equals("png"))
            {
                return this.File(path, "image/png");
            }

            return this.File(path, "image/jpeg");
        }

        [HttpPost]
        public ActionResult ImportExcelStudentShipSchool(int idStudentShipSchool)
        {
            if (this.Request.Files.Count != 0)
            {
                HttpPostedFileBase file = this.Request.Files[0];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        new StudentShipSchoolFacultyBo().Import(idStudentShipSchool, package);
                    }
                }
            }

            return this.Content("upload thành công", "text/plain");
        }

        public ActionResult ThumbnailImageAvatar(string id)
        {
            string dir = this.Server.MapPath("~/FileData/hinhanh/AvatarThumbnail/");
            string path = Path.Combine(dir, id);
            int length = id.Length;
            string duoi = id.Substring(length - 3);
            if (duoi.Equals("png"))
            {
                return this.File(path, "image/png");
            }

            return this.File(path, "image/jpeg");
        }

        public ActionResult ThumbnailImageStudent(string id)
        {
            string dir = this.Server.MapPath("~/FileData/hinhanh/PersonThumbnail/");
            string path = Path.Combine(dir, id);
            int length = id.Length;
            string duoi = id.Substring(length - 3);
            if (duoi.Equals("png"))
            {
                return this.File(path, "image/png");
            }

            return this.File(path, "image/jpeg");
        }

        [HttpPost]
        public ActionResult UploadExcel()
        {
            if (this.Request.Files.Count != 0)
            {
                HttpPostedFileBase file = this.Request.Files[0];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        new StudentBO().ImportStudent3(package);
                    }
                }
            }

            return this.Content("upload thành công", "text/plain");
        }

        [HttpPost]
        public ActionResult UploadExcelRegency()
        {
            if (this.Request.Files.Count != 0)
            {
                HttpPostedFileBase file = this.Request.Files[0];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        new RegencyStudentBo().ImportRegencyStudent(package);
                    }
                }
            }

            return this.Content("upload thành công", "text/plain");
        }

        /// <summary>
        /// Upload điểm sinh viên
        /// </summary>
        /// <param name="idSemester"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadExcelScoteStudent(int idSemester)
        {
            if (this.Request.Files.Count != 0)
            {
                HttpPostedFileBase file = this.Request.Files[0];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        new LearningOutComeBo().ImportScoteStudent(idSemester, package);
                    }
                }
            }

            return this.Content("upload thành công", "text/plain");
        }

        [HttpPost]
        public ActionResult UploadImageAvatar()
        {
            string fileName = string.Empty;
            string dirTh = this.Server.MapPath("~/FileData/hinhanh/AvatarThumbnail/");
            if (this.Request.Files.Count != 0)
            {
                HttpPostedFileBase file = this.Request.Files[0];
                fileName = this.Session["username"] + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + file.FileName;

                if (!Directory.Exists(dirTh))
                {
                    Directory.CreateDirectory(dirTh);
                }

                // file.SaveAs(dir + fileName);
                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200);
                img.Save(dirTh + fileName);
            }

            string link = "http://" + this.Request.Url.Authority + "/File/ThumbnailImageAvatar?id=" + fileName;
            AccountBO accountBo = new AccountBO();
            string linkFile = accountBo.GetAvatarAccount(Convert.ToInt32(this.Session["idAccount"]));
            this.Session["avatar"] = link;
            accountBo.SetAvatarAccount(link, Convert.ToInt32(this.Session["idAccount"]));
            if (linkFile != null)
            {
                linkFile = linkFile.Replace(
                    "http://" + this.Request.Url.Authority + "/File/ThumbnailImageAvatar?id=",
                    dirTh);
                new ManageFile().DeleteFile(linkFile);
            }

            return this.RedirectToAction("AccountInformation", "ManageAccount");
        }

        [HttpPost]
        public ActionResult UploadImagePaper(int idTypePaperStudent)
        {
            string fileName = string.Empty;
            string dirTh = this.Server.MapPath("~/FileData/hinhanh/ImagePaper/");
            if (this.Request.Files.Count != 0)
            {
                HttpPostedFileBase file = this.Request.Files[0];
                fileName = this.Session["username"] + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + file.FileName;

                if (!Directory.Exists(dirTh))
                {
                    Directory.CreateDirectory(dirTh);
                }

                file.SaveAs(dirTh + fileName);
            }

            string link = "http://" + this.Request.Url.Authority + "/File/ImagePaper?id=" + fileName;
            string linkFile = new TypePaperBo().GetLinkFileTypePaperStudent(idTypePaperStudent);
            new TypePaperBo().UpdateLinkFileTypePaperStudent(idTypePaperStudent, link);
            if (linkFile != null)
            {
                linkFile = linkFile.Replace("http://" + this.Request.Url.Authority + "/File/ImagePaper?id=", dirTh);
                new ManageFile().DeleteFile(linkFile);
            }

            return this.Content("upload thành công", "text/plain");
        }

        [HttpPost]
        public ActionResult UploadImageStaff(int idStaff)
        {
            string fileName = string.Empty;
            string dirTh = this.Server.MapPath("~/FileData/hinhanh/PersonThumbnail/");
            if (this.Request.Files.Count != 0)
            {
                HttpPostedFileBase file = this.Request.Files[0];
                fileName = this.Session["username"] + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + file.FileName;

                if (!Directory.Exists(dirTh))
                {
                    Directory.CreateDirectory(dirTh);
                }

                // file.SaveAs(dir + fileName);
                WebImage img = new WebImage(file.InputStream);
                img.Resize(150, 200);
                img.Save(dirTh + fileName);
            }

            string link = "http://" + this.Request.Url.Authority + "/File/ThumbnailImageStudent?id=" + fileName;
            StaffBO staffBo = new StaffBO();
            string linkFile = staffBo.GetImage(idStaff);
            staffBo.SetImage(idStaff, link);
            if (linkFile != null)
            {
                linkFile = linkFile.Replace(
                    "http://" + this.Request.Url.Authority + "/File/ThumbnailImageStudent?id=",
                    dirTh);
                new ManageFile().DeleteFile(linkFile);
            }

            return this.RedirectToAction("InfoStaff", "ManageStaff");
        }

        [HttpPost]
        public ActionResult UploadImageStudent(int idStudent)
        {
            string fileName = string.Empty;
            string dirTh = this.Server.MapPath("~/FileData/hinhanh/PersonThumbnail/");
            if (this.Request.Files.Count != 0)
            {
                HttpPostedFileBase file = this.Request.Files[0];
                fileName = this.Session["username"] + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + file.FileName;

                if (!Directory.Exists(dirTh))
                {
                    Directory.CreateDirectory(dirTh);
                }

                // file.SaveAs(dir + fileName);
                WebImage img = new WebImage(file.InputStream);
                img.Resize(150, 200);
                img.Save(dirTh + fileName);
            }

            string link = "http://" + this.Request.Url.Authority + "/File/ThumbnailImageStudent?id=" + fileName;
            StudentBO studentBo = new StudentBO();
            string linkFile = studentBo.GetImageStudent(idStudent);
            studentBo.SetImageStudent(link, idStudent);
            if (linkFile != null)
            {
                linkFile = linkFile.Replace(
                    "http://" + this.Request.Url.Authority + "/File/ThumbnailImageStudent?id=",
                    dirTh);
                new ManageFile().DeleteFile(linkFile);
            }

            return this.RedirectToAction("EditStudent", "ManageStudent", new { idStudent = idStudent });
        }
    }
}