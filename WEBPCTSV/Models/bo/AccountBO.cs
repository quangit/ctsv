using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Helpers.Common;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.Support;

namespace WEBPCTSV.Models.bo
{
    public class AccountBO
    {
        private DSAContext dsaContext = new DSAContext();
        DSAContext context = new DSAContext();
        int rowInPage = new Configuration().rowInPage;

        public AccountBO()
        { }
        public AccountBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }
        public IPagedList<Account> GetAccountPagedList(int? page)
        {
            IPagedList<Account> account = null;
            // Total item per page
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            account = dsaContext.Accounts.Where(a => a.IsDelete == false).ToList().ToPagedList(pageNumber, pageSize);
            return account;
        }

        public bool checkAuthorityFunction(Account account, string function)
        {
            return (account.DecentralizationGroup.Decentralizations.Where(f => f.IdFunction.Equals(function)).ToList().Count() != 0);
        }

        public Account checkAuthorityAccount(string username, string password)
        {
            if (StringExtension.IsNullOrWhiteSpace(username) || StringExtension.IsNullOrWhiteSpace(password) || RegexUtils.CheckValidUserName(username))
            {
                return null;
            }
            else
            {
                string passwordInMD5 = GetMD5(password.Trim());
                return dsaContext.Accounts.SingleOrDefault(a => (a.UserName.Equals(username.Trim()) && a.Password.Equals(passwordInMD5)));
            }
        }

        public Account GetAccount(int idAccount)
        {
            return dsaContext.Accounts.SingleOrDefault(a => a.IdAccount == idAccount);
        }

        public string GetTypeAccount(int idAccount)
        {
            return context.Accounts.SingleOrDefault(a => a.IdAccount == idAccount).TypeAccount;
        }

        public bool ResetPassword(int idAccount)
        {
            Account account = dsaContext.Accounts.SingleOrDefault(a => a.IdAccount == idAccount);
            if (account != null)
            {
                account.Password = GetMD5(account.UserName);
                dsaContext.SaveChanges();
                return true;
            }
            return false;
        }
        public List<Account> GetListAccount()
        {
            return dsaContext.Accounts.Where(a => a.IsDelete == false).ToList();
        }

        public List<Account> GetListAccount(int page)
        {
            return dsaContext.Accounts.OrderByDescending(st => st.IdAccount).Skip((page - 1) * rowInPage).Take(rowInPage).ToList();
        }

        public List<Account> GetListAccount(string search, int page)
        {
            return dsaContext.Accounts.Where(st => st.UserName.Contains(search) || (st.Students.FirstOrDefault().LastNameStudent + " " + st.Students.FirstOrDefault().FirstNameStudent).ToUpper().Trim().Contains(search.ToUpper().Trim()))
                .OrderByDescending(st => st.IdAccount).Skip((page - 1) * rowInPage).Take(rowInPage).ToList();
        }

        public int TotalPage()
        {
            int toltalStudent = dsaContext.Accounts.ToList().Count;
            return toltalStudent / rowInPage + 1;
        }

        //public int TotalPage(string search)
        //{
        //    int toltalStudent = dsaContext.Accounts.Where(st => st.UserName.Contains(search) || (st.Students.FirstOrDefault().LastNameStudent + " " + st.Students.FirstOrDefault().FirstNameStudent).ToUpper().Trim().Contains(search.ToUpper().Trim())).ToList().Count;
        //    return toltalStudent / rowInPage + 1;
        //}

        public int AddAccountSV(string studentNumber)
        {
            Account account = new Account();
            account.UserName = studentNumber;
            account.Password = GetMD5(studentNumber);
            account.IsDelete = false;
            account.IdDecentralizationGroup = 1;
            account.TypeAccount = "SV";
            context.Accounts.Add(account);
            context.SaveChanges();
            return account.IdAccount;
        }

        public int AddAccountSV(Student student)
        {
            Account account = new Account();
            account.UserName = student.StudentNumber;
            account.Password = GetMD5(student.StudentNumber);
            account.Email = student.Email;
            account.IsDelete = false;
            account.IdDecentralizationGroup = 2;
            account.TypeAccount = "SV";
            context.Accounts.Add(account);
            context.SaveChanges();
            return account.IdAccount;
        }

