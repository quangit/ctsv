namespace WEBPCTSV.Models.bo
{
    using System.Linq;
    using System.Security.Cryptography;

    using WEBPCTSV.Models.bean;

    public class LoginBo
    {
        readonly DSAContext context = new DSAContext();

        public Account GetInforAccount(string username)
        {
            Account account = this.context.Accounts.Single(ac => ac.UserName.Equals(username));
            return account;
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

        public bool Login(string username, string password)
        {
            string pass = this.context.Accounts.Where(ac => ac.UserName.Equals(username)).Single().Password;
            string passMD5 = this.GetMD5(password);
            if (pass.Equals(passMD5))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}