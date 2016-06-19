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
    public class DocumentBO
    {
        private DSAContext dsaContext;

        public DocumentBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public IPagedList<Document> GetListDocument(int? page, string type)
        {
            IPagedList<Document> document = null;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (StringExtension.IsNullOrWhiteSpace(type))
            {
                document = dsaContext.Documents.OrderByDescending(x => x.News.IsPinned).ThenByDescending(x => x.News.CreatedDate).ToPagedList(pageNumber, pageSize);
            }
            else
            {
                document = dsaContext.Documents.Where(s => s.News.Type.Contains(type)).OrderByDescending(x => x.News.IsPinned).ThenByDescending(x => x.News.CreatedDate).ToPagedList(pageNumber, pageSize);
            }
            return document;
        }
        public List<Document> GetTopDocument(int count)
        {
            return dsaContext.Documents.OrderByDescending(e => e.News.CreatedDate).Take(count).ToList();
        }
        public Document GetDocument(int? id)
        {
            return dsaContext.Documents.Find(id);
        }
        public List<Document> FindDocument(string searchString)
        {
            return dsaContext.Documents.Where(d => d.DocumentNumber.Contains(searchString) || d.News.Title.Contains(searchString)).OrderByDescending(e => e.News.CreatedDate).ToList();
        }
        public bool AddDocument(string type, string documentNumber, string note, string title, string description, string contentHtml, string attachedDocuments, string createdBy, bool isPinned)
        {
            try
            {
                Document document = new Document(documentNumber, note);
                News news = new News(title, description, contentHtml, type, "", attachedDocuments, DateTime.Now, createdBy, false, isPinned);
                document.News = news;
                dsaContext.Documents.Add(document);
                dsaContext.SaveChanges();
                //Send mail
                List<string> arrEmails = (from i in dsaContext.EmailSubscriptions select i.Email).ToList();
                string url = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + Define.linkDetailNews[type] + document.IdDocument;
                SendMailService sendMailService = new SendMailService(arrEmails, Define.typeNews[type], title, url, description);
                sendMailService.sendEmail();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateDocument(string idDocument, string type, string documentNumber, string note, string title, string description, string contentHtml, string attachedDocuments, string createdBy, bool isPinned)
        {
            try
            {
                int idDoc = Int32.Parse(idDocument);
                Document document = dsaContext.Documents.Find(idDoc);
                if (!StringExtension.IsNullOrWhiteSpace(type)) document.News.Type = type;
                if (!StringExtension.IsNullOrWhiteSpace(documentNumber)) document.DocumentNumber = documentNumber;
                if (!StringExtension.IsNullOrWhiteSpace(note)) document.Note = note;
                if (!StringExtension.IsNullOrWhiteSpace(title)) document.News.Title = title;
                if (!StringExtension.IsNullOrWhiteSpace(description)) document.News.Description = description;
                if (!StringExtension.IsNullOrWhiteSpace(attachedDocuments)) document.News.AttachedDocuments = attachedDocuments;
                if (!StringExtension.IsNullOrWhiteSpace(createdBy)) document.News.CreatedBy = createdBy;
                document.News.IsPinned = isPinned;
                document.News.UpdatedDate = DateTime.Now;
                dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteDocument(int? idDocument)
        {
            using (var localContext = dsaContext.Database.BeginTransaction())
            {
                try
                {
                    Document document = dsaContext.Documents.Find(idDocument);
                    News news = dsaContext.News.Find(document.IdNews);
                    dsaContext.Documents.Remove(document);
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