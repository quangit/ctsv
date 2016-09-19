namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.Support;

    public class RankingAcademicBo
    {
        readonly DSAContext context = new DSAContext();

        public List<RankingAcademic> Get()
        {
            return this.context.RankingAcademics.ToList();
        }

        public bool Update(FormCollection form)
        {
            try
            {
                RankingAcademic rankingAcademic =
                    this.context.RankingAcademics.Single(r => r.IdRankingAcademic.Equals("suatsac"));
                rankingAcademic.MoneyStudentShip = ConvertObject.ConvertCurrencyToString(form["MoneyExcellent"]);

                RankingAcademic rankingAcademic2 =
                    this.context.RankingAcademics.Single(r => r.IdRankingAcademic.Equals("gioi"));
                rankingAcademic2.MoneyStudentShip = ConvertObject.ConvertCurrencyToString(form["MoneyVeryGood"]);

                RankingAcademic rankingAcademic3 =
                    this.context.RankingAcademics.Single(r => r.IdRankingAcademic.Equals("kha"));
                rankingAcademic3.MoneyStudentShip = ConvertObject.ConvertCurrencyToString(form["MoneyGood"]);

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