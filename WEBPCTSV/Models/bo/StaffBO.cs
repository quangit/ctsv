using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Models.bo
{
    public class StaffBO
    {
        private DSAContext dsaContext;
        DSAContext context = new DSAContext();

        public StaffBO()
        {
 
        }
        public StaffBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public List<Staff> getListStaff()
        {
            return dsaContext.Staffs.Where(s => !s.Position.Equals("Quản Trị")).ToList();
        }


        public bool Insert(FormCollection form)
        {
            try
            {
                Staff staff = new Staff();
                int idAccount = new AccountBO().AddAccountStaff(form["username"]);
                staff.Name = form["name"];
                if (form["IdentityCardNumber"] != "") staff.IdentityCardNumber = Convert.ToDecimal(form["IdentityCardNumber"]);
                if (form["phone"] != "") staff.PhoneNumber = form["phone"];
                staff.Email = form["email"];
                if (form["birthday"] != "") staff.Birthday = Convert.ToDateTime(form["birthday"]);
                staff.Address = form["address"];
                staff.Position = form["Position"];
                //staff.Image = form["Image"];
                staff.IdAccount = idAccount;
                context.Staffs.Add(staff);
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
                Staff staff = context.Staffs.Single(s => s.IdStaff == idStaff);
                staff.Name = form["name"];
                if (form["IdentityCardNumber"] != "") staff.IdentityCardNumber = Convert.ToDecimal(form["IdentityCardNumber"]);
                if (form["phone"] != "") staff.PhoneNumber = form["phone"];
                staff.Email = form["email"];
                if (form["birthday"] != "") staff.Birthday = Convert.ToDateTime(form["birthday"]);
                staff.Address = form["address"];
                staff.Position = form["Position"];
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }
        public bool Delete(int idStaff)
        {
            try
            {
                Staff staff = context.Staffs.Single(s => s.IdStaff == idStaff);
                context.Staffs.Remove(staff);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public List<Staff> Get()
        {
            return context.Staffs.ToList();
        }
        public Staff GetByIdAccount(int idAccount)
        {
            try
            {
                Account account = context.Accounts.SingleOrDefault(a => a.IdAccount == idAccount);
                return account.Staffs.FirstOrDefault();
            }
            catch { }
            return null;
        }

        public string GetImage(int idStaff)
        {
            return context.Staffs.Single(s => s.IdStaff == idStaff).Image;
        }
        public bool SetImage(int idStaff, string link)
        {
            try
            {
                Staff staff = context.Staffs.Single(s => s.IdStaff == idStaff);
                staff.Image = link;
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }
    }
}