namespace WEBPCTSV.Models.bo
{
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class ReasonPaperBo
    {
        readonly DSAContext context = new DSAContext();

        public Paper AddReasonRequest(int idPaper, string reason)
        {
            ReasonRequest reasonRequest = new ReasonRequest();
            reasonRequest.IdPaper = idPaper;
            reasonRequest.Reason = reason;
            this.context.ReasonRequests.Add(reasonRequest);
            this.context.SaveChanges();
            Paper paper = this.context.Papers.Single(pp => pp.IdPaper == idPaper);
            return paper;
        }

        public void DeleteReasonRequest(int idReason)
        {
            ReasonRequest reason = this.context.ReasonRequests.Single(rs => rs.IdReasonRequest == idReason);
            this.context.ReasonRequests.Remove(reason);
            this.context.SaveChanges();
        }

        public ReasonRequest Get(int idReason)
        {
            return this.context.ReasonRequests.SingleOrDefault(r => r.IdReasonRequest == idReason);
        }

        public ReasonRequest GetReasonByIdRequest(int idRequest)
        {
            return this.context.RequestPapers.SingleOrDefault(r => r.IdRequestPaper == idRequest).Reasonrequest;
        }
    }
}