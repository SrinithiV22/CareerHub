using System;

namespace CareerHub.MyExceptions
{
    public class FileUploadException : Exception
    {
        public FileUploadException(string message) : base(message) { }
    }
}

