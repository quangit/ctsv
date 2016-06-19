using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Models.bo
{
    public class ConductBO
    {
        private DSAContext dsaContext;
        public ConductBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        #region Conduct schedule
        public List<ConductSchedule> GetListScheduleByEvent(int idConductEvent)
        {
            return dsaContext.ConductSchedules.Where(c => c.IdConductEvent.Equals(idConductEvent)).ToList();
        }

        #endregion

        #region Conduct event
        public ConductEvent GetConductEventBySemester(int idSemester)
        {
            return dsaContext.ConductEvents.SingleOrDefault(c => c.IdSemester == idSemester);
        } 
        #endregion
        #region Conduct schedule
        #endregion
        #region Conduct schedule
        #endregion
        #region Conduct schedule
        #endregion
    }
}