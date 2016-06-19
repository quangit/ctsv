using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Helpers.Common;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class AccountController : Controller
    {
        private DSAContext dsaContext;
        private AccountBO accountBO;
        //
        // GET: /Account/
        public AccountController()
        {
            dsaContext = new DSAContext();
            accountBO = new AccountBO(dsaContext);
        }
        public ActionResult Login()
        {
            if (Session["AccountSession"] != null)
            {
                return Redirect("TrangChu");
            }
            return View();
        }
        public ActionResult LoginAdmin()
        {
            if (Session["AccountSession"] != null)
            {
                return Redirect("/QuanLy/ThongTin");
            }
            return View();
        }

        public ActionResult Profile()
        {
            if (Session["AccountSession"] != null)
            {
                return View();
            }
            return Redirect("/QuanLy");
        }

        [HttpPost]
        public ActionResult Login(FormCollection col)
        {
            string returnPage = col["returnPage"];
            string username = col["username"];
            string password = col["password"];

            Account account = accountBO.checkAuthorityAccount(username, password);
            if (account != null)
            {
                List<string> functions = (new FunctionBO(dsaContext)).getListFunctionByGroup(account.IdDecentralizationGroup);

                string fullName = "";
                if (account.TypeAccount.Equals("SV"))
                {
                    Student student = account.Students.First();
                    fullName = StringExtension.getFullName(student.FirstNameStudent, student.LastNameStudent);
                    
                }
                else if (account.TypeAccount.Equals("NV"))
                {
                    Staff staff = account.Staffs.First();
                    fullName = staff.Name;
                }
                else if (account.TypeAccount.Equals("GV"))
                {
                    Lecturer lecturer = account.Lecturers.First();
                    fullName = StringExtension.getFullName(lecturer.FirstName, lecturer.LastName);
                }

                AccountSession accountSession = new AccountSession(account.IdAccount, account.UserName, account.TypeAccount, account.Avatar, account.IdDecentralizationGroup, fullName, functions);
                Session["AccountSession"] = accountSession;
                Session["username"] = account.UserName;
                Session["avatar"] = account.Avatar;
                Session["idAccount"] = account.IdAccount;
                Session["idDecenTralizationGroup"] = account.IdDecentralizationGroup;
                return Redirect(returnPage);
            }
            else
            {
                TempData["error"] = "Nhập sai username hoặc password! Kiểm tra lại thông tin.";
                return Redirect(returnPage);
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("AccountSession");
            return Redirect("TrangChu");
        }

        [HttpPost]
        public ActionResult LoginAjax(FormCollection col)
        {
            string username = col["username"];
            string password = col["password"];

            Account account = accountBO.checkAuthorityAccount(username, password);
            if (account != null)
            {
                List<string> functions = (new FunctionBO(dsaContext)).getListFunctionByGroup(account.IdDecentralizationGroup);
                string fullName = "";
                if (account.TypeAccount.Equals("SV"))
                {
                    Student student = account.Students.First();
                    fullName = StringExtension.getFullName(student.FirstNameStudent, student.LastNameStudent);
                }
                else if (account.TypeAccount.Equals("NV"))
                {
                    Staff staff = account.Staffs.First();
                    fullName = staff.Name;
                }
                else if (account.TypeAccount.Equals("GV"))
                {
                    Lecturer lecturer = account.Lecturers.First();
                    fullName = StringExtension.getFullName(lecturer.FirstName, lecturer.LastName);
                }
                AccountSession accountSession = new AccountSession(account.IdAccount, account.UserName, account.TypeAccount, account.Avatar, account.IdDecentralizationGroup, fullName, functions);
                Session["AccountSession"] = accountSession;
                Session["username"] = account.UserName;
                Session["avatar"] = account.Avatar;
                Session["idAccount"] = account.IdAccount;
                Session["idDecenTralizationGroup"] = account.IdDecentralizationGroup;
                return Json(new { success = true, responseText = "Đăng nhập hệ thống thành công!" }, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Nhập sai username hoặc password! Kiểm tra lại thông tin." }, JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult LogoutAjax()
        {
            try
            {
                Session.RemoveAll();
                return Json(new { success = true, responseText = "Đăng xuất thành công!" }, JsonRequestBehavior.DenyGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "Lỗi trong quá trình đăng xuất! Vui lòng thử lại." }, JsonRequestBehavior.DenyGet);
            }
        }
        public ActionResult Subscrible(string mail)
        {
            if (!StringExtension.IsNullOrWhiteSpace(mail))
            {
                try
                {
                    bool isSuccess = accountBO.Subscrible(mail);
                    if (isSuccess)
                    {
                        return Content("Đăng ký nhận mail thành công! Mọi tin tức mới sẽ được gửi qua mail cho bạn!");
                    }
                    else
                    {
                        return Content("Email không hợp lệ! Vui lòng kiểm tra lại thông tin.");
                    }
                }
                catch (Exception e)
                {
                    return Content(e.Message);
                }
            }
            else
            {
                return Content("Email không hợp lệ! Vui lòng kiểm tra lại thông tin.");
            }
        }
    }
}
