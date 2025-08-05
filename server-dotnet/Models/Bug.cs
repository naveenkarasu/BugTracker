using System.ComponentModel.DataAnnotations;

namespace BugTracker.API.Models;

public class Bug
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Open"; // Open, InProgress, Testing, Resolved, Closed, Reopened

    [Required]
    [MaxLength(50)]
    public string Priority { get; set; } = "Medium"; // Low, Medium, High, Critical

    [Required]
    [MaxLength(50)]
    public string Severity { get; set; } = "Medium"; // Low, Medium, High, Critical

    [MaxLength(50)]
    public string? Type { get; set; } // Bug, Feature, Enhancement, Task

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    // Foreign Keys
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;

    public string ReportedById { get; set; } = string.Empty;
    public virtual ApplicationUser ReportedBy { get; set; } = null!;

    public string? AssignedToId { get; set; }
    public virtual ApplicationUser? AssignedTo { get; set; }

    // Navigation properties
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<BugHistory> History { get; set; } = new List<BugHistory>();
}

public class Comment
{
    public int Id { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Foreign Keys
    public int BugId { get; set; }
    public virtual Bug Bug { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;
}

public class BugHistory
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Field { get; set; } = string.Empty; // Status, Priority, AssignedTo, etc.

    [MaxLength(500)]
    public string? OldValue { get; set; }

    [MaxLength(500)]
    public string? NewValue { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public int BugId { get; set; }
    public virtual Bug Bug { get; set; } = null!;

    public string ChangedById { get; set; } = string.Empty;
    public virtual ApplicationUser ChangedBy { get; set; } = null!;
} 