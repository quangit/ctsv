namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class StateBo
    {
        readonly DSAContext context = new DSAContext();

        public List<State> GetListState()
        {
            return this.context.States.ToList();
        }
    }
}