namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class SecondLanguageBo
    {
        readonly DSAContext context = new DSAContext();

        public void Delete(string idSecondLanguage)
        {
            SecondLanguage secondLanguage =
                this.context.SecondLanguages.Single(s => s.IdSecondLanguage.Equals(idSecondLanguage));
            this.context.SecondLanguages.Remove(secondLanguage);
            this.context.SaveChanges();
        }

        public List<SecondLanguage> GetListSecondLanguage()
        {
            return this.context.SecondLanguages.ToList();
        }

        public void Insert(FormCollection form)
        {
            SecondLanguage secondLanguage = new SecondLanguage();
            secondLanguage.SecondLanguageName = form["SecondLanguageName"];
            secondLanguage.IdSecondLanguage = form["IdSecondLanguage"];
            this.context.SecondLanguages.Add(secondLanguage);
            this.context.SaveChanges();
        }

        public void Update(string idSecondLanguage, FormCollection form)
        {
            SecondLanguage secondLanguage =
                this.context.SecondLanguages.Single(s => s.IdSecondLanguage.Equals(idSecondLanguage));
            secondLanguage.SecondLanguageName = form["SecondLanguageName"];
            this.context.SaveChanges();
        }

        public bool UpdateSecondLanguage(string idLanguage, int idStudent)
        {
            SecondLanguageStudent secondLanguage = null;
            try
            {
                try
                {
                    secondLanguage =
                        this.context.SecondLanguageStudents.Where(
                            s => s.IdSecondLanguage.Equals(idLanguage) && s.IdStudent == idStudent).First();
                    if (secondLanguage != null)
                    {
                        this.context.SecondLanguageStudents.Remove(secondLanguage);
                        this.context.SaveChanges();
                        return true;
                    }
                }
                catch
                {
                }

                secondLanguage = new SecondLanguageStudent();
                secondLanguage.IdStudent = idStudent;
                secondLanguage.IdSecondLanguage = idLanguage;
                this.context.SecondLanguageStudents.Add(secondLanguage);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }
    }
}