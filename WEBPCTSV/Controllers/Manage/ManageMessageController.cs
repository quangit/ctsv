using WEBPCTSV.Models.bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;

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
            return RedirectToAction("MessageReceiveUnRead", new { page = 1 });
        }

        public ActionResult MessageReceiveUnRead(int page)
        {
            ViewBag.Message = new MessageBo().GetMessageReceiveUnReadByPage(Convert.ToInt32(Session["idAccount"]), page);
            ViewBag.pageNumber = new MessageBo().TotalPagenumberReceiveUnRead(Convert.ToInt32(Session["idAccount"]), page);
            return View("MessageUnRead");
        }

        public ActionResult GetCountMessageReceiveUnRead()
        {
            if (Session["numberMessagerUnread"] == null)
                Session["numberMessagerUnread"] = 0;
            int numberMessagerUnread = new MessageBo().GetCountMessageReceiveUnRead(Convert.ToInt32(Session["idAccount"]));
            return Content(numberMessagerUnread.ToString(), "text/plain");
        }

        public ActionResult GetLastMessageReceiveUnRead()
        {
            Message message = new MessageBo().GetLastMessageReceiveUnRead(Convert.ToInt32(Session["idAccount"]));
            if (message == null) return null;
            return Json(new { data = message.TextSummary, title = message.TitleMessage, image = message.AccountSender.Avatar });
        }


        public ActionResult MessageReceiveReaded(int page)
        {
            ViewBag.Message = new MessageBo().GetMessageReceiveReadedByPage(Convert.ToInt32(Session["idAccount"]), page);
            ViewBag.pageNumber = new MessageBo().TotalPagenumberReceiveReaded(Convert.ToInt32(Session["idAccount"]), page);
            return View("MessageReaded");
        }
        public ActionResult MessageSended(int page)
        {
            ViewBag.Message = new MessageBo().GetMessageSendByPage(Convert.ToInt32(Session["idAccount"]), page);
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