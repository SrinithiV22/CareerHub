using System;

namespace CareerHub.MyExceptions
{
    public class InvalidEmailFormatException : Exception
    {
        public InvalidEmailFormatException(string message) : base(message) { }
    }
}

