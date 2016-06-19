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
    public class LecturerDocumentSemesterBO
    {
        private DSAContext dsaContext;

        public LecturerDocumentSemesterBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public IPagedList<LecturerDocumentSemester> GetListLecturerDocumentSemester(int? page)
        {
            IPagedList<LecturerDocumentSemester> lecturerDocumentSemester = null;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            lecturerDocumentSemester = dsaContext.LecturerDocumentSemesters.OrderByDescending(l => l.IdDocumentSemester).ToPagedList(pageNumber, pageSize);
            return lecturerDocumentSemester;
        }
        public int AddLecturerDocumentSemester(string lecturerNumber, string type, string firstName, string lastName, string degree, string academicTitle, string position, int idFaculty, string email, string phoneNumber, string address)
        {
            try
            {
                Lecturer lecturer;
                lecturer = new Lecturer(lecturerNumber, type, firstName, lastName, degree, academicTitle, position, idFaculty, email, phoneNumber, address);
                Account account = new Account("GV", lecturerNumber, lecturerNumber, null, null, email, null, false, 4);
                lecturer.Account = account;
                dsaContext.Lecturers.Add(lecturer);
                dsaContext.SaveChanges();
                return lecturer.IdLecturer;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public object GetLecturerDocumentListBySemester(int idSemester)
        {
            var listDocument = dsaContext.LecturerDocumentSemesters.Where(lecDoc => lecDoc.IdSemester == idSemester).Select(lecDoc => new
            {
                idDocumentSemester = lecDoc.IdDocumentSemester,
                documentName = lecDoc.LecturerDocument.DocumentName,
                phase = lecDoc.Phase
            });
            if (listDocument != null && listDocument.Count() > 0)
            {
                return listDocument.ToArray();
            }
            else
            {
                return null;
            }
        }

        public bool UpdateLecturerDocumentSemester(int idLecturer, string lecturerNumber, string lastName, string firstName, string degree, string type, string academicTitle, string position, int idFaculty, string email, string phoneNumber, string address)
        {
            try
            {
                Lecturer lecturer = dsaContext.Lecturers.SingleOrDefault(lec => lec.IdLecturer.Equals(idLecturer));
                lecturer.LecturerNumber = lecturerNumber;
                lecturer.LastName = lastName;
                lecturer.FirstName = firstName;
                lecturer.Degree = degree;
                lecturer.Type = type;
                lecturer.Position = position;
                lecturer.AcademicTitle = academicTitle;
                lecturer.IdFaculty = idFaculty;
                lecturer.Email = email;
                lecturer.Account.Email = email;
                lecturer.Account.UserName = lecturerNumber;
                lecturer.PhoneNumber = phoneNumber;
                lecturer.Address = address;
                // Save data to database
                dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool RemoveLecturerDocumentSemester(int idLecturerDocumentSemester)
        {
            try
            {
                var lecturerDocumentSemester = dsaContext.LecturerDocumentSemesters.Find(idLecturerDocumentSemester);
                dsaContext.LecturerDocumentSemesters.Remove(lecturerDocumentSemester);
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