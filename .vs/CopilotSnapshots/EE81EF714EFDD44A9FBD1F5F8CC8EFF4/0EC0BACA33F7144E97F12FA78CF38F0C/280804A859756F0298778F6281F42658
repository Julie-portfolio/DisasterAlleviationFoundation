using System;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationApp.Models
{
    public class TaskAssignment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string TaskName { get; set; } = "";

        [StringLength(2000)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public int? VolunteerId { get; set; }

        public DateTime? AssignedAt { get; set; }
    }
}
