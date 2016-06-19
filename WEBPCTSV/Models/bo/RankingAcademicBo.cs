using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.Support;

namespace WEBPCTSV.Models.bo
{
    public class RankingAcademicBo
    {
        DSAContext context = new DSAContext();

        public List<RankingAcademic> Get()
        {
            return context.RankingAcademics.ToList();
        }

        public bool Update(FormCollection form)
        {
            try {
                RankingAcademic rankingAcademic = context.RankingAcademics.Single(r => r.IdRankingAcademic.Equals("suatsac"));
                rankingAcademic.MoneyStudentShip = ConvertObject.ConvertCurrencyToString(form["MoneyExcellent"]);

                RankingAcademic rankingAcademic2 = context.RankingAcademics.Single(r => r.IdRankingAcademic.Equals("gioi"));
                rankingAcademic2.MoneyStudentShip = ConvertObject.ConvertCurrencyToString(form["MoneyVeryGood"]);

                RankingAcademic rankingAcademic3 = context.RankingAcademics.Single(r => r.IdRankingAcademic.Equals("kha"));
                rankingAcademic3.MoneyStudentShip = ConvertObject.ConvertCurrencyToString(form["MoneyGood"]);

                context.SaveChanges();
                return true;

            }
            catch { }
            return false;
        }
    }
}