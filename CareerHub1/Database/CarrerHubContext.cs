using CareerHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace CareerHub.Database
{
    public class CareerHubContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobListing> Jobs { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<JobApplication> Applications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-TCFN1CUA;Initial Catalog=careerhub;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}

