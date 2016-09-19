namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class StaffDepartmentOfStudentAffairBo
    {
        readonly DSAContext context = new DSAContext();

        public bool Delete(int idStaff)
        {
            try
            {
                StaffDepartmentOfStudentAffair staffDepartmentOfStudentAffair =
                    this.context.StaffDepartmentOfStudentAffairs.Single(
                        s => s.IdStaffDepartmentOfStudentAffairs == idStaff);
                this.context.StaffDepartmentOfStudentAffairs.Remove(staffDepartmentOfStudentAffair);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public List<StaffDepartmentOfStudentAffair> Get()
        {
            return this.context.StaffDepartmentOfStudentAffairs.ToList();
        }

        public bool Insert(FormCollection form)
        {
            try
            {
                StaffDepartmentOfStudentAffair staffDepartmentOfStudentAffair = new StaffDepartmentOfStudentAffair();
                int idAccount = new AccountBO().AddAccountStaff(form["username"]);
                staffDepartmentOfStudentAffair.StaffName = form["name"];
                if (form["IdentityCardNumber"] != string.Empty) staffDepartmentOfStudentAffair.IdentityCardNumber = Convert.ToDecimal(form["IdentityCardNumber"]);
                if (form["phone"] != string.Empty) staffDepartmentOfStudentAffair.PhoneNumber = Convert.ToDecimal(form["phone"]);
                staffDepartmentOfStudentAffair.Email = form["email"];
                if (form["birthday"] != string.Empty) staffDepartmentOfStudentAffair.Birthday = Convert.ToDateTime(form["birthday"]);
                staffDepartmentOfStudentAffair.Address = form["address"];
                staffDepartmentOfStudentAffair.IdAccount = idAccount;
                this.context.StaffDepartmentOfStudentAffairs.Add(staffDepartmentOfStudentAffair);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public bool Update(FormCollection form)
        {
            try
            {
                int idStaff = Convert.ToInt32(form["idStaff"]);
                StaffDepartmentOfStudentAffair staffDepartmentOfStudentAffair =
                    this.context.StaffDepartmentOfStudentAffairs.Single(
                        s => s.IdStaffDepartmentOfStudentAffairs == idStaff);
                staffDepartmentOfStudentAffair.StaffName = form["name"];
                if (form["IdentityCardNumber"] != string.Empty) staffDepartmentOfStudentAffair.IdentityCardNumber = Convert.ToDecimal(form["IdentityCardNumber"]);
                if (form["phone"] != string.Empty) staffDepartmentOfStudentAffair.PhoneNumber = Convert.ToDecimal(form["phone"]);
                staffDepartmentOfStudentAffair.Email = form["email"];
                if (form["birthday"] != string.Empty) staffDepartmentOfStudentAffair.Birthday = Convert.ToDateTime(form["birthday"]);
                staffDepartmentOfStudentAffair.Address = form["address"];
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