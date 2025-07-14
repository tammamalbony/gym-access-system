// -----------------------------
// File: Endpoints/LogEndpoints.cs
// -----------------------------
using Gym.Api.Services;
using Gym.Api.Auth;

namespace Gym.Api.Endpoints;

public static class LogEndpoints
{
    public static RouteGroupBuilder MapLogEndpoints(this RouteGroupBuilder g)
    {
        var group = g.MapGroup("logs")
            .RequireAuthorization(p => p.RequireRole(Roles.ADMIN));
        group.MapGet("", async (ILogService svc) => Results.Ok(await svc.All()));
        group.MapGet("latest", async (ILogService svc) =>
            await svc.Latest() is { } dto ? Results.Ok(dto) : Results.NoContent());
        return g;
    }
}
