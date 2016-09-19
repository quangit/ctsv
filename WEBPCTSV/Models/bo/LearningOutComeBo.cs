namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Printing;
    using System.Linq;
    using System.Web.Mvc;

    using OfficeOpenXml;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.Support;

    public class LearningOutComeBo
    {
        public static int percentProgress;

        public static List<ErrorImportExcel> ListErrorImportExcels = new List<ErrorImportExcel>();

        readonly DSAContext context = new DSAContext();

        readonly int rowInPage = new Configuration().rowInPage;


        public LearningOutCome Get(int idStudent, int idSemester)
        {
            try
            {
                return
                    this.context.LearningOutComes.FirstOrDefault(
                        s => (s.IdStudent == idStudent) && (s.IdSemester == idSemester));
            }
            catch
            {
            }

            return null;
        }

        public List<LearningOutCome> Get(int page, FormCollection form)
        {
            return
                this.context.LearningOutComes.OrderByDescending(l => l.IdLearningOutComes)
                    .Skip((page - 1) * this.rowInPage)
                    .Take(this.rowInPage)
                    .ToList();
        }

        public List<LearningOutCome> Get(int idSemester, int page, string search)
        {
            return
                this.Search(search)
                    .Where(l => l.IdSemester == idSemester)
                    .OrderByDescending(l => l.IdLearningOutComes)
                    .Skip((page - 1) * this.rowInPage)
                    .Take(this.rowInPage)
                    .ToList();
        }

        public List<LearningOutCome> GetByIdFaculty(int page, int idStudentShipSchoolFaculty, string search)
        {
            List<LearningOutCome> listLearningOutCome = this.GetListByIdFaculty(idStudentShipSchoolFaculty);
            listLearningOutCome = this.Search(listLearningOutCome, search);
            return listLearningOutCome.Skip((page - 1) * this.rowInPage).Take(this.rowInPage).ToList();
        }

        public List<LearningOutCome> GetByIdFacultyCLC(int page, int idStudentShipSchoolFaculty, string search)
        {
            List<LearningOutCome> listLearningOutCome = this.GetListByIdFacultyCLC(idStudentShipSchoolFaculty);
            listLearningOutCome = this.Search(listLearningOutCome, search);
            return listLearningOutCome.Skip((page - 1) * this.rowInPage).Take(this.rowInPage).ToList();
        }

        public int GetCountStudentShip(int idStudentShipSchoolFaculty)
        {
            return this.GetListByIdFaculty(idStudentShipSchoolFaculty).Count;
        }

        public int GetCountStudentShipCLC(int idStudentShipSchoolFaculty)
        {
            return this.GetListByIdFacultyCLC(idStudentShipSchoolFaculty).Count;
        }

        public List<LearningOutCome> GetListByIdFaculty(int idStudentShipSchoolFaculty)
        {
            StudentShipSchoolFaculty studentShipSchoolFaculty =
                this.context.StudentShipSchoolFaculties.Single(
                    s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
            List<LearningOutCome> listLearningOutCome =
                this.GetListConsiderStudenShipSchoolByFaculty(studentShipSchoolFaculty);
            listLearningOutCome = this.GetStudentShipByMoney(
                listLearningOutCome,
                Convert.ToDouble(studentShipSchoolFaculty.TotalMoney));
            return listLearningOutCome;
        }

        public List<LearningOutCome> GetListByIdFacultyCLC(int idStudentShipSchoolFaculty)
        {
            StudentShipSchoolFaculty studentShipSchoolFaculty =
                this.context.StudentShipSchoolFaculties.Single(
                    s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
            List<LearningOutCome> listLearningOutCome =
                this.GetListConsiderStudenShipSchoolByFacultyCLC(studentShipSchoolFaculty);
            listLearningOutCome = this.GetStudentShipByMoneyCLC(
                listLearningOutCome,
                Convert.ToDouble(studentShipSchoolFaculty.TotalMoney),
                Convert.ToDouble(studentShipSchoolFaculty.TuitionFee));
            return listLearningOutCome;
        }

        public List<LearningOutCome> GetListConsiderStudenShipSchoolByFaculty(
            StudentShipSchoolFaculty studentShipSchoolFaculty)
        {
            if (studentShipSchoolFaculty.IdClass == null)
            {
                List<LearningOutCome> listLearningOutCome =
                    this.context.LearningOutComes.Where(
                        l =>
                        l.Student.Class.IdFaculty == studentShipSchoolFaculty.IdFaculty
                        && !l.Student.Class.ClassName.ToUpper().Contains("CLC")
                        && l.IdSemester == studentShipSchoolFaculty.StudentShipSchool.IdSemester).ToList();
                ConditionStudentShipSchool conditionStudentShipSchool =
                    this.context.ConditionStudentShipSchools.Single(
                        c => c.IdStudentShipSchool == studentShipSchoolFaculty.IdStudentShipSchool);
                double creditNumber1 = conditionStudentShipSchool.CreditNumberStudy1;
                double relearnCreditsNumber = conditionStudentShipSchool.relearnCreditsNumber;
                double creditNumberUnQualified = conditionStudentShipSchool.creditNumberUnQualified;
                double pointTraining = conditionStudentShipSchool.pointTraining;
                double pointsShipSchool = conditionStudentShipSchool.pointsShipSchool;
                listLearningOutCome =
                    listLearningOutCome.Where(
                        l =>
                        ((l.CreditsNumber >= (creditNumber1 + l.RelearnCreditsNumber)) || l.isAcceptConsider)
                        && (l.RelearnCreditsNumber <= relearnCreditsNumber)
                        && (l.CreditNumberUnQualified <= creditNumberUnQualified) && (l.PointTraining >= pointTraining)
                        && (l.PointsShipSchool >= pointsShipSchool) && (!l.isDisEnableConsider)).ToList();
                return this.OrderbyPoint(listLearningOutCome);
            }
            else
            {
                List<LearningOutCome> listLearningOutCome =
                    this.context.LearningOutComes.Where(
                        l =>
                        l.Student.Class.IdClass == studentShipSchoolFaculty.IdClass
                        && l.IdSemester == studentShipSchoolFaculty.StudentShipSchool.IdSemester).ToList();
                ConditionStudentShipSchool conditionStudentShipSchool =
                    this.context.ConditionStudentShipSchools.Single(
                        c => c.IdStudentShipSchool == studentShipSchoolFaculty.IdStudentShipSchool);
                double creditNumber1 = conditionStudentShipSchool.CreditNumberStudy1;
                double relearnCreditsNumber = conditionStudentShipSchool.relearnCreditsNumber;
                double creditNumberUnQualified = conditionStudentShipSchool.creditNumberUnQualified;
                double pointTraining = conditionStudentShipSchool.pointTraining;
                double pointsShipSchool = conditionStudentShipSchool.pointsShipSchool;
                listLearningOutCome =
                    listLearningOutCome.Where(
                        l =>
                        ((l.CreditsNumber >= (creditNumber1 + l.RelearnCreditsNumber)) || l.isAcceptConsider)
                        && (l.RelearnCreditsNumber <= relearnCreditsNumber)
                        && (l.CreditNumberUnQualified <= creditNumberUnQualified) && (l.PointTraining >= pointTraining)
                        && (l.PointsShipSchool >= pointsShipSchool) && (!l.isDisEnableConsider)).ToList();
                return this.OrderbyPoint(listLearningOutCome);
            }
        }

        public List<LearningOutCome> GetListConsiderStudenShipSchoolByFacultyCLC(
            StudentShipSchoolFaculty studentShipSchoolFaculty)
        {
            List<LearningOutCome> listLearningOutCome =
                this.context.LearningOutComes.Where(
                    l =>
                    l.Student.Class.IdClass == studentShipSchoolFaculty.IdClass
                    && l.IdSemester == studentShipSchoolFaculty.StudentShipSchool.IdSemester).ToList();
            ConditionStudentShipSchool conditionStudentShipSchool =
                this.context.ConditionStudentShipSchools.Single(
                    c => c.IdStudentShipSchool == studentShipSchoolFaculty.IdStudentShipSchool);
            double creditNumber1 = conditionStudentShipSchool.CreditNumberStudy1;
            double relearnCreditsNumber = conditionStudentShipSchool.relearnCreditsNumber;
            double creditNumberUnQualified = conditionStudentShipSchool.creditNumberUnQualified;
            double pointTraining = conditionStudentShipSchool.pointTraining;
            double pointsShipSchool = conditionStudentShipSchool.pointsShipSchool;
            listLearningOutCome =
                listLearningOutCome.Where(
                    l =>
                    ((l.CreditsNumber >= (creditNumber1 + l.RelearnCreditsNumber)) || l.isAcceptConsider)
                    && (l.RelearnCreditsNumber <= relearnCreditsNumber)
                    && (l.CreditNumberUnQualified <= creditNumberUnQualified) && (l.PointTraining >= pointTraining)
                    && (l.PointsShipSchool >= pointsShipSchool) && (!l.isDisEnableConsider)).ToList();
            return this.OrderbyPoint(listLearningOutCome);
        }

        public string GetMoneyByCount(int idStudentShipSchoolFaculty, int countStudentShip)
        {
            StudentShipSchoolFaculty studentShipSchoolFaculty =
                this.context.StudentShipSchoolFaculties.Single(
                    s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
            int count = 1;
            double totalMoney = 0;
            if (studentShipSchoolFaculty.IdClass == null)
            {
                List<LearningOutCome> listLearningOutCome =
                    this.GetListConsiderStudenShipSchoolByFaculty(studentShipSchoolFaculty);
                foreach (LearningOutCome learningOutCome in listLearningOutCome)
                {
                    double money = Convert.ToDouble(learningOutCome.RankingAcademic.MoneyStudentShip) * 5;
                    totalMoney = totalMoney + money;
                    if (count == countStudentShip) break;
                    count++;
                }
            }
            else
            {
                List<LearningOutCome> listLearningOutCome =
                    this.GetListConsiderStudenShipSchoolByFacultyCLC(studentShipSchoolFaculty);
                foreach (LearningOutCome learningOutCome in listLearningOutCome)
                {
                    double money = 0;
                    double tuitionFee = Convert.ToDouble(studentShipSchoolFaculty.TuitionFee);

                    if (learningOutCome.IdRankingAcademic == "suatsac" || learningOutCome.IdRankingAcademic == "gioi")
                    {
                        if (count == 1)
                        {
                            StudentShipCLC top1 =
                                this.context.StudentShipCLCs.Single(s => s.idStudentShipCLC.Equals("top1"));
                            money = tuitionFee * top1.Percent;
                        }
                        else
                        {
                            StudentShipCLC top2 =
                                this.context.StudentShipCLCs.Single(s => s.idStudentShipCLC.Equals("top2"));
                            money = tuitionFee * top2.Percent;
                        }
                    }
                    else
                    {
                        money = Convert.ToDouble(learningOutCome.RankingAcademic.MoneyStudentShip) * 5;
                    }

                    totalMoney = totalMoney + money;
                    if (count == countStudentShip) break;
                    count++;
                }
            }

            return totalMoney + string.Empty;
        }

        /// <summary>
        /// lấy danh sach từ trên xuống đến khi hết tiền được cấp
        /// </summary>
        /// <param name="listLeaningOutCome"></param>
        /// <param name="totalMoney"></param>
        /// <returns></returns>
        public List<LearningOutCome> GetStudentShipByMoney(List<LearningOutCome> listLeaningOutCome, double totalMoney)
        {
            List<LearningOutCome> listNewLearningOutCome = new List<LearningOutCome>();
            foreach (LearningOutCome learningOutCome in listLeaningOutCome)
            {
                totalMoney = totalMoney - (Convert.ToDouble(learningOutCome.RankingAcademic.MoneyStudentShip) * 5);
                if (totalMoney >= 0)
                {
                    listNewLearningOutCome.Add(learningOutCome);
                }
            }

            return listNewLearningOutCome;
        }

        public List<LearningOutCome> GetStudentShipByMoneyCLC(
            List<LearningOutCome> listLeaningOutCome,
            double totalMoney,
            double tuitionFee)
        {
            List<LearningOutCome> listNewLearningOutCome = new List<LearningOutCome>();
            List<StudentShipCLC> listStudentShipCLC = this.context.StudentShipCLCs.ToList();
            int stt = 1;
            foreach (LearningOutCome learningOutCome in listLeaningOutCome)
            {
                if (learningOutCome.IdRankingAcademic == "suatsac" || learningOutCome.IdRankingAcademic == "gioi")
                {
                    if (stt == 1)
                    {
                        StudentShipCLC top1 = this.context.StudentShipCLCs.Single(
                            s => s.idStudentShipCLC.Equals("top1"));
                        totalMoney = totalMoney - tuitionFee * top1.Percent;
                    }
                    else
                    {
                        StudentShipCLC top2 = this.context.StudentShipCLCs.Single(
                            s => s.idStudentShipCLC.Equals("top2"));
                        totalMoney = totalMoney - tuitionFee * top2.Percent;
                    }
                }
                else
                {
                    totalMoney = totalMoney - (Convert.ToDouble(learningOutCome.RankingAcademic.MoneyStudentShip) * 5);
                }

                stt++;

                if (totalMoney >= 0)
                {
                    listNewLearningOutCome.Add(learningOutCome);
                }
            }

            return listNewLearningOutCome;
        }

        public string GetSURPLUSMoney(int idStudentShipSchoolFaculty)
        {
            StudentShipSchoolFaculty studentShipSchoolFaculty =
                this.context.StudentShipSchoolFaculties.Single(
                    s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
            double totalMoney = Convert.ToDouble(studentShipSchoolFaculty.TotalMoneyOld);
            List<LearningOutCome> listLearningOutCome;
            if (studentShipSchoolFaculty.IdClass == null)
            {
                listLearningOutCome = this.GetListByIdFaculty(idStudentShipSchoolFaculty);
                foreach (LearningOutCome learningOutCome in listLearningOutCome)
                {
                    totalMoney = totalMoney - (Convert.ToDouble(learningOutCome.RankingAcademic.MoneyStudentShip) * 5);
                }
            }
            else
            {
                listLearningOutCome = this.GetListByIdFacultyCLC(idStudentShipSchoolFaculty);
                int stt = 1;
                double tuitionFee = Convert.ToDouble(studentShipSchoolFaculty.TuitionFee);
                foreach (LearningOutCome learningOutCome in listLearningOutCome)
                {
                    if (learningOutCome.IdRankingAcademic == "suatsac" || learningOutCome.IdRankingAcademic == "gioi")
                    {
                        if (stt == 1)
                        {
                            StudentShipCLC top1 =
                                this.context.StudentShipCLCs.Single(s => s.idStudentShipCLC.Equals("top1"));
                            totalMoney = totalMoney - tuitionFee * top1.Percent;
                        }
                        else
                        {
                            StudentShipCLC top2 =
                                this.context.StudentShipCLCs.Single(s => s.idStudentShipCLC.Equals("top2"));
                            totalMoney = totalMoney - tuitionFee * top2.Percent;
                        }
                    }
                    else
                    {
                        totalMoney = totalMoney
                                     - (Convert.ToDouble(learningOutCome.RankingAcademic.MoneyStudentShip) * 5);
                    }

                    stt++;
                }
            }

            return totalMoney + string.Empty;
        }

        public void ImportScoteStudent(int idSemester, ExcelPackage package)
        {
            ListErrorImportExcels.Clear();

            var currentSheet = package.Workbook.Worksheets;
            var workSheet = currentSheet.Last();
            int maxCol = workSheet.Dimension.End.Column;
            int maxRow = workSheet.Dimension.End.Row;
            percentProgress = 0;
            Semester semester = this.context.Semesters.Single(s => s.IdSemester == idSemester);

            for (int rowIterator = 2; rowIterator <= maxRow; rowIterator++)
            {
                string studentNumber = String.Empty;
                try
                {
                    studentNumber = workSheet.Cells[rowIterator, 1].Value.ToString();
                    Student student = this.context.Students.Single(s => s.StudentNumber.Equals(studentNumber));

                    if (student != null)
                    {
                        float points10Scale = (float)Convert.ToDouble(workSheet.Cells[rowIterator, 6].Value.ToString());
                        float creditsNumber = (float)Convert.ToDouble(workSheet.Cells[rowIterator, 3].Value.ToString());
                        float points4Scale = (float)Convert.ToDouble(workSheet.Cells[rowIterator, 4].Value.ToString());
                        int pointStraining = Convert.ToInt32(workSheet.Cells[rowIterator, 7].Value.ToString());
                        float relearnCreditsNumber =
                            (float)Convert.ToDouble(workSheet.Cells[rowIterator, 11].Value.ToString());
                        float pointsAverageShipSchool =
                            (float)Convert.ToDouble(workSheet.Cells[rowIterator, 5].Value.ToString());
                        float creditNumberUnQualified =
                            (float)Convert.ToDouble(workSheet.Cells[rowIterator, 8].Value.ToString());
                        int course = Convert.ToInt32(workSheet.Cells[rowIterator, 10].Value.ToString());

                        // năm học của sinh viên
                        int courseStudent = semester.IdYear - student.Class.Course.AdmissionYear + 1;

                        // nếu học đúng tiến độ
                        bool isSchedule = false;
                        if (course == courseStudent)
                        {
                            isSchedule = true;
                        }

                        float pointPlus = 0;
                        if (student.RegencyStudents.Count > 0)
                        {
                            pointPlus = student.RegencyStudents.First().Regency.PlusPoint.Value;
                        }

                        LearningOutCome learningOutCome = this.Get(student.IdStudent, idSemester);
                        if (learningOutCome == null)
                            this.Insert(
                                student,
                                idSemester,
                                points10Scale,
                                points4Scale,
                                pointStraining,
                                creditsNumber,
                                relearnCreditsNumber,
                                pointsAverageShipSchool,
                                creditNumberUnQualified,
                                isSchedule,
                                pointPlus);
                        else
                            this.Update(
                                student,
                                learningOutCome.IdLearningOutComes,
                                idSemester,
                                points10Scale,
                                points4Scale,
                                pointStraining,
                                creditsNumber,
                                relearnCreditsNumber,
                                pointsAverageShipSchool,
                                creditNumberUnQualified,
                                isSchedule,
                                pointPlus);
                    }

                    percentProgress = (int)(((double)rowIterator / maxRow) * 100);
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

        public bool Insert(
            Student student,
            int idSemester,
            float points10Scale,
            float points4Scale,
            int pointStraining,
            float creditsNumber,
            float relearnCreditsNumber,
            float pointsAverageShipSchool,
            float creditNumberUnQualified,
            bool isSchedule,
            float PointPlus)
        {
            using (DSAContext ct = new DSAContext())
            {
                try
                {
                    LearningOutCome learningOutcome = new LearningOutCome();
                    learningOutcome.IdStudent = student.IdStudent;
                    learningOutcome.IdSemester = idSemester;
                    learningOutcome.Points10Scale = points10Scale;
                    learningOutcome.Points4Scale = points4Scale;
                    learningOutcome.PointTraining = pointStraining;
                    learningOutcome.CreditsNumber = creditsNumber;
                    learningOutcome.RelearnCreditsNumber = relearnCreditsNumber;
                    learningOutcome.PointsAverageShipSchool = pointsAverageShipSchool;
                    learningOutcome.CreditNumberUnQualified = creditNumberUnQualified;
                    learningOutcome.IsSchedule = isSchedule;
                    learningOutcome.PointPlus = PointPlus;

                    float pointPlusRegency = 0;
                    if (student.RegencyStudents.Count > 0)
                    {
                        pointPlusRegency = student.RegencyStudents.FirstOrDefault().Regency.PlusPoint.Value;
                    }

                    learningOutcome.PointsShipSchool =
                        (float)Math.Round(pointsAverageShipSchool + pointPlusRegency, 2);

                    if (learningOutcome.PointsShipSchool > 9 && learningOutcome.PointTraining > 90) learningOutcome.IdRankingAcademic = "suatsac";
                    else if (learningOutcome.PointsShipSchool > 8 && learningOutcome.PointTraining > 80) learningOutcome.IdRankingAcademic = "gioi";
                    else if (learningOutcome.PointsShipSchool > 7 && learningOutcome.PointTraining > 70) learningOutcome.IdRankingAcademic = "kha";
                    else if (learningOutcome.PointsShipSchool > 5 && learningOutcome.PointTraining > 50) learningOutcome.IdRankingAcademic = "trungbinh";
                    else learningOutcome.IdRankingAcademic = "yeu";
                    ct.LearningOutComes.Add(learningOutcome);
                    ct.SaveChanges();
                    return true;
                }
                catch
                {
                }

                return false;
            }
        }

        public List<LearningOutCome> OrderbyPoint(List<LearningOutCome> listLearningOutCome)
        {
            IEnumerable<LearningOutCome> listLearningOutComeExcellent =
                listLearningOutCome.Where(l => l.IdRankingAcademic.Equals("suatsac"))
                    .OrderByDescending(l => l.PointsShipSchool)
                    .ThenByDescending(l => l.PointTraining);
            IEnumerable<LearningOutCome> listLearningOutComeVeryGood =
                listLearningOutCome.Where(l => l.IdRankingAcademic.Equals("gioi"))
                    .OrderByDescending(l => l.PointsShipSchool)
                    .ThenByDescending(l => l.PointTraining);
            IEnumerable<LearningOutCome> listLearningOutComeGood =
                listLearningOutCome.Where(l => l.IdRankingAcademic.Equals("kha"))
                    .OrderByDescending(l => l.PointsShipSchool)
                    .ThenByDescending(l => l.PointTraining);
            List<LearningOutCome> list = new List<LearningOutCome>();
            list.AddRange(listLearningOutComeExcellent);
            list.AddRange(listLearningOutComeVeryGood);
            list.AddRange(listLearningOutComeGood);
            return list;
        }

        public void SaveStudentShipSchoolStudent(int idStudentShipSchoolFaculty)
        {
            List<LearningOutCome> listLearningOutCome = this.GetListByIdFaculty(idStudentShipSchoolFaculty);
            new StudentShipSchoolStudentBo().SaveStudentShipSchoolStudent(
                listLearningOutCome,
                idStudentShipSchoolFaculty);
        }

        public void SaveStudentShipSchoolStudentCLC(int idStudentShipSchoolFaculty)
        {
            List<LearningOutCome> listLearningOutCome = this.GetListByIdFacultyCLC(idStudentShipSchoolFaculty);
            new StudentShipSchoolStudentBo().SaveStudentShipSchoolStudentCLC(
                listLearningOutCome,
                idStudentShipSchoolFaculty);
        }

        public List<LearningOutCome> Search(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return
                    this.context.LearningOutComes.Where(
                        l =>
                        l.Student.StudentNumber.Contains(search)
                        || (l.Student.LastNameStudent + l.Student.FirstNameStudent).ToUpper()
                               .Trim()
                               .Contains(search.ToUpper().Trim())
                        || l.Student.Class.ClassName.ToUpper().Trim().Contains(search.ToUpper().Trim())
                        || l.Student.Class.Faculty.FacultyName.ToUpper().Trim().Contains(search.ToUpper().Trim())
                        || l.RankingAcademic.NameRankingAcademic.Equals(search)).ToList();
            }
            else
            {
                return this.context.LearningOutComes.ToList();
            }
        }

        public List<LearningOutCome> Search(List<LearningOutCome> list, string search)
        {
            if (search != null)
                return
                    list.Where(
                        l =>
                        l.Student.StudentNumber.Contains(search)
                        || (l.Student.LastNameStudent + string.Empty + l.Student.FirstNameStudent).ToUpper()
                               .Trim()
                               .Contains(search.ToUpper().Trim())
                        || l.Student.Class.ClassName.ToUpper().Trim().Contains(search.ToUpper().Trim())
                        || l.Student.Class.Faculty.FacultyName.ToUpper().Trim().Contains(search.ToUpper().Trim()))
                        .ToList();
            else return list;
        }

        public PageNumber TotalPage(int page, FormCollection form)
        {
            PageNumber pageNumber = new PageNumber();
            int toltalStudent = this.context.LearningOutComes.ToList().Count;
            pageNumber.PageNumberTotal = toltalStudent / this.rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/StudentShip/ListScoteStudent?page=";
            return pageNumber;
        }

        public PageNumber TotalPage(int idSemester, int page, string search, string tumlum)
        {
            PageNumber pageNumber = new PageNumber();
            int toltalStudent = 1;
            try
            {
                toltalStudent = this.Search(search).Where(l => l.IdSemester == idSemester).ToList().Count;
            }
            catch
            {
            }

            pageNumber.PageNumberTotal = toltalStudent / this.rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/StudentShip/ListScoteStudent?idSemester=" + idSemester + "&page=";
            return pageNumber;
        }

        public PageNumber TotalPage(int page, int idStudentShipSchoolFaculty, string search)
        {
            PageNumber pageNumber = new PageNumber();
            List<LearningOutCome> listLearningOutCome = this.GetListByIdFaculty(idStudentShipSchoolFaculty);
            listLearningOutCome = this.Search(listLearningOutCome, search);
            int toltalStudent = listLearningOutCome.Count;
            pageNumber.PageNumberTotal = toltalStudent / this.rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/StudentShip/ListStudentShipSchoolFaculty?idStudentShipSchoolFaculty="
                              + idStudentShipSchoolFaculty + "&&page=";
            return pageNumber;
        }

        public bool Update(
            Student student,
            int idLearningOutComes,
            int idSemester,
            float points10Scale,
            float points4Scale,
            int pointStraining,
            float creditsNumber,
            float relearnCreditsNumber,
            float pointsAverageShipSchool,
            float creditNumberUnQualified,
            bool isSchedule,
            float PointPlus)
        {
            using (DSAContext ct = new DSAContext())
            {
                try
                {
                    LearningOutCome learningOutcome =
                        ct.LearningOutComes.Single(l => l.IdLearningOutComes == idLearningOutComes);
                    learningOutcome.IdSemester = idSemester;
                    learningOutcome.Points10Scale = points10Scale;
                    learningOutcome.Points4Scale = points4Scale;
                    learningOutcome.PointTraining = pointStraining;
                    learningOutcome.CreditsNumber = creditsNumber;
                    learningOutcome.RelearnCreditsNumber = relearnCreditsNumber;
                    learningOutcome.PointsAverageShipSchool = pointsAverageShipSchool;
                    learningOutcome.CreditNumberUnQualified = creditNumberUnQualified;
                    learningOutcome.IsSchedule = isSchedule;
                    learningOutcome.PointPlus = PointPlus;

                    float pointPlusRegency = 0;
                    if (student.RegencyStudents.Count > 0)
                    {
                        pointPlusRegency = student.RegencyStudents.FirstOrDefault().Regency.PlusPoint.Value;
                    }

                    learningOutcome.PointsShipSchool =
                        (float)Math.Round(pointsAverageShipSchool + pointPlusRegency, 2);

                    if (learningOutcome.PointsShipSchool > 9 && learningOutcome.PointTraining > 90) learningOutcome.IdRankingAcademic = "suatsac";
                    else if (learningOutcome.PointsShipSchool > 8 && learningOutcome.PointTraining > 80) learningOutcome.IdRankingAcademic = "gioi";
                    else if (learningOutcome.PointsShipSchool > 7 && learningOutcome.PointTraining > 70) learningOutcome.IdRankingAcademic = "kha";
                    else if (learningOutcome.PointsShipSchool > 5 && learningOutcome.PointTraining > 50) learningOutcome.IdRankingAcademic = "trungbinh";
                    else learningOutcome.IdRankingAcademic = "yeu";
                    ct.SaveChanges();
                    return true;
                }
                catch
                {
                }

                return false;
            }
        }

        public void UpdateIsAcceptConsider(int idLearningOutCome)
        {
            try
            {
                LearningOutCome learningOutCome =
                    this.context.LearningOutComes.SingleOrDefault(l => l.IdLearningOutComes == idLearningOutCome);
                learningOutCome.isAcceptConsider = !learningOutCome.isAcceptConsider;
                this.context.SaveChanges();
            }
            catch
            {
            }
        }

        public void UpdateIsDisEnableConsider(int idLearningOutCome)
        {
            try
            {
                LearningOutCome learningOutCome =
                    this.context.LearningOutComes.SingleOrDefault(l => l.IdLearningOutComes == idLearningOutCome);
                learningOutCome.isDisEnableConsider = !learningOutCome.isDisEnableConsider;
                this.context.SaveChanges();
            }
            catch
            {
            }
        }
    }
}