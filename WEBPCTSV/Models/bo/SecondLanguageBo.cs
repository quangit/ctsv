using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class SecondLanguageBo
    {
        DSAContext context = new DSAContext();
        public List<SecondLanguage> GetListSecondLanguage()
        {
            return context.SecondLanguages.ToList();
        }

        public bool UpdateSecondLanguage(string idLanguage, int idStudent)
        {
            SecondLanguageStudent secondLanguage = null;
            try
            {
                try
                {
                    secondLanguage = context.SecondLanguageStudents.Where(s => s.IdSecondLanguage.Equals(idLanguage) && s.IdStudent == idStudent).First();
                    if (secondLanguage != null)
                    {
                        context.SecondLanguageStudents.Remove(secondLanguage);
                        context.SaveChanges();
                        return true;
                    }
                }
                catch { }
                secondLanguage = new SecondLanguageStudent();
                secondLanguage.IdStudent = idStudent;
                secondLanguage.IdSecondLanguage = idLanguage;
                context.SecondLanguageStudents.Add(secondLanguage);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
            
        }

        public void Insert(FormCollection form)
        {
            SecondLanguage secondLanguage = new SecondLanguage();
            secondLanguage.SecondLanguageName = form["SecondLanguageName"];
            secondLanguage.IdSecondLanguage = form["IdSecondLanguage"];
            context.SecondLanguages.Add(secondLanguage);
            context.SaveChanges();
        }

        public void Update(string idSecondLanguage, FormCollection form)
        {
            SecondLanguage secondLanguage = context.SecondLanguages.Single(s=>s.IdSecondLanguage.Equals(idSecondLanguage));
            secondLanguage.SecondLanguageName = form["SecondLanguageName"];
            context.SaveChanges();
        }

        public void Delete(string idSecondLanguage)
        {
            SecondLanguage secondLanguage = context.SecondLanguages.Single(s => s.IdSecondLanguage.Equals(idSecondLanguage));
            context.SecondLanguages.Remove(secondLanguage);
            context.SaveChanges();
        }
    }
}