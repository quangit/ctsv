using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Helpers.Common;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV.Models.bo
{
    public class ConductEventBO
    {
        private DSAContext dsaContext;
        public ConductEventBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public ConductEvent GetConductEventBySemester(int idSemester)
        {
            return dsaContext.ConductEvents.SingleOrDefault(c => c.IdSemester == idSemester);
        }

        public List<ConductEvent> GetListConductEvent()
        {
            try
            {
                return dsaContext.ConductEvents.OrderByDescending(c => c.IdSemester).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ConductEvent Get(int id)
        {
            try
            {
                return dsaContext.ConductEvents.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int AddConductEvent(int idSemester, int idConductForm)
        {
            ConductEvent conductEventExist = dsaContext.ConductEvents.SingleOrDefault(c => c.IdSemester == idSemester);
            if (conductEventExist != null)
            {
                throw new MyException("Đã tồn tại đợt đánh giá rèn luyện cho kỳ " + conductEventExist.Semester.SemesterYear.SemesterYearName + " năm học " + conductEventExist.Semester.Year.YearName);
            }
            else
            {
                try
                {
                    ConductEvent conductEvent = new ConductEvent(idSemester, idConductForm);
                    dsaContext.ConductEvents.Add(conductEvent);
                    dsaContext.SaveChanges();
                    return conductEvent.IdConductEvent;
                }
                catch
                {
                    throw new MyException("Xảy ra lỗi trong quá trình cập nhật dữ liệu!");
                }
            }
        }

        public bool UpdateConductEvent(int idConductEvent, int idSemester, int idConductForm)
        {
            ConductEvent conductEvent = dsaContext.ConductEvents.Find(idConductEvent);
            if (conductEvent != null)
            {
                if (conductEvent.IdSemester != idSemester)
                {
                    ConductEvent conductEventExist = dsaContext.ConductEvents.SingleOrDefault(c => c.IdConductEvent == conductEvent.IdConductEvent && c.IdSemester == idSemester);
                    if (conductEventExist != null)
                    {
                        throw new MyException("Đã tồn đợt đánh giá rèn luyện cho học kỳ " + conductEventExist.Semester.SemesterYear.SemesterYearName + " năm học " + conductEventExist.Semester.Year.YearName);
                    }
                }
                conductEvent.IdSemester = idSemester;
                conductEvent.IdConductForm = idConductForm;
                dsaContext.SaveChanges();
                return true;
            }
            else
            {
                throw new MyException("Không tìm thấy lịch đánh giá!");
            }
        }
    }
}