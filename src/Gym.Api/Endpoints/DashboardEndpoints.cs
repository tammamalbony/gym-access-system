// -----------------------------
// File: Endpoints/DashboardEndpoints.cs
// -----------------------------
using Gym.Api.Services;

namespace Gym.Api.Endpoints;

public static class DashboardEndpoints
{
    public static RouteGroupBuilder MapDashboardEndpoints(this RouteGroupBuilder g)
    {
        var group = g.MapGroup("dashboard").RequireAuthorization();
        group.MapGet("summary", async (IDashboardService svc) => Results.Ok(await svc.Summary()));
        group.MapGet("late-members", async (IDashboardService svc) => Results.Ok(await svc.LateMembers()));
        return g;
    }
}
