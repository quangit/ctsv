using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bo;
using WEBPCTSV.Models.Support;
using WEBPCTSV.Helpers.Common;

namespace WEBPCTSV.Models.bo
{
    public class PaperBo
    {
        DSAContext context = new DSAContext();
        public void AddPaper(string paperName, string content, string note)
        {
            Paper paper = new Paper();
            paper.PaperName = paperName;
            paper.ContentPaper = content;
            paper.Note = note;
            context.Papers.Add(paper);
            context.SaveChanges();
        }
        public void AddPaper(FormCollection form)
        {
            Paper paper = new Paper();
            paper.PaperName = form["namePaper"]; ;
            paper.ContentPaper = form["editor1"];
            paper.Note = form["notePaper"];
            paper.PriceNormal = form["priceNormal"];
            paper.PriceUrgency = form["priceUrgency"];
            paper.WaittingPeriodNormal = Convert.ToInt32(form["waittingPeriodNormal"]);
            paper.WaittingPeriodUrgency = Convert.ToInt32(form["waittingPeriodUrgency"]);
            context.Papers.Add(paper);
            context.SaveChanges();
        }

        public List<Paper> GetListPaper()
        {
            List<Paper> listPaper = context.Papers.ToList();
            return listPaper;
        }

        public Paper GetPaper(int id)
        {
            Paper paper = context.Papers.Single(pp => pp.IdPaper == id);
            return paper;
        }

        public String GetContentListPaper(List<int> listIdRequest)
        {
            string content = "";
            for (int i = 0; i < listIdRequest.Count; i++)
            {
                int idRequest = listIdRequest[i];
                RequestPaper request = context.RequestPapers.Single(r => r.IdRequestPaper == idRequest);
                Paper paper = ReplacePaper(request.Reasonrequest.IdPaper, request.IdAccountRequest);
                for (int j = 0; j < request.NumberPaper;j++ )
                {
                    content = content + paper.ContentPaper;
                }
            }
            return content;
        }
        public Paper ReplacePaper(int idPaper, int idAccount)
        {
            Paper paper = GetPaper(idPaper);
            try
            {

                Account account = new AccountBO().GetAccount(idAccount);
                string content = paper.ContentPaper;
                if (account.TypeAccount.Trim().Equals("SV"))
                {
                    Student student = new StudentBO().GetStudent(account.Students.First().IdStudent);

                    content = content.Replace("#hovaten", student.LastNameStudent + " " + student.FirstNameStudent);
                    content = content.Replace("#lop", student.Class.ClassName);

                    content = content.Replace("#ngaysinh", (student.BirthDay != null ? student.BirthDay.Value.ToString("dd/MM/yyyy") : ""));
                    if (student.Sex != null) content = content.Replace("#gioitinh", student.Sex);
                    content = content.Replace("#matruong", "DUT");
                    content = content.Replace("#tentruong", "Đại học Bách Khoa - Đại Học Đà Nẵng");

                    if (student.MadeOfStudy != null) content = content.Replace("#loaihinhdaotao", student.MadeOfStudy.MadeOfStudyName);
                    else content = content.Replace("#loaihinhdaotao", "");

                    content = content.Replace("#trinhdodaotao", "Đại học");
                    if (student.Class != null)
                    {
                        content = content.Replace("#nganh", student.Class.Faculty.FacultyName);
                        content = content.Replace("#khoa", student.Class.Faculty.FacultyName);
                        int beginCourse = Convert.ToInt32(student.Class.Course.AdmissionYear);
                        int endCourse = beginCourse + 5;
                        content = content.Replace("#khoahoc", beginCourse + "-" + endCourse);
                    }

                    content = content.Replace("#sothesinhvien", student.StudentNumber);

                    if (student.DateAdmission != null)
                    {
                        content = content.Replace("#ngaynhaphoc", student.DateAdmission.Value.ToShortDateString());
                        content = content.Replace("#ngayratruong", student.DateAdmission.Value.AddMonths(student.Class.NumberMonthSchool).ToShortDateString());
                    }
                    else
                    {
                        content = content.Replace("#ngaynhaphoc", "");
                        content = content.Replace("#ngayratruong", "");
                    }



                    content = content.Replace("#thoigianhoctaitruong", student.Class.NumberMonthSchool + "");

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

                    content = content.Replace("#socmnd", "");
                    content = content.Replace("#ngaycapcmnd", "");
                    content = content.Replace("#noicapcmnd", "");


                    if (student.IdWardBirthPlace != null)
                    {
                        content = content.Replace("#noisinh(tinh)", student.WardBirthPlace.District.Province.ProvinceName);
                    }
                    else
                    {
                        content = content.Replace("#noisinh(tinh)", "");
                    }

                    content = content.Replace("#khongmiengiam", "<input type='checkbox' " + (student.ObjectTuitionFee == 1 ? "checked/>" : "/>"));
                    content = content.Replace("#giamhocphi", "<input type='checkbox'" + (student.ObjectTuitionFee == 2 ? "checked/>" : "/>"));
                    content = content.Replace("#mienhocphi", "<input type='checkbox' " + (student.ObjectTuitionFee == 3 ? "checked/>" : "/>"));
                    content = content.Replace("#mocoi", "<input type='checkbox' " + (student.IsOrphan.Value ? "checked/>" : "/>"));
                    content = content.Replace("#khongmocoi", "<input type='checkbox' " + (!student.IsOrphan.Value ? "checked/>" : "/>"));

                    if (student.AdditionalPermanentResidence != null)
                    {
                        content = content.Replace("#hokhauthuongtru", student.AdditionalPermanentResidence);
                    }
                    else
                    {
                        if (student.IdWardPermanentResidence != null)
                        {
                            content = content.Replace("#hokhauthuongtru", student.WardPermanentResidence.WardName + "," + student.WardPermanentResidence.District.DistrictName + "," + student.WardPermanentResidence.District.Province.ProvinceName);
                        }
                        else
                        {
                            content = content.Replace("#hokhauthuongtru", "");
                        }
                    }

                    if (content.Contains("#bangkequarenluyen"))
                    {
                        string table = GetPointTranning(student);
                        content = content.Replace("#bangkequarenluyen", table);
                    }


                    content = content.Replace("#ngay", DateTime.Now.Day.ToString());
                    content = content.Replace("#thang", DateTime.Now.Month.ToString());
                    content = content.Replace("#nam", DateTime.Now.Year.ToString());


                }
                paper.ContentPaper = content;

            }
            catch { }
            return paper;

        }

