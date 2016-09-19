namespace WEBPCTSV.Helpers.Common
{
    using System.Text.RegularExpressions;

    public static class RegexUtils
    {
        public static bool CheckValidUserName(string username)
        {
            Regex regex = new Regex(@"^[\w\d]$");
            return regex.IsMatch(username);
        }
    }
}