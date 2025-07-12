
// =============================
// File: Endpoints/AuthEndpoints.cs (unchanged)
// =============================
using Gym.Api.Auth;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder g)
    {
        g.MapPost("auth/login", async (LoginRequest req, GymContext db, ITokenService tok) =>
        {
            string hash = Hash(req.Password);
            var user = await db.AppUsers.FirstOrDefaultAsync(u => u.Username == req.Username && u.PasswordHash == hash);
            if (user is null) return Results.Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var access  = tok.GenerateAccessToken(claims);
            var refresh = tok.GenerateRefreshToken();
            // TODO: persist refresh token & expiry
            return Results.Ok(new { access, refresh });
        }).AllowAnonymous();
        return g;
    }

    private static string Hash(string pwd) => Convert.ToHexString(KeyDerivation.Pbkdf2(pwd, new byte[16], KeyDerivationPrf.HMACSHA256, 600000, 32));

    public record LoginRequest(string Username, string Password);
}
