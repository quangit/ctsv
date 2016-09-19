namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class TypeInputBo
    {
        readonly DSAContext context = new DSAContext();

        public List<TypeInput> GetListTypeInput()
        {
            return this.context.TypeInputs.ToList();
        }
    }
}