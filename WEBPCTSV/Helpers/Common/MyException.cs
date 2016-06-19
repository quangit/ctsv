using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Helpers.Common
{
    public class MyException : Exception
    {
        public MyException()
            : base()
        {

        }
        public MyException(string message)
            : base(message)
        {

        }
    }
}