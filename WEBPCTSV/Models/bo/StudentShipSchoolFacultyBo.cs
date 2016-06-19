
using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.Support;
using System.IO;
using ClosedXML.Excel;
using OfficeOpenXml;

namespace WEBPCTSV.Models.bo
{
    public class StudentShipSchoolFacultyBo
    {
        DSAContext context = new DSAContext();

        public List<StudentShipSchoolFaculty> Get(int idStudentShipSchool)
        {
            return context.StudentShipSchoolFaculties.Where(s => s.IdStudentShipSchool == idStudentShipSchool).ToList();
        }

        public StudentShipSchoolFaculty GetById(int idStudentShipSchoolFaculty)
        {
            return context.StudentShipSchoolFaculties.Single(s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
        }

        public string GetSURPLUS(int idStudentShipSchool)
        {
            try
            {
                List<StudentShipSchoolFaculty> listStudentShipSchoolFaculty = context.StudentShipSchoolFaculties.Where(s => s.IdStudentShipSchool == idStudentShipSchool && s.IdFaculty != null).ToList();
                StudentShipSchool studentShipSchool = context.StudentShipSchools.Single(s => s.IdStudentShipSchool == idStudentShipSchool);
                double surplus = Convert.ToDouble(studentShipSchool.TotalMoney);
                foreach (StudentShipSchoolFaculty st in listStudentShipSchoolFaculty)
                {
                    surplus = surplus - Convert.ToDouble(st.TotalMoney);
                }

                return surplus.ToString();
            }
            catch { }
            return "0";

        }


        public void Import(int idStudentShipSchool, ExcelPackage package)
        {
            var currentSheet = package.Workbook.Worksheets;
            var workSheet = currentSheet.First();
            ImportMoneyStudentShipSchoolFaculty(idStudentShipSchool, workSheet);
            var workSheetCLC = currentSheet.Last();
            ImportMoneyStudentShipSchoolFacultyCLC(idStudentShipSchool, workSheetCLC);
            new StudentShipSchoolBo().UpdateMoney(idStudentShipSchool);
        }


        public void ImportMoneyStudentShipSchoolFaculty(int idStudentShipSchool, ExcelWorksheet workSheet)
        {
            int maxCol = workSheet.Dimension.End.Column;
            int maxRow = workSheet.Dimension.End.Row;

            for (int rowIterator = 2; rowIterator <= maxRow; rowIterator++)
            {
                try
                {
                    string facultyNumber = workSheet.Cells[rowIterator, 3].Value.ToString();
                    if (facultyNumber != "" || facultyNumber != null)
                    {
                        string totalMoney = workSheet.Cells[rowIterator, 13].Value.ToString();
                        int idFaculty = new FacultyBO().Get(facultyNumber).IdFaculty;
                        InsertStudentShipFaculty(idStudentShipSchool, idFaculty, null, totalMoney, null);
                    }
                }
                catch { }
            }
        }

        public void ImportMoneyStudentShipSchoolFacultyCLC(int idStudentShipSchool, ExcelWorksheet workSheet)
        {
            int maxCol = workSheet.Dimension.End.Column;
            int maxRow = workSheet.Dimension.End.Row;

            for (int rowIterator = 2; rowIterator <= maxRow; rowIterator++)
            {
                try
                {
                    string className = workSheet.Cells[rowIterator, 5].Value.ToString();
                    if (className != "" || className != null)
                    {
                        string totalMoney = workSheet.Cells[rowIterator, 12].Value.ToString();
                        int idClass = new ClassBO().Get(className).IdClass;
                        string tuitionFee = workSheet.Cells[rowIterator, 13].Value.ToString();
                        InsertStudentShipFaculty(idStudentShipSchool, null, idClass, totalMoney, tuitionFee);
                    }
                }
                catch { }
            }
        }



        public bool InsertStudentShipFaculty(int idStudentShipSchool, int? idFaculty, int? idClass, string totalMoney, string tuitionFee)
        {
            try
            {
                StudentShipSchoolFaculty studentShipSchoolFaculty = context.StudentShipSchoolFaculties.SingleOrDefault(s => s.IdStudentShipSchool == idStudentShipSchool && s.IdFaculty == idFaculty && s.IdClass == idClass);
                if (studentShipSchoolFaculty == null)
                {
                    studentShipSchoolFaculty = new StudentShipSchoolFaculty();
                    studentShipSchoolFaculty.IdClass = idClass;
                    studentShipSchoolFaculty.IdFaculty = idFaculty;
                    studentShipSchoolFaculty.IdStudentShipSchool = idStudentShipSchool;
                    studentShipSchoolFaculty.TotalMoney = totalMoney;
                    studentShipSchoolFaculty.TotalMoneyOld = totalMoney;
                    studentShipSchoolFaculty.TuitionFee = tuitionFee;
                    context.StudentShipSchoolFaculties.Add(studentShipSchoolFaculty);
                }
                else
                {
                    studentShipSchoolFaculty.TotalMoney = totalMoney;
                    //studentShipSchoolFaculty.TotalMoneyOld = totalMoney;
                    studentShipSchoolFaculty.TuitionFee = tuitionFee;
                }
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public bool Update(int idStudentShipSchoolFaculty, FormCollection form)
        {
            try
            {
                StudentShipSchoolFaculty studentShipSchoolFaculty = context.StudentShipSchoolFaculties.Single(s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
                studentShipSchoolFaculty.TotalMoney = ConvertObject.ConvertCurrencyToString(form["MoneyStudentShipFaculty"]);
                context.SaveChanges();
                new StudentShipSchoolBo().UpdateMoney(studentShipSchoolFaculty.IdStudentShipSchool);
                return true;
            }
            catch { }
            return false;
        }

        public bool MoveMoneyStudentShip(string money, int idStudentShipSchoolFaculty)
        {
            try
            {
                StudentShipSchoolFaculty studentShipSchoolFaculty = context.StudentShipSchoolFaculties.Single(s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
                StudentShipSchoolFaculty studentShipSchoolFacultyReceive = context.StudentShipSchoolFaculties.Single(s => s.IdStudentShipSchool == studentShipSchoolFaculty.IdStudentShipSchool && s.IdFaculty == studentShipSchoolFaculty.Class.IdFaculty);
                studentShipSchoolFaculty.TotalMoneyOld = (Convert.ToDouble(studentShipSchoolFaculty.TotalMoneyOld) - Convert.ToDouble(money)) + "";
                studentShipSchoolFacultyReceive.TotalMoneyOld = (Convert.ToDouble(studentShipSchoolFacultyReceive.TotalMoneyOld) + Convert.ToDouble(money)) + "";
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public int AddAdditionalConsiderStudentShipSchoolFaculty(int idStudentShipSchool, string totalMoney)
        {
            try
            {
                StudentShipSchoolFaculty studentShipSchoolFaculty = GetAdditionalConsiderStudentShipSchoolFaculty(idStudentShipSchool);
                if (studentShipSchoolFaculty == null)
                {
                    studentShipSchoolFaculty = new StudentShipSchoolFaculty();
                    studentShipSchoolFaculty.IdStudentShipSchool = idStudentShipSchool;
                    studentShipSchoolFaculty.TotalMoney = totalMoney;
                    context.StudentShipSchoolFaculties.Add(studentShipSchoolFaculty);
                    context.SaveChanges();
                }
                else
                {
                    studentShipSchoolFaculty.TotalMoney = totalMoney;
                    context.SaveChanges();
                }
                return studentShipSchoolFaculty.IdStudentShipSchoolFaculty;
            }
            catch { }
            return 0;
        }

        public StudentShipSchoolFaculty GetAdditionalConsiderStudentShipSchoolFaculty(int idStudentShipSchool)
        {
            try
            {
                return context.StudentShipSchoolFaculties.Single(s => s.IdStudentShipSchool == idStudentShipSchool && s.IdClass == null && s.IdFaculty == null);
            }
            catch { }
            return null;
        }


        public MemoryStream ExportListStudentShipschoolFaculty(int idStudentShipSchoolFaculty)
        {
            StudentShipSchoolFaculty studentShipSchoolFaculty = context.StudentShipSchoolFaculties.Single(s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
            List<StudentShipSchoolStudent> listStudentShipSchoolStudent = studentShipSchoolFaculty.StudentShipSchoolStudents.ToList();
            MemoryStream fileStream = new MemoryStream();
            var workbook = new XLWorkbook();
            String sheetName = "";
            if (studentShipSchoolFaculty.IdClass != null)
            {
                sheetName = "Lớp " + studentShipSchoolFaculty.Class.ClassName;
            }
            else
            {
                sheetName = "Khoa " + studentShipSchoolFaculty.Faculty.FacultyName;

            }
            sheetName = sheetName.RemoveSpecialCharacters();
            var ws = workbook.Worksheets.Add(sheetName);
            //worksheet.Cell("A1").Value = "Hello World!";

            ws.Cell(2, 2).Value = sheetName;

            ws.Cell(2, 2).Style.Font.Bold = true;
            ws.Cell(2, 2).Style.Font.FontSize = 22;

            ws.Cell(4, 1).Value = "Stt";
            ws.Cell(4, 1).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(1).Width = 5;

            ws.Cell(4, 2).Value = "MSSV";
            ws.Cell(4, 2).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(2).Width = 14;

            ws.Cell(4, 3).Value = "Họ và tên";
            ws.Cell(4, 3).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(3).Width = 30;

            ws.Cell(4, 4).Value = "Lớp";
            ws.Cell(4, 4).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(4).Width = 10;

            ws.Cell(4, 5).Value = "Loại HB";
            ws.Cell(4, 5).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(5).Width = 10;

            ws.Cell(4, 6).Value = "Số tiền";
            ws.Cell(4, 6).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(6).Width = 18;

            int row = 5;
            foreach (StudentShipSchoolStudent st in listStudentShipSchoolStudent)
            {
                ws.Cell(row, 1).Value = row;
                ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(row, 2).Value = st.LearningOutCome.Student.StudentNumber;
                ws.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(row, 3).Value = st.LearningOutCome.Student.LastNameStudent + " " + st.LearningOutCome.Student.FirstNameStudent;
                ws.Cell(row, 4).Value = st.LearningOutCome.Student.Class.ClassName;


                ws.Cell(row, 5).Value = st.LearningOutCome.RankingAcademic.NameRankingAcademic;


                ws.Cell(row, 6).Value = ConvertObject.ConvertCurrency(st.Money) + " VNĐ";
                ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                row++;
            }
            row++;
            ws.Cell(row, 3).Value = "Tổng tiền";
            ws.Cell(row, 3).Style.Fill.BackgroundColor = XLColor.Red;
            ws.Cell(row, 3).Style.Font.Bold = true;
            ws.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            
            ws.Cell(row, 4).Style.Fill.BackgroundColor = XLColor.Red;
            
            ws.Cell(row, 5).Style.Fill.BackgroundColor = XLColor.Red;

            ws.Cell(row, 6).Value = ConvertObject.ConvertCurrency(studentShipSchoolFaculty.TotalMoney)+ " VNĐ";
            ws.Cell(row, 6).Style.Fill.BackgroundColor = XLColor.Red;
            ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            workbook.SaveAs(fileStream);
            fileStream.Position = 0;

            return fileStream;
        }
        public XLWorkbook ExportListStudentShipschoolFaculty(StudentShipSchoolFaculty studentShipSchoolFaculty, XLWorkbook workbook)
        {
            List<StudentShipSchoolStudent> listStudentShipSchoolStudent = studentShipSchoolFaculty.StudentShipSchoolStudents.ToList();
            
            String sheetName = "";
            if (studentShipSchoolFaculty.IdClass != null)
            {
                sheetName = "Lớp " + studentShipSchoolFaculty.Class.ClassName;
            }
            else
            {
                sheetName = "Khoa " + studentShipSchoolFaculty.Faculty.FacultyName;
            }
            sheetName = sheetName.RemoveSpecialCharacters();
            var ws = workbook.Worksheets.Add(sheetName);
            //worksheet.Cell("A1").Value = "Hello World!";

            ws.Cell(2, 2).Value = sheetName;

            ws.Cell(2, 2).Style.Font.Bold = true;
            ws.Cell(2, 2).Style.Font.FontSize = 22;

            ws.Cell(4, 1).Value = "Stt";
            ws.Cell(4, 1).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(1).Width = 5;

            ws.Cell(4, 2).Value = "MSSV";
            ws.Cell(4, 2).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(2).Width = 14;

            ws.Cell(4, 3).Value = "Họ và tên";
            ws.Cell(4, 3).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(3).Width = 30;

            ws.Cell(4, 4).Value = "Lớp";
            ws.Cell(4, 4).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(4).Width = 10;

            ws.Cell(4, 5).Value = "Loại HB";
            ws.Cell(4, 5).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(5).Width = 10;

            ws.Cell(4, 6).Value = "Số tiền";
            ws.Cell(4, 6).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(6).Width = 18;

            int row = 5;
            foreach (StudentShipSchoolStudent st in listStudentShipSchoolStudent)
            {
                ws.Cell(row, 1).Value = row;
                ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(row, 2).Value = st.LearningOutCome.Student.StudentNumber;
                ws.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(row, 3).Value = st.LearningOutCome.Student.LastNameStudent + " " + st.LearningOutCome.Student.FirstNameStudent;
                ws.Cell(row, 4).Value = st.LearningOutCome.Student.Class.ClassName;


                ws.Cell(row, 5).Value = st.LearningOutCome.RankingAcademic.NameRankingAcademic;


                ws.Cell(row, 6).Value = ConvertObject.ConvertCurrency(st.Money) + " VNĐ";
                ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                row++;
            }
            row++;
            ws.Cell(row, 3).Value = "Tổng tiền";
            ws.Cell(row, 3).Style.Fill.BackgroundColor = XLColor.Red;
            ws.Cell(row, 3).Style.Font.Bold = true;
            ws.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            ws.Cell(row, 4).Style.Fill.BackgroundColor = XLColor.Red;

            ws.Cell(row, 5).Style.Fill.BackgroundColor = XLColor.Red;

            ws.Cell(row, 6).Value = ConvertObject.ConvertCurrency(studentShipSchoolFaculty.TotalMoney) + " VNĐ";
            ws.Cell(row, 6).Style.Fill.BackgroundColor = XLColor.Red;
            ws.Cell(row, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            return workbook;
        }


    }
}