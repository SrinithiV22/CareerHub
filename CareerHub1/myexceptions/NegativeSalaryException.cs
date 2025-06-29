using System;

namespace CareerHub.MyExceptions
{
    public class NegativeSalaryException : Exception
    {
        public NegativeSalaryException(string message) : base(message) { }
    }
}

