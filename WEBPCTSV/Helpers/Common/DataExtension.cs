using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Helpers.Common
{
    public static class DataExtension
    {
        public static string GetGradeEvaluated(int point)
        {
            if (point >= 90)
            {
                return "Xuất sắc";
            }
            else if (80 <= point && point < 90)
            {
                return "Giỏi";
            }
            else if (65 <= point && point < 80)
            {
                return "Khá";
            }
            else if (50 <= point && point < 65)
            {
                return "Trung bình";
            }
            else if (35 <= point && point < 50)
            {
                return "Yếu";
            }
            else
            {
                return "Kém";
            }
        }
        public static string GetGradeEvaluatedEN(int point)
        {
            if (point >= 90)
            {
                return "Excellent";
            }
            else if (80 <= point && point < 90)
            {
                return " Very good";
            }
            else if (65 <= point && point < 80)
            {
                return "Good";
            }
            else if (50 <= point && point < 65)
            {
                return "Average";
            }
            else if (35 <= point && point < 50)
            {
                return "Weak";
            }
            else
            {
                return "Least";
            }
        }
    }
}