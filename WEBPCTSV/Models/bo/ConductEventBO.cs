namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Helpers.Common;
    using WEBPCTSV.Models.bean;

    public class ConductEventBO
    {
        private readonly DSAContext dsaContext;

        public ConductEventBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public int AddConductEvent(int idSemester, int idConductForm)
        {
            ConductEvent conductEventExist =
                this.dsaContext.ConductEvents.SingleOrDefault(c => c.IdSemester == idSemester);
            if (conductEventExist != null)
            {
                throw new MyException(
                    "Đã tồn tại đợt đánh giá rèn luyện cho kỳ "
                    + conductEventExist.Semester.SemesterYear.SemesterYearName + " năm học "
                    + conductEventExist.Semester.Year.YearName);
            }
            else
            {
                try
                {
                    ConductEvent conductEvent = new ConductEvent(idSemester, idConductForm);
                    this.dsaContext.ConductEvents.Add(conductEvent);
                    this.dsaContext.SaveChanges();
                    return conductEvent.IdConductEvent;
                }
                catch
                {
                    throw new MyException("Xảy ra lỗi trong quá trình cập nhật dữ liệu!");
                }
            }
        }

        public ConductEvent Get(int id)
        {
            try
            {
                return this.dsaContext.ConductEvents.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ConductEvent GetConductEventBySemester(int idSemester)
        {
            return this.dsaContext.ConductEvents.SingleOrDefault(c => c.IdSemester == idSemester);
        }

        public List<ConductEvent> GetListConductEvent()
        {
            try
            {
                return this.dsaContext.ConductEvents.OrderByDescending(c => c.IdSemester).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateConductEvent(int idConductEvent, int idSemester, int idConductForm)
        {
            ConductEvent conductEvent = this.dsaContext.ConductEvents.Find(idConductEvent);
            if (conductEvent != null)
            {
                if (conductEvent.IdSemester != idSemester)
                {
                    ConductEvent conductEventExist =
                        this.dsaContext.ConductEvents.SingleOrDefault(
                            c => c.IdConductEvent == conductEvent.IdConductEvent && c.IdSemester == idSemester);
                    if (conductEventExist != null)
                    {
                        throw new MyException(
                            "Đã tồn đợt đánh giá rèn luyện cho học kỳ "
                            + conductEventExist.Semester.SemesterYear.SemesterYearName + " năm học "
                            + conductEventExist.Semester.Year.YearName);
                    }
                }

                conductEvent.IdSemester = idSemester;
                conductEvent.IdConductForm = idConductForm;
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