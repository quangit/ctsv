using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class StudentShipCLCBo
    {
        DSAContext context = new DSAContext();
        public List<StudentShipCLC> Get()
        {
            return context.StudentShipCLCs.ToList();
        }
        public StudentShipCLC GetTop1()
        {
            return context.StudentShipCLCs.Single(s => s.idStudentShipCLC.Equals("top1"));
        }
        public StudentShipCLC GetTop2()
        {
            return context.StudentShipCLCs.Single(s => s.idStudentShipCLC.Equals("top2"));
        }
    }
}