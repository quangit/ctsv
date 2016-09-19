namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PagedList;

    using WEBPCTSV.Helpers.Common;
    using WEBPCTSV.Models.bean;

    public class LecturerBO
    {
        private readonly DSAContext dsaContext;

        public LecturerBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public int AddLecturer(
            string lecturerNumber,
            string type,
            string firstName,
            string lastName,
            string degree,
            string academicTitle,
            string position,
            int idFaculty,
            string email,
            string phoneNumber,
            string address)
        {
            try
            {
                Lecturer lecturer;
                lecturer = new Lecturer(
                    lecturerNumber,
                    type,
                    firstName,
                    lastName,
                    degree,
                    academicTitle,
                    position,
                    idFaculty,
                    email,
                    phoneNumber,
                    address);
                Account account = new Account(
                    "GV",
                    lecturerNumber,
                    StringExtension.GetMD5(lecturerNumber),
                    null,
                    null,
                    email,
                    null,
                    false,
                    4);
                this.dsaContext.Accounts.Add(account);
                this.dsaContext.SaveChanges();
                lecturer.IdAccount = account.IdAccount;
                this.dsaContext.Lecturers.Add(lecturer);
                this.dsaContext.SaveChanges();
                return lecturer.IdLecturer;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Lecturer GetLecturerById(int idLecturer)
        {
            return this.dsaContext.Lecturers.SingleOrDefault(l => l.IdLecturer.Equals(idLecturer));
        }

        public Lecturer GetLecturerByNumber(string lecturerNumber)
        {
            return this.dsaContext.Lecturers.SingleOrDefault(l => l.LecturerNumber.Equals(lecturerNumber));
        }

        public List<LecturerClass> GetLecturerClasses(int idLecturer)
        {
            return this.dsaContext.LecturerClasses.Where(classes => classes.IdLecturer.Equals(idLecturer)).ToList();
        }

        public IPagedList<Lecturer> GetListLecturer(int? page)
        {
            IPagedList<Lecturer> lecturers = null;
            int pageSize = 5;
            int pageNumber = page ?? 1;
            lecturers = this.dsaContext.Lecturers.OrderByDescending(l => l.IdFaculty).ToPagedList(pageNumber, pageSize);
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
                searchResult =
                    this.dsaContext.Lecturers.Where(
                        lec =>
                        (lec.LecturerClasses.Where(
                            lecClass =>
                            lecClass.IdSemester.Equals(idSemester) && lecClass.Class.ClassName.Contains(searchString))
                             .Count() > 0)).ToList();
                if (searchResult.Count() == 0)
                {
                    // Search by faculty
                    searchResult =
                        this.dsaContext.Lecturers.Where(
                            lec =>
                            (lec.Faculty.NumberFaculty.Equals(searchString) || lec.Faculty.Acronym.Equals(searchString)
                             || lec.Faculty.FacultyName.Contains(searchString))).ToList();
                    if (searchResult.Count() == 0)
                    {
                        // Search by lecturer
                        searchResult =
                            this.dsaContext.Lecturers.Where(
                                lec =>
                                ((lec.LastName + " " + lec.FirstName).Contains(searchString)
                                 || (lec.LastName.Contains(searchString) || lec.FirstName.Contains(searchString))))
                                .ToList();
                    }
                }
            }
            else
            {
                // return all lecturer
                searchResult = this.dsaContext.Lecturers.ToList();
            }

            return searchResult.ToPagedList(pageNumber, pageSize);
        }

        public List<Lecturer> GetListLecturerAll()
        {
            return this.dsaContext.Lecturers.OrderByDescending(l => l.IdFaculty).ToList();
            
        }

        public List<Lecturer> GetListLecturerByFaculty(int idFaculty)
        {
            return
                this.dsaContext.Lecturers.Where(l => l.IdFaculty == idFaculty)
                    .OrderByDescending(l => l.FirstName)
                    .ToList();
            
        }

        public bool RemoveLecturer(int idLecturer)
        {
            using (var context = this.dsaContext.Database.BeginTransaction())
            {
                try
                {
                    var lecturer = this.GetLecturerById(idLecturer);
                    if (lecturer != null)
                    {
                        var lecturerClasses = this.GetLecturerClasses(idLecturer);
                        foreach (LecturerClass lecClass in lecturerClasses)
                        {
                            int idLecturerClass = lecClass.IdLecturerClass;
                            this.dsaContext.LecturerClassDocuments.RemoveRange(
                                this.dsaContext.LecturerClassDocuments.Where(
                                    doc => doc.IdLecturerClass.Equals(idLecturerClass)));
                        }

                        this.dsaContext.SaveChanges();
                        this.dsaContext.LecturerClasses.RemoveRange(lecturerClasses);
                        this.dsaContext.SaveChanges();
                        int idAccount = lecturer.IdAccount;
                        this.dsaContext.Lecturers.Remove(lecturer);
                        this.dsaContext.SaveChanges();
                        Account accountLecturer = this.dsaContext.Accounts.Find(idAccount);
                        if (accountLecturer != null)
                        {
                            this.dsaContext.Accounts.Remove(accountLecturer);
                            this.dsaContext.SaveChanges();
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

        public bool UpdateLecturer(
            int idLecturer,
            string lecturerNumber,
            string lastName,
            string firstName,
            string degree,
            string type,
            string academicTitle,
            string position,
            int idFaculty,
            string email,
            string phoneNumber,
            string address)
        {
            try
            {
                Lecturer lecturer = this.dsaContext.Lecturers.SingleOrDefault(lec => lec.IdLecturer.Equals(idLecturer));
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
                Account account = this.dsaContext.Accounts.Find(idAccount);
                account.Email = email;
                account.UserName = lecturerNumber;
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