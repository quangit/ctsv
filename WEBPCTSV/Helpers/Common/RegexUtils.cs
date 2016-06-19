using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WEBPCTSV.Helpers.Common
{
    public static class RegexUtils
    {
        public static bool CheckValidUserName(string username)
        {
            Regex regex = new Regex(@"^[\w\d]$");
            return regex.IsMatch(username);
        }
    }
}