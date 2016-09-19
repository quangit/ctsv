namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class DistrictBo
    {
        readonly DSAContext context = new DSAContext();

        public List<District> GetListDistrict(string idProvince)
        {
            return this.context.Districts.Where(d => d.IdProvince.Equals(idProvince)).ToList();
        }

        public string GetOptionDistrict(string idProvince)
        {
            List<District> listDistrict = this.context.Districts.Where(d => d.IdProvince.Equals(idProvince)).ToList();
            string stringOption = "<option value='0'>Chọn huyện<option>";
            foreach (District district in listDistrict)
            {
                stringOption += "<option value=" + district.IdDistrict + ">" + district.DistrictName + "<option>";
            }

            return stringOption;
        }
    }
}