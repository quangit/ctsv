using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class RegencyBo
    {
        DSAContext context = new DSAContext();

        public List<Regency> GetListRegency()
        {
            return context.Regencies.ToList();
        }

        public bool UpdateRegencyStudent(string idRegency,int idStudent)
        {
            try
            {
                RegencyStudent regencyStudent = null;
                try
                {
                    regencyStudent = context.RegencyStudents.Where(r => r.IdRegency.Equals(idRegency) && r.IdStudent == idStudent).First();
                    if(regencyStudent!=null)
                    {
                        context.RegencyStudents.Remove(regencyStudent);
                        context.SaveChanges();
                        return true;
                    }
                }
                catch { }
                regencyStudent = new RegencyStudent();
                regencyStudent.IdStudent = idStudent;
                regencyStudent.IdRegency = idRegency;
                context.RegencyStudents.Add(regencyStudent);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public void Insert(FormCollection form)
        {
            Regency regency = new Regency();
            regency.IdRegency = form["IdRegency"];
            regency.RegencyName = form["RegencyName"];
            if (form["PriorityPoint"] != "") regency.PriorityPoint = Convert.ToInt32(form["PriorityPoint"]);
            if (form["PlusPoint"]!="") regency.PlusPoint = (float)Convert.ToDouble(form["PlusPoint"]);
            context.Regencies.Add(regency);
            context.SaveChanges();
        }

        public void Update(string idRegency,FormCollection form)
        {
            Regency regency = context.Regencies.Single(r => r.IdRegency.Equals(idRegency));
            regency.RegencyName = form["RegencyName"];
            if (form["PriorityPoint"] != "") regency.PriorityPoint = Convert.ToInt32(form["PriorityPoint"]);
            if (form["PlusPoint"] != "") regency.PlusPoint = (float)Convert.ToDouble(form["PlusPoint"]);
            context.SaveChanges();
        }

        public void Delete(string idRegency)
        {
            Regency regency = context.Regencies.Single(r => r.IdRegency.Equals(idRegency));
            context.Regencies.Remove(regency);
            context.SaveChanges();
        }

    }
}