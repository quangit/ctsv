namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class ProvinceBo
    {
        readonly DSAContext context = new DSAContext();

        public List<Province> GetListCourse(string idState)
        {
            return this.context.Provinces.Where(p => p.IdState.Equals(idState)).ToList();
        }
    }
}