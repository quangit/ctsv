namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    public class SemesterController : Controller
    {
        // GET: Semester
        public ActionResult Index()
        {
            return this.View();
        }
    }
}