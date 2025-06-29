using System;
using System.Text.RegularExpressions;
using CareerHub.MyExceptions;

namespace CareerHub.ExceptionPrograms
{
    public class EmailValidation
    {
        public void ValidateEmail()
        {
            try
            {
                Console.WriteLine("Enter your email address:");
                string email = Console.ReadLine();

                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (!Regex.IsMatch(email, pattern))
                    throw new InvalidEmailFormatException("Invalid email format!");

                Console.WriteLine("Email is valid. Registration can proceed.");
            }
            catch (InvalidEmailFormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

