namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using WEBPCTSV.Helpers;
    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.Support;

    public class MessageBo
    {
        readonly DSAContext context = new DSAContext();

        readonly int rowInPage = new Configuration().rowInPage;

        public void ChangeIsReaded(int idMessage)
        {
            Message message = this.context.Messages.Single(m => m.IdMessage == idMessage);
            message.isReaded = true;
            this.context.SaveChanges();
        }

        public int GetCountMessageReceiveUnRead(int idAccount)
        {
            return this.context.Messages.Where(m => m.IdAccountReceiver == idAccount && m.isReaded == false).Count();
        }

        public Message GetLastMessageReceiveUnRead(int idAccount)
        {
            return
                this.context.Messages.Where(m => m.IdAccountReceiver == idAccount && m.isReaded == false)
                    .OrderByDescending(m => m.IdMessage)
                    .FirstOrDefault();
        }

        public List<Message> GetMessageReceive(int idAccount)
        {
            return
                this.context.Messages.Where(m => m.IdAccountReceiver == idAccount)
                    .OrderByDescending(m => m.IdMessage)
                    .ToList();
        }

        public List<Message> GetMessageReceiveReaded(int idAccount)
        {
            return
                this.context.Messages.Where(m => m.IdAccountReceiver == idAccount && m.isReaded == true)
                    .OrderByDescending(m => m.IdMessage)
                    .ToList();
        }

        public List<Message> GetMessageReceiveReadedByPage(int idAccount, int page)
        {
            List<Message> listMessage = this.GetMessageReceiveReaded(idAccount);
            listMessage = listMessage.Skip((page - 1) * this.rowInPage).Take(this.rowInPage).ToList();
            return listMessage;
        }

        public List<Message> GetMessageReceiveUnRead(int idAccount)
        {
            return
                this.context.Messages.Where(m => m.IdAccountReceiver == idAccount && m.isReaded == false)
                    .OrderByDescending(m => m.IdMessage)
                    .ToList();
        }

        public List<Message> GetMessageReceiveUnReadByPage(int idAccount, int page)
        {
            List<Message> listMessage = this.GetMessageReceiveUnRead(idAccount);
            listMessage = listMessage.Skip((page - 1) * this.rowInPage).Take(this.rowInPage).ToList();
            return listMessage;
        }

        public List<Message> GetMessageSend(int idAccount)
        {
            return
                this.context.Messages.Where(m => m.IdAccountSender == idAccount)
                    .OrderByDescending(m => m.IdMessage)
                    .ToList();
        }

        public List<Message> GetMessageSendByPage(int idAccount, int page)
        {
            List<Message> listMessage = this.GetMessageSend(idAccount);
            listMessage = listMessage.Skip((page - 1) * this.rowInPage).Take(this.rowInPage).ToList();
            return listMessage;
        }

        public void SendMail(Account accountSender, Account accountReceiver)
        {
            if (!string.IsNullOrEmpty(accountReceiver.Email))
            {
                List<string> listEmail = new List<string>();
                listEmail.Add(accountReceiver.Email);
                string link = "http://" + HttpContext.Current.Request.Url.Authority
                              + "/ManageMessage/MessageReceiveUnRead?page=1";
                string content = "Chào " + ConvertObject.GetPersonNamebyAccount(accountReceiver) + ". Vào lúc "
                                 + DateTime.Now.ToLongDateString() + " bạn nhận được 1 tin nhắn từ  "
                                 + ConvertObject.GetPersonNamebyAccount(accountSender)
                                 + ". Click vào link sau để biết thêm chi tiết";
                SendMailService sendMailService = new SendMailService(listEmail, "Nhắc nhở", "Nhắc nhở", link, content);
                sendMailService.sendEmail();
            }
        }

        public bool SendMessage(int idAccountSender, FormCollection form)
        {
            try
            {
                Message message = new Message();
                message.IdAccountSender = idAccountSender;
                Account accountSender = new AccountBO().GetAccount(idAccountSender);
                Account accountReceiver = new AccountBO().GetAccountByName(form["AccountReceiver"]);
                message.IdAccountReceiver = accountReceiver.IdAccount;
                message.TitleMessage = form["TitleMessage"];
                message.ContentMessage = form["editor1"];

                string text = ConvertObject.RemoveHtmlTags(form["editor1"]);
                text = text.Substring(0, text.Length < 70 ? text.Length : 70);
                message.TextSummary = text;
                message.Time = DateTime.Now;
                message.isReaded = false;
                this.context.Messages.Add(message);
                this.context.SaveChanges();
                if (form["notifyByMail"] == "on")
                {
                    this.SendMail(accountSender, accountReceiver);
                }

                return true;
            }
            catch
            {
            }

            return false;
        }

        public bool SendMessage(int idAccountSender, string nameAccountReceiver, string title, string content)
        {
            try
            {
                Message message = new Message();
                message.IdAccountSender = idAccountSender;
                Account accountReceiver = new AccountBO().GetAccountByName(nameAccountReceiver);
                message.IdAccountReceiver = accountReceiver.IdAccount;
                message.TitleMessage = title;
                message.ContentMessage = content;
                string text = ConvertObject.RemoveHtmlTags(content);
                text = text.Substring(0, text.Length < 70 ? text.Length : 70);
                message.TextSummary = text;
                message.Time = DateTime.Now;
                message.isReaded = false;
                this.context.Messages.Add(message);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public PageNumber TotalPagenumberReceiveReaded(int idAccount, int page)
        {
            List<Message> listMessage = this.GetMessageReceiveReaded(idAccount);
            PageNumber pageNumber = new PageNumber();
            int toltalStudent = listMessage.Count;
            pageNumber.PageNumberTotal = toltalStudent / this.rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/ManageMessage/MessageReceiveReaded?page=";
            return pageNumber;
        }

        public PageNumber TotalPagenumberReceiveUnRead(int idAccount, int page)
        {
            List<Message> listMessage = this.GetMessageReceiveUnRead(idAccount);
            PageNumber pageNumber = new PageNumber();
            int toltalStudent = listMessage.Count;
            pageNumber.PageNumberTotal = toltalStudent / this.rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/ManageMessage/MessageReceiveUnRead?page=";
            return pageNumber;
        }

        public PageNumber TotalPagenumberSend(int idAccount, int page)
        {
            List<Message> listMessage = this.GetMessageSend(idAccount);
            PageNumber pageNumber = new PageNumber();
            int toltalStudent = listMessage.Count;
            pageNumber.PageNumberTotal = toltalStudent / this.rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/ManageMessage/MessageSended?page=";
            return pageNumber;
        }
    }
}