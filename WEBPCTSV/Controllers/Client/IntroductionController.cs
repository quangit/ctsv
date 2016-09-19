namespace WEBPCTSV.Controllers
{
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class IntroductionController : Controller
    {
        readonly DSAContext dsaContext;

        public IntroductionController()
        {
            this.dsaContext = new DSAContext();
        }

        public ActionResult FunctionAndDuty()
        {
            return this.View();
        }

        public ActionResult HumanResource()
        {
            StaffBO staffBO = new StaffBO(this.dsaContext);
            return this.View(staffBO.getListStaff());
        }
    }
}