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
    public class LecturerBO
    {
        private DSAContext dsaContext;

        public LecturerBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public List<Lecturer> GetListLecturerAll()
        {
            return dsaContext.Lecturers.OrderByDescending(l => l.IdFaculty).ToList();;
        }

        public IPagedList<Lecturer> GetListLecturer(int? page)
        {
            IPagedList<Lecturer> lecturers = null;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            lecturers = dsaContext.Lecturers.OrderByDescending(l => l.IdFaculty).ToPagedList(pageNumber, pageSize);
            return lecturers;
        }
        public IPagedList<Lecturer> GetListLecturer(int? page, string searchString, int idSemester)
        {
            // Page size
            int pageSize = 10;

            // Get parameter
            int pageNumber = page ?? 1;

            List<Lecturer> searchResult = null;
            if (!StringExtension.IsNullOrWhiteSpace(searchString))
            {
                // Search by class
                searchResult = dsaContext.Lecturers.Where(lec => (lec.LecturerClasses.Where(lecClass => lecClass.IdSemester.Equals(idSemester)
                                                          && lecClass.Class.ClassName.Contains(searchString)).Count() > 0
                                                          )).ToList();
                if (searchResult.Count() == 0)
                {
                    // Search by faculty
                    searchResult = dsaContext.Lecturers.Where(lec => (lec.Faculty.NumberFaculty.Equals(searchString) || lec.Faculty.Acronym.Equals(searchString) || lec.Faculty.FacultyName.Contains(searchString))).ToList();
                    if (searchResult.Count() == 0)
                    {
                        // Search by lecturer
                        searchResult = dsaContext.Lecturers.Where(lec => ((lec.LastName + " " + lec.FirstName).Contains(searchString) || (lec.LastName.Contains(searchString) || lec.FirstName.Contains(searchString)))).ToList();
                    }
                }
            }
            else
            {
                // return all lecturer
                searchResult = dsaContext.Lecturers.ToList();
            }
            return searchResult.ToPagedList(pageNumber, pageSize);
        }

        public int AddLecturer(string lecturerNumber, string type, string firstName, string lastName, string degree, string academicTitle, string position, int idFaculty, string email, string phoneNumber, string address)
        {
            try
            {
                Lecturer lecturer;
                lecturer = new Lecturer(lecturerNumber, type, firstName, lastName, degree, academicTitle, position, idFaculty, email, phoneNumber, address);
                Account account = new Account("GV", lecturerNumber, StringExtension.GetMD5(lecturerNumber), null, null, email, null, false, 4);
                dsaContext.Accounts.Add(account);
                dsaContext.SaveChanges();
                lecturer.IdAccount = account.IdAccount;
                dsaContext.Lecturers.Add(lecturer);
                dsaContext.SaveChanges();
                return lecturer.IdLecturer;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Lecturer GetLecturerById(int idLecturer)
        {
            return dsaContext.Lecturers.SingleOrDefault(l => l.IdLecturer.Equals(idLecturer));
        }
        public Lecturer GetLecturerByNumber(string lecturerNumber)
        {
            return dsaContext.Lecturers.SingleOrDefault(l => l.LecturerNumber.Equals(lecturerNumber));
        }

        public bool UpdateLecturer(int idLecturer, string lecturerNumber, string lastName, string firstName, string degree, string type, string academicTitle, string position, int idFaculty, string email, string phoneNumber, string address)
        {
                try
                {
                    Lecturer lecturer = dsaContext.Lecturers.SingleOrDefault(lec => lec.IdLecturer.Equals(idLecturer));
                    int idAccount = lecturer.IdAccount;
                    lecturer.LecturerNumber = lecturerNumber;
                    lecturer.LastName = lastName;
                    lecturer.FirstName = firstName;
                    lecturer.Degree = degree;
                    lecturer.Type = type;
                    lecturer.Position = position;
                    lecturer.AcademicTitle = academicTitle;
                    lecturer.IdFaculty = idFaculty;
                    lecturer.Email = email;
                    lecturer.PhoneNumber = phoneNumber;
                    lecturer.Address = address;
                    Account account = dsaContext.Accounts.Find(idAccount);
                    account.Email = email;
                    account.UserName = lecturerNumber;
                    dsaContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
        }
        public List<LecturerClass> GetLecturerClasses(int idLecturer)
        {
            return dsaContext.LecturerClasses.Where(classes => classes.IdLecturer.Equals(idLecturer)).ToList();
        }

        public bool RemoveLecturer(int idLecturer)
        {
            using (var context = dsaContext.Database.BeginTransaction())
            {
                try
                {
                    var lecturer = GetLecturerById(idLecturer);
                    if (lecturer != null)
                    {
                        var lecturerClasses = GetLecturerClasses(idLecturer);
                        foreach (LecturerClass lecClass in lecturerClasses)
                        {
                            int idLecturerClass = lecClass.IdLecturerClass;
                            dsaContext.LecturerClassDocuments.RemoveRange(dsaContext.LecturerClassDocuments.Where(doc => doc.IdLecturerClass.Equals(idLecturerClass)));
                        }
                        dsaContext.SaveChanges();
                        dsaContext.LecturerClasses.RemoveRange(lecturerClasses);
                        dsaContext.SaveChanges();
                        int idAccount = lecturer.IdAccount;
                        dsaContext.Lecturers.Remove(lecturer);
                        dsaContext.SaveChanges();
                        Account accountLecturer = dsaContext.Accounts.Find(idAccount);
                        if (accountLecturer != null)
                        {
                            dsaContext.Accounts.Remove(accountLecturer);
                            dsaContext.SaveChanges();
                        }
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

        public List<Lecturer> GetListLecturerByFaculty(int idFaculty)
        {
            return dsaContext.Lecturers.Where(l => l.IdFaculty == idFaculty).OrderByDescending(l => l.FirstName).ToList(); ;
        }
    }
}