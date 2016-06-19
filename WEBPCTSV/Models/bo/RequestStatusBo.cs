using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class RequestStatusBo
    {
        DSAContext context = new DSAContext();
        public List<RequestStatus> GetRequestStatus()
        {
            return context.RequestStatus.ToList();

        }
    }
}