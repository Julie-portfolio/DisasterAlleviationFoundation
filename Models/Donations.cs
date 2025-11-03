using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationApp.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        public string Category { get; set; } = ""; // e.g., Food, Clothing, Medical, Money

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public string? DonorName { get; set; }

        [EmailAddress]
        public string? DonorEmail { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; } = "pending"; // pending or distributed
    }
}