        public string GetPointTranning(Student student)
        {
            List<Semester> listSemester = new SemesterBO().GetSemesterByYear(student.Class.Course.AdmissionYear);
            List<ConductResultSemester> listResult = new List<ConductResultSemester>();
            List<ConductResult> listConductResult = student.ConductResults.OrderBy(r => r.ConductEvent.Semester.BeginTime).ToList();
            foreach (Semester semester in listSemester)
            {
                ConductResult conductResult = null;
                try
                {
                    conductResult = student.ConductResults.Where(c => c.ConductEvent.IdSemester == semester.IdSemester && c.IsApproved == true).OrderByDescending(c => c.IdConductSchedule).FirstOrDefault();
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
                            total += Int32.Parse(pair.Split(':')[1]);
                        }
                        catch { }
                    }
                    ConductResultSemester conductResultSemester = new ConductResultSemester(conductResult.ConductEvent.IdSemester, conductResult.ConductEvent.Semester.SemesterYear.SemesterYearName, conductResult.ConductEvent.Semester.Year.YearName, total);
                    listResult.Add(conductResultSemester);
                }
            }

            string table = "<table border='1' style='width:100%'> <thead><tr><th align='center'>STT</th><th align='center'>Học kỳ</th><th align='center'>Năm học</th><th align='center'>Điểm rèn luyện</th><th align='center'>xếp loại</th><th align='center'>Ghi chú</th></tr> </thead><tbody>";
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

        public bool UpdatePaper(string content, int idPaper)
        {
            try
            {
                Paper paper = context.Papers.Single(pp => pp.IdPaper == idPaper);
                paper.ContentPaper = content;
                context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public bool UpdatePaper(int idPaper, FormCollection form)
        {
            try
            {
                Paper paper = context.Papers.Single(pp => pp.IdPaper == idPaper);
                paper.PriceNormal = form["priceNormal"];
                paper.PriceUrgency = form["priceUrgency"];
                paper.WaittingPeriodNormal = Convert.ToInt32(form["waittingPeriodNormal"]);
                paper.WaittingPeriodUrgency = Convert.ToInt32(form["waittingPeriodUrgency"]);
                context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public bool DeletePaper(int idPaper)
        {
            try
            {
                Paper paper = context.Papers.SingleOrDefault(p => p.IdPaper == idPaper);
                context.Papers.Remove(paper);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }
    }
}