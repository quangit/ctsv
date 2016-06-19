using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class StaffDepartmentOfStudentAffairBo
    {
        DSAContext context = new DSAContext();
        public bool Insert(FormCollection form)
        {
            try
            {
                StaffDepartmentOfStudentAffair staffDepartmentOfStudentAffair = new StaffDepartmentOfStudentAffair();
                int idAccount =  new AccountBO().AddAccountStaff(form["username"]);
                staffDepartmentOfStudentAffair.StaffName = form["name"];
                if (form["IdentityCardNumber"]!="") staffDepartmentOfStudentAffair.IdentityCardNumber = Convert.ToDecimal(form["IdentityCardNumber"]);
                if (form["phone"] != "") staffDepartmentOfStudentAffair.PhoneNumber = Convert.ToDecimal(form["phone"]);
                staffDepartmentOfStudentAffair.Email = form["email"];
                if (form["birthday"] != "") staffDepartmentOfStudentAffair.Birthday = Convert.ToDateTime(form["birthday"]);
                staffDepartmentOfStudentAffair.Address = form["address"];
                staffDepartmentOfStudentAffair.IdAccount = idAccount;
                context.StaffDepartmentOfStudentAffairs.Add(staffDepartmentOfStudentAffair);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public bool Update(FormCollection form)
        {
            try
            {
                int idStaff = Convert.ToInt32(form["idStaff"]);
                StaffDepartmentOfStudentAffair staffDepartmentOfStudentAffair = context.StaffDepartmentOfStudentAffairs.Single(s => s.IdStaffDepartmentOfStudentAffairs == idStaff);                
                staffDepartmentOfStudentAffair.StaffName = form["name"];
                if (form["IdentityCardNumber"] != "") staffDepartmentOfStudentAffair.IdentityCardNumber = Convert.ToDecimal(form["IdentityCardNumber"]);
                if (form["phone"] != "") staffDepartmentOfStudentAffair.PhoneNumber = Convert.ToDecimal(form["phone"]);
                staffDepartmentOfStudentAffair.Email = form["email"];
                if (form["birthday"] != "") staffDepartmentOfStudentAffair.Birthday = Convert.ToDateTime(form["birthday"]);
                staffDepartmentOfStudentAffair.Address = form["address"];
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }
        public bool Delete(int idStaff)
        {
            try {
                StaffDepartmentOfStudentAffair staffDepartmentOfStudentAffair = context.StaffDepartmentOfStudentAffairs.Single(s => s.IdStaffDepartmentOfStudentAffairs == idStaff);
                context.StaffDepartmentOfStudentAffairs.Remove(staffDepartmentOfStudentAffair);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public List<StaffDepartmentOfStudentAffair> Get()
        {
            return context.StaffDepartmentOfStudentAffairs.ToList();
        }
    }
}