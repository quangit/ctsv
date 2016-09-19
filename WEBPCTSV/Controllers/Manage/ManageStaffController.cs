namespace WEBPCTSV.Controllers
{
    using System;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bo;
    using WEBPCTSV.Models.Support;

    public class ManageStaffController : Controller
    {
        public ActionResult AddStaff(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlynhanvien")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            new StaffBO().Insert(form);
            return this.RedirectToAction("ListStaff");
        }

        public ActionResult DeleteStaff(int idStaff)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlynhanvien")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            new StaffBO().Delete(idStaff);
            return this.RedirectToAction("ListStaff");
        }

        public ActionResult InfoStaff()
        {
            this.ViewBag.staff = new StaffBO().GetByIdAccount(Convert.ToInt32(this.Session["idAccount"]));
            return this.View("InfoStaff");
        }

        // GET: ManageStaff
        public ActionResult ListStaff()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlynhanvien")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            this.ViewBag.listStaff = new StaffBO().Get();
            return this.View("ListStaff");
        }

        public ActionResult UpdateStaff(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlynhanvien")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            new StaffBO().Update(form);
            return this.RedirectToAction("ListStaff");
        }

        public ActionResult UpdateStaffByPerson(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(this.Session["idDecenTralizationGroup"]), "quanlynhanvien")) return this.RedirectToAction("NotAccess", "ManageDecentralization");
            new StaffBO().Update(form);
            return this.RedirectToAction("InfoStaff");
        }
    }
}