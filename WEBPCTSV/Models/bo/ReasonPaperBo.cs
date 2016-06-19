using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class ReasonPaperBo
    {
        DSAContext context = new DSAContext();

        public ReasonRequest Get(int idReason)
        {
            return context.ReasonRequests.SingleOrDefault(r => r.IdReasonRequest == idReason);
        }
        public Paper AddReasonRequest(int idPaper, string reason)
        {
            ReasonRequest reasonRequest = new ReasonRequest();
            reasonRequest.IdPaper = idPaper;
            reasonRequest.Reason = reason;
            context.ReasonRequests.Add(reasonRequest);
            context.SaveChanges();
            Paper paper = context.Papers.Single(pp => pp.IdPaper == idPaper);
            return paper;
        }

        public void DeleteReasonRequest(int idReason)
        {
            ReasonRequest reason = context.ReasonRequests.Single(rs => rs.IdReasonRequest == idReason);
            context.ReasonRequests.Remove(reason);
            context.SaveChanges();
        }


    }
}