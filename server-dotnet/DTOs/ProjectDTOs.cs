using System.ComponentModel.DataAnnotations;

namespace BugTracker.API.DTOs;

public class CreateProjectRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "Active";
}

public class UpdateProjectRequest
{
    [MaxLength(200)]
    public string? Name { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? Status { get; set; }
}

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public UserDto CreatedBy { get; set; } = null!;
    public List<ProjectMemberDto> Members { get; set; } = new List<ProjectMemberDto>();
    public int BugCount { get; set; }
}

public class ProjectListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserDto CreatedBy { get; set; } = null!;
    public int BugCount { get; set; }
    public int MemberCount { get; set; }
}

public class ProjectMemberDto
{
    public string UserId { get; set; } = string.Empty;
    public UserDto User { get; set; } = null!;
    public string Role { get; set; } = string.Empty;
    public DateTime JoinedAt { get; set; }
}

public class AddProjectMemberRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = "Member";
}

public class UpdateProjectMemberRequest
{
    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = string.Empty;
} 