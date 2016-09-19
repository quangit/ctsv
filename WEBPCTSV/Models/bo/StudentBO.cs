namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using OfficeOpenXml;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.Support;

    public class StudentBO
    {
        public static int percentProgress;

        public static List<ErrorImportExcel> ListErrorImportExcels = new List<ErrorImportExcel>();

        readonly DSAContext context = new DSAContext();

        private readonly DSAContext dsaContext;

        readonly int rowInPage = new Configuration().rowInPage;

        public StudentBO()
        {
        }

        public StudentBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public void AddStudent(FormCollection form)
        {
            Student student = new Student();
            student.LastNameStudent = form["lastName"];
            student.FirstNameStudent = form["firstName"];
            student.StudentNumber = form["studentNumber"];
            student.Sex = form["sexRadio"];
            if (student.Sex == null) student.Sex = string.Empty;
            student.IdClass = Convert.ToInt32(form["selectClass"]);
            if (student.IdClass == 0) student.IdClass = null;
            student.MobilePhoneNumber = form["mobilePhoneNumber"];
            student.LandlineNumber = form["landlinePhoneNumber"];
            student.Email = form["email"];
            student.IdentityCard = form["identityCardNumber"];
            string identityCardDate = form["identityCardDate"];
            if (!identityCardDate.Equals(string.Empty)) student.DateOfIssuanceIdentityCard = Convert.ToDateTime(identityCardDate);
            student.IdProvinceIdentityCard = form["identityCardPlace"];
            if (student.IdProvinceIdentityCard.Equals(string.Empty)) student.IdProvinceIdentityCard = null;
            student.idState = form["selectState"];
            AccountBO accountBo = new AccountBO();
            student.IdAccount = accountBo.AddAccountSV(student.StudentNumber);
            this.context.Students.Add(student);
            this.context.SaveChanges();
        }

        public void AddStudent(string studentNumber, string studentName, string birthDay, string sex, int idClass)
        {
            using (DSAContext ct = new DSAContext())
            {
                try
                {
                    Student student = null;
                    string[] listString = studentName.Split(' ');
                    try
                    {
                        student = ct.Students.Single(s => s.StudentNumber.Equals(studentNumber));
                    }
                    catch
                    {
                    }

                    if (student != null)
                    {
                        student.FirstNameStudent = listString[listString.Count() - 1];
                        student.LastNameStudent = studentName.Replace(student.FirstNameStudent, string.Empty);
                        student.BirthDay = Convert.ToDateTime(birthDay);
                        student.Sex = sex;
                        student.IdClass = idClass;
                        student.IsOrphan = false;
                        student.ObjectTuitionFee = 1;
                        ct.SaveChanges();
                    }
                    else
                    {
                        student = new Student();
                        student.StudentNumber = studentNumber;
                        int idAccount = new AccountBO().AddAccountSV(studentNumber);
                        student.IdAccount = idAccount;
                        student.FirstNameStudent = listString[listString.Count() - 1];
                        student.LastNameStudent = studentName.Replace(student.FirstNameStudent, string.Empty);
                        if (birthDay != string.Empty) student.BirthDay = Convert.ToDateTime(birthDay);
                        student.Sex = sex;
                        student.IdClass = idClass;
                        student.IsOrphan = false;
                        student.ObjectTuitionFee = 1;
                        ct.Students.Add(student);
                        ct.SaveChanges();
                    }
                }
                catch
                {
                }
            }
        }

        public void EditStudent(int idStudent, FormCollection form)
        {
            Student student = this.context.Students.Single(st => st.IdStudent.Equals(idStudent));
            if (!string.IsNullOrEmpty(form["lastName"])) student.LastNameStudent = form["lastName"];
            if (!string.IsNullOrEmpty(form["fristName"])) student.FirstNameStudent = form["firstName"];
            if (!string.IsNullOrEmpty(form["studentNumber"])) student.StudentNumber = form["studentNumber"];
            student.Sex = form["sexRadio"];
            string birthDay = form["birthday"];
            if (birthDay != string.Empty) student.BirthDay = Convert.ToDateTime(birthDay);
            if (!string.IsNullOrEmpty(form["selectClass"])) student.IdClass = Convert.ToInt32(form["selectClass"]);
            if (student.IdClass == 0) student.IdClass = null;
            student.MobilePhoneNumber = form["mobilePhoneNumber"];
            student.LandlineNumber = form["landlinePhoneNumber"];
            student.Email = form["email"];
            this.context.SaveChanges();
        }

        public void EditStudent2(int idStudent, FormCollection form)
        {
            Student student = this.context.Students.Single(st => st.IdStudent.Equals(idStudent));
            student.idState = form["selectState"];
            student.IdentityCard = form["identityCardNumber"];
            string dateOfIssuanceIdentityCard = form["identityCardDate"];
            if (dateOfIssuanceIdentityCard != string.Empty) student.DateOfIssuanceIdentityCard = Convert.ToDateTime(dateOfIssuanceIdentityCard);
            if (form["identityCardPlace"] != string.Empty) student.IdProvinceIdentityCard = form["identityCardPlace"];
            if (form["selectWard_Permanent"] != string.Empty) student.IdWardPermanentResidence = form["selectWard_Permanent"];
            student.AdditionalPermanentResidence = form["additionalPermanentplace"];
            if (form["boardingRadio"] == "true") student.BoardingAddress = true;
            if (form["boardingRadio"] == "false") student.BoardingAddress = false;
            if (form["selectWard_ExternAddress"] != string.Empty) student.IdWardExternAddress = form["selectWard_ExternAddress"];
            student.AdditionalExternAddress = form["additionalExternAddress"];
            if (form["selectWard_NativeLand"] != string.Empty) student.IdWardNativeLand = form["selectWard_NativeLand"];
            student.AdditionalNativeLand = form["additionalNativeLand"];
            if (form["selectWard_BirthPlace"] != string.Empty) student.IdWardBirthPlace = form["selectWard_BirthPlace"];
            student.AdditionalBirthplace = form["additionalBirthPlace"];
            if (form["Religion"] != string.Empty) student.IdReligion = Convert.ToInt32(form["Religion"]);
            if (form["Ethnic"] != string.Empty) student.IdEthnic = Convert.ToInt32(form["Ethnic"]);
            if (form["Area"] != string.Empty) student.IdArea = form["Area"];
            student.HightSchoolName = form["hightSchoolName"];
            if (form["selectPlaceSchoolDistrict"] != string.Empty) student.IdDistrictHightSchoolName = form["selectPlaceSchoolDistrict"];

            if (form["RegistrationTemporaryResidenceTime"] != string.Empty)
                student.RegistrationTemporaryResidenceTime =
                    Convert.ToDateTime(form["RegistrationTemporaryResidenceTime"]);
            student.TemporaryResidenceNotebookNumber = form["TemporaryResidenceNotebookNumber"];
            student.PhoneNumberHouseholder = form["PhoneNumberHouseholder"];
            this.context.SaveChanges();
        }

        public void EditStudent3(int idStudent, FormCollection form)
        {
            Student student = this.context.Students.Single(st => st.IdStudent.Equals(idStudent));
            student.HealthInsuranceNumber = form["HealthInsuranceNumber"];
            student.RegisteredMedicalArea = form["registeredMedicalArea"];
            if (form["selectBloodGroup"] != string.Empty) student.IdBloodGroup = Convert.ToInt32(form["selectBloodGroup"]);
            student.HealthStatus = form["healthStatus"];
            if (form["numberbloodDonors"] != string.Empty) student.NumberBloodDonors = Convert.ToInt32(form["numberbloodDonors"]);
            if (form["weight"] != string.Empty) student.Weight = Convert.ToInt32(form["weight"]);
            if (form["height"] != string.Empty) student.Height = Convert.ToInt32(form["height"]);
            this.context.SaveChanges();
        }

        public void EditStudent4(int idStudent, FormCollection form)
        {
            Student student = this.context.Students.Single(st => st.IdStudent.Equals(idStudent));
            if (form["dateAdmission"] != string.Empty) student.DateAdmission = Convert.ToDateTime(form["dateAdmission"]);
            student.EducationalBackground = form["academicLevel"];
            if (!string.IsNullOrEmpty(form["madeOfStudy"])) student.IdMadeOfStudy = Convert.ToInt32(form["madeOfStudy"]);
            if (!string.IsNullOrEmpty(form["typeInput"])) student.IdTypeInput = Convert.ToInt32(form["typeInput"]);
            if (!string.IsNullOrEmpty(form["IdPreferentialPolicyBeneficiary"])) student.IdPreferentialPolicyBeneficiaries = Convert.ToInt32(form["IdPreferentialPolicyBeneficiary"]);
            student.IsOrphan = Convert.ToBoolean(form["OrPhan"]);
            student.ObjectTuitionFee = Convert.ToInt32(form["ObjectTuitionFee"]);
            this.context.SaveChanges();
        }

        public string GetImageStudent(int idStudent)
        {
            return this.context.Students.SingleOrDefault(s => s.IdStudent == idStudent).LinkAvatar;
        }

        public List<Student> GetListSearchStudent(string search)
        {
            List<Student> listStudent = null;
            if (!string.IsNullOrEmpty(search))
            {
                listStudent =
                    this.context.Students.Where(
                        st =>
                        st.StudentNumber.Contains(search)
                        || (st.LastNameStudent + string.Empty + st.FirstNameStudent).ToUpper()
                               .Trim()
                               .Contains(search.ToUpper().Trim())
                        || st.Class.ClassName.ToUpper().Trim().Contains(search.ToUpper().Trim())
                        || st.Class.Faculty.FacultyName.ToUpper().Trim().Contains(search.ToUpper().Trim())).ToList();
            }
            else
            {
                listStudent = this.context.Students.ToList();
            }

            return listStudent;
        }

        public List<Student> GetListStudent()
        {
            return this.context.Students.ToList();
        }

        public List<Student> GetListStudent(int page, string search)
        {
            List<Student> listStudent = this.GetListSearchStudent(search);
            return
                listStudent.OrderByDescending(st => st.IdStudent)
                    .Skip((page - 1) * this.rowInPage)
                    .Take(this.rowInPage)
                    .ToList();
        }

        public List<Student> GetListStudentByClass(int idClass)
        {
            return this.context.Students.Where(s => s.IdClass == idClass).ToList();
        }

        public List<Student> GetListStudentByFaculty(int idFaculty)
        {
            try
            {
                return
                    this.dsaContext.Students.Where(
                        s =>
                        s.Class.Faculty.IdFaculty == idFaculty && s.IsGraduated == false && s.IsExpelled == false
                        && s.IsReserved == false).ToList();
            }
            catch
            {
                return null;
            }
        }

        public Student GetStudent(int idStudent)
        {
            return this.context.Students.Single(st => st.IdStudent == idStudent);
        }

        public Student GetStudentByIdAccount(int idAccount)
        {
            Account account = new AccountBO().GetAccount(idAccount);
            return account.Students.First();
        }

        public Student GetStudentByStudentNumber(string StudentNumber)
        {
            try
            {
                return this.context.Students.Single(s => s.StudentNumber.Equals(StudentNumber));
            }
            catch
            {
            }

            return null;
        }

        public void ImportStudent(ExcelPackage package)
        {
            var currentSheet = package.Workbook.Worksheets;
            var workSheet = currentSheet.Last();
            int maxCol = workSheet.Dimension.End.Column;
            int maxRow = workSheet.Dimension.End.Row;
            percentProgress = 0;

            for (int rowIterator = 2; rowIterator <= maxRow; rowIterator++)
            {
                try
                {
                    string studentNumber = workSheet.Cells[rowIterator, 2].Value.ToString();
                    string studentName = workSheet.Cells[rowIterator, 3].Value.ToString();
                    string birthDay = string.Empty;
                    if (workSheet.Cells[rowIterator, 5].Value != null)
                    {
                        birthDay = workSheet.Cells[rowIterator, 5].Value.ToString();
                    }

                    string sex = workSheet.Cells[rowIterator, 6].Value.ToString();
                    if (sex.Trim().Equals("1"))
                    {
                        sex = "Nam";
                    }
                    else
                    {
                        sex = "Nữ";
                    }

                    if (workSheet.Cells[rowIterator, 7].Value != null)
                    {
                        string className = workSheet.Cells[rowIterator, 7].Value.ToString();
                        Class c = new ClassBO().Get(className);
                        if (c == null)
                        {
                            string facultyNumber = workSheet.Cells[rowIterator, 8].Value.ToString();
                            string courseName = className.Substring(0, 2);
                            c = new ClassBO().Insert(className, courseName, facultyNumber);
                        }

                        this.AddStudent(studentNumber, studentName, birthDay, sex, c.IdClass);
                        percentProgress = (int)(((double)rowIterator / maxRow) * 100);
                    }
                }
                catch
                {
                }
            }
        }

        public void ImportStudent2(ExcelPackage package)
        {
            var currentSheet = package.Workbook.Worksheets;
            var workSheet = currentSheet.Last();
            int maxCol = workSheet.Dimension.End.Column;
            int maxRow = workSheet.Dimension.End.Row;
            percentProgress = 0;

            for (int rowIterator = 2; rowIterator <= maxRow; rowIterator++)
            {
                try
                {
                    using (DSAContext ct = new DSAContext())
                    {
                        string studentNumber = workSheet.Cells[rowIterator, 1].Value.ToString();

                        Student student = ct.Students.Single(s => s.StudentNumber.Equals(studentNumber));

                        // string studentName = workSheet.Cells[rowIterator, 3].Value.ToString();
                        // string[] listString = studentName.Split(' ');
                        // student.FirstNameStudent = listString[listString.Count() - 1];
                        // student.LastNameStudent = studentName.Replace(student.FirstNameStudent, "");

                        // if (workSheet.Cells[rowIterator, 5].Value != null)
                        // {
                        // student.BirthDay = Convert.ToDateTime(workSheet.Cells[rowIterator, 5].Value.ToString());
                        // }

                        // string sex = workSheet.Cells[rowIterator, 4].Value.ToString();
                        // if (sex.Trim().Equals("1"))
                        // {
                        // student.Sex = "Nam";
                        // }
                        // else
                        // {
                        // student.Sex = "Nữ";
                        // }
                        if (workSheet.Cells[rowIterator, 6].Value != null)
                        {
                            try
                            {
                                string ethnic = workSheet.Cells[rowIterator, 6].Value.ToString();
                                Ethnic et = ct.Ethnics.Single(e => e.EthnicName.Equals(ethnic));
                                student.IdEthnic = et.IdEthnic;
                            }
                            catch
                            {
                            }
                        }

                        if (workSheet.Cells[rowIterator, 7].Value != null)
                        {
                            try
                            {
                                string state = workSheet.Cells[rowIterator, 7].Value.ToString();
                                State st = ct.States.Single(c => c.StateName.Equals(state));
                                student.idState = st.IdState;
                            }
                            catch
                            {
                            }
                        }

                        if (workSheet.Cells[rowIterator, 20].Value != null)
                        {
                            try
                            {
                                string madeofStudy = workSheet.Cells[rowIterator, 20].Value.ToString();
                                MadeOfStudy mod = ct.MadeOfStudies.Single(m => m.MadeOfStudyName.Equals(madeofStudy));
                                student.IdMadeOfStudy = mod.idMadeOfStudy;
                            }
                            catch
                            {
                            }
                        }

                        if (workSheet.Cells[rowIterator, 24].Value != null)
                        {
                            try
                            {
                                student.IdentityCard = workSheet.Cells[rowIterator, 24].Value.ToString();
                            }
                            catch
                            {
                            }
                        }

                        if (workSheet.Cells[rowIterator, 25].Value != null)
                        {
                            try
                            {
                                student.DateOfIssuanceIdentityCard =
                                    DateTime.ParseExact(
                                        workSheet.Cells[rowIterator, 25].Value.ToString(),
                                        "dd/MM/yyyy",
                                        CultureInfo.InvariantCulture);
                            }
                            catch
                            {
                            }
                        }

                        if (workSheet.Cells[rowIterator, 26].Value != null)
                        {
                            try
                            {
                                string PlaceIdentityCard =
                                    workSheet.Cells[rowIterator, 26].Value.ToString().Replace("CA ", string.Empty);
                                Province province =
                                    ct.Provinces.Single(
                                        p => p.ProvinceName.ToUpper().Trim().Equals(PlaceIdentityCard.ToUpper().Trim()));
                                student.IdProvinceIdentityCard = province.IdProvince;
                            }
                            catch
                            {
                            }
                        }

                        if (workSheet.Cells[rowIterator, 32].Value != null)
                        {
                            try
                            {
                                student.AdditionalPermanentResidence = workSheet.Cells[rowIterator, 32].Value.ToString();
                            }
                            catch
                            {
                            }
                        }

                        if (workSheet.Cells[rowIterator, 33].Value != null)
                        {
                            try
                            {
                                student.MobilePhoneNumber = workSheet.Cells[rowIterator, 33].Value.ToString();
                            }
                            catch
                            {
                            }
                        }

                        if (workSheet.Cells[rowIterator, 38].Value != null)
                        {
                            try
                            {
                                student.Email = workSheet.Cells[rowIterator, 38].Value.ToString();
                                Account account = ct.Accounts.Single(a => a.IdAccount == student.IdAccount);
                                account.Email = student.Email;
                            }
                            catch
                            {
                            }
                        }

                        ct.SaveChanges();

                        percentProgress = (int)(((double)rowIterator / maxRow) * 100);
                    }
                }
                catch
                {
                }
            }
        }

        public void ImportStudent3(ExcelPackage package)
        {
            var currentSheet = package.Workbook.Worksheets;
            var workSheet = currentSheet.Last();
            int maxCol = workSheet.Dimension.End.Column;
            int maxRow = workSheet.Dimension.End.Row;
            percentProgress = 0;
            ListErrorImportExcels.Clear();
            for (int rowIterator = 2; rowIterator <= maxRow; rowIterator++)
            {
                string studentNumber = String.Empty;
                try
                {
                    using (DSAContext ct = new DSAContext())
                    {
                        studentNumber = workSheet.Cells[rowIterator, 2].Value.ToString();
                        if (!string.IsNullOrEmpty(studentNumber))
                        {
                            Student student = ct.Students.SingleOrDefault(s => s.StudentNumber.Equals(studentNumber));
                            if (student == null)
                            {
                                student = new Student();
                                student.StudentNumber = studentNumber;
                            }

                            string studentName = workSheet.Cells[rowIterator, 3].Value.ToString();
                            string[] listString = studentName.Split(' ');
                            student.FirstNameStudent = listString[listString.Count() - 1];
                            student.LastNameStudent = studentName.Replace(student.FirstNameStudent, string.Empty);

                            if (workSheet.Cells[rowIterator, 5].Value != null)
                            {
                                student.BirthDay = Convert.ToDateTime(workSheet.Cells[rowIterator, 5].Value.ToString());
                            }

                            string sex = workSheet.Cells[rowIterator, 4].Value.ToString();
                            if (sex.Trim().Equals("1"))
                            {
                                student.Sex = "Nam";
                            }
                            else
                            {
                                student.Sex = "Nữ";
                            }

                            string className = workSheet.Cells[rowIterator, 15].Value.ToString();
                            Class cl = new ClassBO().Get(className.Trim());
                            if (cl == null)
                            {
                                string facultyNumber = workSheet.Cells[rowIterator, 16].Value.ToString();
                                string courseName = className.Substring(0, 2);
                                cl = new ClassBO().Insert(className, courseName, facultyNumber);
                            }

                            student.IdClass = cl.IdClass;

                            if (workSheet.Cells[rowIterator, 6].Value != null)
                            {
                                try
                                {
                                    string ethnic = workSheet.Cells[rowIterator, 6].Value.ToString();
                                    Ethnic et = ct.Ethnics.Single(e => e.EthnicName.Equals(ethnic));
                                    student.IdEthnic = et.IdEthnic;
                                }
                                catch (Exception e)
                                {
                                    ListErrorImportExcels.Add(new ErrorImportExcel
                                    {
                                        Row = rowIterator,
                                        ErrorName = " Column 6  IdClass :" + e.Message,
                                        NumberStudent = studentNumber,
                                    });
                                }
                            }

                            if (workSheet.Cells[rowIterator, 7].Value != null)
                            {
                                try
                                {
                                    string state = workSheet.Cells[rowIterator, 7].Value.ToString();
                                    State st = ct.States.Single(c => c.StateName.Equals(state));
                                    student.idState = st.IdState;
                                }
                                catch (Exception e)
                                {
                                    ListErrorImportExcels.Add(new ErrorImportExcel
                                    {
                                        Row = rowIterator,
                                        ErrorName = " Column 7  state :" + e.Message,
                                        NumberStudent = studentNumber,
                                    });
                                }
                            }

                            if (workSheet.Cells[rowIterator, 20].Value != null)
                            {
                                try
                                {
                                    string madeofStudy = workSheet.Cells[rowIterator, 20].Value.ToString();
                                    MadeOfStudy mod = ct.MadeOfStudies.Single(
                                        m => m.MadeOfStudyName.Equals(madeofStudy));
                                    student.IdMadeOfStudy = mod.idMadeOfStudy;
                                }
                                catch (Exception e)
                                {
                                    ListErrorImportExcels.Add(new ErrorImportExcel
                                    {
                                        Row = rowIterator,
                                        ErrorName = " Column 20  madeofStudy :" + e.Message,
                                        NumberStudent = studentNumber,
                                    });
                                }
                            }

                            if (workSheet.Cells[rowIterator, 24].Value != null)
                            {
                                try
                                {
                                    student.IdentityCard = workSheet.Cells[rowIterator, 24].Value.ToString();
                                }
                                catch (Exception e)
                                {
                                    ListErrorImportExcels.Add(new ErrorImportExcel
                                    {
                                        Row = rowIterator,
                                        ErrorName = " Column 24  IdentityCard :" + e.Message,
                                        NumberStudent = studentNumber,
                                    });
                                }
                            }

                            if (workSheet.Cells[rowIterator, 25].Value != null)
                            {
                                try
                                {
                                    student.DateOfIssuanceIdentityCard =
                                        DateTime.ParseExact(
                                            workSheet.Cells[rowIterator, 25].Value.ToString(),
                                            "dd/MM/yyyy",
                                            CultureInfo.InvariantCulture);
                                }
                                catch (Exception e)
                                {
                                    ListErrorImportExcels.Add(new ErrorImportExcel
                                    {
                                        Row = rowIterator,
                                        ErrorName = " Column 25  DateOfIssuanceIdentityCard :" + e.Message,
                                        NumberStudent = studentNumber,
                                    });
                                }
                            }

                            if (workSheet.Cells[rowIterator, 26].Value != null)
                            {
                                try
                                {
                                    string PlaceIdentityCard =
                                        workSheet.Cells[rowIterator, 26].Value.ToString().Replace("CA ", string.Empty);
                                    Province province =
                                        ct.Provinces.SingleOrDefault(
                                            p => p.ProvinceName.ToUpper().Trim().Contains(PlaceIdentityCard.ToString()));
                                    student.IdProvinceIdentityCard = province.IdProvince;
                                }
                                catch (Exception e)
                                {
                                    ListErrorImportExcels.Add(new ErrorImportExcel
                                    {
                                        Row = rowIterator,
                                        ErrorName = " Column 26  PlaceIdentityCard :" + e.Message,
                                        NumberStudent = studentNumber,
                                    });
                                }
                            }

                            if (workSheet.Cells[rowIterator, 32].Value != null)
                            {
                                try
                                {
                                    student.AdditionalPermanentResidence =
                                        workSheet.Cells[rowIterator, 32].Value.ToString();
                                }
                                catch (Exception e)
                                {
                                    ListErrorImportExcels.Add(new ErrorImportExcel
                                    {
                                        Row = rowIterator,
                                        ErrorName = " Column 32  AdditionalPermanentResidence :" + e.Message,
                                        NumberStudent = studentNumber,
                                    });
                                }
                            }

                            if (workSheet.Cells[rowIterator, 33].Value != null)
                            {
                                try
                                {
                                    student.MobilePhoneNumber = workSheet.Cells[rowIterator, 33].Value.ToString();
                                }
                                catch (Exception e)
                                {
                                    ListErrorImportExcels.Add(new ErrorImportExcel
                                    {
                                        Row = rowIterator,
                                        ErrorName = " Column 33  MobilePhoneNumber :"+e.Message,
                                        NumberStudent = studentNumber,
                                    });
                                }
                            }

                            if (workSheet.Cells[rowIterator, 38].Value != null)
                            {
                                try
                                {
                                    student.Email = workSheet.Cells[rowIterator, 38].Value.ToString();
                                }
                                catch (Exception e)
                                {
                                    ListErrorImportExcels.Add(new ErrorImportExcel
                                    {
                                        Row = rowIterator,
                                        ErrorName = " Column 38  Email :" + e.Message,
                                        NumberStudent = studentNumber,
                                    });
                                }
                            }

                            if (student.IdStudent == 0)
                            {
                                int idAccount = new AccountBO().AddAccountSV(student);
                                student.IdAccount = idAccount;
                                ct.Students.Add(student);
                            }
                            else
                            {
                                Account account = ct.Accounts.Single(a => a.IdAccount == student.IdAccount);
                                account.Email = student.Email;
                            }

                            ct.SaveChanges();

                            percentProgress = (int)(((double)rowIterator / maxRow) * 100);
                        }
                    }
                }
                catch(Exception e)
                {
                    ListErrorImportExcels.Add(new ErrorImportExcel
                    {
                        Row = rowIterator,
                        ErrorName = e.ToString(),
                        NumberStudent = studentNumber,
                    });
                }
            }
        }
        public List<ErrorImportExcel> GetListErrorImportExcel()
        {
            return ListErrorImportExcels;
        }
        public int GetCountListErrorImportExcel()
        {
            return ListErrorImportExcels.Count;
        }

        public void ProcessEditStudentRelative(int idStudent, FormCollection form)
        {
            Student student = this.context.Students.Single(st => st.IdStudent.Equals(idStudent));
            if (form["idFamilyComposition"] != string.Empty) student.IdFamilyComposition = Convert.ToInt32(form["idFamilyComposition"]);

            this.context.SaveChanges();
        }

        public void SetImageStudent(string link, int idStudent)
        {
            Student student = this.context.Students.SingleOrDefault(s => s.IdStudent == idStudent);
            student.LinkAvatar = link;
            this.context.SaveChanges();
        }

        public int TotalPage(string search)
        {
            List<Student> listStudent = this.GetListSearchStudent(search);
            int toltalStudent = listStudent.Count;
            return toltalStudent / this.rowInPage + 1;
        }

        public void UpdatePoliticalParty(int idStudent)
        {
            Student student = this.context.Students.Single(st => st.IdStudent.Equals(idStudent));
            if (student.IsPoliticalParty != null)
            {
                student.IsPoliticalParty = !student.IsPoliticalParty;
            }
            else
            {
                student.IsPoliticalParty = true;
            }

            this.context.SaveChanges();
        }

        public void UpdateYouthUnion(int idStudent)
        {
            Student student = this.context.Students.Single(st => st.IdStudent.Equals(idStudent));
            if (student.IsYouthUnion != null)
            {
                student.IsYouthUnion = !student.IsYouthUnion;
            }
            else
            {
                student.IsYouthUnion = true;
            }

            this.context.SaveChanges();
        }
    }
}