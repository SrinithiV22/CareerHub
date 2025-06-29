using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CareerHub.Models;

namespace CareerHub.Database
{
    public class DatabaseManager
    {
        private string connectionString = "Data Source=LAPTOP-TCFN1CUA;Initial Catalog=careerhub;Integrated Security=True;TrustServerCertificate=True";

        // Initialize Database: Create Tables if not exist
        public void InitializeDatabase()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string createCompanies = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='companies' AND xtype='U')
                CREATE TABLE companies (
                    companyid INT PRIMARY KEY IDENTITY(1,1),  -- Add IDENTITY here
                    companyname VARCHAR(50),
                    location VARCHAR(100)
                );";


                string createJobs = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='jobs' AND xtype='U')
                CREATE TABLE jobs (
                    jobid INT PRIMARY KEY IDENTITY(1,1),
                    companyid INT FOREIGN KEY REFERENCES companies(companyid),
                    jobtitle VARCHAR(150),
                    jobdescription VARCHAR(250),
                    joblocation VARCHAR(50),
                    salary DECIMAL(10,2),
                    jobtype VARCHAR(50),
                    posteddate DATETIME
                );";

                string createApplicants = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='applicants' AND xtype='U')
                CREATE TABLE applicants (
                    applicantid INT PRIMARY KEY IDENTITY(1,1),
                    fname VARCHAR(255),
                    lname VARCHAR(255),
                    email VARCHAR(255) UNIQUE,
                    phone VARCHAR(20),
                    resume VARCHAR(255)
                );";

                string createApplications = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='applications' AND xtype='U')
                CREATE TABLE applications (
                    applicationid INT PRIMARY KEY IDENTITY(1,1),
                    jobid INT FOREIGN KEY REFERENCES jobs(jobid),
                    applicantid INT FOREIGN KEY REFERENCES applicants(applicantid),
                    applicationdate DATETIME,
                    coverletter VARCHAR(255)
                );";

                SqlCommand cmd1 = new SqlCommand(createCompanies, con);
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand(createJobs, con);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand(createApplicants, con);
                cmd3.ExecuteNonQuery();

                SqlCommand cmd4 = new SqlCommand(createApplications, con);
                cmd4.ExecuteNonQuery();

                Console.WriteLine("Database initialized successfully.");
            }
        }

        // Insert Company with ID check
        public void InsertCompany(Company company)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if the company ID already exists
                string checkQuery = "SELECT COUNT(*) FROM companies WHERE companyid = @CompanyID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@CompanyID", company.CompanyID);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0) // ID does not exist, safe to insert
                {
                    string query = "INSERT INTO companies (companyid, companyname, location) VALUES (@CompanyID, @CompanyName, @Location)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CompanyID", company.CompanyID);
                    cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                    cmd.Parameters.AddWithValue("@Location", company.Location);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Company inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Company ID already exists. Please use a different ID.");
                }
            }
        }

        // Insert Applicant
        public void InsertApplicant(Applicant applicant)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if the applicant ID already exists
                string checkQuery = "SELECT COUNT(*) FROM applicants WHERE applicantid = @ApplicantID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@ApplicantID", applicant.ApplicantID);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    string query = "INSERT INTO applicants (applicantid, fname, lname, email, phone, resume) VALUES (@ApplicantID, @FName, @LName, @Email, @Phone, @Resume)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ApplicantID", applicant.ApplicantID);
                    cmd.Parameters.AddWithValue("@FName", applicant.FirstName);
                    cmd.Parameters.AddWithValue("@LName", applicant.LastName);
                    cmd.Parameters.AddWithValue("@Email", applicant.Email);
                    cmd.Parameters.AddWithValue("@Phone", applicant.Phone);
                    cmd.Parameters.AddWithValue("@Resume", applicant.Resume);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Applicant inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Applicant ID already exists. Please use a different ID.");
                }
            }
        }


        // Insert Job Application
        public void InsertJobListing(JobListing job)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if the job ID already exists
                string checkQuery = "SELECT COUNT(*) FROM jobs WHERE jobid = @JobID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@JobID", job.JobID);

                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    string query = @"INSERT INTO jobs (jobid, companyid, jobtitle, jobdescription, joblocation, salary, jobtype, posteddate) 
                             VALUES (@JobID, @CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@JobID", job.JobID);
                    cmd.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                    cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                    cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                    cmd.Parameters.AddWithValue("@Salary", job.Salary);
                    cmd.Parameters.AddWithValue("@JobType", job.JobType);
                    cmd.Parameters.AddWithValue("@PostedDate", job.PostedDate);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Job listing inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Job ID already exists. Please use a different ID.");
                }
            }
        }

        // Get All Job Listings
        public List<JobListing> GetJobListings()
        {
            List<JobListing> jobs = new List<JobListing>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM jobs";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jobs.Add(new JobListing
                    {
                        JobID = Convert.ToInt32(reader["jobid"]),
                        CompanyID = Convert.ToInt32(reader["companyid"]),
                        JobTitle = reader["jobtitle"].ToString(),
                        JobDescription = reader["jobdescription"].ToString(),
                        JobLocation = reader["joblocation"].ToString(),
                        Salary = Convert.ToDecimal(reader["salary"]),
                        JobType = reader["jobtype"].ToString(),
                        PostedDate = Convert.ToDateTime(reader["posteddate"])
                    });
                }
            }
            return jobs;
        }

        // Get All Companies
        public List<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM companies";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    companies.Add(new Company
                    {
                        CompanyID = Convert.ToInt32(reader["companyid"]),
                        CompanyName = reader["companyname"].ToString(),
                        Location = reader["location"].ToString()
                    });
                }
            }
            return companies;
        }

        // Get All Applicants
        public List<Applicant> GetApplicants()
        {
            List<Applicant> applicants = new List<Applicant>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM applicants";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    applicants.Add(new Applicant
                    {
                        ApplicantID = Convert.ToInt32(reader["applicantid"]),
                        FirstName = reader["fname"].ToString(),
                        LastName = reader["lname"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Resume = reader["resume"].ToString()
                    });
                }
            }
            return applicants;
        }

        // Get Applications for a Specific Job
        public List<JobApplication> GetApplicationsForJob(int jobID)
        {
            List<JobApplication> applications = new List<JobApplication>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM applications WHERE jobid = @JobID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@JobID", jobID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    applications.Add(new JobApplication
                    {
                        ApplicationID = Convert.ToInt32(reader["applicationid"]),
                        JobID = Convert.ToInt32(reader["jobid"]),
                        ApplicantID = Convert.ToInt32(reader["applicantid"]),
                        ApplicationDate = Convert.ToDateTime(reader["applicationdate"]),
                        CoverLetter = reader["coverletter"].ToString()
                    });
                }
            }
            return applications;
        }

        // Get Jobs by Company
        public List<JobListing> GetJobsByCompany(int companyID)
        {
            List<JobListing> jobs = new List<JobListing>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM jobs WHERE companyid = @CompanyID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CompanyID", companyID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jobs.Add(new JobListing
                    {
                        JobID = Convert.ToInt32(reader["jobid"]),
                        CompanyID = Convert.ToInt32(reader["companyid"]),
                        JobTitle = reader["jobtitle"].ToString(),
                        JobDescription = reader["jobdescription"].ToString(),
                        JobLocation = reader["joblocation"].ToString(),
                        Salary = Convert.ToDecimal(reader["salary"]),
                        JobType = reader["jobtype"].ToString(),
                        PostedDate = Convert.ToDateTime(reader["posteddate"])
                    });
                }
            }
            return jobs;
        }

        internal List<Applicant> GetApplicants(int jobID)
        {
            throw new NotImplementedException();
        }

        public void RetrieveJobListings()
        {
            using (var context = new CareerHubContext())
            {
                var jobs = context.Jobs
                    .Join(context.Companies,
                          job => job.CompanyID,
                          company => company.CompanyID,
                          (job, company) => new
                          {
                              job.JobTitle,
                              CompanyName = company.CompanyName,
                              job.Salary
                          }).ToList();

                Console.WriteLine("Job Listings:");
                foreach (var job in jobs)
                {
                    Console.WriteLine($"Job Title: {job.JobTitle}, Company: {job.CompanyName}, Salary: {job.Salary}");
                }
            }
        }
        public void CreateApplicantProfile(Applicant applicant)
        {
            try
            {
                using (var context = new CareerHubContext())
                {
                    context.Applicants.Add(applicant);
                    context.SaveChanges();
                    Console.WriteLine("Applicant profile created successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating profile: {ex.Message}");
            }
        }

        public void SubmitJobApplication(JobApplication application)
        {
            try
            {
                using (var context = new CareerHubContext())
                {
                    context.Applications.Add(application);
                    context.SaveChanges();
                    Console.WriteLine("Job application submitted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error submitting job application: {ex.Message}");
            }
        }
        public void PostCompanyJob(JobListing job)
        {
            try
            {
                using (var context = new CareerHubContext())
                {
                    context.Jobs.Add(job);
                    context.SaveChanges();
                    Console.WriteLine("Job posted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting job: {ex.Message}");
            }
        }

        public void GetJobsBySalaryRange(decimal minSalary, decimal maxSalary)
        {
            try
            {
                using (var context = new CareerHubContext())
                {
                    var jobs = context.Jobs
                        .Where(job => job.Salary >= minSalary && job.Salary <= maxSalary)
                        .Join(context.Companies,
                              job => job.CompanyID,
                              company => company.CompanyID,
                              (job, company) => new
                              {
                                  job.JobTitle,
                                  CompanyName = company.CompanyName,
                                  job.Salary
                              }).ToList();

                    Console.WriteLine("Jobs within Salary Range:");
                    foreach (var job in jobs)
                    {
                        Console.WriteLine($"Job Title: {job.JobTitle}, Company: {job.CompanyName}, Salary: {job.Salary}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving jobs: {ex.Message}");
            }
        }




    }
}

