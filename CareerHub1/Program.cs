using System;
using CareerHub.Database;
using CareerHub.Models;

namespace CareerHub1
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager dbManager = new DatabaseManager();

            // Step 1: Initialize Database
            dbManager.InitializeDatabase();

            // Step 2: Insert a Company
            Company company = new Company
            {
                CompanyID = 1,
                CompanyName = "Google",
                Location = "Bangalore"
            };
            dbManager.InsertCompany(company);

            // Step 3: Insert a Job Listing
            JobListing job = new JobListing
            {
                CompanyID = 1, // must match an existing company ID
                JobTitle = "Software Engineer",
                JobDescription = "Develop and maintain software solutions.",
                JobLocation = "Chennai",
                Salary = 75000,
                JobType = "Full-Time",
                PostedDate = DateTime.Now
            };
            dbManager.InsertJobListing(job);

            // Step 4: Insert an Applicant
            Applicant applicant = new Applicant
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "9876543210",
                Resume = "resume.pdf"
            };
            dbManager.InsertApplicant(applicant);

            // Step 5: Insert a Job Application
            JobApplication application = new JobApplication
            {
                JobID = 1, // existing job ID
                ApplicantID = 1, // existing applicant ID
                ApplicationDate = DateTime.Now,
                CoverLetter = "Excited to apply!"
            };
            dbManager.SubmitJobApplication(application);

            // Step 6: Retrieve and Display Job Listings
            dbManager.RetrieveJobListings();

            // Step 7: Test Applicant Profile Creation (Entity Framework)
            Applicant newApplicant = new Applicant
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Phone = "9123456780",
                Resume = "jane_resume.pdf"
            };
            dbManager.CreateApplicantProfile(newApplicant);

            // Step 8: Test Job Application Submission (Entity Framework)
            JobApplication newApplication = new JobApplication
            {
                JobID = 1, // existing job ID
                ApplicantID = 2, // new applicant ID
                ApplicationDate = DateTime.Now,
                CoverLetter = "Looking forward to this opportunity."
            };
            dbManager.SubmitJobApplication(newApplication);

            // Step 9: Test Company Job Posting (Entity Framework)
            JobListing newJob = new JobListing
            {
                CompanyID = 1,
                JobTitle = "Data Analyst",
                JobDescription = "Analyze data trends and insights.",
                JobLocation = "Hyderabad",
                Salary = 60000,
                JobType = "Part-Time",
                PostedDate = DateTime.Now
            };
            dbManager.PostCompanyJob(newJob);

            // Step 10: Test Salary Range Query
            dbManager.GetJobsBySalaryRange(50000, 80000);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
