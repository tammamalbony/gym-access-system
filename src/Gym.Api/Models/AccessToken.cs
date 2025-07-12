// =============================
// File: Models/AccessToken.cs
// =============================
namespace Gym.Api.Models;

public class AccessToken
{
    public long TokenId { get; set; }
    public long MemberId { get; set; }
    public string TokenValue { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool Revoked { get; set; }
}
