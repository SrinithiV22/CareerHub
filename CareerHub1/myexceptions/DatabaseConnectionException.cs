using System;

namespace CareerHub.MyExceptions
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException(string message) : base(message) { }
    }
}
