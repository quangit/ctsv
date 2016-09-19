namespace WEBPCTSV.Controllers
{
    using System;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageMessageController : Controller
    {
        public ActionResult ChangeIsReaded(int idMessage)
        {
            new MessageBo().ChangeIsReaded(idMessage);
            return this.Content("1", "text/plain");
        }

        public ActionResult GetCountMessageReceiveUnRead()
        {
            if (this.Session["numberMessagerUnread"] == null) this.Session["numberMessagerUnread"] = 0;
            int numberMessagerUnread =
                new MessageBo().GetCountMessageReceiveUnRead(Convert.ToInt32(this.Session["idAccount"]));
            return this.Content(numberMessagerUnread.ToString(), "text/plain");
        }

        public ActionResult GetLastMessageReceiveUnRead()
        {
            Message message = new MessageBo().GetLastMessageReceiveUnRead(Convert.ToInt32(this.Session["idAccount"]));
            if (message == null) return null;
            return
                this.Json(
                    new
                        {
                            data = message.TextSummary,
                            title = message.TitleMessage,
                            image = message.AccountSender.Avatar
                        });
        }

        // GET: ManageMessage
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult MessageReceiveReaded(int page)
        {
            if (this.Session["idAccount"] == null) return this.RedirectToAction("Index", "Home");
            this.ViewBag.Message =
                new MessageBo().GetMessageReceiveReadedByPage(Convert.ToInt32(this.Session["idAccount"]), page);
            this.ViewBag.pageNumber =
                new MessageBo().TotalPagenumberReceiveReaded(Convert.ToInt32(this.Session["idAccount"]), page);
            return this.View("MessageReaded");
        }

        public ActionResult MessageReceiveUnRead(int page)
        {
            if (this.Session["idAccount"] == null) return this.RedirectToAction("Index", "Home");
            this.ViewBag.Message =
                new MessageBo().GetMessageReceiveUnReadByPage(Convert.ToInt32(this.Session["idAccount"]), page);
            this.ViewBag.pageNumber =
                new MessageBo().TotalPagenumberReceiveUnRead(Convert.ToInt32(this.Session["idAccount"]), page);
            return this.View("MessageUnRead");
        }

        public ActionResult MessageSended(int page)
        {
            if (this.Session["idAccount"] == null) return this.RedirectToAction("Index", "Home");
            this.ViewBag.Message = new MessageBo().GetMessageSendByPage(
                Convert.ToInt32(this.Session["idAccount"]),
                page);
            this.ViewBag.pageNumber = new MessageBo().TotalPagenumberSend(
                Convert.ToInt32(this.Session["idAccount"]),
                page);
            return this.View("MessageSended");
        }

        [ValidateInput(false)]
        public ActionResult SendMessage(FormCollection form)
        {
            new MessageBo().SendMessage(Convert.ToInt32(this.Session["idAccount"]), form);
            return this.RedirectToAction("MessageReceiveUnRead", new { page = 1 });
        }
    }
}