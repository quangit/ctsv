using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class WardBo
    {
        DSAContext context = new DSAContext();
        public List<Ward> GetListWard(string idDistrict)
        {
            return context.Wards.Where(m=>m.IdDistrict.Equals(idDistrict)).ToList();
        }

        public String GetOptionWard(string idDistrict)
        {
            List<Ward> listWard = context.Wards.Where(m => m.IdDistrict.Equals(idDistrict)).ToList();
            string stringOption = "";
            foreach (Ward ward in listWard)
            {
                stringOption += "<option value=" + ward.IdWard + ">" + ward.WardName + "<option>";
            }
            return stringOption;
        }
    }
}