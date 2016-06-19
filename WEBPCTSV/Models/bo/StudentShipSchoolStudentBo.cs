using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class StudentShipSchoolStudentBo
    {
        DSAContext context = new DSAContext();
        public static int percentProgress;



        public void SaveStudentShipSchoolStudent(List<LearningOutCome>listLearningOutCome,int idStudentShipSchoolFaculty)
        {
            percentProgress = 0;
            double i = 0;
            double count = listLearningOutCome.Count;
            Delete(idStudentShipSchoolFaculty);
            foreach(LearningOutCome learningOutCome in listLearningOutCome)
            {
                string money = Convert.ToDouble(learningOutCome.RankingAcademic.MoneyStudentShip) * 5+"";
                Insert(learningOutCome.IdLearningOutComes, idStudentShipSchoolFaculty, learningOutCome.RankingAcademic.NameRankingAcademic, money);
                i++;
                percentProgress = (int)(((double)i / count) * 100);
            }
        }
        public void SaveStudentShipSchoolStudentCLC(List<LearningOutCome> listLearningOutCome, int idStudentShipSchoolFaculty)
        {
            percentProgress = 0;
            double i = 1;
            double count = listLearningOutCome.Count;
            Delete(idStudentShipSchoolFaculty);
            StudentShipSchoolFaculty studentShipSchoolFaculty = new StudentShipSchoolFacultyBo().GetById(idStudentShipSchoolFaculty);
            foreach (LearningOutCome learningOutCome in listLearningOutCome)
            {
                string money="";
                double tuitionFee = Convert.ToDouble(studentShipSchoolFaculty.TuitionFee);
                if (learningOutCome.IdRankingAcademic == "suatsac" || learningOutCome.IdRankingAcademic == "gioi")
                {
                    if (i == 1)
                    {
                        StudentShipCLC top1 = context.StudentShipCLCs.Single(s => s.idStudentShipCLC.Equals("top1"));
                        money = (tuitionFee * top1.Percent)+"";
                    }
                    else
                    {
                        StudentShipCLC top2 = context.StudentShipCLCs.Single(s => s.idStudentShipCLC.Equals("top2"));
                        money = (tuitionFee * top2.Percent) + "";
                    }
                }
                else
                {
                    money=(Convert.ToDouble(learningOutCome.RankingAcademic.MoneyStudentShip) * 5)+"";
                }
                Insert(learningOutCome.IdLearningOutComes, idStudentShipSchoolFaculty, learningOutCome.RankingAcademic.NameRankingAcademic, money);
                
                percentProgress = (int)(((double)i / count) * 100);
                i++;
            }
        }
        public bool Insert(int idLearningOutComes, int idStudentShipSchoolFaculty, string rank, string money)
        {
            try
            {
                StudentShipSchoolStudent studentShipSchoolStudent = new StudentShipSchoolStudent();
                studentShipSchoolStudent.IdLearningOutComes=idLearningOutComes;
                studentShipSchoolStudent.IdStudentShipSchoolFaculty=idStudentShipSchoolFaculty;
                studentShipSchoolStudent.Rank = rank ;
                studentShipSchoolStudent.Money=money;
                context.StudentShipSchoolStudents.Add(studentShipSchoolStudent);
                context.SaveChanges();
                return true;
            }
            catch {  }
            return false;
        }

        public bool Update(int idStudentShipSchoolStudent, string rank, string money)
        {
            try
            {
                StudentShipSchoolStudent studentShipSchoolStudent = context.StudentShipSchoolStudents.Single(s => s.IdStudentshipSchoolStudent==idStudentShipSchoolStudent);
                studentShipSchoolStudent.Rank = rank;
                studentShipSchoolStudent.Money = Convert.ToDouble(money) * 5 + "";
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }
        public  void Delete(int idStudentShipSchoolFaculty)
        {
            List<StudentShipSchoolStudent> listStudentShipSchoolStudent = context.StudentShipSchoolStudents.Where(s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty).ToList();
            context.StudentShipSchoolStudents.RemoveRange(listStudentShipSchoolStudent);
            context.SaveChanges();
        }

        public StudentShipSchoolStudent Get(int idLearningOutComes, int idStudentShipSchoolFaculty)
        {
            try
            {
                return context.StudentShipSchoolStudents.Single(s => s.IdLearningOutComes == idLearningOutComes && s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
            }
            catch { }
            return null;
        }

        public List<StudentShipSchoolStudent> GetList(int idStudentShipSchoolFaculty)
        {
            try
            {
                return context.StudentShipSchoolStudents.Where(s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty).ToList();
            }
            catch { }
            return null;
        }


    }
}