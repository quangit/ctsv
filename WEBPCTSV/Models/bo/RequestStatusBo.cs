namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class RequestStatusBo
    {
        readonly DSAContext context = new DSAContext();

        public List<RequestStatus> GetRequestStatus()
        {
            return this.context.RequestStatus.ToList();
        }
    }
}