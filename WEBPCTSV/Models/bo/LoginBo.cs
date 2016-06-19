using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class LoginBo
    {
        DSAContext context = new DSAContext();

        public bool Login(string username , string password)
        {
            string pass = context.Accounts.Where(ac => ac.UserName.Equals(username)).Single().Password;
            string passMD5 = GetMD5(password);
            if (pass.Equals(passMD5)) { return true; }
            else {return false;}
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

        public Account GetInforAccount(string username)
        {
            Account account = context.Accounts.Single(ac => ac.UserName.Equals(username));
            return account;
        }
    }
}