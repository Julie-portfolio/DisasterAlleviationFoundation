using System;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationApp.Models
{
    public class Disaster
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = "";

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = "";

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = "";

        [Display(Name = "Date/Time")]
        [Required]
        public DateTime OccurredAt { get; set; }

        [Required]
        [StringLength(50)]
        public string Severity { get; set; } = ""; // Low, Medium, High, Critical

        public string? EvidencePath { get; set; }
        public string? UserId { get; set; } // IdentityUser Id
    }
}
