namespace WEBPCTSV.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Helpers.Common;
    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class AccountController : Controller
    {
        private readonly AccountBO accountBO;

        private readonly DSAContext dsaContext;

        // GET: /Account/
        public AccountController()
        {
            this.dsaContext = new DSAContext();
            this.accountBO = new AccountBO(this.dsaContext);
        }

        public ActionResult Login()
        {
            if (this.Session["AccountSession"] != null)
            {
                return this.Redirect("TrangChu");
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection col)
        {
            string returnPage = col["returnPage"];
            string username = col["username"];
            string password = col["password"];

            Account account = this.accountBO.checkAuthorityAccount(username, password);
            if (account != null)
            {
                List<string> functions =
                    (new FunctionBO(this.dsaContext)).getListFunctionByGroup(account.IdDecentralizationGroup);

                string fullName = string.Empty;
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

                AccountSession accountSession = new AccountSession(
                    account.IdAccount,
                    account.UserName,
                    account.TypeAccount,
                    account.Avatar,
                    account.IdDecentralizationGroup,
                    fullName,
                    functions);
                this.Session["AccountSession"] = accountSession;
                this.Session["username"] = account.UserName;
                this.Session["avatar"] = account.Avatar;
                this.Session["idAccount"] = account.IdAccount;
                this.Session["idDecenTralizationGroup"] = account.IdDecentralizationGroup;
                return this.Redirect(returnPage);
            }
            else
            {
                this.TempData["error"] = "Nhập sai username hoặc password! Kiểm tra lại thông tin.";
                return this.Redirect(returnPage);
            }
        }

        public ActionResult LoginAdmin()
        {
            if (this.Session["AccountSession"] != null)
            {
                return this.Redirect("/QuanLy/ThongTin");
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult LoginAjax(FormCollection col)
        {
            string username = col["username"];
            string password = col["password"];

            Account account = this.accountBO.checkAuthorityAccount(username, password);
            if (account != null)
            {
                List<string> functions =
                    (new FunctionBO(this.dsaContext)).getListFunctionByGroup(account.IdDecentralizationGroup);
                string fullName = string.Empty;
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

                AccountSession accountSession = new AccountSession(
                    account.IdAccount,
                    account.UserName,
                    account.TypeAccount,
                    account.Avatar,
                    account.IdDecentralizationGroup,
                    fullName,
                    functions);
                this.Session["AccountSession"] = accountSession;
                this.Session["username"] = account.UserName;
                this.Session["avatar"] = account.Avatar;
                this.Session["idAccount"] = account.IdAccount;
                this.Session["idDecenTralizationGroup"] = account.IdDecentralizationGroup;
                return this.Json(
                    new { success = true, responseText = "Đăng nhập hệ thống thành công!" },
                    JsonRequestBehavior.DenyGet);
            }
            else
            {
                return
                    this.Json(
                        new
                            {
                                success = false,
                                responseText = "Nhập sai username hoặc password! Kiểm tra lại thông tin."
                            },
                        JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult Logout()
        {
            this.Session.Remove("AccountSession");
            return this.Redirect("TrangChu");
        }

        public ActionResult LogoutAjax()
        {
            try
            {
                this.Session.RemoveAll();
                return this.Json(
                    new { success = true, responseText = "Đăng xuất thành công!" },
                    JsonRequestBehavior.DenyGet);
            }
            catch
            {
                return
                    this.Json(
                        new { success = false, responseText = "Lỗi trong quá trình đăng xuất! Vui lòng thử lại." },
                        JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult Profile()
        {
            if (this.Session["AccountSession"] != null)
            {
                return this.View();
            }

            return this.Redirect("/QuanLy");
        }

        public ActionResult Subscrible(string mail)
        {
            if (!StringExtension.IsNullOrWhiteSpace(mail))
            {
                try
                {
                    bool isSuccess = this.accountBO.Subscrible(mail);
                    if (isSuccess)
                    {
                        return
                            this.Content("Đăng ký nhận mail thành công! Mọi tin tức mới sẽ được gửi qua mail cho bạn!");
                    }
                    else
                    {
                        return this.Content("Email không hợp lệ! Vui lòng kiểm tra lại thông tin.");
                    }
                }
                catch (Exception e)
                {
                    return this.Content(e.Message);
                }
            }
            else
            {
                return this.Content("Email không hợp lệ! Vui lòng kiểm tra lại thông tin.");
            }
        }
    }
}