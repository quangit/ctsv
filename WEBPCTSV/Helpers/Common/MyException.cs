namespace WEBPCTSV.Helpers.Common
{
    using System;

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