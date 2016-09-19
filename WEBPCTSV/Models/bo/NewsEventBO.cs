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

    public class NewsEventBO
    {
        private readonly DSAContext dsaContext;

        public NewsEventBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public bool AddNewsEvent(
            string eventTime,
            string eventVenue,
            string requirement,
            string beginDate,
            string endDate,
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
                string type = "SuKien";
                NewsEvent newsEvent = new NewsEvent(eventTime, eventVenue, requirement, beginDate, endDate);
                if (StringExtension.IsNullOrWhiteSpace(image)) image = "/images/Sukien.jpg";
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
                newsEvent.News = news;
                this.dsaContext.NewsEvents.Add(newsEvent);
                this.dsaContext.SaveChanges();

                // Send mail
                List<string> arrEmails = (from i in this.dsaContext.EmailSubscriptions select i.Email).ToList();
                string url = "http://" + HttpContext.Current.Request.Url.Host + ":"
                             + HttpContext.Current.Request.Url.Port + Define.linkDetailNews[type]
                             + newsEvent.IdNewsEvent;
                SendMailService sendMailService = new SendMailService(
                    arrEmails,
                    Define.typeNews[type],
                    title,
                    url,
                    description);
                sendMailService.sendEmail();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteNewsEvent(int? idNewsEvent)
        {
            using (var localContext = this.dsaContext.Database.BeginTransaction())
            {
                try
                {
                    NewsEvent newsEvent = this.dsaContext.NewsEvents.Find(idNewsEvent);
                    News news = this.dsaContext.News.Find(newsEvent.IdNews);
                    this.dsaContext.NewsEvents.Remove(newsEvent);
                    this.dsaContext.SaveChanges();
                    this.dsaContext.News.Remove(news);
                    this.dsaContext.SaveChanges();
                    localContext.Commit();
                    return true;
                }
                catch (Exception)
                {
                    localContext.Rollback();
                    return false;
                }
            }
        }

        public List<NewsEvent> FindNewsEvent(string searchString)
        {
            return
                this.dsaContext.NewsEvents.Where(
                    e => e.EventVenue.Contains(searchString) || e.News.Title.Contains(searchString))
                    .OrderByDescending(e => e.News.CreatedDate)
                    .ToList();
        }

        public IPagedList<NewsEvent> GetListNewsEvent(int? page)
        {
            IPagedList<NewsEvent> newsEvent = null;

            // Tạo biến lưu số sản phẩm trên trang
            int pageSize = 5;

            // Tạo biến số trang
            int pageNumber = page ?? 1;

            // Lọc danh sách các tin tức theo trang
            newsEvent =
                this.dsaContext.NewsEvents.ToList()
                    .OrderByDescending(x => x.News.IsPinned)
                    .ThenByDescending(x => x.News.CreatedDate)
                    .ToPagedList(pageNumber, pageSize);
            return newsEvent;
        }

        public NewsEvent GetNewsEvent(int? id)
        {
            return this.dsaContext.NewsEvents.Find(id);
        }

        public List<NewsEvent> GetTopNewsEvent(int count)
        {
            return this.dsaContext.NewsEvents.OrderByDescending(e => e.News.CreatedDate).Take(count).ToList();
        }

        public bool UpdateNewsEvent(
            string idNewsEvent,
            string eventTime,
            string eventVenue,
            string beginDate,
            string endDate,
            string requirement,
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
                int idNews = int.Parse(idNewsEvent);
                NewsEvent newsEvent = this.dsaContext.NewsEvents.Find(idNews);
                if (!StringExtension.IsNullOrWhiteSpace(eventTime)) newsEvent.EventTime = eventTime;
                if (!StringExtension.IsNullOrWhiteSpace(eventVenue)) newsEvent.EventVenue = eventVenue;
                if (!StringExtension.IsNullOrWhiteSpace(requirement)) newsEvent.Requirement = requirement;
                try
                {
                    newsEvent.BeginDate = DateTime.Parse(beginDate);
                }
                catch (Exception)
                {
                }

                try
                {
                    newsEvent.EndDate = DateTime.Parse(endDate);
                }
                catch (Exception)
                {
                }

                if (!StringExtension.IsNullOrWhiteSpace(title)) newsEvent.News.Title = title;
                if (!StringExtension.IsNullOrWhiteSpace(description)) newsEvent.News.Description = description;
                if (!StringExtension.IsNullOrWhiteSpace(contentHtml)) newsEvent.News.ContentHtml = contentHtml;
                if (!StringExtension.IsNullOrWhiteSpace(image)) newsEvent.News.Image = image;
                if (!StringExtension.IsNullOrWhiteSpace(createdBy)) newsEvent.News.CreatedBy = createdBy;
                newsEvent.News.AttachedDocuments = attachedDocuments;
                newsEvent.News.IsPinned = isPinned;
                newsEvent.News.UpdatedDate = DateTime.Now;
                this.dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}