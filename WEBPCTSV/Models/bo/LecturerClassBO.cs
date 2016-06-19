using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPCTSV.Helpers.Common;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Helpers;
using System.Web.Mvc;


namespace WEBPCTSV.Models.bo
{
    public class LecturerClassBO
    {
        private DSAContext dsaContext;

        public LecturerClassBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public bool AddClassOwner(int idLecturer, int idSemester, int idClass, string endDate, string scheduleFirst, string roomFirst, string scheduleSecond, string roomSecond)
        {
            using (var contextTransaction = dsaContext.Database.BeginTransaction())
            {
                try
                {
                    LecturerClass lecturerClass;
                    lecturerClass = new LecturerClass(idLecturer, idSemester, idClass, endDate, scheduleFirst, roomFirst, scheduleSecond, roomSecond);
                    // Save data to database
                    dsaContext.LecturerClasses.Add(lecturerClass);
                    dsaContext.SaveChanges();

                    //Add list document have to submit in semester
                    var docSemester = dsaContext.LecturerDocumentSemesters.Where(lecDocSemester => lecDocSemester.IdSemester.Equals(idSemester)).ToList();
                    //bool isSelectedDocument = false;
                    foreach (LecturerDocumentSemester doc in docSemester)
                    {
                        LecturerClassDocument lecDocument = new LecturerClassDocument(lecturerClass.IdLecturerClass, doc.IdDocumentSemester, false);
                        dsaContext.LecturerClassDocuments.Add(lecDocument);
                    }
                    dsaContext.SaveChanges();
                    contextTransaction.Commit();
                    return true;
                }
                catch
                {
                    contextTransaction.Rollback();
                    return false;
                }
            };
        }

        public bool RemoveClassOwner(int idLecturerClass, int idLecturer)
        {
            using (var contextTransaction = dsaContext.Database.BeginTransaction())
            {
                try
                {
                    var lecturerClassDocumentArr = dsaContext.LecturerClassDocuments.Where(lecDoc => lecDoc.IdLecturerClass.Equals(idLecturerClass));
                    foreach (LecturerClassDocument lecturerClassDocument in lecturerClassDocumentArr)
                    {
                        dsaContext.LecturerClassDocuments.Remove(lecturerClassDocument);
                    }
                    dsaContext.SaveChanges();
                    LecturerClass lecturerClass = dsaContext.LecturerClasses.SingleOrDefault(lecClass => lecClass.IdLecturerClass.Equals(idLecturerClass));
                    dsaContext.LecturerClasses.Remove(lecturerClass);
                    dsaContext.SaveChanges();
                    contextTransaction.Commit();
                    return true;
                }
                catch
                {
                    contextTransaction.Rollback();
                    return false;
                }
            };
        }

        public bool UpdateClassOwner(int idLecturerClass, int idSemester, int idClass, DateTime endDate, string scheduleFirst, string scheduleSecond, string roomFirst, string roomSecond, FormCollection documents)
        {
            using (var contextTransaction = dsaContext.Database.BeginTransaction())
            {
                try
                {
                    // Entity not allow to update primary key so - update key value via raw sql
                    dsaContext.Database.ExecuteSqlCommand("UPDATE LECTURERCLASS SET IdSemester = {0}, IdClass = {1}, EndDate = {2}, ScheduleFirst = {3}, RoomFirst = {4}, ScheduleSecond = {5}, RoomSecond = {6} WHERE IdLecturerClass = {7}",
                        idSemester, idClass, endDate, scheduleFirst, roomFirst, scheduleSecond, roomSecond, idLecturerClass);
                    // Save data to database
                    dsaContext.SaveChanges();

                    // Remove all document in last change
                    dsaContext.LecturerClassDocuments.RemoveRange(dsaContext.LecturerClassDocuments.Where(lecClass => lecClass.IdLecturerClass.Equals(idLecturerClass)));

                    //Add list document
                    var docs = dsaContext.LecturerDocumentSemesters.Where(lecDoc => lecDoc.IdSemester.Equals(idSemester)).ToList();
                    //bool isSelectedDocument = false;
                    foreach (LecturerDocumentSemester doc in docs)
                    {
                        LecturerClassDocument lecClassDoc;
                        if (documents["D" + doc.IdDocumentSemester] != null && !(documents["D" + doc.IdDocumentSemester]).Equals(""))
                        {
                            lecClassDoc = new LecturerClassDocument(idLecturerClass, doc.IdDocumentSemester, true);
                        }
                        else
                        {
                            lecClassDoc = new LecturerClassDocument(idLecturerClass, doc.IdDocumentSemester, false);
                        }
                        dsaContext.LecturerClassDocuments.Add(lecClassDoc);
                    }
                    dsaContext.SaveChanges();
                    contextTransaction.Commit();
                    return true;
                }
                catch
                {
                    contextTransaction.Rollback();
                    return false;
                }

            };
        }

        public object GetDetailClassOwner(int idLecturerClass)
        {
            try
            {
                var lecturerClass = dsaContext.LecturerClasses.Single(lecClass => lecClass.IdLecturerClass.Equals(idLecturerClass));
                var listDocument = dsaContext.LecturerClassDocuments.Where(lecDoc => lecDoc.IdLecturerClass.Equals(idLecturerClass)).Select(doc => new
                {
                    idDocumentSemester = doc.IdDocumentSemester,
                    isApproved = doc.IsApproved,
                });
                var listDocumentSemester = dsaContext.LecturerDocumentSemesters.Where(lecDoc => lecDoc.IdSemester.Equals(lecturerClass.IdSemester)).Select(lecDoc => new
                {
                    idDocumentSemester = lecDoc.IdDocumentSemester,
                    documentName = lecDoc.LecturerDocument.DocumentName,
                    phase = lecDoc.Phase
                });
                var toJson = new
                {
                    idLecturerClass = lecturerClass.IdLecturerClass,
                    idLecturer = lecturerClass.IdLecturer,
                    idSemester = lecturerClass.IdSemester,
                    idClass = lecturerClass.IdClass,
                    endDate = lecturerClass.EndDate.ToString("yyyy-MM-dd"),
                    scheduleFirst = (lecturerClass.ScheduleFirst != null) ? lecturerClass.ScheduleFirst.Value.ToString("yyyy-MM-dd") : lecturerClass.ScheduleFirst.ToString(),
                    roomFirst = lecturerClass.RoomFirst,
                    scheduleSecond = (lecturerClass.ScheduleSecond != null) ? lecturerClass.ScheduleSecond.Value.ToString("yyyy-MM-dd") : lecturerClass.ScheduleSecond.ToString(),
                    roomSecond = lecturerClass.RoomSecond,
                    documents = listDocument.ToArray(),
                    listDocumentSemester = listDocumentSemester.ToArray()
                };
                if (lecturerClass != null)
                {
                    return toJson;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LecturerClass> GetLecturerClasses(int idLecturer)
        {
            return dsaContext.LecturerClasses.Where(classes => classes.IdLecturer.Equals(idLecturer)).ToList();
        }
    }
}