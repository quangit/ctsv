﻿namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Helpers.Common;
    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.Support;

    public class PaperBo
    {
        readonly DSAContext context = new DSAContext();

        public void AddPaper(string paperName, string content, string note)
        {
            Paper paper = new Paper();
            paper.PaperName = paperName;
            paper.ContentPaper = content;
            paper.Note = note;
            this.context.Papers.Add(paper);
            this.context.SaveChanges();
        }

        public void AddPaper(FormCollection form)
        {
            Paper paper = new Paper();
            paper.PaperName = form["namePaper"];
            
            paper.ContentPaper = form["editor1"];
            paper.Note = form["notePaper"];
            paper.PriceNormal = form["priceNormal"];
            paper.PriceUrgency = form["priceUrgency"];
            paper.WaittingPeriodNormal = Convert.ToInt32(form["waittingPeriodNormal"]);
            paper.WaittingPeriodUrgency = Convert.ToInt32(form["waittingPeriodUrgency"]);
            this.context.Papers.Add(paper);
            this.context.SaveChanges();
        }

        public bool DeletePaper(int idPaper)
        {
            try
            {
                Paper paper = this.context.Papers.SingleOrDefault(p => p.IdPaper == idPaper);
                this.context.Papers.Remove(paper);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public string DetailPrintPaper(int idPaper, int idAccountRequest, int numberPaper)
        {
            Paper paper = this.ReplacePaper(idPaper, idAccountRequest);

            string printPaper = string.Empty;
            for (int i = 0; i < numberPaper; i++)
            {
                printPaper += paper.ContentPaper;
            }

            return printPaper;
        }

        public string GetContentListPaper(List<int> listIdRequest)
        {
            string content = string.Empty;
            for (int i = 0; i < listIdRequest.Count; i++)
            {
                int idRequest = listIdRequest[i];
                RequestPaper request = this.context.RequestPapers.Single(r => r.IdRequestPaper == idRequest);
                string tam = this.ReplacePaper2(request.Reasonrequest.IdPaper, request.IdAccountRequest);
                for (int j = 0; j < request.NumberPaper; j++)
                {
                    content = content + tam;
                }
            }

            return content;
        }

        public string GetContentListPaperByClass(int idReason, int idClass)
        {
            string content = string.Empty;
            List<RequestPaper> listRequest = new RequestPaperBo().GetRequestByClass(idReason, idClass);
            foreach (RequestPaper request in listRequest)
            {
                string tam = this.ReplacePaper2(request.Reasonrequest.IdPaper, request.IdAccountRequest);
                for (int j = 0; j < request.NumberPaper; j++)
                {
                    content = content + tam;
                }
            }

            return content;
        }

        public List<Paper> GetListPaper()
        {
            List<Paper> listPaper = this.context.Papers.ToList();
            return listPaper;
        }

        public Paper GetPaper(int id)
        {
            Paper paper = this.context.Papers.Single(pp => pp.IdPaper == id);
            return paper;
        }

        public double GetPointAverageTrainning(List<ConductResultSemester> listResult)
        {
            double pointAverage = 0;
            int count = 0;
            foreach (ConductResultSemester cd in listResult)
            {
                pointAverage += (double)cd.Point;
                count++;
            }

            pointAverage = Math.Round(pointAverage / ((double)count), 2);
            return pointAverage;
        }

        public List<ConductResultSemester> GetPointTranning(Student student)
        {
            List<Semester> listSemester = new SemesterBO().GetSemesterByYear(student.Class.Course.AdmissionYear);
            List<ConductResultSemester> listResult = new List<ConductResultSemester>();
            List<ConductResult> listConductResult =
                student.ConductResults.OrderBy(r => r.ConductEvent.Semester.BeginTime).ToList();
            foreach (Semester semester in listSemester)
            {
                ConductResult conductResult = null;
                try
                {
                    conductResult =
                        student.ConductResults.Where(
                            c => c.ConductEvent.IdSemester == semester.IdSemester && c.IsApproved == true)
                            .OrderByDescending(c => c.IdConductSchedule)
                            .FirstOrDefault();
                }
                catch
                {
                    conductResult = null;
                }

                int total = 0;
                if (conductResult != null)
                {
                    string[] arrPairValue = conductResult.PartialPoint.Split(';');
                    foreach (string pair in arrPairValue)
                    {
                        try
                        {
                            total += int.Parse(pair.Split(':')[1]);
                        }
                        catch
                        {
                        }
                    }

                    ConductResultSemester conductResultSemester =
                        new ConductResultSemester(
                            conductResult.ConductEvent.IdSemester,
                            conductResult.ConductEvent.Semester.SemesterYear.SemesterYearName,
                            conductResult.ConductEvent.Semester.Year.YearName,
                            total);
                    listResult.Add(conductResultSemester);
                }
            }

            return listResult;
        }

        public string GetTableENPointTranning(List<ConductResultSemester> listResult)
        {
            string table =
                "<table border='1' style='width:100%'> <thead><tr><td align='center'><b> No. </b> </td><td align='center'><b> Semester </b></td><td align='center'><b> Year </b></td><td align='center'><b> Point Training </b></td><td align='center'><b> Rank </b></td><td align='center'><b> Note </b></td></tr> </thead><tbody>";
            int i = 1;
            foreach (ConductResultSemester cd in listResult)
            {
                table += "<tr>";
                table += "<td align='center'>" + (i++) + "</td>";
                table += "<td align='center'>" + cd.SemesterName + "</td>";
                table += "<td align='center'>" + cd.SemesterYear + "</td>";
                table += "<td align='center'>" + cd.Point + "</td>";
                table += "<td align='center'>" + DataExtension.GetGradeEvaluatedEN(cd.Point) + "</td>";
                table += "<td align='center'></td></tr>";
            }

            table += "</tbody></table>";
            return table;
        }

        public string GetTablePointTranning(List<ConductResultSemester> listResult)
        {
            string table =
                "<table border='1' style='width:100%'> <thead><tr><td align='center'><b> STT </b> </td><td align='center'><b> Học kỳ </b></td><td align='center'><b> Năm học </b></td><td align='center'><b> Điểm rèn luyện </b></td><td align='center'><b> Xếp loại </b></td><td align='center'><b> Ghi chú </b></td></tr> </thead><tbody>";
            int i = 1;
            foreach (ConductResultSemester cd in listResult)
            {
                table += "<tr>";
                table += "<td align='center'>" + (i++) + "</td>";
                table += "<td align='center'>" + cd.SemesterName + "</td>";
                table += "<td align='center'>" + cd.SemesterYear + "</td>";
                table += "<td align='center'>" + cd.Point + "</td>";
                table += "<td align='center'>" + DataExtension.GetGradeEvaluated(cd.Point) + "</td>";
                table += "<td align='center'></td></tr>";
            }

            table += "</tbody></table>";
            return table;
        }

        public string Replace(string content, Student student)
        {
            content = content.Replace("#hovaten", student.LastNameStudent + " " + student.FirstNameStudent);
            content = content.Replace(
                "#fullname",
                ConvertObject.ToEnglish(student.LastNameStudent + " " + student.FirstNameStudent));

            content = content.Replace("#lop", student.Class.ClassName);

            content = content.Replace(
                "#ngaysinh",
                student.BirthDay != null ? student.BirthDay.Value.ToString("dd/MM/yyyy") : string.Empty);
            content = content.Replace(
                "#birthday",
                student.BirthDay != null ? student.BirthDay.Value.ToLongDateString() : string.Empty);
            if (student.Sex != null)
            {
                string sex = student.Sex;
                content = content.Replace("#gioitinh", student.Sex);
                if (sex.Equals("Nam")) content = content.Replace("#sex", "male");
                else content = content.Replace("#sex", "female");
            }

            content = content.Replace("#matruong", "DUT");

            content = content.Replace("#tentruong", "Đại học Bách Khoa - Đại Học Đà Nẵng");
            content = content.Replace("#school", " UNIVERSITY OF SCIENCE AND TECHNOLOGY - THE UNIVERSITY OF DANANG");

            if (student.MadeOfStudy != null)
            {
                content = content.Replace("#loaihinhdaotao", student.MadeOfStudy.MadeOfStudyName);
                content = content.Replace("#madeofstudy", student.MadeOfStudy.MadeOfStudyNameEN);
            }
            else
            {
                content = content.Replace("#loaihinhdaotao", string.Empty);
                content = content.Replace("#madeofstudy", string.Empty);
            }

            content = content.Replace("#trinhdodaotao", "Đại học");
            content = content.Replace("#leveloftraining", "Undergraduate");

            if (student.Class != null)
            {
                content = content.Replace("#nganh", student.Class.Faculty.FacultyName);
                content = content.Replace("#major", student.Class.Faculty.FacultyNameEN);

                content = content.Replace("#khoa", student.Class.Faculty.FacultyName);
                content = content.Replace("#faculty", student.Class.Faculty.FacultyNameEN);

                int beginCourse = Convert.ToInt32(student.Class.Course.AdmissionYear);
                int endCourse = beginCourse + 5;
                content = content.Replace("#khoahoc", beginCourse + "-" + endCourse);
                content = content.Replace("#khtuyensinh", student.Class.Course.AdmissionYear.ToString());
            }

            content = content.Replace("#sothesinhvien", student.StudentNumber);

            if (student.DateAdmission != null)
            {
                content = content.Replace("#ngaynhaphoc", student.DateAdmission.Value.ToShortDateString());
                content = content.Replace(
                    "#ngayratruong",
                    student.DateAdmission.Value.AddMonths(student.Class.NumberMonthSchool).ToShortDateString());
            }
            else
            {
                content = content.Replace("#ngaynhaphoc", string.Empty);
                content = content.Replace("#ngayratruong", string.Empty);
            }

            content = content.Replace("#thoigianhoctaitruong", student.Class.NumberMonthSchool + string.Empty);

            content = content.Replace("#tienhocphithang", ConvertObject.ConvertCurrency(student.Class.MoneyOfMonth));

            if (student.IdentityCard != null)
            {
                content = content.Replace("#socmnd", student.IdentityCard);
            }

            if (student.DateOfIssuanceIdentityCard != null)
            {
                content = content.Replace("#ngaycapcmnd", student.DateOfIssuanceIdentityCard.Value.ToShortDateString());
            }

            if (student.IdProvinceIdentityCard != null) content = content.Replace("#noicapcmnd", student.ProvinceIdentityCard.ProvinceName);

            content = content.Replace("#socmnd", string.Empty);
            content = content.Replace("#ngaycapcmnd", string.Empty);
            content = content.Replace("#noicapcmnd", string.Empty);

            if (student.IdWardBirthPlace.Equals("00000"))
            {
                if (student.AdditionalBirthplace != null)
                {
                    List<string> list = student.AdditionalBirthplace.Split(',').ToList();
                    List<string> birthplace = list.LastOrDefault().Split(' ').ToList();
                    int length = birthplace.Count;
                    if (length > 2)
                    {
                        if (birthplace.LastOrDefault().Contains("Hu"))
                        {
                            content = content.Replace(
                                "#noisinh(tinh)",
                                birthplace[length - 3] + " " + birthplace[length - 2] + " " + birthplace[length - 1]);
                            content = content.Replace(
                                "#birthPlace",
                                ConvertObject.ToEnglish(
                                    birthplace[length - 3] + " " + birthplace[length - 2] + " " + birthplace[length - 1])
                                + "," + ConvertObject.ToEnglish(student.State.StateName));
                        }
                        else
                        {
                            content = content.Replace(
                                "#noisinh(tinh)",
                                birthplace[length - 2] + " " + birthplace[length - 1]);
                            content = content.Replace(
                                "#birthPlace",
                                ConvertObject.ToEnglish(birthplace[length - 2] + " " + birthplace[length - 1]) + ","
                                + ConvertObject.ToEnglish(student.State.StateName));
                        }
                    }
                    else
                    {
                        content = content.Replace("#noisinh(tinh)", list.LastOrDefault());
                        content = content.Replace(
                            "#birthPlace",
                            ConvertObject.ToEnglish(list.LastOrDefault()) + ","
                            + ConvertObject.ToEnglish(student.State.StateName));
                    }
                }
                else
                {
                    content = content.Replace("#noisinh(tinh)", string.Empty);
                    content = content.Replace("#birthPlace", string.Empty);
                }
            }
            else
            {
                content = content.Replace("#noisinh(tinh)", student.WardBirthPlace.District.Province.ProvinceName);
                content = content.Replace(
                    "#birthPlace",
                    ConvertObject.ToEnglish(student.WardBirthPlace.District.Province.ProvinceName) + ","
                    + ConvertObject.ToEnglish(student.State.StateName));
            }

            content = content.Replace(
                "#khongmiengiam",
                "<input type='checkbox' " + (student.ObjectTuitionFee == 1 ? "checked/>" : "/>"));
            content = content.Replace(
                "#giamhocphi",
                "<input type='checkbox'" + (student.ObjectTuitionFee == 2 ? "checked/>" : "/>"));
            content = content.Replace(
                "#mienhocphi",
                "<input type='checkbox' " + (student.ObjectTuitionFee == 3 ? "checked/>" : "/>"));
            content = content.Replace(
                "#mocoi",
                "<input type='checkbox' " + (student.IsOrphan.Value ? "checked/>" : "/>"));
            content = content.Replace(
                "#khongmocoi",
                "<input type='checkbox' " + (!student.IsOrphan.Value ? "checked/>" : "/>"));

            if (student.AdditionalPermanentResidence != null)
            {
                content = content.Replace("#hokhauthuongtru", student.AdditionalPermanentResidence);
            }
            else
            {
                if (student.IdWardPermanentResidence != null)
                {
                    content = content.Replace(
                        "#hokhauthuongtru",
                        student.WardPermanentResidence.WardName + ","
                        + student.WardPermanentResidence.District.DistrictName + ","
                        + student.WardPermanentResidence.District.Province.ProvinceName);
                }
                else
                {
                    content = content.Replace("#hokhauthuongtru", string.Empty);
                }
            }

            if (content.Contains("#bangkequarenluyen"))
            {
                content = this.ReplacePointTranning(student, content);
            }

            if (content.Contains("#tablepointraining"))
            {
                content = this.ReplacePointTranningEN(student, content);
            }

            content = content.Replace("#ngay", DateTime.Now.Day.ToString());
            content = content.Replace("#thang", DateTime.Now.Month.ToString());
            content = content.Replace("#nam", DateTime.Now.Year.ToString());

            return content;
        }

        public Paper ReplacePaper(int idPaper, int idAccount)
        {
            Paper paper = this.GetPaper(idPaper);
            try
            {
                Account account = new AccountBO().GetAccount(idAccount);
                string content = paper.ContentPaper;
                if (account.TypeAccount.Trim().Equals("SV"))
                {
                    Student student = new StudentBO().GetStudent(account.Students.First().IdStudent);
                    content = this.Replace(content, student);
                }

                paper.ContentPaper = content;
            }
            catch
            {
            }

            return paper;
        }

        public string ReplacePaper2(int idPaper, int idAccount)
        {
            Paper paper = this.GetPaper(idPaper);
            string content = paper.ContentPaper;
            try
            {
                Account account = new AccountBO().GetAccount(idAccount);
                if (account.TypeAccount.Trim().Equals("SV"))
                {
                    Student student = new StudentBO().GetStudent(account.Students.First().IdStudent);
                    content = this.Replace(content, student);
                }
            }
            catch
            {
            }

            return content;
        }

        public string ReplacePointTranning(Student student, string content)
        {
            List<ConductResultSemester> listResult = this.GetPointTranning(student);
            string table = this.GetTablePointTranning(listResult);
            content = content.Replace("#bangkequarenluyen", table);
            double pointAverage = this.GetPointAverageTrainning(listResult);
            content = content.Replace("#diemtrungbinhrenluyen", pointAverage.ToString());
            string rank = DataExtension.GetGradeEvaluated(Convert.ToInt32(pointAverage));
            content = content.Replace("#xeploairenluyen", rank);
            return content;
        }

        public string ReplacePointTranningEN(Student student, string content)
        {
            List<ConductResultSemester> listResult = this.GetPointTranning(student);
            string table = this.GetTableENPointTranning(listResult);
            content = content.Replace("#tablepointraining", table);
            double pointAverage = this.GetPointAverageTrainning(listResult);
            content = content.Replace("#diemtrungbinhrenluyen", pointAverage.ToString());
            string rank = DataExtension.GetGradeEvaluatedEN(Convert.ToInt32(pointAverage));
            content = content.Replace("#ranktraining", rank);
            return content;
        }

        public bool UpdatePaper(string content, int idPaper)
        {
            try
            {
                Paper paper = this.context.Papers.Single(pp => pp.IdPaper == idPaper);
                paper.ContentPaper = content;
                this.context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePaper(int idPaper, FormCollection form)
        {
            try
            {
                Paper paper = this.context.Papers.Single(pp => pp.IdPaper == idPaper);
                paper.PriceNormal = form["priceNormal"];
                paper.PriceUrgency = form["priceUrgency"];
                paper.WaittingPeriodNormal = Convert.ToInt32(form["waittingPeriodNormal"]);
                paper.WaittingPeriodUrgency = Convert.ToInt32(form["waittingPeriodUrgency"]);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}