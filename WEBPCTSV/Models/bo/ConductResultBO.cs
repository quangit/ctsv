namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class ConductResultBO
    {
        private readonly DSAContext dsaContext;

        public ConductResultBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public bool AddConductResult(
            int idStudent,
            int idConductSchedule,
            int idConductEvent,
            bool isApproved,
            bool isSaved,
            string partialPoint)
        {
            try
            {
                ConductResult conductResult = new ConductResult(
                    idStudent,
                    idConductSchedule,
                    idConductEvent,
                    isApproved,
                    isSaved,
                    partialPoint);
                this.dsaContext.ConductResults.Add(conductResult);
                this.dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int checkEvaluatedConduct(int idStudent, int idConductSchedule)
        {
            try
            {
                ConductResult conductResult =
                    this.dsaContext.ConductResults.SingleOrDefault(
                        c => c.IdStudent == idStudent && c.IdConductSchedule == idConductSchedule);
                if (conductResult == null)
                {
                    return -1;
                }
                else return conductResult.IdConductResult;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public ConductResult GetConductResult(int idStudent, int idConductSchedule)
        {
            try
            {
                ConductResult conductResult =
                    this.dsaContext.ConductResults.SingleOrDefault(
                        c => c.IdStudent == idStudent && c.IdConductSchedule == idConductSchedule);
                return conductResult;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateConductResult(
            int idStudent,
            int idConductSchedule,
            int idConductEvent,
            bool isApproved,
            bool isSaved,
            string partialPoint)
        {
            try
            {
                ConductResult conductResult = this.GetConductResult(idStudent, idConductSchedule);
                conductResult.IsApproved = isApproved;
                conductResult.IsSaved = isSaved;
                conductResult.PartialPoint = partialPoint;
                this.dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}