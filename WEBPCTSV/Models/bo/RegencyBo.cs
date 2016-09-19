namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class RegencyBo
    {
        readonly DSAContext context = new DSAContext();

        public void Delete(string idRegency)
        {
            Regency regency = this.context.Regencies.Single(r => r.IdRegency.Equals(idRegency));
            this.context.Regencies.Remove(regency);
            this.context.SaveChanges();
        }

        public List<Regency> GetListRegency()
        {
            return this.context.Regencies.ToList();
        }

        public void Insert(FormCollection form)
        {
            Regency regency = new Regency();
            regency.IdRegency = form["IdRegency"];
            regency.RegencyName = form["RegencyName"];
            if (form["PriorityPoint"] != string.Empty) regency.PriorityPoint = Convert.ToInt32(form["PriorityPoint"]);
            if (form["PlusPoint"] != string.Empty) regency.PlusPoint = (float)Convert.ToDouble(form["PlusPoint"]);
            this.context.Regencies.Add(regency);
            this.context.SaveChanges();
        }

        public void Update(string idRegency, FormCollection form)
        {
            Regency regency = this.context.Regencies.Single(r => r.IdRegency.Equals(idRegency));
            regency.RegencyName = form["RegencyName"];
            if (form["PriorityPoint"] != string.Empty) regency.PriorityPoint = Convert.ToInt32(form["PriorityPoint"]);
            if (form["PlusPoint"] != string.Empty) regency.PlusPoint = (float)Convert.ToDouble(form["PlusPoint"]);
            this.context.SaveChanges();
        }

        public bool UpdateRegencyStudent(string idRegency, int idStudent)
        {
            try
            {
                RegencyStudent regencyStudent = null;
                try
                {
                    regencyStudent =
                        this.context.RegencyStudents.Where(
                            r => r.IdRegency.Equals(idRegency) && r.IdStudent == idStudent).First();
                    if (regencyStudent != null)
                    {
                        this.context.RegencyStudents.Remove(regencyStudent);
                        this.context.SaveChanges();
                        return true;
                    }
                }
                catch
                {
                }

                regencyStudent = new RegencyStudent();
                regencyStudent.IdStudent = idStudent;
                regencyStudent.IdRegency = idRegency;
                this.context.RegencyStudents.Add(regencyStudent);
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