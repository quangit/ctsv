using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ClosedXML.Excel;
using WEBPCTSV.Models.Support;

namespace WEBPCTSV.Models.bo
{
    public class StudentShipSchoolBo
    {
        DSAContext context = new DSAContext();
        public static int percentProgress = 1; 

        public StudentShipSchool Get(int idSemester)
        {
            try
            {
                return context.StudentShipSchools.Single(s => s.IdSemester == idSemester);
            }
            catch { }
            return null;
        }

        public StudentShipSchool GetById(int idStudentShipSchool)
        {
            return context.StudentShipSchools.SingleOrDefault(s => s.IdStudentShipSchool == idStudentShipSchool);
        }


        public string GetMoney(int idSemester)
        {
            try
            {
                return Get(idSemester).TotalMoney;
            }
            catch { }
            return "0";

        }




        public bool Insert(FormCollection form)
        {
            try
            {
                int idSemester = Convert.ToInt32(form["selectSemester"]);
                StudentShipSchool studentShipSchool = Get(idSemester);
                if (studentShipSchool == null)
                {
                    studentShipSchool = new StudentShipSchool();
                    studentShipSchool.IdSemester = Convert.ToInt32(form["selectSemester"]);
                    studentShipSchool.TotalMoney = form["totalMoney"];
                    studentShipSchool.Status = "Đang xữ lý";                 
                    context.StudentShipSchools.Add(studentShipSchool);
                    context.SaveChanges();
                    new ConditionStudentShipSchoolBo().InsertByDefault(studentShipSchool.IdStudentShipSchool);

                }
                else {
                    studentShipSchool.TotalMoney = form["totalMoney"];
                    context.SaveChanges();
                }
                
                
                return true;
            }
            catch { }
            return false;

        }

        public bool UpdateMoney(int idStudentShipSchool)
        {
            try {
                StudentShipSchool studentShipSchool = GetById(idStudentShipSchool);
                List<StudentShipSchoolFaculty> listStudentShipSchoolFaculty = studentShipSchool.StudentShipSchoolFaculties.ToList();
                double money = 0;
                foreach (StudentShipSchoolFaculty studentShipSchoolFaculty in listStudentShipSchoolFaculty)
                {
                    money += Convert.ToDouble(studentShipSchoolFaculty.TotalMoney);
                }
                studentShipSchool.TotalMoney = money + "";
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;           
        }

        public MemoryStream ExportListStudentShips(int idStudentShipSchool)
        {
            MemoryStream fileStream = new MemoryStream();
            var workbook = new XLWorkbook();

            StudentShipSchool studentShipSchool = context.StudentShipSchools.Single(s => s.IdStudentShipSchool == idStudentShipSchool);
            List<StudentShipSchoolFaculty> listStudentShipFaculty = studentShipSchool.StudentShipSchoolFaculties.ToList();
            
            workbook = ExportGeneralStudentShip(studentShipSchool, workbook);
            //percentProgress = 10;
            double i = 1;
            double length = studentShipSchool.StudentShipSchoolFaculties.Count;
            foreach(StudentShipSchoolFaculty studentShipSchoolFaculty in listStudentShipFaculty)
            {
                workbook = new StudentShipSchoolFacultyBo().ExportListStudentShipschoolFaculty(studentShipSchoolFaculty, workbook);
                
                percentProgress = (int)((i / length) * 100);
                i++;
            }
            workbook.SaveAs(fileStream);
            fileStream.Position = 0;
            percentProgress = 1;

            return fileStream;
        }

        public XLWorkbook ExportGeneralStudentShip(StudentShipSchool studentShipSchool, XLWorkbook workbook)
        {
            List<StudentShipSchoolFaculty> listStudentShipSchoolFaculty = studentShipSchool.StudentShipSchoolFaculties.ToList();
            var ws = workbook.Worksheets.Add("Tổng quát");
            //worksheet.Cell("A1").Value = "Hello World!";

            ws.Cell(2, 2).Value = "Tổng quát";

            ws.Cell(2, 2).Style.Font.Bold = true;
            ws.Cell(2, 2).Style.Font.FontSize = 22;

            ws.Cell(4, 1).Value = "Stt";
            ws.Cell(4, 1).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(1).Width = 5;

            ws.Cell(4, 2).Value = "Khoa/Lớp";
            ws.Cell(4, 2).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(2).Width = 50;

            ws.Cell(4, 3).Value = "Số tiền";
            ws.Cell(4, 3).Style.Fill.BackgroundColor = XLColor.FromName("PowderBlue");
            ws.Cell(4, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Column(3).Width = 20;

            

            int row = 5;
            foreach (StudentShipSchoolFaculty st in listStudentShipSchoolFaculty)
            {
                ws.Cell(row, 1).Value = row;
                ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                if(st.IdClass!=null)
                {
                    ws.Cell(row, 2).Value = "Lớp: "+st.Class.ClassName;
                }
                else
                {
                    ws.Cell(row, 2).Value ="Khoa: " +st.Faculty.FacultyName;
                }
                

                ws.Cell(row, 3).Value = ConvertObject.ConvertCurrency(st.TotalMoney) + " VND";

                row++;
            }
            row++;
            ws.Cell(row, 2).Value = "Tổng tiền";
            ws.Cell(row, 2).Style.Fill.BackgroundColor = XLColor.Red;
            ws.Cell(row, 2).Style.Font.Bold = true;
            ws.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Cell(row, 3).Value = ConvertObject.ConvertCurrency(studentShipSchool.TotalMoney) + " VNĐ";
            ws.Cell(row, 3).Style.Fill.BackgroundColor = XLColor.Red;


            return workbook;
        }
    }
}