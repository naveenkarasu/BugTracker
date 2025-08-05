using System.ComponentModel.DataAnnotations;

namespace BugTracker.API.DTOs;

public class CreateBugRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Priority { get; set; } = "Medium";

    [Required]
    [MaxLength(50)]
    public string Severity { get; set; } = "Medium";

    [MaxLength(50)]
    public string? Type { get; set; }

    [Required]
    public int ProjectId { get; set; }

    public string? AssignedToId { get; set; }
}

public class UpdateBugRequest
{
    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? Status { get; set; }

    [MaxLength(50)]
    public string? Priority { get; set; }

    [MaxLength(50)]
    public string? Severity { get; set; }

    [MaxLength(50)]
    public string? Type { get; set; }

    public string? AssignedToId { get; set; }
}

public class BugDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string? Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public ProjectDto Project { get; set; } = null!;
    public UserDto ReportedBy { get; set; } = null!;
    public UserDto? AssignedTo { get; set; }
    public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    public List<BugHistoryDto> History { get; set; } = new List<BugHistoryDto>();
}

public class BugListDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserDto ReportedBy { get; set; } = null!;
    public UserDto? AssignedTo { get; set; }
    public string ProjectName { get; set; } = string.Empty;
}

public class BugFilterRequest
{
    public int? ProjectId { get; set; }
    public string? Status { get; set; }
    public string? Priority { get; set; }
    public string? Severity { get; set; }
    public string? AssignedToId { get; set; }
    public string? ReportedById { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? SortBy { get; set; } = "CreatedAt";
    public bool SortDescending { get; set; } = true;
} 