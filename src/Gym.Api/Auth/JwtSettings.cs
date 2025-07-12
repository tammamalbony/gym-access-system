// Auth/JwtSettings.cs
namespace Gym.Api.Auth;

public class JwtSettings
{
    public required string Key { get; init; }  // 32-byte secret
    public required string Issuer { get; init; }  // e.g. "GymAccess"
    public required string Audience { get; init; }  // e.g. "GymAccessUsers"

    public int AccessTokenMinutes { get; init; } = 60;  // lifetime of access token
    public int RefreshTokenDays { get; init; } = 7;   // lifetime of refresh token
}
