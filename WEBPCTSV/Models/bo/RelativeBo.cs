namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class RelativeBo
    {
        readonly DSAContext context = new DSAContext();

        public void AddRelative(int idStudent, FormCollection form)
        {
            if (form["relativeName"] != string.Empty)
            {
                Relative relative = new Relative();
                relative.IdStudent = idStudent;
                relative.relativeName = form["relativeName"];
                relative.RelativesLastName = form["lastNameRelative"];
                relative.RelativeFirstName = form["firstNameRelative"];
                if (form["selectWard_RelativeAddress"] != string.Empty) relative.IdWard = form["selectWard_RelativeAddress"];
                relative.AddtionalRelativesAddress = form["addtionalRelativesAddress"];
                if (form["cellPhoneRelative"] != string.Empty) relative.CellphoneNumberRelatives = Convert.ToDecimal(form["cellPhoneRelative"]);
                if (form["landlineRelative"] != string.Empty) relative.LandlineNumberRelatives = Convert.ToDecimal(form["landlineRelative"]);
                this.context.Relatives.Add(relative);
                this.context.SaveChanges();
            }
        }

        public void DeleteRelative(int idRelative)
        {
            Relative relative = this.context.Relatives.Single(r => r.IdRelatives == idRelative);
            this.context.Relatives.Remove(relative);
            this.context.SaveChanges();
        }
    }
}