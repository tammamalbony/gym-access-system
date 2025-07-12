
// =============================
// File: Models/Member.cs
// =============================
namespace Gym.Api.Models;

public class Member
{
    public long MemberId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName  { get; set; } = string.Empty;
    public string Email     { get; set; } = string.Empty;
    public string? Phone    { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? IdScanPath { get; set; }
    public bool KycComplete { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
