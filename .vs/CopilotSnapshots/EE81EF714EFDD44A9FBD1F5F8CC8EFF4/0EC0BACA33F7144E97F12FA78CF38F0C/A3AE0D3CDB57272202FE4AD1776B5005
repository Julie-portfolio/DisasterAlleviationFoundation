using System;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationApp.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [StringLength(500)]
        public string Skills { get; set; } = "";

        [StringLength(100)]
        public string Availability { get; set; } = ""; // e.g., Weekdays, Weekends
    }
}
