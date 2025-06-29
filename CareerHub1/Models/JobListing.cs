using System;
using System.Collections.Generic;
using CareerHub.Database;
using System.ComponentModel.DataAnnotations;
namespace CareerHub.Models
{
    public class JobListing
    {
        [Key]
        public int JobID { get; set; }
        public int CompanyID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }

        // Method to allow applicants to apply for the job
        public void Apply(int applicantID, string coverLetter)
        {
            DatabaseManager db = new DatabaseManager();
            JobApplication application = new JobApplication
            {
                JobID = this.JobID,
                ApplicantID = applicantID,
                ApplicationDate = DateTime.Now,
                CoverLetter = coverLetter
            };
            db.SubmitJobApplication(application);

            Console.WriteLine("Application submitted successfully.");
        }

        // Method to retrieve applicants for this job
        public List<Applicant> GetApplicants()
        {
            DatabaseManager db = new DatabaseManager();
            return db.GetApplicants(this.JobID);
        }
    }
}


