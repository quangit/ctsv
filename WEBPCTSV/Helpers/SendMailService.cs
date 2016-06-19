using Hangfire;
using Postal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Helpers
{
    public class SendMailService
    {
        private List<string> arrEmail;
        private string link;
        private string title;
        private string categories;
        private string content;

        public List<string> ArrEmail
        {
            set
            {
                if (value.Count != 0)
                    arrEmail = value;
                else
                    arrEmail = null;
            }
            get
            {
                return arrEmail;
            }
        }
        public string Link
        {
            set
            {
                link = value;
            }
            get
            {
                return link;
            }
        }
        public string Content
        {
            set
            {
                content = value;
            }
            get
            {
                return content;
            }
        }
        public SendMailService(List<string> arrEmail, string categories, string title, string link, string content)
        {
            this.arrEmail = arrEmail;
            this.link = link;
            this.title = title;
            this.categories = categories;
            this.content = content;
        }
        public void sendEmail()
        {
            foreach (string email in arrEmail)
            {
                BackgroundJob.Enqueue(() => NotifyNewEmail(email, categories, title, link, content));
            }
        }
        public static void NotifyNewEmail(string toEmail, string categories, string title, string link, string content)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));

            var emailService = new EmailService(engines);
            // Get comment and send a notification.
            var email = new NewCommentEmail
                    {
                        To = toEmail,
                        Title = title,
                        Categories = categories,
                        Link = link,
                        Comment = content
                    };

            emailService.Send(email);
        }
    }

}