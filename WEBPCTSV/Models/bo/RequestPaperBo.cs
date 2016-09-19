namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using WEBPCTSV.Helpers;
    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.Support;

    public class RequestPaperBo
    {
        readonly DSAContext context = new DSAContext();

        readonly int rowInPage = new Configuration().rowInPage;

        public RequestPaper AddRequest(int idStudent, int idReason, int numberpaper)
        {
            Student student = this.context.Students.SingleOrDefault(s => s.IdStudent == idStudent);
            RequestPaper request = new RequestPaper();
            ReasonRequest reason = new ReasonPaperBo().Get(idReason);
            request.IdReasonRequest = idReason;
            request.TimeRequest = DateTime.Now;
            request.NumberPaper = numberpaper;
            DateTime timeReceive = DateTime.Now;
            request.IdRequestStatus = 2;
            request.isImportant = false;
            timeReceive = DateTime.Now.AddDays(reason.Paper.WaittingPeriodNormal);

            request.TimeReceivePaper = this.GetTimeReceive(timeReceive);
            request.IdAccountRequest = student.IdAccount.Value;
            this.context.RequestPapers.Add(request);
            this.context.SaveChanges();
            Account account = this.context.Accounts.SingleOrDefault(a => a.IdAccount == student.IdAccount.Value);
            this.SendMail(request, account, reason);
            return request;
        }

        public void AddRequestPaper(int idAccount, FormCollection form)
        {
            RequestPaper request = new RequestPaper();
            int idReason = Convert.ToInt32(form["selectReason"]);
            ReasonRequest reason = new ReasonPaperBo().Get(idReason);
            string otherReason = form["otherReason"];
            request.ContentReason = otherReason;
            request.IdReasonRequest = idReason;
            request.TimeRequest = DateTime.Now;
            request.NumberPaper = Convert.ToInt32(form["numberPaper"]);
            DateTime timeReceive = DateTime.Now;
            if (reason.Reason.Equals("khác"))
            {
                request.IdRequestStatus = 1;
            }
            else
            {
                request.IdRequestStatus = 2;
            }

            request.isImportant = Convert.ToBoolean(form["isImportant"]);
            if (request.isImportant)
            {
                timeReceive = DateTime.Now.AddDays(reason.Paper.WaittingPeriodUrgency);
            }
            else
            {
                timeReceive = DateTime.Now.AddDays(reason.Paper.WaittingPeriodNormal);
            }

            request.TimeReceivePaper = this.GetTimeReceive(timeReceive);
            request.IdAccountRequest = idAccount;
            this.context.RequestPapers.Add(request);
            this.context.SaveChanges();
            Account account = this.context.Accounts.SingleOrDefault(a => a.IdAccount == idAccount);
            this.SendMail(request, account, reason);
        }

        public void DeleteRequestPaper(int idRequestPaper)
        {
            RequestPaper request = this.context.RequestPapers.Single(rq => rq.IdRequestPaper == idRequestPaper);
            this.context.RequestPapers.Remove(request);
            this.context.SaveChanges();
        }

        public string GetInfoFooterPaper(int idAccount)
        {
            string infoFooterPaper = string.Empty;
            Account account = new AccountBO().GetAccount(idAccount);
            infoFooterPaper += DateTime.Now.ToString("hh_mm_ss_dd_MM_yy");
            infoFooterPaper += "_";
            infoFooterPaper += account.UserName;
            return infoFooterPaper;
        }

        public List<RequestPaper> GetListAllRequestPaperByValue(int value)
        {
            List<RequestPaper> listRequestPaper = null;
            if (value == 0)
            {
                listRequestPaper = this.context.RequestPapers.OrderByDescending(rq => rq.IdRequestPaper).ToList();
            }
            else
            {
                listRequestPaper =
                    this.context.RequestPapers.Where(rq => rq.IdRequestStatus == value)
                        .OrderByDescending(rq => rq.IdRequestPaper)
                        .ToList();
            }

            return listRequestPaper;
        }

        public PageNumber GetPageNumber(int idRequestStatus, int Page, string search)
        {
            PageNumber pageNumber = new PageNumber();
            int CountTotal;
            CountTotal = this.SearchRequestPaper(idRequestStatus, search).Count;
            pageNumber.PageNumberTotal = CountTotal / this.rowInPage + 1;
            pageNumber.PageNumberCurrent = Page;
            return pageNumber;
        }

        public List<RequestPaper> GetRequestByClass(int idReason, int idClass)
        {
            List<RequestPaper> listRequest =
                this.context.RequestPapers.Where(
                    c =>
                    c.AccountRequest.Students.FirstOrDefault().IdClass == idClass
                    && (c.IdRequestStatus == 1 || c.IdRequestStatus == 2) && c.IdReasonRequest == idReason).ToList();
            return listRequest;
        }

        public PageNumber GetSendPageNumber(int idAccount, int Page)
        {
            PageNumber pageNumber = new PageNumber();
            int CountTotal =
                this.context.RequestPapers.Where(rq => rq.IdAccountRequest == idAccount)
                    .OrderByDescending(rq => rq.IdRequestPaper)
                    .ToList()
                    .Count;
            pageNumber.PageNumberTotal = CountTotal / this.rowInPage + 1;
            pageNumber.PageNumberCurrent = Page;
            return pageNumber;
        }

        public DateTime GetTimeReceive(DateTime time)
        {
            DayOfWeek day = time.DayOfWeek;
            if (day == DayOfWeek.Sunday)
            {
                time.AddDays(1);
            }
            else if (day == DayOfWeek.Saturday)
            {
                time.AddDays(2);
            }

            return time;
        }

        public List<RequestPaper> ListAllRequestPaper(int value, int Page, string search)
        {
            List<RequestPaper> listRequestPaper = this.SearchRequestPaper(value, search);

            listRequestPaper = listRequestPaper.Skip((Page - 1) * this.rowInPage).Take(this.rowInPage).ToList();

            return listRequestPaper;
        }

        public List<RequestPaper> ListRequestPaper(int idAccount, int Page)
        {
            List<RequestPaper> listRequestPaper =
                this.context.RequestPapers.Where(rq => rq.IdAccountRequest == idAccount)
                    .OrderByDescending(rq => rq.IdRequestPaper)
                    .Skip((Page - 1) * this.rowInPage)
                    .Take(this.rowInPage)
                    .ToList();
            return listRequestPaper;
        }

        public List<RequestPaper> SearchRequestPaper(int value, string search)
        {
            List<RequestPaper> listRequestPaper = this.GetListAllRequestPaperByValue(value);
            if (string.IsNullOrEmpty(search))
            {
            }
            else
            {
                search = search.ToUpper().Trim();
                listRequestPaper =
                    listRequestPaper.Where(
                        r =>
                        r.AccountRequest.UserName.ToUpper().Contains(search)
                        || (r.AccountRequest.Students.First().LastNameStudent
                             + r.AccountRequest.Students.First().FirstNameStudent).ToUpper().Trim().Contains(search)
                        || r.Reasonrequest.Paper.PaperName.ToUpper().Contains(search)).ToList();
            }

            return listRequestPaper;
        }

        public void SendMail(RequestPaper request, Account account, ReasonRequest reason)
        {
            if (!string.IsNullOrEmpty(account.Email))
            {
                List<string> listEmail = new List<string>();
                listEmail.Add(account.Email);
                string link = "http://" + HttpContext.Current.Request.Url.Authority
                              + "/ManageRequest/ListSendRequestPaper?page=1";
                string content = "Chào " + ConvertObject.GetPersonNamebyAccount(account) + ". Vào lúc "
                                 + DateTime.Now.ToLongDateString() + " bạn đã gửi yêu cầu xin giấy "
                                 + reason.Paper.PaperName + " với lý do " + reason.Reason + "."
                                 + " Thời gian bạn nhận giấy là: " + request.TimeReceivePaper.Value.ToShortDateString();
                SendMailService sendMailService = new SendMailService(
                    listEmail,
                    "yêu cầu",
                    "Xin cấp giấy " + reason.Paper.PaperName,
                    link,
                    content);
                sendMailService.sendEmail();
                new MessageBo().SendMessage(1, account.UserName, "Xin cấp giấy " + reason.Paper.PaperName, content);
            }
        }

        public void UpdateRequest(int idRequest, int numberPaper)
        {
            RequestPaper request = this.context.RequestPapers.SingleOrDefault(r => r.IdRequestPaper == idRequest);
            request.NumberPaper = numberPaper;
            this.context.SaveChanges();
        }

        public void UpdateStatusCancelRequestPaper(int idRequestPaper, int idAccount)
        {
            RequestPaper request = this.context.RequestPapers.Single(rq => rq.IdRequestPaper == idRequestPaper);
            request.IdRequestStatus = 3;
            request.IdAccountProcess = idAccount;
            this.context.SaveChanges();
        }

        public void UpdateStatusProcessRequestPaper(int idRequestPaper, int idAccount)
        {
            RequestPaper request = this.context.RequestPapers.Single(rq => rq.IdRequestPaper == idRequestPaper);
            if (request.IdRequestStatus == 1)
            {
                request.IdRequestStatus = 2;
            }
            else if (request.IdRequestStatus == 2)
            {
                request.IdRequestStatus = 4;
            }

            request.IdAccountProcess = idAccount;
            this.context.SaveChanges();
        }
    }
}