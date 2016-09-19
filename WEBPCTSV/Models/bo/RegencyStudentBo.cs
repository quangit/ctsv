namespace WEBPCTSV.Models.bo
{
    using System.Linq;

    using OfficeOpenXml;

    using WEBPCTSV.Models.bean;

    public class RegencyStudentBo
    {
        public static int percentProgress;

        readonly DSAContext context = new DSAContext();

        public RegencyStudent Get(string idRegency, int idStudent)
        {
            try
            {
                return
                    this.context.RegencyStudents.Single(r => r.IdRegency.Equals(idRegency) && r.IdStudent == idStudent);
            }
            catch
            {
            }

            return null;
        }

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
                    if (this.Get(idRegency, student.IdStudent) == null) this.Insert(idRegency, student.IdStudent);
                    percentProgress = (int)(((double)rowIterator / maxRow) * 100);
                }
                catch
                {
                }
            }
        }

        public void Insert(string idRegency, int idStudent)
        {
            try
            {
                RegencyStudent regencyStudent = new RegencyStudent();
                Regency regency = this.context.Regencies.Single(r => r.IdRegency.Equals(idRegency));
                regencyStudent.IdRegency = regency.IdRegency;
                regencyStudent.IdStudent = idStudent;
                this.context.RegencyStudents.Add(regencyStudent);
                this.context.SaveChanges();
            }
            catch
            {
            }
        }

        public void ResetProgress()
        {
            percentProgress = 0;
        }

        public void Update(string idRegency, int idRegencyStudent)
        {
            try
            {
                RegencyStudent regencyStudent =
                    this.context.RegencyStudents.Single(r => r.IdRegencyStudent == idRegencyStudent);
                regencyStudent.IdRegency = idRegency;
                this.context.SaveChanges();
            }
            catch
            {
            }
        }
    }
}