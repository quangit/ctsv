using WEBPCTSV.Models.bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Controllers
{
    public class ManageMessageController : Controller
    {
        // GET: ManageMessage
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult SendMessage(FormCollection form)
        {
            new MessageBo().SendMessage(Convert.ToInt32(Session["idAccount"]), form);
            return RedirectToAction("MessageReceiveUnRead");
        }

        public ActionResult MessageReceiveUnRead(int page)
        {
            ViewBag.Message = new MessageBo().GetMessageReceiveUnReadByPage(Convert.ToInt32(Session["idAccount"]),page);
            ViewBag.pageNumber = new MessageBo().TotalPagenumberReceiveUnRead(Convert.ToInt32(Session["idAccount"]), page);
            return View("MessageUnRead");
        }
        public ActionResult MessageReceiveReaded(int page)
        {
            ViewBag.Message= new MessageBo().GetMessageReceiveReadedByPage(Convert.ToInt32(Session["idAccount"]),page);
            ViewBag.pageNumber = new MessageBo().TotalPagenumberReceiveReaded(Convert.ToInt32(Session["idAccount"]), page);
            return View("MessageReaded");
        }
        public ActionResult MessageSended(int page)
        {
            ViewBag.Message = new MessageBo().GetMessageSendByPage(Convert.ToInt32(Session["idAccount"]),page);
            ViewBag.pageNumber = new MessageBo().TotalPagenumberSend(Convert.ToInt32(Session["idAccount"]), page);
            return View("MessageSended");
        }

        public ActionResult ChangeIsReaded(int idMessage)
        {
            new MessageBo().ChangeIsReaded(idMessage);
            return Content("1", "text/plain");
        }
    }
}