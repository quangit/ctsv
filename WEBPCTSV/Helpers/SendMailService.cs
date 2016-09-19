namespace WEBPCTSV.Helpers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Hosting;
    using System.Web.Mvc;

    using Hangfire;

    using Postal;

    using WEBPCTSV.Models.bean;

    public class SendMailService
    {
        private List<string> arrEmail;

        private readonly string categories;

        private readonly string title;

        public SendMailService(List<string> arrEmail, string categories, string title, string link, string content)
        {
            this.arrEmail = arrEmail;
            this.Link = link;
            this.title = title;
            this.categories = categories;
            this.Content = content;
        }

        public List<string> ArrEmail
        {
            get
            {
                return this.arrEmail;
            }

            set
            {
                if (value.Count != 0) this.arrEmail = value;
                else this.arrEmail = null;
            }
        }

        public string Content { get; set; }

        public string Link { get; set; }

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

        public void sendEmail()
        {
            foreach (string email in this.arrEmail)
            {
                BackgroundJob.Enqueue(() => NotifyNewEmail(email, this.categories, this.title, this.Link, this.Content));
            }
        }
    }
}