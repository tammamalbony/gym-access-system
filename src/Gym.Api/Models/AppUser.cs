
// =============================
// File: Models/AppUser.cs
// =============================
namespace Gym.Api.Models;

public enum UserRole
{
    DataEntry,
    Admin,
    Support
}

public class AppUser
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
    public DateTime CreatedAt { get; set; }
}
