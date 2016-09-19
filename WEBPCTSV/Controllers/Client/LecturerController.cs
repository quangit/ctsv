namespace WEBPCTSV.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class LecturerController : Controller
    {
        readonly DSAContext dsaContext;

        readonly LecturerBO lecturerBO;

        public LecturerController()
        {
            this.dsaContext = new DSAContext();
            this.lecturerBO = new LecturerBO(this.dsaContext);
        }

        public ActionResult LecturerList(int? page)
        {
            this.ViewBag.classes = this.dsaContext.Classes.ToList();
            this.ViewBag.semesters = from semester in this.dsaContext.Semesters select semester;
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult LecturerList(int? page, FormCollection col)
        {
            string searchBox = col["SearchBox"];
            string searchSemester = col["SearchSemester"];
            int idSemester;
            try
            {
                idSemester = int.Parse(col["SearchSemester"]);
            }
            catch (Exception)
            {
                idSemester = new SemesterBO(this.dsaContext).GetSemesterCurrent().IdSemester;
            }

            this.ViewBag.IdSemester = idSemester;
            return this.View(
                "../PartialViews/PartialFilterLecturer",
                this.lecturerBO.GetListLecturer(page, searchBox, idSemester));
        }

        public ActionResult LecturerOperation()
        {
            return this.View();
        }
    }
}