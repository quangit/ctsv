namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class MadeOfStudyBo
    {
        readonly DSAContext context = new DSAContext();

        public List<MadeOfStudy> GetListMadeOfStudy()
        {
            return this.context.MadeOfStudies.ToList();
        }
    }
}