using OfficeOpenXml;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace WEBPCTSV.Models.bo
{
    public class StudentBO
    {
        DSAContext context = new DSAContext();
        int rowInPage = new Configuration().rowInPage;
        public static int percentProgress;

        private DSAContext dsaContext;
        public StudentBO()
        {}
        public StudentBO(DSAContext dsaContext)
        {
            this.dsaContext = dsaContext;
        }

        public List<Student> GetListStudentByClass(int idClass)
        {
            return dsaContext.Students.Where(s => s.IdClass == idClass).ToList();
        }

        public void AddStudent(FormCollection form)
        {
            Student student = new Student();
            student.LastNameStudent = form["lastName"];
            student.FirstNameStudent = form["firstName"];
            student.StudentNumber = form["studentNumber"];
            student.Sex = form["sexRadio"];
            if (student.Sex == null) student.Sex = "";
            student.IdClass = Convert.ToInt32(form["selectClass"]);
            if (student.IdClass == 0) student.IdClass = null;
            student.MobilePhoneNumber = form["mobilePhoneNumber"];
            student.LandlineNumber = form["landlinePhoneNumber"];
            student.Email = form["email"];
            student.IdentityCard = form["identityCardNumber"];
            string identityCardDate = form["identityCardDate"];
            if (!identityCardDate.Equals("")) student.DateOfIssuanceIdentityCard = Convert.ToDateTime(identityCardDate);
            student.IdProvinceIdentityCard = form["identityCardPlace"];
            if (student.IdProvinceIdentityCard.Equals("")) student.IdProvinceIdentityCard = null;
            student.idState = form["selectState"];
            AccountBO accountBo = new AccountBO();
            student.IdAccount = accountBo.AddAccount(student.StudentNumber);
            context.Students.Add(student);
            context.SaveChanges();

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
                    string birthDay = "";
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
                        AddStudent(studentNumber, studentName, birthDay, sex, c.IdClass);
                        percentProgress = (int)(((double)rowIterator / maxRow) * 100);
                    }

                }
                catch { }
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

                        //string studentName = workSheet.Cells[rowIterator, 3].Value.ToString();
                        //string[] listString = studentName.Split(' ');
                        //student.FirstNameStudent = listString[listString.Count() - 1];
                        //student.LastNameStudent = studentName.Replace(student.FirstNameStudent, "");

                        //if (workSheet.Cells[rowIterator, 5].Value != null)
                        //{
                        //    student.BirthDay = Convert.ToDateTime(workSheet.Cells[rowIterator, 5].Value.ToString());
                        //}

                        //string sex = workSheet.Cells[rowIterator, 4].Value.ToString();
                        //if (sex.Trim().Equals("1"))
                        //{
                        //    student.Sex = "Nam";
                        //}
                        //else
                        //{
                        //    student.Sex = "Nữ";
                        //}

                        if (workSheet.Cells[rowIterator, 6].Value != null)
                        {
                            try {
                                string ethnic = workSheet.Cells[rowIterator, 6].Value.ToString();
                                Ethnic et = ct.Ethnics.Single(e => e.EthnicName.Equals(ethnic));
                                student.IdEthnic = et.IdEthnic;
                            }
                            catch { }
                            
                        }



                        if (workSheet.Cells[rowIterator, 7].Value != null)
                        {
                            try {
                                string state = workSheet.Cells[rowIterator, 7].Value.ToString();
                                State st = ct.States.Single(c => c.StateName.Equals(state));
                                student.idState = st.IdState;
                            }
                            catch { }
                            
                        }

                        if (workSheet.Cells[rowIterator, 20].Value != null)
                        {
                            try {
                                string madeofStudy = workSheet.Cells[rowIterator, 20].Value.ToString();
                                MadeOfStudy mod = ct.MadeOfStudies.Single(m => m.MadeOfStudyName.Equals(madeofStudy));
                                student.IdMadeOfStudy = mod.idMadeOfStudy;
                            }
                            catch { }
                            
                        }


                        if (workSheet.Cells[rowIterator, 24].Value != null)
                        {
                            try {
                                student.IdentityCard = workSheet.Cells[rowIterator, 24].Value.ToString();
                            }
                            catch { }
                            
                        }

                        if (workSheet.Cells[rowIterator, 25].Value != null)
                        {
                            try {
                                student.DateOfIssuanceIdentityCard = DateTime.ParseExact(workSheet.Cells[rowIterator, 25].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                            catch { }
                            
                        }

                        if (workSheet.Cells[rowIterator, 26].Value != null)
                        {
                            try {
                                string PlaceIdentityCard = workSheet.Cells[rowIterator, 26].Value.ToString().Replace("CA ", "");
                                Province province = ct.Provinces.Single(p => p.ProvinceName.ToUpper().Trim().Equals(PlaceIdentityCard.ToUpper().Trim()));
                                student.IdProvinceIdentityCard = province.IdProvince;
                            }
                            catch { }
                            
                        }

                        if (workSheet.Cells[rowIterator, 32].Value != null)
                        {
                            try {
                                student.AdditionalPermanentResidence = workSheet.Cells[rowIterator, 32].Value.ToString();
                            }
                            catch { }
                            
                        }

                        if (workSheet.Cells[rowIterator, 33].Value != null)
                        {
                            try {
                                student.MobilePhoneNumber = workSheet.Cells[rowIterator, 33].Value.ToString();
                            }
                            catch { }
                            
                        }

                        if (workSheet.Cells[rowIterator, 38].Value != null)
                        {
                            try {
                                student.Email = workSheet.Cells[rowIterator, 38].Value.ToString();
                                Account account = ct.Accounts.Single(a => a.IdAccount == student.IdAccount);
                                account.Email = student.Email;
                            }
                            catch { }
                            
                        }

                        ct.SaveChanges();

                        percentProgress = (int)(((double)rowIterator / maxRow) * 100);
                    }

                }
                catch { }
            }
        }

        public void AddStudent(string studentNumber, string studentName, string birthDay, string sex, int idClass)
        {
            using (DSAContext ct = new DSAContext())
            {
                try
                {
                    Student student = null;
                    string[] listString = studentName.Split(' ');
                    try {
                        student = ct.Students.Single(s => s.StudentNumber.Equals(studentNumber));
                    }
                    catch { }
                    
                    
                    if (student != null)
                    {
                        student.FirstNameStudent = listString[listString.Count() - 1];
                        student.LastNameStudent = studentName.Replace(student.FirstNameStudent, "");
                        student.BirthDay = Convert.ToDateTime(birthDay);
                        student.Sex = sex;
                        student.IdClass = idClass;
                        ct.SaveChanges();
                    }
                    else
                    {
                        student = new Student();
                        student.StudentNumber = studentNumber;
                        int idAccount = new AccountBO().AddAccount(studentNumber);
                        student.IdAccount = idAccount;
                        student.FirstNameStudent = listString[listString.Count() - 1];
                        student.LastNameStudent = studentName.Replace(student.FirstNameStudent, "");
                        if (birthDay != "") student.BirthDay = Convert.ToDateTime(birthDay);
                        student.Sex = sex;
                        student.IdClass = idClass;
                        ct.Students.Add(student);
                        ct.SaveChanges();
                    }
                }
                catch { }
            }
            


        }

        public void EditStudent(int idStudent, FormCollection form)
        {
            Student student = context.Students.Single(st => st.IdStudent.Equals(idStudent));
            if (!String.IsNullOrEmpty(form["lastName"])) student.LastNameStudent = form["lastName"];
            if (!String.IsNullOrEmpty(form["fristName"])) student.FirstNameStudent = form["firstName"];
            if (!String.IsNullOrEmpty(form["studentNumber"])) student.StudentNumber = form["studentNumber"];
            student.Sex = form["sexRadio"];
            string birthDay = form["birthday"];
            if (birthDay != "") student.BirthDay = Convert.ToDateTime(birthDay);
            if (!String.IsNullOrEmpty(form["selectClass"])) student.IdClass = Convert.ToInt32(form["selectClass"]);
            if (student.IdClass == 0) student.IdClass = null;
            student.MobilePhoneNumber = form["mobilePhoneNumber"];
            student.LandlineNumber = form["landlinePhoneNumber"];
            student.Email = form["email"];
            context.SaveChanges();
        }

        public void EditStudent2(int idStudent, FormCollection form)
        {
            Student student = context.Students.Single(st => st.IdStudent.Equals(idStudent));
            student.idState = form["selectState"];
            student.IdentityCard = form["identityCardNumber"];
            string dateOfIssuanceIdentityCard = form["identityCardDate"];
            if (dateOfIssuanceIdentityCard != "") student.DateOfIssuanceIdentityCard = Convert.ToDateTime(dateOfIssuanceIdentityCard);
            if (form["identityCardPlace"] != "") student.IdProvinceIdentityCard = form["identityCardPlace"];
            if (form["selectWard_Permanent"] != "") student.IdWardPermanentResidence = form["selectWard_Permanent"];
            student.AdditionalPermanentResidence = form["additionalPermanentplace"];
            if (form["boardingRadio"] == "true") student.BoardingAddress = true;
            if (form["boardingRadio"] == "false") student.BoardingAddress = false;
            if (form["selectWard_ExternAddress"] != "") student.IdWardExternAddress = form["selectWard_ExternAddress"];
            student.AdditionalExternAddress = form["additionalExternAddress"];
            if (form["selectWard_NativeLand"] != "") student.IdWardNativeLand = form["selectWard_NativeLand"];
            student.AdditionalNativeLand = form["additionalNativeLand"];
            if (form["selectWard_BirthPlace"] != "") student.IdWardBirthPlace = form["selectWard_BirthPlace"];
            student.AdditionalBirthplace = form["additionalBirthPlace"];
            if (form["Religion"] != "") student.IdReligion = Convert.ToInt32(form["Religion"]);
            if (form["Ethnic"] != "") student.IdEthnic = Convert.ToInt32(form["Ethnic"]);
            if (form["Area"] != "") student.IdArea = form["Area"];
            student.HightSchoolName = form["hightSchoolName"];
            if (form["selectPlaceSchoolDistrict"] != "")
                student.IdDistrictHightSchoolName = form["selectPlaceSchoolDistrict"];

            if (form["RegistrationTemporaryResidenceTime"] != "") student.RegistrationTemporaryResidenceTime = Convert.ToDateTime(form["RegistrationTemporaryResidenceTime"]);
            student.TemporaryResidenceNotebookNumber = form["TemporaryResidenceNotebookNumber"];
            student.PhoneNumberHouseholder = form["PhoneNumberHouseholder"];
            context.SaveChanges();

        }

        public void EditStudent3(int idStudent, FormCollection form)
        {
            Student student = context.Students.Single(st => st.IdStudent.Equals(idStudent));
            student.HealthInsuranceNumber = form["HealthInsuranceNumber"];
            student.RegisteredMedicalArea = form["registeredMedicalArea"];
            if (form["selectBloodGroup"] != "") student.IdBloodGroup = Convert.ToInt32(form["selectBloodGroup"]);
            student.HealthStatus = form["healthStatus"];
            if (form["numberbloodDonors"] != "") student.NumberBloodDonors = Convert.ToInt32(form["numberbloodDonors"]);
            if (form["weight"] != "") student.Weight = Convert.ToInt32(form["weight"]);
            if (form["height"] != "") student.Height = Convert.ToInt32(form["height"]);
            context.SaveChanges();
        }

        public void EditStudent4(int idStudent, FormCollection form)
        {
            Student student = context.Students.Single(st => st.IdStudent.Equals(idStudent));
            if (form["dateAdmission"] != "") student.DateAdmission = Convert.ToDateTime(form["dateAdmission"]);
            student.EducationalBackground = form["academicLevel"];
            if (!String.IsNullOrEmpty(form["madeOfStudy"])) student.IdMadeOfStudy = Convert.ToInt32(form["madeOfStudy"]);
            if (!String.IsNullOrEmpty(form["typeInput"])) student.IdTypeInput = Convert.ToInt32(form["typeInput"]);
            if (!String.IsNullOrEmpty(form["IdPreferentialPolicyBeneficiary"])) student.IdPreferentialPolicyBeneficiaries = Convert.ToInt32(form["IdPreferentialPolicyBeneficiary"]);
            student.IsOrphan = Convert.ToBoolean(form["OrPhan"]);
            student.ObjectTuitionFee = Convert.ToInt32(form["ObjectTuitionFee"]);
            context.SaveChanges();
        }

        public void ProcessEditStudentRelative(int idStudent, FormCollection form)
        {
            Student student = context.Students.Single(st => st.IdStudent.Equals(idStudent));
            if (form["idFamilyComposition"] != "") student.IdFamilyComposition = Convert.ToInt32(form["idFamilyComposition"]);

            context.SaveChanges();
        }

        public List<Student> GetListStudent()
        {
            return context.Students.ToList();
        }

        public List<Student> GetListStudent(int page, string search)
        {
            List<Student> listStudent = GetListSearchStudent(search);
            return listStudent.OrderByDescending(st => st.IdStudent).Skip((page - 1) * rowInPage).Take(rowInPage).ToList();
        }

        public List<Student> GetListSearchStudent(string search)
        {
            
            List<Student> listStudent = null;
            if (!string.IsNullOrEmpty(search))
            {
                listStudent = context.Students.Where(st => st.StudentNumber.Contains(search)
                || (st.LastNameStudent + "" + st.FirstNameStudent).ToUpper().Trim().Contains(search.ToUpper().Trim())
                || (st.Class.ClassName.ToUpper().Trim().Contains(search.ToUpper().Trim()))
                || (st.Class.Faculty.FacultyName.ToUpper().Trim().Contains(search.ToUpper().Trim()))
                ).ToList();
            }
            else
            {
                listStudent = context.Students.ToList();
            }
            return listStudent;
        }


        public int TotalPage(string search)
        {
            List<Student> listStudent = GetListSearchStudent(search);
            int toltalStudent = listStudent.Count;
            return toltalStudent / rowInPage + 1;
        }


        public Student GetStudent(int idStudent)
        {
            return context.Students.Single(st => st.IdStudent == idStudent);
        }

        public Student GetStudentByStudentNumber(string StudentNumber)
        {
            try
            {
                return context.Students.Single(s => s.StudentNumber.Equals(StudentNumber));
            }
            catch { }
            return null;
        }
        public Student GetStudentByIdAccount(int idAccount)
        {
            Account account = new AccountBO().GetAccount(idAccount);
            return account.Students.First();
        }

        public void UpdateYouthUnion(int idStudent)
        {
            Student student = context.Students.Single(st => st.IdStudent.Equals(idStudent));
            if (student.IsYouthUnion != null)
            {
                student.IsYouthUnion = !student.IsYouthUnion;
            }
            else
            {
                student.IsYouthUnion = true;
            }

            context.SaveChanges();
        }
        public void UpdatePoliticalParty(int idStudent)
        {
            Student student = context.Students.Single(st => st.IdStudent.Equals(idStudent));
            if (student.IsPoliticalParty != null)
            {
                student.IsPoliticalParty = !student.IsPoliticalParty;
            }
            else
            {
                student.IsPoliticalParty = true;
            }

            context.SaveChanges();
        }

        public void SetImageStudent(string link, int idStudent)
        {
            Student student = context.Students.SingleOrDefault(s => s.IdStudent == idStudent);
            student.LinkAvatar = link;
            context.SaveChanges();
        }

        public string GetImageStudent(int idStudent)
        {
            return context.Students.SingleOrDefault(s => s.IdStudent == idStudent).LinkAvatar;
        }

        public List<Student> GetListStudentByFaculty(int idFaculty)
        {
            try
            {
                return dsaContext.Students.Where(s => s.Class.Faculty.IdFaculty == idFaculty && s.IsGraduated == false && s.IsExpelled == false && s.IsReserved == false).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}