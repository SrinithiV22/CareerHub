using System;
using CareerHub.MyExceptions;

namespace CareerHub.ExceptionPrograms
{
    public class DeadlineChecker
    {
        public void CheckDeadline(DateTime deadline)
        {
            try
            {
                Console.WriteLine("Enter application date (yyyy-mm-dd):");
                DateTime applicationDate = DateTime.Parse(Console.ReadLine());

                if (applicationDate > deadline)
                    throw new ApplicationDeadlineException("Application deadline has passed.");

                Console.WriteLine("Application submitted successfully.");
            }
            catch (ApplicationDeadlineException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid date format.");
            }
        }
    }
}
