namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class ReligionBo
    {
        readonly DSAContext context = new DSAContext();

        public List<Religion> GetReligion()
        {
            return this.context.Religions.ToList();
        }
    }
}