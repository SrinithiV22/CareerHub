using System;
using System.ComponentModel.DataAnnotations; 
namespace CareerHub.Models
{
    public class JobApplication
    {
        [Key]
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public int ApplicantID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CoverLetter { get; set; }
    }
}
