using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Helpers.Common;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class LecturerController : Controller
    {
        DSAContext dsaContext;
        LecturerBO lecturerBO;

        public LecturerController()
        {
            dsaContext = new DSAContext();
            lecturerBO = new LecturerBO(dsaContext);
        }

        public ActionResult LecturerList(int? page)
        {
            ViewBag.classes = dsaContext.Classes.ToList();
            ViewBag.semesters = from semester in dsaContext.Semesters select semester;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult LecturerList(int? page, FormCollection col)
        {
            string searchBox = col["SearchBox"];
            string searchSemester = col["SearchSemester"];
            int idSemester;
            try {
                idSemester = Int32.Parse(col["SearchSemester"]);
            } catch(Exception){
                idSemester = new SemesterBO(dsaContext).GetSemesterCurrent().IdSemester;
            }
            ViewBag.IdSemester = idSemester;
            return View("../PartialViews/PartialFilterLecturer", lecturerBO.GetListLecturer(page, searchBox, idSemester));
        }

        public ActionResult LecturerOperation()
        {
            return View();
        }
    }
}
