using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Helpers.Common;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Helpers;


namespace WEBPCTSV.Models.bo
{
    public class NewsEventBO
    {
        private DSAContext dsaContext;

        public NewsEventBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public IPagedList<NewsEvent> GetListNewsEvent(int? page)
        {
            IPagedList<NewsEvent> newsEvent = null;
            //Tạo biến lưu số sản phẩm trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            //Lọc danh sách các tin tức theo trang
            newsEvent = dsaContext.NewsEvents.ToList().OrderByDescending(x => x.News.IsPinned).ThenByDescending(x => x.News.CreatedDate).ToPagedList(pageNumber, pageSize);
            return newsEvent;
        }
        public List<NewsEvent> GetTopNewsEvent(int count)
        {
            return dsaContext.NewsEvents.OrderByDescending(e => e.News.CreatedDate).Take(count).ToList();
        }
        public NewsEvent GetNewsEvent(int? id)
        {
            return dsaContext.NewsEvents.Find(id);
        }
        public List<NewsEvent> FindNewsEvent(string searchString)
        {
            return dsaContext.NewsEvents.Where(e => e.EventVenue.Contains(searchString) || e.News.Title.Contains(searchString)).OrderByDescending(e => e.News.CreatedDate).ToList();
        }
        public bool AddNewsEvent(string eventTime, string eventVenue, string requirement, string beginDate, string endDate, string title, string description, string contentHtml, string image, string attachedDocuments, string createdBy, bool isPinned)
        {
            try
            {
                string type = "SuKien";
                NewsEvent newsEvent = new NewsEvent(eventTime,eventVenue,requirement,beginDate,endDate);
                if (StringExtension.IsNullOrWhiteSpace(image)) image = "/images/Sukien.jpg";
                News news = new News(title, description, contentHtml, type, image, attachedDocuments, DateTime.Now, createdBy, false, isPinned);
                newsEvent.News = news;
                dsaContext.NewsEvents.Add(newsEvent);
                dsaContext.SaveChanges();
                //Send mail
                List<string> arrEmails = (from i in dsaContext.EmailSubscriptions select i.Email).ToList();
                string url = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + Define.linkDetailNews[type] + newsEvent.IdNewsEvent;
                SendMailService sendMailService = new SendMailService(arrEmails, Define.typeNews[type], title, url, description);
                sendMailService.sendEmail();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateNewsEvent(string idNewsEvent, string eventTime, string eventVenue, string beginDate, string endDate, string requirement, string title, string description, string contentHtml, string image, string attachedDocuments, string createdBy, bool isPinned)
        {
            try
            {
                int idNews = Int32.Parse(idNewsEvent);
                NewsEvent newsEvent = dsaContext.NewsEvents.Find(idNews);
                if (!StringExtension.IsNullOrWhiteSpace(eventTime)) newsEvent.EventTime = eventTime;
                if (!StringExtension.IsNullOrWhiteSpace(eventVenue)) newsEvent.EventVenue = eventVenue;
                if (!StringExtension.IsNullOrWhiteSpace(requirement)) newsEvent.Requirement = requirement;
                try
                {
                    newsEvent.BeginDate = DateTime.Parse(beginDate);
                }
                catch (Exception) { }
                try
                {
                    newsEvent.EndDate = DateTime.Parse(endDate);
                }
                catch (Exception) { }
                if (!StringExtension.IsNullOrWhiteSpace(title)) newsEvent.News.Title = title;
                if (!StringExtension.IsNullOrWhiteSpace(description)) newsEvent.News.Description = description;
                if (!StringExtension.IsNullOrWhiteSpace(contentHtml)) newsEvent.News.ContentHtml = contentHtml;
                if (!StringExtension.IsNullOrWhiteSpace(image)) newsEvent.News.Image = image;
                if (!StringExtension.IsNullOrWhiteSpace(createdBy)) newsEvent.News.CreatedBy = createdBy;
                newsEvent.News.AttachedDocuments = attachedDocuments;
                newsEvent.News.IsPinned = isPinned;
                newsEvent.News.UpdatedDate = DateTime.Now;
                dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteNewsEvent(int? idNewsEvent)
        {
            using (var localContext = dsaContext.Database.BeginTransaction())
            {
                try
                {
                    NewsEvent newsEvent = dsaContext.NewsEvents.Find(idNewsEvent);
                    News news = dsaContext.News.Find(newsEvent.IdNews);
                    dsaContext.NewsEvents.Remove(newsEvent);
                    dsaContext.SaveChanges();
                    dsaContext.News.Remove(news);
                    dsaContext.SaveChanges();
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
    }
}