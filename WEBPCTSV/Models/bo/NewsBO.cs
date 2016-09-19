namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using PagedList;

    using WEBPCTSV.Helpers;
    using WEBPCTSV.Helpers.Common;
    using WEBPCTSV.Models.bean;

    public class NewsBO
    {
        private readonly DSAContext dsaContext;

        public NewsBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public bool AddNews(
            string type,
            string title,
            string description,
            string contentHtml,
            string image,
            string attachedDocuments,
            string createdBy,
            bool isPinned)
        {
            try
            {
                if (StringExtension.IsNullOrWhiteSpace(image)) image = "/images/placeholderMini.jpg";
                News news = new News(
                    title,
                    description,
                    contentHtml,
                    type,
                    image,
                    attachedDocuments,
                    DateTime.Now,
                    createdBy,
                    false,
                    isPinned);
                this.dsaContext.News.Add(news);
                this.dsaContext.SaveChanges();

                // Send mail
                if (!type.Equals("Banner"))
                {
                    List<string> arrEmails = (from i in this.dsaContext.EmailSubscriptions select i.Email).ToList();
                    string url = "http://" + HttpContext.Current.Request.Url.Host + ":"
                                 + HttpContext.Current.Request.Url.Port + Define.linkDetailNews[type] + news.IdNews;
                    SendMailService sendMailService = new SendMailService(
                        arrEmails,
                        Define.typeNews[type],
                        title,
                        url,
                        description);
                    sendMailService.sendEmail();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteNews(int? idNews)
        {
            try
            {
                News news = this.dsaContext.News.Find(idNews);
                this.dsaContext.News.Remove(news);
                this.dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<News> FindNews(string searchString, string type)
        {
            return
                this.dsaContext.News.Where(n => n.Type.Equals(type) && n.Title.Contains(searchString))
                    .OrderByDescending(e => e.CreatedDate)
                    .ToList();
        }

        public List<News> GetBanner(int count)
        {
            return
                this.dsaContext.News.Where(n => n.Type.Equals("Banner") && n.IsPinned == true)
                    .OrderByDescending(e => e.CreatedDate)
                    .Take(count)
                    .ToList();
        }

        public IPagedList<News> GetListNews(int? page, string type)
        {
            IPagedList<News> news = null;
            int pageSize = 5;
            int pageNumber = page ?? 1;
            if (StringExtension.IsNullOrWhiteSpace(type))
            {
                news =
                    this.dsaContext.News.Where(
                        n =>
                        n.Type.Equals("ThongBao") || n.Type.Equals("TinTuc") || n.Type.Equals("LegalEducation")
                        || n.Type.Equals("MedicalStudent") || n.Type.Equals("RelationCompany"))
                        .OrderByDescending(x => x.IsPinned)
                        .ThenByDescending(x => x.CreatedDate)
                        .ToPagedList(pageNumber, pageSize);
            }
            else
            {
                news =
                    this.dsaContext.News.Where(n => n.Type.Contains(type))
                        .OrderByDescending(x => x.IsPinned)
                        .ThenByDescending(x => x.CreatedDate)
                        .ToPagedList(pageNumber, pageSize);
            }

            return news;
        }

        public News GetNews(int? id)
        {
            return this.dsaContext.News.Find(id);
        }

        public List<News> GetTopNews(int count, string type)
        {
            if (StringExtension.IsNullOrWhiteSpace(type))
            {
                return
                    this.dsaContext.News.Where(
                        n =>
                        n.Type.Equals("ThongBao") || n.Type.Equals("LegalEducation") || n.Type.Equals("MedicalStudent")
                        || n.Type.Equals("RelationCompany")).OrderByDescending(x => x.CreatedDate).Take(count).ToList();
            }
            else
            {
                return
                    this.dsaContext.News.Where(n => n.Type.Equals(type))
                        .OrderByDescending(e => e.CreatedDate)
                        .Take(count)
                        .ToList();
            }
        }

        public bool PinToTop(int idNews)
        {
            try
            {
                News news = this.dsaContext.News.Find(idNews);
                news.IsPinned = !news.IsPinned;
                this.dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateNews(
            string idNews,
            string type,
            string title,
            string description,
            string contentHtml,
            string image,
            string attachedDocuments,
            string createdBy,
            bool isPinned)
        {
            try
            {
                int idNewsInt = int.Parse(idNews);
                News news = this.dsaContext.News.Find(idNewsInt);
                if (!StringExtension.IsNullOrWhiteSpace(type)) news.Type = type;
                if (!StringExtension.IsNullOrWhiteSpace(title)) news.Title = title;
                if (!StringExtension.IsNullOrWhiteSpace(description)) news.Description = description;
                if (!StringExtension.IsNullOrWhiteSpace(contentHtml)) news.ContentHtml = contentHtml;
                if (!StringExtension.IsNullOrWhiteSpace(image)) news.Image = image;
                if (!StringExtension.IsNullOrWhiteSpace(createdBy)) news.CreatedBy = createdBy;
                news.AttachedDocuments = attachedDocuments;
                news.IsPinned = isPinned;
                news.UpdatedDate = DateTime.Now;
                this.dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ViewCount(int idNews)
        {
            try
            {
                News news = this.dsaContext.News.Find(idNews);
                news.ViewCount = news.ViewCount + 1;
                this.dsaContext.SaveChanges();
            }
            catch (Exception)
            {
            }
        }
    }
}