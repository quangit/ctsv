using WEBPCTSV.Models.bo;
using WEBPCTSV.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Controllers
{
    public class ManageStaffController : Controller
    {
        // GET: ManageStaff
        public ActionResult ListStaff()
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlynhanvien")) return RedirectToAction("NotAccess", "ManageDecentralization");
            ViewBag.listStaff = new StaffBO().Get();
            return View("ListStaff");
        }

        public ActionResult AddStaff(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlynhanvien")) return RedirectToAction("NotAccess", "ManageDecentralization");
            new StaffBO().Insert(form);
            return RedirectToAction("ListStaff");
        }

        public ActionResult UpdateStaff(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlynhanvien")) return RedirectToAction("NotAccess", "ManageDecentralization");
            new StaffBO().Update(form);
            return RedirectToAction("ListStaff");
        }
        public ActionResult UpdateStaffByPerson(FormCollection form)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlynhanvien")) return RedirectToAction("NotAccess", "ManageDecentralization");
            new StaffBO().Update(form);
            return RedirectToAction("InfoStaff");
        }

        public ActionResult DeleteStaff(int idStaff)
        {
            if (!CheckDecentralization.Check(Convert.ToInt32(Session["idDecenTralizationGroup"]), "quanlynhanvien")) return RedirectToAction("NotAccess", "ManageDecentralization");
            new StaffBO().Delete(idStaff);
            return RedirectToAction("ListStaff");
        }

        public ActionResult InfoStaff()
        {
            ViewBag.staff = new StaffBO().GetByIdAccount(Convert.ToInt32(Session["idAccount"]));
            return View("InfoStaff");
        }

    }
}