using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class DistrictBo
    {
        DSAContext context = new DSAContext();
        public List<District> GetListDistrict(string idProvince)
        {
            return context.Districts.Where(d=>d.IdProvince.Equals(idProvince)).ToList();
        }

        public String GetOptionDistrict(string idProvince)
        {
            List<District>listDistrict = context.Districts.Where(d => d.IdProvince.Equals(idProvince)).ToList();
            string stringOption = "<option value='0'>Chọn huyện<option>";
            foreach(District district in listDistrict)
            {
                stringOption += "<option value=" + district.IdDistrict + ">" + district.DistrictName + "<option>";
            }
            return stringOption;
        }
    }
}