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
    public class QuestionBO
    {
        private DSAContext dsaContext;

        public QuestionBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }
        public IPagedList<Question> GetListNewQuestion(int? page)
        {
            IPagedList<Question> question = null;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            question = dsaContext.Questions.OrderBy(q => q.ReplyDate).ThenByDescending(q => q.IdQuestion).ToPagedList(pageNumber, pageSize);
            return question;
        }
        public IPagedList<Question> GetListQuestionReplied(int? page)
        {
            IPagedList<Question> question = null;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            question = dsaContext.Questions.Where(q => q.Reply != null && !q.Reply.Equals("")).OrderByDescending(x => x.IdQuestion).ToPagedList(pageNumber, pageSize);
            return question;
        }
        public IPagedList<Question> GetCommonQuestion(int? page)
        {
            IPagedList<Question> question = null;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            question = dsaContext.Questions.Where(q => q.Infomation.Equals("admin")).OrderByDescending(x => x.CreatedDate).ToPagedList(pageNumber, pageSize);
            return question;
        }
        public List<Question> GetTopQuestion(int count)
        {
            return dsaContext.Questions.Where(q => q.Reply != null && !q.Reply.Equals("")).OrderByDescending(e => e.CreatedDate).Take(count).ToList();
        }
        public List<Question> GetRandomQuestion(int count)
        {
            return dsaContext.Questions.Where(q => q.Reply != null && !q.Reply.Equals("")).OrderBy(q => Guid.NewGuid()).Take(count).ToList();
        }
        public Question GetQuestion(int? id)
        {
            return dsaContext.Questions.Find(id);
        }
        public List<Question> FindQuestion(string searchString)
        {
            return dsaContext.Questions.Where(q => (q.Reply != null && !q.Reply.Equals("")) && (q.Field.Contains(searchString) || q.Title.Contains(searchString))).OrderByDescending(e => e.CreatedDate).ToList();
        }

        internal int AddQuestion(string typeRequest, string information, string name, string email, string field, string title, string contentHtml)
        {
            try
            {
                Question question = new Question(typeRequest, information, name, email, field, title, contentHtml);
                dsaContext.Questions.Add(question);
                dsaContext.SaveChanges();
                return question.IdQuestion;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        internal bool UpdateQuestion(int idQuestion, string typeRequest, string information, string name, string email, string field, string title, string contentHtml, string reply, string replyBy, bool isEdit)
        {
            try
            {
                Question question = dsaContext.Questions.Find(idQuestion);
                question.TypeRequest = typeRequest;
                question.Infomation = information;
                question.Name = name;
                question.Field = field;
                question.Title = title;
                question.ContentHtml = contentHtml;
                question.Reply = reply;
                question.ReplyBy = replyBy;
                question.ReplyDate = DateTime.Now;
                dsaContext.SaveChanges();
                if (!isEdit)
                {
                    //Send mail
                    List<string> arrEmails = new List<string>();
                    arrEmails.Add(email);
                    string url = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "/TuVan/DanhSachCauHoi/ChiTiet?id=" + idQuestion;
                    SendMailService sendMailService = new SendMailService(arrEmails, "câu hỏi", title, url, "Câu hỏi của bạn đã được trả lời. Hãy truy cập link để xem thông tin!");
                    sendMailService.sendEmail();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        internal bool DeleteQuestion(int? id)
        {
            try
            {
                Question question = dsaContext.Questions.Find(id);
                dsaContext.Questions.Remove(question);
                dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}