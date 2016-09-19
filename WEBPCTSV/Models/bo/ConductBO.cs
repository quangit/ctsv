namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class ConductBO
    {
        private readonly DSAContext dsaContext;

        public ConductBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public ConductEvent GetConductEventBySemester(int idSemester)
        {
            return this.dsaContext.ConductEvents.SingleOrDefault(c => c.IdSemester == idSemester);
        }

        public List<ConductSchedule> GetListScheduleByEvent(int idConductEvent)
        {
            return this.dsaContext.ConductSchedules.Where(c => c.IdConductEvent.Equals(idConductEvent)).ToList();
        }
    }
}