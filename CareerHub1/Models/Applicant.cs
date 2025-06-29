using System;
using CareerHub.Database;
using System.ComponentModel.DataAnnotations;

namespace CareerHub.Models
{
    public class Applicant
    {
        [Key]
        public int ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }

        // Method to create applicant profile
        public void CreateProfile(string email, string firstName, string lastName, string phone, string resume)
        {
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.Resume = resume;

            DatabaseManager db = new DatabaseManager();
            db.InsertApplicant(this);
            Console.WriteLine("Applicant profile created successfully.");
        }

        // Method to apply for a specific job
        public void ApplyForJob(int jobID, string coverLetter)
        {
            DatabaseManager db = new DatabaseManager();
            JobApplication application = new JobApplication
            {
                JobID = jobID,
                ApplicantID = this.ApplicantID,
                ApplicationDate = DateTime.Now,
                CoverLetter = coverLetter
            };
            db.SubmitJobApplication(application);

            Console.WriteLine("Applied for the job successfully.");
        }
    }
}
