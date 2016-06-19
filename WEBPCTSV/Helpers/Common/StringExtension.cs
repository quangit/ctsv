using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WEBPCTSV.Helpers.Common
{
    public static class StringExtension
    {

        //// str - the source string
        //// index- the start location to replace at (0-based)
        //// length - the number of characters to be removed before inserting
        //// replace - the string that is replacing characters
        public static bool IsNullOrWhiteSpace(string text)
        {
            if (text == null)
            {
                return true;
            }
            return string.IsNullOrWhiteSpace(text.Trim());
        }
        public static string ReplaceAt(this string str, int index, int length, string replace)
        {
            return str.Remove(index, Math.Min(length, str.Length - index))
                    .Insert(index, replace);
        }

        public static string RemoveIncorrectSpace(this string str)
        {
            str = str.Trim();
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\s{2,}", " ");
            return str;
        }


        // To get part of full name: last name, first name
        public static string[] GetPartOfFullName(string fullName)
        {
            if (fullName != null)
            {
                fullName = RemoveIncorrectSpace(fullName);
                fullName = ReplaceAt(fullName, fullName.LastIndexOf(" "), 1, ";");
                return (fullName.Split(';'));
            }
            return null;
        }

        // convert to unsign text
        public static string ConvertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        // Generate username from first name and last name
        public static string GenerateUsernameByName(string firstName, string lastName)
        {
            string result = "";
            lastName = ConvertToUnSign(lastName);
            firstName = ConvertToUnSign(firstName);
            string[] arrWordOfName = lastName.Split(' ');
            foreach (string part in arrWordOfName)
            {
                result += part[0];
            }
            result += firstName;
            return result.ToLower();
        }

        // Return string with limit word
        public static string GetLimitSubString(string text, int count)
        {
            string[] arrWord = text.Split(' ');
            if (arrWord.Count() < count)
            {
                return text;
            }
            else
            {
                string result = "";
                for (int i = 0; i < count; i++)
                {
                    result += arrWord[i] + " ";
                }
                return result;
            }
        }

        // tostring of Degree and academic title
        public static string GeneratePrefixLecturer(string academicTitle, string degree, string type)
        {
            string result = "";
            if (!string.IsNullOrEmpty(academicTitle))
            {
                result += (academicTitle + ".");
            }
            if (!string.IsNullOrEmpty(degree))
            {
                result += (degree + ".");
            }
            if (!string.IsNullOrEmpty(type))
            {
                result += (type + ".");
            }
            return result;
        }

        public static string getFullName(string firstName, string lastName)
        {
            return lastName + " " + firstName;
        }

        // Convert datetime in string to datetime
        // CultureInfo.InvariantCulture Not all cultures use the same format for dates and decimal / currency values. 
        public static DateTime ConvertStringToDateTime(string dateTimeStr, string formatStr)
        {
            return DateTime.ParseExact(dateTimeStr, formatStr, CultureInfo.InvariantCulture); ;
        }

        public static string GetListNumbering(string indexItem)
        {
            if (indexItem.Length == 2)
            {
                return IntToRoman(Int32.Parse(indexItem))+".";
            }
            if (indexItem.Length == 4)
            {
                return Int32.Parse(indexItem.Substring(2))+".";
            }
            if (indexItem.Length == 6)
            {
                return "  -";
            }
            if (indexItem.Length == 8)
            {
                return "    +";
            }
            return "";
        }
        public static string IntToRoman(int num)
        {
            string[] thou = { "", "M", "MM", "MMM" };
            string[] hun = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
            string[] ten = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
            string[] ones = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
            string roman = "";
            roman += thou[(int)(num / 1000) % 10];
            roman += hun[(int)(num / 100) % 10];
            roman += ten[(int)(num / 10) % 10];
            roman += ones[num % 10];

            return roman;
        }

        public static string GetMD5(string password)
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
    }
}