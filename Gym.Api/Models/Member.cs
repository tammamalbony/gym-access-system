
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
}