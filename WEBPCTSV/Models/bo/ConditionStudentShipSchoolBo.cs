namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;

    public class ConditionStudentShipSchoolBo
    {
        readonly DSAContext context = new DSAContext();

        public ConditionStudentShipSchool Get(int idStudentShipSchoolFaculty)
        {
            StudentShipSchoolFaculty studentShipSchoolFaculty =
                this.context.StudentShipSchoolFaculties.Single(
                    s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
            return
                this.context.ConditionStudentShipSchools.Single(
                    c => c.IdStudentShipSchool == studentShipSchoolFaculty.IdStudentShipSchool);
        }

        public bool InsertByDefault(int idStudentShipSchool)
        {
            try
            {
                ConditionStudentShipSchool conditionStudentShipSchool = new ConditionStudentShipSchool();
                conditionStudentShipSchool.IsSchedule = true;
                conditionStudentShipSchool.pointTraining = 70;
                conditionStudentShipSchool.pointsShipSchool = 7;
                conditionStudentShipSchool.relearnCreditsNumber = 0;
                conditionStudentShipSchool.CreditNumberStudy1 = 14;
                conditionStudentShipSchool.creditNumberUnQualified = 0;
                conditionStudentShipSchool.IdStudentShipSchool = idStudentShipSchool;
                this.context.ConditionStudentShipSchools.Add(conditionStudentShipSchool);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public bool Update(int idStudentShipSchoolFaculty, FormCollection form)
        {
            try
            {
                StudentShipSchoolFaculty studentShipSchoolFaculty =
                    this.context.StudentShipSchoolFaculties.Single(
                        s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
                ConditionStudentShipSchool conditionStudentShipSchool =
                    this.context.ConditionStudentShipSchools.Single(
                        c => c.IdStudentShipSchool == studentShipSchoolFaculty.IdStudentShipSchool);
                conditionStudentShipSchool.IsSchedule = true;
                conditionStudentShipSchool.pointTraining = Convert.ToDouble(form["PointTraining"]);
                conditionStudentShipSchool.pointsShipSchool = Convert.ToDouble(form["PointsShipSchool"]);
                conditionStudentShipSchool.relearnCreditsNumber = Convert.ToDouble(form["RelearnCreditsNumber"]);
                conditionStudentShipSchool.CreditNumberStudy1 = Convert.ToDouble(form["CreditNumber1"]);
                conditionStudentShipSchool.creditNumberUnQualified = Convert.ToDouble(form["CreditNumberUnQualified"]);
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