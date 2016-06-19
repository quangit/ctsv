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
    public class NewsJobBO
    {
        private DSAContext dsaContext;

        public NewsJobBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public IPagedList<NewsJob> GetListNewsJob(int? page)
        {
            IPagedList<NewsJob> newsEvent = null;
            //Tạo biến lưu số sản phẩm trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            //Lọc danh sách các tin tức theo trang
            newsEvent = dsaContext.NewsJobs.ToList().OrderByDescending(x => x.News.IsPinned).ThenByDescending(x => x.News.CreatedDate).ToPagedList(pageNumber, pageSize);
            return newsEvent;
        }
        public List<NewsJob> GetTopNewsJob(int count)
        {
            return dsaContext.NewsJobs.OrderByDescending(e => e.News.CreatedDate).Take(count).ToList();
        }
        public NewsJob GetNewsJob(int? id)
        {
            return dsaContext.NewsJobs.Find(id);
        }
        public List<NewsJob> FindNewsJob(string searchString)
        {
            return dsaContext.NewsJobs.Where(j => j.Company.Contains(searchString) || j.News.Title.Contains(searchString)).OrderByDescending(j => j.News.CreatedDate).ToList();
        }
        public bool AddNewsJob(string company, string requirement, string title, string description, string contentHtml, string image, string attachedDocuments, string createdBy, bool isPinned)
        {
            try
            {
                string type = "TuyenDung";
                NewsJob newsJob = new NewsJob(company, requirement);
                if (StringExtension.IsNullOrWhiteSpace(image)) image = "/images/Tuyendung.jpg";
                News news = new News(title, description, contentHtml, type, image, attachedDocuments, DateTime.Now, createdBy, false, isPinned);
                newsJob.News = news;
                dsaContext.NewsJobs.Add(newsJob);
                dsaContext.SaveChanges();
                //Send mail
                List<string> arrEmails = (from i in dsaContext.EmailSubscriptions select i.Email).ToList();
                string url = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + Define.linkDetailNews[type] + newsJob.IdNewsJob;
                SendMailService sendMailService = new SendMailService(arrEmails, Define.typeNews[type], title, url, description);
                sendMailService.sendEmail();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateNewsJob(string idNewsJob, string company, string requirement, string title, string description, string contentHtml, string image, string attachedDocuments, string createdBy, bool isPinned)
        {
            try
            {
                int idNews = Int32.Parse(idNewsJob);
                NewsJob newsJob = dsaContext.NewsJobs.Find(idNews);
                if (!StringExtension.IsNullOrWhiteSpace(company)) newsJob.Company = company;
                if (!StringExtension.IsNullOrWhiteSpace(requirement)) newsJob.Requirement = requirement;
                if (!StringExtension.IsNullOrWhiteSpace(title)) newsJob.News.Title = title;
                if (!StringExtension.IsNullOrWhiteSpace(description)) newsJob.News.Description = description;
                if (!StringExtension.IsNullOrWhiteSpace(contentHtml)) newsJob.News.ContentHtml = contentHtml;
                if (!StringExtension.IsNullOrWhiteSpace(image)) newsJob.News.Image = image;
                if (!StringExtension.IsNullOrWhiteSpace(createdBy)) newsJob.News.CreatedBy = createdBy;
                newsJob.News.AttachedDocuments = attachedDocuments;
                newsJob.News.IsPinned = isPinned;
                newsJob.News.UpdatedDate = DateTime.Now;
                dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteNewsJob(int? idNewsJob)
        {
            using (var localContext = dsaContext.Database.BeginTransaction())
            {
                try
                {
                    NewsJob newsJob = dsaContext.NewsJobs.Find(idNewsJob);
                    News news = dsaContext.News.Find(newsJob.IdNews);
                    dsaContext.NewsJobs.Remove(newsJob);
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