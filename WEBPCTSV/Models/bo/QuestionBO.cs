namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using PagedList;

    using WEBPCTSV.Helpers;
    using WEBPCTSV.Models.bean;

    public class QuestionBO
    {
        private readonly DSAContext dsaContext;

        public QuestionBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public List<Question> FindQuestion(string searchString)
        {
            return
                this.dsaContext.Questions.Where(
                    q =>
                    (q.Reply != null && !q.Reply.Equals(string.Empty))
                    && (q.Field.Contains(searchString) || q.Title.Contains(searchString)))
                    .OrderByDescending(e => e.CreatedDate)
                    .ToList();
        }

        public IPagedList<Question> GetCommonQuestion(int? page)
        {
            IPagedList<Question> question = null;
            int pageSize = 5;
            int pageNumber = page ?? 1;
            question =
                this.dsaContext.Questions.Where(q => q.Infomation.Equals("admin"))
                    .OrderByDescending(x => x.CreatedDate)
                    .ToPagedList(pageNumber, pageSize);
            return question;
        }

        public IPagedList<Question> GetListNewQuestion(int? page)
        {
            IPagedList<Question> question = null;
            int pageSize = 5;
            int pageNumber = page ?? 1;
            question =
                this.dsaContext.Questions.OrderBy(q => q.ReplyDate)
                    .ThenByDescending(q => q.IdQuestion)
                    .ToPagedList(pageNumber, pageSize);
            return question;
        }

        public IPagedList<Question> GetListQuestionReplied(int? page)
        {
            IPagedList<Question> question = null;
            int pageSize = 5;
            int pageNumber = page ?? 1;
            question =
                this.dsaContext.Questions.Where(q => q.Reply != null && !q.Reply.Equals(string.Empty))
                    .OrderByDescending(x => x.IdQuestion)
                    .ToPagedList(pageNumber, pageSize);
            return question;
        }

        public Question GetQuestion(int? id)
        {
            return this.dsaContext.Questions.Find(id);
        }

        public List<Question> GetRandomQuestion(int count)
        {
            return
                this.dsaContext.Questions.Where(q => q.Reply != null && !q.Reply.Equals(string.Empty))
                    .OrderBy(q => Guid.NewGuid())
                    .Take(count)
                    .ToList();
        }

        public List<Question> GetTopQuestion(int count)
        {
            return
                this.dsaContext.Questions.Where(q => q.Reply != null && !q.Reply.Equals(string.Empty))
                    .OrderByDescending(e => e.CreatedDate)
                    .Take(count)
                    .ToList();
        }

        internal int AddQuestion(
            string typeRequest,
            string information,
            string name,
            string email,
            string field,
            string title,
            string contentHtml)
        {
            try
            {
                Question question = new Question(typeRequest, information, name, email, field, title, contentHtml);
                this.dsaContext.Questions.Add(question);
                this.dsaContext.SaveChanges();
                return question.IdQuestion;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        internal bool DeleteQuestion(int? id)
        {
            try
            {
                Question question = this.dsaContext.Questions.Find(id);
                this.dsaContext.Questions.Remove(question);
                this.dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool UpdateQuestion(
            int idQuestion,
            string typeRequest,
            string information,
            string name,
            string email,
            string field,
            string title,
            string contentHtml,
            string reply,
            string replyBy,
            bool isEdit)
        {
            try
            {
                Question question = this.dsaContext.Questions.Find(idQuestion);
                question.TypeRequest = typeRequest;
                question.Infomation = information;
                question.Name = name;
                question.Field = field;
                question.Title = title;
                question.ContentHtml = contentHtml;
                question.Reply = reply;
                question.ReplyBy = replyBy;
                question.ReplyDate = DateTime.Now;
                this.dsaContext.SaveChanges();
                if (!isEdit)
                {
                    // Send mail
                    List<string> arrEmails = new List<string>();
                    arrEmails.Add(email);
                    string url = "http://" + HttpContext.Current.Request.Url.Host + ":"
                                 + HttpContext.Current.Request.Url.Port + "/TuVan/DanhSachCauHoi/ChiTiet?id="
                                 + idQuestion;
                    SendMailService sendMailService = new SendMailService(
                        arrEmails,
                        "câu hỏi",
                        title,
                        url,
                        "Câu hỏi của bạn đã được trả lời. Hãy truy cập link để xem thông tin!");
                    sendMailService.sendEmail();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}