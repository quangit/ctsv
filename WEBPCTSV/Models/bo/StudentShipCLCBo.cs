namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class StudentShipCLCBo
    {
        readonly DSAContext context = new DSAContext();

        public List<StudentShipCLC> Get()
        {
            return this.context.StudentShipCLCs.ToList();
        }

        public StudentShipCLC GetTop1()
        {
            return this.context.StudentShipCLCs.Single(s => s.idStudentShipCLC.Equals("top1"));
        }

        public StudentShipCLC GetTop2()
        {
            return this.context.StudentShipCLCs.Single(s => s.idStudentShipCLC.Equals("top2"));
        }
    }
}