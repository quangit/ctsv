using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class RelativeBo
    {
        DSAContext context = new DSAContext();
        public void AddRelative(int idStudent ,FormCollection form)
        {
            if(form["relativeName"]!="")
            {
                Relative relative = new Relative();
                relative.IdStudent = idStudent;
                relative.relativeName = form["relativeName"];
                relative.RelativesLastName = form["lastNameRelative"];
                relative.RelativeFirstName = form["firstNameRelative"];
                if (form["selectWard_RelativeAddress"] != "") relative.IdWard = form["selectWard_RelativeAddress"];
                relative.AddtionalRelativesAddress = form["addtionalRelativesAddress"];
                if (form["cellPhoneRelative"] != "") relative.CellphoneNumberRelatives = Convert.ToDecimal(form["cellPhoneRelative"]);
                if (form["landlineRelative"] != "") relative.LandlineNumberRelatives = Convert.ToDecimal(form["landlineRelative"]);
                context.Relatives.Add(relative);
                context.SaveChanges();
            }
        }

        public void DeleteRelative(int idRelative)
        {
            Relative relative = context.Relatives.Single(r => r.IdRelatives == idRelative);
            context.Relatives.Remove(relative);
            context.SaveChanges();
        }
    }
}