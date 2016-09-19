namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class WardBo
    {
        readonly DSAContext context = new DSAContext();

        public List<Ward> GetListWard(string idDistrict)
        {
            return this.context.Wards.Where(m => m.IdDistrict.Equals(idDistrict)).ToList();
        }

        public string GetOptionWard(string idDistrict)
        {
            List<Ward> listWard = this.context.Wards.Where(m => m.IdDistrict.Equals(idDistrict)).ToList();
            string stringOption = string.Empty;
            foreach (Ward ward in listWard)
            {
                stringOption += "<option value=" + ward.IdWard + ">" + ward.WardName + "<option>";
            }

            return stringOption;
        }
    }
}