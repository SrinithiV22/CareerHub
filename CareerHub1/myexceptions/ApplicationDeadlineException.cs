using System;

namespace CareerHub.MyExceptions
{
    public class ApplicationDeadlineException : Exception
    {
        public ApplicationDeadlineException(string message) : base(message) { }
    }
}

