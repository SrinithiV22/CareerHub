using System;
using System.Collections.Generic;
using CareerHub.MyExceptions;

namespace CareerHub.ExceptionPrograms
{
    public class SalaryValidation
    {
        public void CalculateAverageSalary(List<decimal> salaries)
        {
            try
            {
                decimal totalSalary = 0;

                foreach (var salary in salaries)
                {
                    if (salary < 0)
                        throw new NegativeSalaryException("Negative salary detected!");

                    totalSalary += salary;
                }

                decimal average = totalSalary / salaries.Count;
                Console.WriteLine($"Average Salary: {average}");
            }
            catch (NegativeSalaryException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

