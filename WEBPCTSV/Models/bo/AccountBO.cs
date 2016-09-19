namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Web.Mvc;

    using PagedList;

    using WEBPCTSV.Helpers.Common;
    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.Support;

    public class AccountBO
    {
        readonly DSAContext context = new DSAContext();

        private readonly DSAContext dsaContext = new DSAContext();

        readonly int rowInPage = new Configuration().rowInPage;

        public AccountBO()
        {
        }

        public AccountBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public int AddAccountStaff(string username)
        {
            Account account = new Account();
            account.UserName = username;
            account.Password = this.GetMD5(username);
            account.IsDelete = false;
            account.IdDecentralizationGroup = 1;
            account.TypeAccount = "NV";
            this.context.Accounts.Add(account);
            this.context.SaveChanges();
            return account.IdAccount;
        }

        // public int TotalPage(string search)
        // {
        // int toltalStudent = dsaContext.Accounts.Where(st => st.UserName.Contains(search) || (st.Students.FirstOrDefault().LastNameStudent + " " + st.Students.FirstOrDefault().FirstNameStudent).ToUpper().Trim().Contains(search.ToUpper().Trim())).ToList().Count;
        // return toltalStudent / rowInPage + 1;
        // }
        public int AddAccountSV(string studentNumber)
        {
            Account account = new Account();
            account.UserName = studentNumber;
            account.Password = this.GetMD5(studentNumber);
            account.IsDelete = false;
            account.IdDecentralizationGroup = 1;
            account.TypeAccount = "SV";
            this.context.Accounts.Add(account);
            this.context.SaveChanges();
            return account.IdAccount;
        }

        public void SaveAccount(int idAccount,string email)
        {
            Account account = this.dsaContext.Accounts.Single(a => a.IdAccount == idAccount);
            account.Email = email;
            this.dsaContext.SaveChanges();
        }

        public int AddAccountSV(Student student)
        {
            Account account = new Account();
            account.UserName = student.StudentNumber;
            account.Password = this.GetMD5(student.StudentNumber);
            account.Email = student.Email;
            account.IsDelete = false;
            account.IdDecentralizationGroup = 2;
            account.TypeAccount = "SV";
            this.context.Accounts.Add(account);
            this.context.SaveChanges();
            return account.IdAccount;
        }

        public void ChangeDecentralizationGroup(int idAccount, int idGroup)
        {
            Account account = this.dsaContext.Accounts.Single(a => a.IdAccount == idAccount);
            account.IdDecentralizationGroup = idGroup;
            this.dsaContext.SaveChanges();
        }

        public string ChangePassword(int idAccount, string oldPassword, string newPassword)
        {
            try
            {
                Account account = this.dsaContext.Accounts.Single(a => a.IdAccount == idAccount);
                string md5 = this.GetMD5(oldPassword);
                if (md5.Equals(account.Password))
                {
                    account.Password = this.GetMD5(newPassword);
                    this.dsaContext.SaveChanges();
                    return "Cập nhật mật khẩu thành công";
                }
                else
                {
                }
            }
            catch
            {
            }

            return "Cập nhật mật khẩu không thành công sai mật khẩu hoặc tài khoản";
        }

        public Account checkAuthorityAccount(string username, string password)
        {
            if (StringExtension.IsNullOrWhiteSpace(username) || StringExtension.IsNullOrWhiteSpace(password)
                || RegexUtils.CheckValidUserName(username))
            {
                return null;
            }
            else
            {
                string passwordInMD5 = this.GetMD5(password.Trim());
                return
                    this.dsaContext.Accounts.SingleOrDefault(
                        a => (a.UserName.Equals(username.Trim()) && a.Password.Equals(passwordInMD5)));
            }
        }

        public bool checkAuthorityFunction(Account account, string function)
        {
            return
                account.DecentralizationGroup.Decentralizations.Where(f => f.IdFunction.Equals(function))
                     .ToList()
                     .Count() != 0;
        }

        public Account GetAccount(int idAccount)
        {
            return this.dsaContext.Accounts.SingleOrDefault(a => a.IdAccount == idAccount);
        }

        public Account GetAccountByName(string username)
        {
            return this.context.Accounts.Single(a => a.UserName.Equals(username));
        }

        public IPagedList<Account> GetAccountPagedList(int? page)
        {
            IPagedList<Account> account = null;

            // Total item per page
            int pageSize = 5;
            int pageNumber = page ?? 1;
            account = this.dsaContext.Accounts.Where(a => a.IsDelete == false)
                .ToList()
                .ToPagedList(pageNumber, pageSize);
            return account;
        }

        public string GetAvatarAccount(int idAccount)
        {
            return this.context.Accounts.Single(a => a.IdAccount == idAccount).Avatar;
        }

        public List<Account> GetListAccount()
        {
            return this.dsaContext.Accounts.Where(a => a.IsDelete == false).ToList();
        }

        public List<Account> GetListAccount(int page)
        {
            return
                this.dsaContext.Accounts.OrderByDescending(st => st.IdAccount)
                    .Skip((page - 1) * this.rowInPage)
                    .Take(this.rowInPage)
                    .ToList();
        }

        public List<Account> GetListAccount(string search, int page)
        {
            return
                this.dsaContext.Accounts.Where(
                    st =>
                    st.UserName.Contains(search)
                    || (st.Students.FirstOrDefault().LastNameStudent + " "
                        + st.Students.FirstOrDefault().FirstNameStudent).ToUpper()
                           .Trim()
                           .Contains(search.ToUpper().Trim()))
                    .OrderByDescending(st => st.IdAccount)
                    .Skip((page - 1) * this.rowInPage)
                    .Take(this.rowInPage)
                    .ToList();
        }

        public List<Account> GetListAccount(int page, string search)
        {
            List<Account> listAccount = this.GetListSearchAccount(search);
            return
                listAccount.OrderByDescending(st => st.IdAccount)
                    .Skip((page - 1) * this.rowInPage)
                    .Take(this.rowInPage)
                    .ToList();
        }

        public List<Account> GetListSearchAccount(string search)
        {
            List<Account> listAccount = new List<Account>();
            if (!string.IsNullOrEmpty(search))
            {
                try
                {
                    // IEnumerable<Account> listAccountStaff = context.Accounts.Where(st =>  st.TypeAccount.Equals("NV"));
                    // listAccountStaff = listAccountStaff.Where(st => (st.Staffs.First().Name.Contains(search))).ToList();
                    // listAccount.AddRange(listAccountStaff);

                    // List<Account> listAccountStudent = context.Accounts.Where(st => (st.TypeAccount.Equals("SV"))).ToList();
                    // listAccountStudent = listAccountStudent.Where(st => (st.UserName.Contains(search))
                    // || st.Students.First().StudentNumber.Contains(search)
                    // || (st.Students.First().LastNameStudent + "" + st.Students.First().FirstNameStudent).ToUpper().Trim().Contains(search.ToUpper().Trim())
                    // ).ToList();
                    // listAccount.AddRange(listAccountStudent);

                    // List<Account> listAccountLecture = context.Accounts.Where(st => (st.TypeAccount.Equals("GV"))).ToList();
                    // listAccountStudent = listAccountStudent.Where(st => (st.UserName.Contains(search)) || (st.Lecturers.First().LastName + st.Lecturers.First().FirstName).ToUpper().Trim().Equals(search.ToUpper().Trim())).ToList();
                    // listAccount.AddRange(listAccountStudent);
                    listAccount =
                        this.context.Accounts.Where(a => a.UserName.Contains(search) || a.TypeAccount.Contains(search))
                            .ToList();
                }
                catch
                {
                }
            }
            else
            {
                listAccount = this.context.Accounts.ToList();
            }

            return listAccount;
        }

        public string GetMD5(string password)
        {
            string str_md5 = string.Empty;
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(password);
            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);
            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");
            }

            return str_md5;
        }

        public string GetTypeAccount(int idAccount)
        {
            return this.context.Accounts.SingleOrDefault(a => a.IdAccount == idAccount).TypeAccount;
        }

        public bool ResetPassword(int idAccount)
        {
            Account account = this.dsaContext.Accounts.SingleOrDefault(a => a.IdAccount == idAccount);
            if (account != null)
            {
                account.Password = this.GetMD5(account.UserName);
                this.dsaContext.SaveChanges();
                return true;
            }

            return false;
        }

        public void SetAvatarAccount(string link, int idAccount)
        {
            Account account = this.context.Accounts.Single(a => a.IdAccount == idAccount);
            account.Avatar = link;
            this.context.SaveChanges();
        }

        public bool Subscrible(string mail)
        {
            int index =
                (from i in this.dsaContext.EmailSubscriptions where i.Email.Equals(mail.Trim()) select i).ToList()
                    .Count();
            if (index != 0) throw new Exception("Email đã tồn tại! Vui lòng chọn email khác");
            else
            {
                EmailSubscription email = new EmailSubscription { Email = mail, SubscribedDate = DateTime.Now };
                this.dsaContext.EmailSubscriptions.Add(email);
                this.dsaContext.SaveChanges();
                return true;
            }
        }

        public int TotalPage()
        {
            int toltalStudent = this.dsaContext.Accounts.ToList().Count;
            return toltalStudent / this.rowInPage + 1;
        }

        public int TotalPage(string search)
        {
            List<Account> listAccount = this.GetListSearchAccount(search);
            int toltalStudent = listAccount.Count;
            return toltalStudent / this.rowInPage + 1;
        }

        public void UpdateAccount(FormCollection form)
        {
            int idAccount = Convert.ToInt32(form["idAccount"]);
            string email = form["email"];
            string username = form["username"];
            DateTime timelock = Convert.ToDateTime(form["locktime"]);
            Account account = this.dsaContext.Accounts.Single(a => a.IdAccount == idAccount);
            account.UserName = username;
            account.Email = email;
            account.TimeLock = timelock;
            this.dsaContext.SaveChanges();
        }
    }
}