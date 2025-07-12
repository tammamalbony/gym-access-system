
// =============================
// File: Models/AppUser.cs
// =============================
namespace Gym.Api.Models;

public class AppUser
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // ADMIN | DATA_ENTRY | SUPPORT
}
