using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Helpers;

namespace WEBPCTSV.Models.bo
{
    public class MessageBo
    {
        DSAContext context = new DSAContext();
        int rowInPage = new Configuration().rowInPage;

        public List<Message> GetMessageReceive(int idAccount)
        {
            return context.Messages.Where(m => m.IdAccountReceiver == idAccount).OrderByDescending(m => m.IdMessage).ToList();
        }
        public List<Message> GetMessageReceiveReaded(int idAccount)
        {
            return context.Messages.Where(m => m.IdAccountReceiver == idAccount&&m.isReaded==true).OrderByDescending(m => m.IdMessage).ToList();
        }
        public List<Message> GetMessageReceiveReadedByPage(int idAccount,int page)
        {
            List<Message> listMessage = GetMessageReceiveReaded(idAccount);
            listMessage = listMessage.Skip((page - 1) * rowInPage).Take(rowInPage).ToList();
            return listMessage;

        }
        public PageNumber TotalPagenumberReceiveReaded(int idAccount, int page)
        {
            List<Message> listMessage = GetMessageReceiveReaded(idAccount);
            PageNumber pageNumber = new PageNumber();
            int toltalStudent = listMessage.Count;
            pageNumber.PageNumberTotal = toltalStudent / rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/ManageMessage/MessageReceiveReaded?page=";
            return pageNumber;
        }

        
        public List<Message> GetMessageReceiveUnRead(int idAccount)
        {
            return context.Messages.Where(m => m.IdAccountReceiver == idAccount && m.isReaded == false).OrderByDescending(m => m.IdMessage).ToList();
        }

        public int GetCountMessageReceiveUnRead(int idAccount)
        {
            return context.Messages.Where(m => m.IdAccountReceiver == idAccount && m.isReaded == false).Count();
        }

        public Message GetLastMessageReceiveUnRead(int idAccount)
        {
            return context.Messages.Where(m => m.IdAccountReceiver == idAccount && m.isReaded == false).OrderByDescending(m => m.IdMessage).FirstOrDefault();
        }

        public List<Message> GetMessageReceiveUnReadByPage(int idAccount, int page)
        {
            List<Message> listMessage = GetMessageReceiveUnRead(idAccount);
            listMessage = listMessage.Skip((page - 1) * rowInPage).Take(rowInPage).ToList();
            return listMessage;

        }
        public PageNumber TotalPagenumberReceiveUnRead(int idAccount, int page)
        {
            List<Message> listMessage = GetMessageReceiveUnRead(idAccount);
            PageNumber pageNumber = new PageNumber();
            int toltalStudent = listMessage.Count;
            pageNumber.PageNumberTotal = toltalStudent / rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/ManageMessage/MessageReceiveUnRead?page=";
            return pageNumber;
        }

        public void ChangeIsReaded(int idMessage)
        {
            Message message = context.Messages.Single(m => m.IdMessage == idMessage);
            message.isReaded = true;
            context.SaveChanges();
        }

        public List<Message> GetMessageSend(int idAccount)
        {
            return context.Messages.Where(m => m.IdAccountSender == idAccount).OrderByDescending(m=>m.IdMessage).ToList();
        }
        public List<Message> GetMessageSendByPage(int idAccount, int page)
        {
            List<Message> listMessage = GetMessageSend(idAccount);
            listMessage = listMessage.Skip((page - 1) * rowInPage).Take(rowInPage).ToList();
            return listMessage;
        }
        public PageNumber TotalPagenumberSend(int idAccount, int page)
        {
            List<Message> listMessage = GetMessageSend(idAccount);
            PageNumber pageNumber = new PageNumber();
            int toltalStudent = listMessage.Count;
            pageNumber.PageNumberTotal = toltalStudent / rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/ManageMessage/MessageSended?page=";
            return pageNumber;
        }

        public bool SendMessage(int idAccountSender,FormCollection form)
        {
            try {
                Message message = new Message();
                message.IdAccountSender = idAccountSender;
                Account accountSender = new AccountBO().GetAccount(idAccountSender);
                Account accountReceiver = new AccountBO().GetAccountByName(form["AccountReceiver"]);
                message.IdAccountReceiver = accountReceiver.IdAccount;
                message.TitleMessage = form["TitleMessage"];
                message.ContentMessage = form["editor1"];
                
                string text = ConvertObject.RemoveHtmlTags(form["editor1"]);
                text = text.Substring(0, (text.Length < 70 ? text.Length : 70));
                message.TextSummary = text;
                message.Time = DateTime.Now;
                message.isReaded = false;
                context.Messages.Add(message);
                context.SaveChanges();
                if (form["notifyByMail"] == "on")
                {
                    SendMail(accountSender,accountReceiver);
                }
                return true;
            }
            catch { }
            return false;
        }
        public void SendMail( Account accountSender, Account accountReceiver)
        {
            if (!String.IsNullOrEmpty(accountReceiver.Email))
            {
                List<string> listEmail = new List<string>();
                listEmail.Add(accountReceiver.Email);
                string link = "http://" + HttpContext.Current.Request.Url.Authority + "/ManageMessage/MessageReceiveUnRead?page=1";
                string content = "Chào " + ConvertObject.GetPersonNamebyAccount(accountReceiver) + ". Vào lúc " + DateTime.Now.ToLongDateString() + " bạn nhận được 1 tin nhắn từ  " + ConvertObject.GetPersonNamebyAccount(accountSender) + ". Click vào link sau để biết thêm chi tiết";
                SendMailService sendMailService = new SendMailService(listEmail, "Nhắc nhở", "Nhắc nhở", link, content);
                sendMailService.sendEmail();
                
            }

        }

        public bool SendMessage(int idAccountSender,string nameAccountReceiver,string title,string content)
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
                text = text.Substring(0, (text.Length < 70 ? text.Length : 70));
                message.TextSummary = text;
                message.Time = DateTime.Now;
                message.isReaded = false;
                context.Messages.Add(message);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

    }
}