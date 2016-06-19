using OfficeOpenXml;
using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class RegencyStudentBo
    {
        DSAContext context = new DSAContext();
        public static int percentProgress;
        public void ImportRegencyStudent(ExcelPackage package)
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
                    Student student = new StudentBO().GetStudentByStudentNumber(studentNumber);
                    string idRegency = workSheet.Cells[rowIterator, 6].Value.ToString();
                    if(Get(idRegency,student.IdStudent)==null)Insert(idRegency, student.IdStudent);
                    percentProgress = (int)(((double)rowIterator / maxRow) * 100);
                }
                catch { }
            }
        }
        public RegencyStudent Get(string idRegency, int idStudent)
        {
            try {
                return context.RegencyStudents.Single(r => r.IdRegency.Equals(idRegency) && r.IdStudent == idStudent);
            }
            catch { }
            return null;
        }

        public void ResetProgress()
        {
            percentProgress = 0;
        }

        public void Insert(string idRegency, int idStudent)
        {
            try
            {
                RegencyStudent regencyStudent = new RegencyStudent();
                Regency regency = context.Regencies.Single(r => r.IdRegency.Equals(idRegency));
                regencyStudent.IdRegency = regency.IdRegency;
                regencyStudent.IdStudent = idStudent;
                context.RegencyStudents.Add(regencyStudent);
                context.SaveChanges();
            }
            catch { }
        }

        public void Update(string idRegency, int idRegencyStudent)
        {
            try
            {
                RegencyStudent regencyStudent = context.RegencyStudents.Single(r => r.IdRegencyStudent == idRegencyStudent);
                regencyStudent.IdRegency = idRegency;
                context.SaveChanges();
            }
            catch { }
        }

        

        
    }
}