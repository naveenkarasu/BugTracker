using BugTracker.API.DTOs;
using BugTracker.API.Models;

namespace BugTracker.API.Services;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<UserDto> GetCurrentUserAsync(string userId);
    Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request);
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(string userId);
    Task<bool> UpdateUserAsync(string userId, UserDto userDto);
    Task<bool> DeleteUserAsync(string userId);
} 