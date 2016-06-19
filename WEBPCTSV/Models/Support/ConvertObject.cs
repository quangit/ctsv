﻿using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WEBPCTSV.Models.Support
{
    public class ConvertObject
    {
        DSAContext context = new DSAContext();

        public static string GetPersonNamebyAccount(Account account)
        {
            try
            {
                if (account.TypeAccount.Equals("SV"))
                {
                    Student student = account.Students.First();
                    return (student.LastNameStudent + " " + student.FirstNameStudent);
                }
                if (account.TypeAccount.Equals("NV"))
                {
                    Staff staff = account.Staffs.First();
                    return staff.Name;
                }
                if (account.TypeAccount.Equals("GV"))
                {
                    Lecturer lecturer = account.Lecturers.First();
                    return (lecturer.LastName+" "+lecturer.FirstName);
                }
            }
            catch { }
            
            return "";
        }



        public static string RemoveHtmlTags(string text)
        {
            
            bool isRemove =false;
            var builder = new StringBuilder();
            for(int i=0;i<text.Length;i++)
            {
                if (text[i] == '<') isRemove = true;
                if (text[i] == '>') { isRemove = false; continue; }
                if (!isRemove) builder.Append(text[i]);
            }
            builder.Append("...");
            text= builder.ToString();
            return text;
        }

        public static string ConvertCurrency(string money)
        {
            try {
                int length = money.Length;
                int du = length % 3;
                for (int i = length - 1; i > 0; i--)
                {
                    if ((i % 3) == du)
                        money = money.Insert(i, ".");
                }
                return money;
            }
            catch { }
            return null;
        }

        public static string ConvertCurrencyToString(string currency)
        {
            try {
                return currency.Replace(".", "");
            }
            catch { }
            return null;
        }
    }
}