using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Models.bo
{
    public class ConductResultBO
    {
        private DSAContext dsaContext;
        public ConductResultBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }
        public int checkEvaluatedConduct(int idStudent, int idConductSchedule)
        {
            try
            {
                ConductResult conductResult = dsaContext.ConductResults.SingleOrDefault(c => c.IdStudent == idStudent && c.IdConductSchedule == idConductSchedule);
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
                ConductResult conductResult = dsaContext.ConductResults.SingleOrDefault(c => c.IdStudent == idStudent && c.IdConductSchedule == idConductSchedule);
                return conductResult;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddConductResult(int idStudent, int idConductSchedule, int idConductEvent, bool isApproved, bool isSaved, string partialPoint)
        {
            try
            {
                ConductResult conductResult = new ConductResult(idStudent, idConductSchedule, idConductEvent, isApproved, isSaved, partialPoint);
                dsaContext.ConductResults.Add(conductResult);
                dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateConductResult(int idStudent, int idConductSchedule, int idConductEvent, bool isApproved, bool isSaved, string partialPoint)
        {
            try
            {
                ConductResult conductResult = GetConductResult(idStudent, idConductSchedule);
                conductResult.IsApproved = isApproved;
                conductResult.IsSaved = isSaved;
                conductResult.PartialPoint = partialPoint;
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