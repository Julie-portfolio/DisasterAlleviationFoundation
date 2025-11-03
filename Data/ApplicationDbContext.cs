using DisasterAlleviationApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Donation> Donations { get; set; } = default!;
    public DbSet<Disaster> Disasters { get; set; } = default!;
    public DbSet<Volunteer> Volunteers { get; set; } = default!;
    public DbSet<TaskAssignment> TaskAssignments { get; set; } = default!;
    public DbSet<AuditLog> AuditLogs { get; set; } = default!;
}
