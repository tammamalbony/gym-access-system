// -----------------------------
// File: Endpoints/AlertEndpoints.cs
// -----------------------------
using Gym.Api.Services;
using Gym.Api.Auth;

namespace Gym.Api.Endpoints;

public static class AlertEndpoints
{
    public static RouteGroupBuilder MapAlertEndpoints(this RouteGroupBuilder g)
    {
        var group = g.MapGroup("alerts")
            .RequireAuthorization(p => p.RequireRole(Roles.ADMIN));
        group.MapGet("", async (IAlertService svc) => Results.Ok(await svc.All()));
        return g;
    }
}
