namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class StaffBO
    {
        readonly DSAContext context = new DSAContext();

        private readonly DSAContext dsaContext;

        public StaffBO()
        {
        }

        public StaffBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public bool Delete(int idStaff)
        {
            try
            {
                Staff staff = this.context.Staffs.Single(s => s.IdStaff == idStaff);
                this.context.Staffs.Remove(staff);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public List<Staff> Get()
        {
            return this.context.Staffs.ToList();
        }

        public Staff GetByIdAccount(int idAccount)
        {
            try
            {
                Account account = this.context.Accounts.SingleOrDefault(a => a.IdAccount == idAccount);
                return account.Staffs.FirstOrDefault();
            }
            catch
            {
            }

            return null;
        }

        public string GetImage(int idStaff)
        {
            return this.context.Staffs.Single(s => s.IdStaff == idStaff).Image;
        }

        public List<Staff> getListStaff()
        {
            return this.dsaContext.Staffs.Where(s => !s.Position.Equals("Quản Trị")).ToList();
        }

        public bool Insert(FormCollection form)
        {
            try
            {
                Staff staff = new Staff();
                int idAccount = new AccountBO().AddAccountStaff(form["username"]);
                staff.Name = form["name"];
                if (form["IdentityCardNumber"] != string.Empty) staff.IdentityCardNumber = Convert.ToDecimal(form["IdentityCardNumber"]);
                if (form["phone"] != string.Empty) staff.PhoneNumber = form["phone"];
                staff.Email = form["email"];
                if (form["birthday"] != string.Empty) staff.Birthday = Convert.ToDateTime(form["birthday"]);
                staff.Address = form["address"];
                staff.Position = form["Position"];

                // staff.Image = form["Image"];
                staff.IdAccount = idAccount;
                this.context.Staffs.Add(staff);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public bool SetImage(int idStaff, string link)
        {
            try
            {
                Staff staff = this.context.Staffs.Single(s => s.IdStaff == idStaff);
                staff.Image = link;
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
                Staff staff = this.context.Staffs.Single(s => s.IdStaff == idStaff);
                staff.Name = form["name"];
                if (form["IdentityCardNumber"] != string.Empty) staff.IdentityCardNumber = Convert.ToDecimal(form["IdentityCardNumber"]);
                if (form["phone"] != string.Empty) staff.PhoneNumber = form["phone"];
                staff.Email = form["email"];
                if (form["birthday"] != string.Empty) staff.Birthday = Convert.ToDateTime(form["birthday"]);
                staff.Address = form["address"];
                staff.Position = form["Position"];
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