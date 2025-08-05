using System.ComponentModel.DataAnnotations;

namespace BugTracker.API.Models;

public class Project
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Active"; // Active, Completed, OnHold, Cancelled

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Foreign Keys
    public string CreatedById { get; set; } = string.Empty;
    public virtual ApplicationUser CreatedBy { get; set; } = null!;

    // Navigation properties
    public virtual ICollection<Bug> Bugs { get; set; } = new List<Bug>();
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
}

public class ProjectMember
{
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = "Member"; // Owner, Admin, Developer, Tester, Viewer

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
} 