        public string GetMD5(string password)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(password);
            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);
            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");
            }
            return str_md5;
        }

        public void ChangeDecentralizationGroup(int idAccount, int idGroup)
        {
            Account account = dsaContext.Accounts.Single(a => a.IdAccount == idAccount);
            account.IdDecentralizationGroup = idGroup;
            dsaContext.SaveChanges();
        }

        public string ChangePassword(int idAccount, string oldPassword, string newPassword)
        {
            try
            {
                Account account = dsaContext.Accounts.Single(a => a.IdAccount == idAccount);
                string md5 = GetMD5(oldPassword);
                if (md5.Equals(account.Password))
                {
                    account.Password = GetMD5(newPassword);
                    dsaContext.SaveChanges();
                    return "Cập nhật mật khẩu thành công";
                }
                else
                {

                }
            }
            catch { }

            return "Cập nhật mật khẩu không thành công sai mật khẩu hoặc tài khoản";

        }

        public void UpdateAccount(FormCollection form)
        {
            int idAccount = Convert.ToInt32(form["idAccount"]);
            string email = form["email"];
            string username = form["username"];
            DateTime timelock = Convert.ToDateTime(form["locktime"]);
            Account account = dsaContext.Accounts.Single(a => a.IdAccount == idAccount);
            account.UserName = username;
            account.Email = email;
            account.TimeLock = timelock;
            dsaContext.SaveChanges();
        }
        public bool Subscrible(string mail)
        {
            int index = (from i in dsaContext.EmailSubscriptions where i.Email.Equals(mail.Trim()) select i).ToList().Count();
            if (index != 0)
                throw new Exception("Email đã tồn tại! Vui lòng chọn email khác");
            else
            {
                EmailSubscription email = new EmailSubscription
                {
                    Email = mail,
                    SubscribedDate = DateTime.Now
                };
                dsaContext.EmailSubscriptions.Add(email);
                dsaContext.SaveChanges();
                return true;
            }
        }
        public Account GetAccountByName(string username)
        {
            return context.Accounts.Single(a => a.UserName.Equals(username));
        }


        public List<Account> GetListAccount(int page, string search)
        {
            List<Account> listAccount = GetListSearchAccount(search);
            return listAccount.OrderByDescending(st => st.IdAccount).Skip((page - 1) * rowInPage).Take(rowInPage).ToList();
        }

        public List<Account> GetListSearchAccount(string search)
        {
            List<Account> listAccount = new List<Account>();
            if (!string.IsNullOrEmpty(search))
            {
                try
                {

                    //IEnumerable<Account> listAccountStaff = context.Accounts.Where(st =>  st.TypeAccount.Equals("NV"));
                    //listAccountStaff = listAccountStaff.Where(st => (st.Staffs.First().Name.Contains(search))).ToList();
                    //listAccount.AddRange(listAccountStaff);

                //    List<Account> listAccountStudent = context.Accounts.Where(st => (st.TypeAccount.Equals("SV"))).ToList();
                //    listAccountStudent = listAccountStudent.Where(st => (st.UserName.Contains(search))
                //        || st.Students.First().StudentNumber.Contains(search)
                //|| (st.Students.First().LastNameStudent + "" + st.Students.First().FirstNameStudent).ToUpper().Trim().Contains(search.ToUpper().Trim())
                //        ).ToList();
                //    listAccount.AddRange(listAccountStudent);

                //    List<Account> listAccountLecture = context.Accounts.Where(st => (st.TypeAccount.Equals("GV"))).ToList();
                //    listAccountStudent = listAccountStudent.Where(st => (st.UserName.Contains(search)) || (st.Lecturers.First().LastName + st.Lecturers.First().FirstName).ToUpper().Trim().Equals(search.ToUpper().Trim())).ToList();
                //    listAccount.AddRange(listAccountStudent);
                    listAccount = context.Accounts.Where(a=>a.UserName.Contains(search)||a.TypeAccount.Contains(search)).ToList();

                }
                catch { }

            }
            else
            {
                listAccount = context.Accounts.ToList();
            }
            return listAccount;
        }

        public void SetAvatarAccount(string link, int idAccount)
        {
            Account account = context.Accounts.Single(a => a.IdAccount == idAccount);
            account.Avatar = link;
            context.SaveChanges();
        }
        public string GetAvatarAccount(int idAccount)
        {
            return context.Accounts.Single(a => a.IdAccount == idAccount).Avatar;
        }

        public int TotalPage(string search)
        {
            List<Account> listAccount = GetListSearchAccount(search);
            int toltalStudent = listAccount.Count;
            return toltalStudent / rowInPage + 1;
        }


        public int AddAccountStaff(string username)
        {
            Account account = new Account();
            account.UserName = username;
            account.Password = GetMD5(username);
            account.IsDelete = false;
            account.IdDecentralizationGroup = 1;
            account.TypeAccount = "NV";
            context.Accounts.Add(account);
            context.SaveChanges();
            return account.IdAccount;
        }




    }
}