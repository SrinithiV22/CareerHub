using System;
using System.Collections.Generic;
using CareerHub.Database;
using System.ComponentModel.DataAnnotations;

namespace CareerHub.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }

        // Method to post a new job listing
        public void PostJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)
        {
            DatabaseManager db = new DatabaseManager();
            JobListing job = new JobListing
            {
                CompanyID = this.CompanyID,
                JobTitle = jobTitle,
                JobDescription = jobDescription,
                JobLocation = jobLocation,
                Salary = salary,
                JobType = jobType,
                PostedDate = DateTime.Now
            };
            db.InsertJobListing(job);
            Console.WriteLine("Job posted successfully.");
        }

        // Method to get all jobs posted by this company
        public List<JobListing> GetJobs()
        {
            DatabaseManager db = new DatabaseManager();
            return db.GetJobsByCompany(this.CompanyID);
        }
    }
}

