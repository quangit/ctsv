using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Helpers.Common;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Models.bo
{
    public class ConductScheduleBO
    {
        private DSAContext dsaContext;
        public ConductScheduleBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public List<ConductSchedule> GetListScheduleByEvent(int idConductEvent)
        {
            return dsaContext.ConductSchedules.Where(c => c.IdConductEvent.Equals(idConductEvent)).ToList();
        }

        public bool UpdateSchedule(int idConductSchedule, int idDecentralizationGroup, DateTime begin, DateTime end)
        {
            ConductSchedule conductSchedule = dsaContext.ConductSchedules.Find(idConductSchedule);
            if (conductSchedule != null)
            {
                if (conductSchedule.IdDecenTralizationGroup != idDecentralizationGroup)
                {
                    ConductSchedule conductScheduleExist = dsaContext.ConductSchedules.SingleOrDefault(s => s.IdConductEvent == conductSchedule.IdConductEvent && s.IdDecenTralizationGroup == idDecentralizationGroup);
                    if (conductScheduleExist != null)
                    {
                        throw new MyException("Đã tồn tại lịch đánh giá cho nhóm " + conductScheduleExist.DecentralizationGroup.DecentralizationGroupName);
                    }
                }
                conductSchedule.IdDecenTralizationGroup = idDecentralizationGroup;
                conductSchedule.BeginDateEvaluate = begin;
                conductSchedule.EndDateEvaluate = end;
                dsaContext.SaveChanges();
                return true;
            }
            else
            {
                throw new MyException("Không tìm thấy lịch đánh giá!");
            }
        }

        public bool AddSchedule(int idConductEvent, int idDecentralizationGroup, DateTime begin, DateTime end)
        {
            ConductSchedule conductSchedule = new ConductSchedule(idConductEvent, idDecentralizationGroup, begin, end);
            ConductSchedule conductScheduleExist = dsaContext.ConductSchedules.SingleOrDefault(s => s.IdConductEvent == idConductEvent && s.IdDecenTralizationGroup == idDecentralizationGroup);
            if (conductScheduleExist != null)
            {
                throw new MyException("Đã tồn tại lịch đánh giá cho nhóm " + conductScheduleExist.DecentralizationGroup.DecentralizationGroupName);
            }
            else
            {
                dsaContext.ConductSchedules.Add(conductSchedule);
                dsaContext.SaveChanges();
                return true;
            }
        }

        public bool DeleteSchedule(int idConductSchedule)
        {
            try
            {
                ConductSchedule conductSchedule = dsaContext.ConductSchedules.Find(idConductSchedule);
                if (conductSchedule != null)
                {
                    dsaContext.ConductResults.RemoveRange(dsaContext.ConductResults.Where(r => r.IdConductSchedule == idConductSchedule));
                    dsaContext.ConductSchedules.Remove(conductSchedule);
                    dsaContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteConductEvent(int idConductEvent)
        {

            using (var context = dsaContext.Database.BeginTransaction())
            {
                try
                {
                    ConductEvent conductEvent = dsaContext.ConductEvents.Find(idConductEvent);
                    if (conductEvent != null)
                    {
                        dsaContext.ConductResults.RemoveRange(dsaContext.ConductResults.Where(r => r.IdConductEvent == idConductEvent));
                        dsaContext.ConductSchedules.RemoveRange(dsaContext.ConductSchedules.Where(s => s.IdConductEvent == idConductEvent));
                        dsaContext.ConductEvents.Remove(conductEvent);
                        dsaContext.SaveChanges();
                        context.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    context.Rollback();
                    return false;
                }
            }
        }
    }
}