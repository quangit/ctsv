namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Helpers.Common;
    using WEBPCTSV.Models.bean;

    public class ConductScheduleBO
    {
        private readonly DSAContext dsaContext;

        public ConductScheduleBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public bool AddSchedule(int idConductEvent, int idDecentralizationGroup, DateTime begin, DateTime end)
        {
            ConductSchedule conductSchedule = new ConductSchedule(idConductEvent, idDecentralizationGroup, begin, end);
            ConductSchedule conductScheduleExist =
                this.dsaContext.ConductSchedules.SingleOrDefault(
                    s => s.IdConductEvent == idConductEvent && s.IdDecenTralizationGroup == idDecentralizationGroup);
            if (conductScheduleExist != null)
            {
                throw new MyException(
                    "Đã tồn tại lịch đánh giá cho nhóm "
                    + conductScheduleExist.DecentralizationGroup.DecentralizationGroupName);
            }
            else
            {
                this.dsaContext.ConductSchedules.Add(conductSchedule);
                this.dsaContext.SaveChanges();
                return true;
            }
        }

        public bool DeleteConductEvent(int idConductEvent)
        {
            using (var context = this.dsaContext.Database.BeginTransaction())
            {
                try
                {
                    ConductEvent conductEvent = this.dsaContext.ConductEvents.Find(idConductEvent);
                    if (conductEvent != null)
                    {
                        this.dsaContext.ConductResults.RemoveRange(
                            this.dsaContext.ConductResults.Where(r => r.IdConductEvent == idConductEvent));
                        this.dsaContext.ConductSchedules.RemoveRange(
                            this.dsaContext.ConductSchedules.Where(s => s.IdConductEvent == idConductEvent));
                        this.dsaContext.ConductEvents.Remove(conductEvent);
                        this.dsaContext.SaveChanges();
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

        public bool DeleteSchedule(int idConductSchedule)
        {
            try
            {
                ConductSchedule conductSchedule = this.dsaContext.ConductSchedules.Find(idConductSchedule);
                if (conductSchedule != null)
                {
                    this.dsaContext.ConductResults.RemoveRange(
                        this.dsaContext.ConductResults.Where(r => r.IdConductSchedule == idConductSchedule));
                    this.dsaContext.ConductSchedules.Remove(conductSchedule);
                    this.dsaContext.SaveChanges();
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

        public List<ConductSchedule> GetListScheduleByEvent(int idConductEvent)
        {
            return this.dsaContext.ConductSchedules.Where(c => c.IdConductEvent.Equals(idConductEvent)).ToList();
        }

        public bool UpdateSchedule(int idConductSchedule, int idDecentralizationGroup, DateTime begin, DateTime end)
        {
            ConductSchedule conductSchedule = this.dsaContext.ConductSchedules.Find(idConductSchedule);
            if (conductSchedule != null)
            {
                if (conductSchedule.IdDecenTralizationGroup != idDecentralizationGroup)
                {
                    ConductSchedule conductScheduleExist =
                        this.dsaContext.ConductSchedules.SingleOrDefault(
                            s =>
                            s.IdConductEvent == conductSchedule.IdConductEvent
                            && s.IdDecenTralizationGroup == idDecentralizationGroup);
                    if (conductScheduleExist != null)
                    {
                        throw new MyException(
                            "Đã tồn tại lịch đánh giá cho nhóm "
                            + conductScheduleExist.DecentralizationGroup.DecentralizationGroupName);
                    }
                }

                conductSchedule.IdDecenTralizationGroup = idDecentralizationGroup;
                conductSchedule.BeginDateEvaluate = begin;
                conductSchedule.EndDateEvaluate = end;
                this.dsaContext.SaveChanges();
                return true;
            }
            else
            {
                throw new MyException("Không tìm thấy lịch đánh giá!");
            }
        }
    }
}