// =============================
// File: Auth/TokenService.cs
// =============================
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gym.Api.Auth;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
}

public class TokenService(ILogger<TokenService> log, IOptions<JwtSettings> options)
    : ITokenService
{
    private readonly JwtSettings _cfg = options.Value;
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key   = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_cfg.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _cfg.Issuer,
            audience: _cfg.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_cfg.AccessTokenMinutes),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateRefreshToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
}