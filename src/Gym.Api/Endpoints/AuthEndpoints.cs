
// =============================
// File: Endpoints/AuthEndpoints.cs (unchanged)
// =============================
using Gym.Api.Auth;
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
            var user = await db.AppUsers.FirstOrDefaultAsync(u => u.Username == req.Username);
            if (user is null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Results.Unauthorized();

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

        g.MapPost("auth/signup", async (SignupRequest req, GymContext db) =>
        {
            if (await db.AppUsers.AnyAsync(u => u.Username == req.Username))
                return Results.BadRequest("Username already exists");
            var user = new AppUser
            {
                Username = req.Username,
                Email = req.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
                Role = UserRole.DATA_ENTRY,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow
            };
            db.AppUsers.Add(user);
            await db.SaveChangesAsync();
            return Results.Created($"/api/users/{user.UserId}", new { user.UserId, user.Username });
        }).AllowAnonymous();
        return g;
    }

    public record LoginRequest(string Username, string Password);
    public record SignupRequest(string Username, string Email, string Password);
}
