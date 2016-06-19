using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class ConditionStudentShipSchoolBo
    {
        DSAContext context = new DSAContext();

        public bool InsertByDefault(int idStudentShipSchool)
        {
            try {
                ConditionStudentShipSchool conditionStudentShipSchool = new ConditionStudentShipSchool();
                conditionStudentShipSchool.IsSchedule = true;
                conditionStudentShipSchool.pointTraining = 70;
                conditionStudentShipSchool.pointsShipSchool = 7;
                conditionStudentShipSchool.relearnCreditsNumber = 0;
                conditionStudentShipSchool.CreditNumberStudy1 = 14;
                conditionStudentShipSchool.creditNumberUnQualified = 0;
                conditionStudentShipSchool.IdStudentShipSchool = idStudentShipSchool;
                context.ConditionStudentShipSchools.Add(conditionStudentShipSchool);
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
                ConditionStudentShipSchool conditionStudentShipSchool = context.ConditionStudentShipSchools.Single(c=>c.IdStudentShipSchool==studentShipSchoolFaculty.IdStudentShipSchool);
                conditionStudentShipSchool.IsSchedule = true;
                conditionStudentShipSchool.pointTraining = Convert.ToDouble(form["PointTraining"]);
                conditionStudentShipSchool.pointsShipSchool = Convert.ToDouble(form["PointsShipSchool"]);
                conditionStudentShipSchool.relearnCreditsNumber = Convert.ToDouble(form["RelearnCreditsNumber"]);
                conditionStudentShipSchool.CreditNumberStudy1 = Convert.ToDouble(form["CreditNumber1"]);
                conditionStudentShipSchool.creditNumberUnQualified = Convert.ToDouble(form["CreditNumberUnQualified"]);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public ConditionStudentShipSchool Get(int idStudentShipSchoolFaculty)
        {
            StudentShipSchoolFaculty studentShipSchoolFaculty = context.StudentShipSchoolFaculties.Single(s => s.IdStudentShipSchoolFaculty == idStudentShipSchoolFaculty);
            return context.ConditionStudentShipSchools.Single(c => c.IdStudentShipSchool == studentShipSchoolFaculty.IdStudentShipSchool);
        }


    }
}