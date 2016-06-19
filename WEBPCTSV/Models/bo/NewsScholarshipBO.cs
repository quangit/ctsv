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
    public class NewsScholarshipBO
    {
        private DSAContext dsaContext;

        public NewsScholarshipBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public IPagedList<NewsScholarship> GetListNewsScholarship(int? page, string type)
        {
            IPagedList<NewsScholarship> newsScholarship = null;
            //Tạo biến lưu số sản phẩm trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            //Lọc danh sách các tin tức theo trang
            newsScholarship = dsaContext.NewsScholarships.Where(s => s.News.Type.Contains(type)).OrderByDescending(x => x.News.IsPinned).ThenByDescending(x => x.News.CreatedDate).ToPagedList(pageNumber, pageSize);
            return newsScholarship;
        }
        public List<NewsScholarship> GetTopNewsScholarship(int count)
        {
            return dsaContext.NewsScholarships.OrderByDescending(e => e.News.CreatedDate).Take(count).ToList();
        }
        public NewsScholarship GetNewsScholarship(int? id)
        {
            return dsaContext.NewsScholarships.Find(id);
        }
        public List<NewsScholarship> FindNewsScholarship(string searchString)
        {
            return dsaContext.NewsScholarships.Where(s => s.Sponsor.Contains(searchString) || s.News.Title.Contains(searchString)).OrderByDescending(e => e.News.CreatedDate).ToList();
        }
        public bool AddNewsScholarship(string type, string sponsor, string requirement, string title, string description, string contentHtml, string image, string attachedDocuments, string createdBy, bool isPinned)
        {
            try
            {
                NewsScholarship newsScholarship = new NewsScholarship(sponsor, requirement);
                if (StringExtension.IsNullOrWhiteSpace(image)) image = "/images/hocbong.jpg";
                News news = new News(title, description, contentHtml, type, image, attachedDocuments, DateTime.Now, createdBy, false, isPinned);
                newsScholarship.News = news;
                dsaContext.NewsScholarships.Add(newsScholarship);
                dsaContext.SaveChanges();
                //Send mail
                List<string> arrEmails = (from i in dsaContext.EmailSubscriptions select i.Email).ToList();
                string url = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + Define.linkDetailNews[type] + newsScholarship.IdNewsScholarship;
                SendMailService sendMailService = new SendMailService(arrEmails, Define.typeNews[type], title, url, description);
                sendMailService.sendEmail();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateNewsScholarship(string idNewsScholarship, string type, string sponsor, string requirement, string title, string description, string contentHtml, string image, string attachedDocuments, string createdBy, bool isPinned)
        {
            try
            {
                int idNews = Int32.Parse(idNewsScholarship);
                NewsScholarship newsScholarship = dsaContext.NewsScholarships.Find(idNews);
                if (!StringExtension.IsNullOrWhiteSpace(type)) newsScholarship.News.Type = type;
                if (!StringExtension.IsNullOrWhiteSpace(sponsor)) newsScholarship.Sponsor = sponsor;
                if (!StringExtension.IsNullOrWhiteSpace(requirement)) newsScholarship.Requirement = requirement;
                if (!StringExtension.IsNullOrWhiteSpace(title)) newsScholarship.News.Title = title;
                if (!StringExtension.IsNullOrWhiteSpace(description)) newsScholarship.News.Description = description;
                if (!StringExtension.IsNullOrWhiteSpace(contentHtml)) newsScholarship.News.ContentHtml = contentHtml;
                if (!StringExtension.IsNullOrWhiteSpace(image)) newsScholarship.News.Image = image;
                if (!StringExtension.IsNullOrWhiteSpace(createdBy)) newsScholarship.News.CreatedBy = createdBy;
                newsScholarship.News.AttachedDocuments = attachedDocuments;
                newsScholarship.News.IsPinned = isPinned;
                newsScholarship.News.UpdatedDate = DateTime.Now;
                dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteNewsScholarship(int? idNewsScholarship)
        {
            using (var localContext = dsaContext.Database.BeginTransaction())
            {
                try
                {
                    NewsScholarship newsScholarship = dsaContext.NewsScholarships.Find(idNewsScholarship);
                    News news = dsaContext.News.Find(newsScholarship.IdNews);
                    dsaContext.NewsScholarships.Remove(newsScholarship);
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