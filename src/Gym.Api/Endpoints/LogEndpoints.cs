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
        var group = g.MapGroup("logs").RequireAuthorization(Roles.ADMIN);
        group.MapGet("", async (ILogService svc) => Results.Ok(await svc.All()));
        return g;
    }
}
