using OfficeOpenXml;
using WEBPCTSV.Models.bo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WEBPCTSV.Models.Support;
using ClosedXML.Excel;

namespace WEBPCTSV.Controllers
{
    public class FileController : Controller
    {
        static int percentProgress = 0;
        [HttpPost]
        public ActionResult UploadImageAvatar()
        {
            string fileName = "";
            string dirTh = Server.MapPath("~/FileData/hinhanh/AvatarThumbnail/");
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                fileName = Session["username"] + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + file.FileName;
                
                if (!Directory.Exists(dirTh))
                {
                    Directory.CreateDirectory(dirTh);
                }
                //file.SaveAs(dir + fileName);

                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200);
                img.Save(dirTh + fileName);
            }
            string link = "http://" + Request.Url.Authority + "/File/ThumbnailImageAvatar?id=" + fileName;
            AccountBO accountBo = new AccountBO();
            string linkFile = accountBo.GetAvatarAccount(Convert.ToInt32(Session["idAccount"]));
            Session["avatar"] = link;
            accountBo.SetAvatarAccount(link, Convert.ToInt32(Session["idAccount"]));
            if(linkFile!=null)
            {
                linkFile =  linkFile.Replace("http://" + Request.Url.Authority + "/File/ThumbnailImageAvatar?id=", dirTh);
                new ManageFile().DeleteFile(linkFile);
            }
            return RedirectToAction("AccountInformation","ManageAccount");
        }

        [HttpPost]
        public ActionResult UploadImageStaff(int idStaff)
        {
            string fileName = "";
            string dirTh = Server.MapPath("~/FileData/hinhanh/PersonThumbnail/");
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                fileName = Session["username"] + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + file.FileName;

                if (!Directory.Exists(dirTh))
                {
                    Directory.CreateDirectory(dirTh);
                }
                //file.SaveAs(dir + fileName);

                WebImage img = new WebImage(file.InputStream);
                img.Resize(150, 200);
                img.Save(dirTh + fileName);
            }
            string link = "http://" + Request.Url.Authority + "/File/ThumbnailImageStudent?id=" + fileName;
            StaffBO staffBo = new StaffBO();
            string linkFile = staffBo.GetImage(idStaff);
            staffBo.SetImage(idStaff, link);
            if (linkFile != null)
            {
                linkFile = linkFile.Replace("http://" + Request.Url.Authority + "/File/ThumbnailImageStudent?id=", dirTh);
                new ManageFile().DeleteFile(linkFile);
            }
            return RedirectToAction("InfoStaff", "ManageStaff");
        }

        [HttpPost]
        public ActionResult UploadImageStudent(int idStudent)
        {
            string fileName = "";
            string dirTh = Server.MapPath("~/FileData/hinhanh/PersonThumbnail/");
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                fileName = Session["username"] + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + file.FileName;

                if (!Directory.Exists(dirTh))
                {
                    Directory.CreateDirectory(dirTh);
                }
                //file.SaveAs(dir + fileName);

                WebImage img = new WebImage(file.InputStream);
                img.Resize(150, 200);
                img.Save(dirTh + fileName);
            }
            string link = "http://" + Request.Url.Authority + "/File/ThumbnailImageStudent?id=" + fileName;
            StudentBO studentBo = new StudentBO();
            string linkFile = studentBo.GetImageStudent(idStudent);
            studentBo.SetImageStudent(link, idStudent);
            if (linkFile != null)
            {
                linkFile = linkFile.Replace("http://" + Request.Url.Authority + "/File/ThumbnailImageStudent?id=", dirTh);
                new ManageFile().DeleteFile(linkFile);
            }
            return RedirectToAction("EditStudent", "ManageStudent", new { idStudent= idStudent});
        }

        public ActionResult ThumbnailImageAvatar(string id)
        {
            string dir = Server.MapPath("~/FileData/hinhanh/AvatarThumbnail/");
            string path = Path.Combine(dir, id);
            int length = id.Length;
            string duoi = id.Substring(length - 3);
            if (duoi.Equals("png"))
            {
                return base.File(path, "image/png");
            }

            return base.File(path, "image/jpeg");
        }

        public ActionResult ThumbnailImageStudent(string id)
        {
            string dir = Server.MapPath("~/FileData/hinhanh/PersonThumbnail/");
            string path = Path.Combine(dir, id);
            int length = id.Length;
            string duoi = id.Substring(length - 3);
            if (duoi.Equals("png"))
            {
                return base.File(path, "image/png");
            }

            return base.File(path, "image/jpeg");
        }

        [HttpPost]
        public ActionResult UploadImagePaper(int idTypePaperStudent)
        {
            string fileName = "";
            string dirTh = Server.MapPath("~/FileData/hinhanh/ImagePaper/");
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                fileName = Session["username"] + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + file.FileName;
                
                if (!Directory.Exists(dirTh))
                {
                    Directory.CreateDirectory(dirTh);
                }
                file.SaveAs(dirTh + fileName);
            }
            string link = "http://" + Request.Url.Authority + "/File/ImagePaper?id=" + fileName;
            string linkFile = new TypePaperBo().GetLinkFileTypePaperStudent(idTypePaperStudent);
            new TypePaperBo().UpdateLinkFileTypePaperStudent(idTypePaperStudent, link);
            if (linkFile != null)
            {
                linkFile = linkFile.Replace("http://" + Request.Url.Authority + "/File/ImagePaper?id=", dirTh);
                new ManageFile().DeleteFile(linkFile);
            }
            return Content("upload thành công", "text/plain");
        }

        public ActionResult ImagePaper(string id)
        {
            string dir = Server.MapPath("~/FileData/hinhanh/ImagePaper/");
            string path = Path.Combine(dir, id);
            int length = id.Length;
            string duoi = id.Substring(length - 3);
            if (duoi.Equals("png"))
            {
                return base.File(path, "image/png");
            }

            return base.File(path, "image/jpeg");
        }

        [HttpPost]
        public ActionResult UploadExcel()
        {

            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                       new StudentBO().ImportStudent(package);
                    }
                    
                }
            }
            return Content("upload thành công", "text/plain");
        }

        [HttpPost]
        public ActionResult UploadExcelRegency()
        {

            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
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
            return Content("upload thành công", "text/plain");
        }

        [HttpPost]
        public ActionResult UploadExcelScoteStudent(int idSemester)
        {

            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        new LearningOutComeBo().ImportScoteStudent(idSemester,package);
                    }

                }
            }
            return Content("upload thành công", "text/plain");
        }

        [HttpPost]
        public ActionResult ImportExcelStudentShipSchool(int idStudentShipSchool)
        {

            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        new StudentShipSchoolFacultyBo().Import(idStudentShipSchool,package);
                    }

                }
            }
            return Content("upload thành công", "text/plain");
        }


        public ActionResult GetInforProgress()
        {
            percentProgress= StudentBO.percentProgress;
            return Content(percentProgress+"", "text/plain");
        }

        public ActionResult GetInforProgressImportScote()
        {
            percentProgress = LearningOutComeBo.percentProgress;
            return Content(percentProgress + "", "text/plain");
        }

        public ActionResult GetInforProgressImportRegency()
        {
            percentProgress = RegencyStudentBo.percentProgress;
            return Content(percentProgress + "", "text/plain");
        }

        //public ActionResult ExportListStudentShip(int idStudentShipSchoolFaculty)
        //{
        //    string dir = Server.MapPath("~/FileData/hinhanh/AvatarThumbnail/");
        //    FileInfo newFile = new FileInfo(dir + @"\" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "hocbong.xlsx");
        //    ExcelPackage pck = new ExcelPackage(newFile);
        //    //Add the Content sheet
        //    pck = new StudentShipSchoolFacultyBo().ExportListStudentShipschoolFaculty(idStudentShipSchoolFaculty, pck);
        //    //ws.View.ShowGridLines = false;
        //    pck.Save();
        //    //BinaryFormatter bf = new BinaryFormatter();
        //    //using (MemoryStream ms = new MemoryStream())
        //    //{
        //    //    bf.Serialize(ms, pck);
        //    //    return View();
        //    //}
        //    byte[] filedata = System.IO.File.ReadAllBytes(newFile.FullName);

        //    string contentType = MimeMapping.GetMimeMapping(newFile.FullName);

        //    var cd = new System.Net.Mime.ContentDisposition
        //    {
        //        FileName = newFile.Name,
        //        Inline = true,
        //    };

        //    Response.AppendHeader("Content-Disposition", cd.ToString());

        //    return File(filedata, contentType);
        //}

        public ActionResult ExportListStudentShip(int idStudentShipSchoolFaculty)
        {
            MemoryStream fs = new StudentShipSchoolFacultyBo().ExportListStudentShipschoolFaculty(idStudentShipSchoolFaculty);
            return File(fs,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MyFileName.xlsx");
        }

        public ActionResult ExportStudentShip(int idStudentShipSchool)
        {
            MemoryStream fs = new StudentShipSchoolBo().ExportListStudentShips(idStudentShipSchool);
            return File(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MyFileName.xlsx");
        }
        
    }
}