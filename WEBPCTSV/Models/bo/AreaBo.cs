namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class AreaBo
    {
        readonly DSAContext context = new DSAContext();

        public List<Area> GetListArea()
        {
            return this.context.Areas.ToList();
        }
    }
}