namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class StudentShipSchoolStudentBo
    {
        public static int percentProgress;

        readonly DSAContext context = new DSAContext();

        public void Delete(int idStudentShipSchoolFaculty)
        {
            List<StudentShipSchoolStudent> listStudentShipSchoolStudent =
                this.context.StudentShipSchoolStudents.Where(
                    s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty).ToList();
            this.context.StudentShipSchoolStudents.RemoveRange(listStudentShipSchoolStudent);
            this.context.SaveChanges();
        }

        public StudentShipSchoolStudent Get(int idLearningOutComes, int idStudentShipSchoolFaculty)
        {
            try
            {
                return
                    this.context.StudentShipSchoolStudents.Single(
                        s =>
                        s.IdLearningOutComes == idLearningOutComes
                        && s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
            }
            catch
            {
            }

            return null;
        }

        public List<StudentShipSchoolStudent> GetList(int idStudentShipSchoolFaculty)
        {
            try
            {
                return
                    this.context.StudentShipSchoolStudents.Where(
                        s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty).ToList();
            }
            catch
            {
            }

            return null;
        }

        public bool Insert(int idLearningOutComes, int idStudentShipSchoolFaculty, string rank, string money)
        {
            try
            {
                StudentShipSchoolStudent studentShipSchoolStudent = new StudentShipSchoolStudent();
                studentShipSchoolStudent.IdLearningOutComes = idLearningOutComes;
                studentShipSchoolStudent.IdStudentShipSchoolFaculty = idStudentShipSchoolFaculty;
                studentShipSchoolStudent.Rank = rank;
                studentShipSchoolStudent.Money = money;
                this.context.StudentShipSchoolStudents.Add(studentShipSchoolStudent);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public void SaveStudentShipSchoolStudent(
            List<LearningOutCome> listLearningOutCome,
            int idStudentShipSchoolFaculty)
        {
            percentProgress = 0;
            double i = 0;
            double count = listLearningOutCome.Count;
            this.Delete(idStudentShipSchoolFaculty);
            foreach (LearningOutCome learningOutCome in listLearningOutCome)
            {
                string money = Convert.ToDouble(learningOutCome.RankingAcademic.MoneyStudentShip) * 5 + string.Empty;
                this.Insert(
                    learningOutCome.IdLearningOutComes,
                    idStudentShipSchoolFaculty,
                    learningOutCome.RankingAcademic.NameRankingAcademic,
                    money);
                i++;
                percentProgress = (int)(((double)i / count) * 100);
            }
        }

        public void SaveStudentShipSchoolStudentCLC(
            List<LearningOutCome> listLearningOutCome,
            int idStudentShipSchoolFaculty)
        {
            percentProgress = 0;
            double i = 1;
            double count = listLearningOutCome.Count;
            this.Delete(idStudentShipSchoolFaculty);
            StudentShipSchoolFaculty studentShipSchoolFaculty =
                new StudentShipSchoolFacultyBo().GetById(idStudentShipSchoolFaculty);
            foreach (LearningOutCome learningOutCome in listLearningOutCome)
            {
                string money = string.Empty;
                double tuitionFee = Convert.ToDouble(studentShipSchoolFaculty.TuitionFee);
                if (learningOutCome.IdRankingAcademic == "suatsac" || learningOutCome.IdRankingAcademic == "gioi")
                {
                    if (i == 1)
                    {
                        StudentShipCLC top1 = this.context.StudentShipCLCs.Single(
                            s => s.idStudentShipCLC.Equals("top1"));
                        money = (tuitionFee * top1.Percent) + string.Empty;
                    }
                    else
                    {
                        StudentShipCLC top2 = this.context.StudentShipCLCs.Single(
                            s => s.idStudentShipCLC.Equals("top2"));
                        money = (tuitionFee * top2.Percent) + string.Empty;
                    }
                }
                else
                {
                    money = (Convert.ToDouble(learningOutCome.RankingAcademic.MoneyStudentShip) * 5) + string.Empty;
                }

                this.Insert(
                    learningOutCome.IdLearningOutComes,
                    idStudentShipSchoolFaculty,
                    learningOutCome.RankingAcademic.NameRankingAcademic,
                    money);

                percentProgress = (int)(((double)i / count) * 100);
                i++;
            }
        }

        public bool Update(int idStudentShipSchoolStudent, string rank, string money)
        {
            try
            {
                StudentShipSchoolStudent studentShipSchoolStudent =
                    this.context.StudentShipSchoolStudents.Single(
                        s => s.IdStudentshipSchoolStudent == idStudentShipSchoolStudent);
                studentShipSchoolStudent.Rank = rank;
                studentShipSchoolStudent.Money = Convert.ToDouble(money) * 5 + string.Empty;
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }
    }
}