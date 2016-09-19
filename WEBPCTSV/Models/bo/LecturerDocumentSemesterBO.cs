namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Linq;

    using PagedList;

    using WEBPCTSV.Models.bean;

    public class LecturerDocumentSemesterBO
    {
        private readonly DSAContext dsaContext;

        public LecturerDocumentSemesterBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public int AddLecturerDocumentSemester(
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
                Account account = new Account("GV", lecturerNumber, lecturerNumber, null, null, email, null, false, 4);
                lecturer.Account = account;
                this.dsaContext.Lecturers.Add(lecturer);
                this.dsaContext.SaveChanges();
                return lecturer.IdLecturer;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public object GetLecturerDocumentListBySemester(int idSemester)
        {
            var listDocument =
                this.dsaContext.LecturerDocumentSemesters.Where(lecDoc => lecDoc.IdSemester == idSemester)
                    .Select(
                        lecDoc =>
                        new
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

        public IPagedList<LecturerDocumentSemester> GetListLecturerDocumentSemester(int? page)
        {
            IPagedList<LecturerDocumentSemester> lecturerDocumentSemester = null;
            int pageSize = 5;
            int pageNumber = page ?? 1;
            lecturerDocumentSemester =
                this.dsaContext.LecturerDocumentSemesters.OrderByDescending(l => l.IdDocumentSemester)
                    .ToPagedList(pageNumber, pageSize);
            return lecturerDocumentSemester;
        }

        public bool RemoveLecturerDocumentSemester(int idLecturerDocumentSemester)
        {
            try
            {
                var lecturerDocumentSemester = this.dsaContext.LecturerDocumentSemesters.Find(
                    idLecturerDocumentSemester);
                this.dsaContext.LecturerDocumentSemesters.Remove(lecturerDocumentSemester);
                this.dsaContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateLecturerDocumentSemester(
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