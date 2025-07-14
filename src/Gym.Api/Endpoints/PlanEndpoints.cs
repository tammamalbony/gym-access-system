
// -----------------------------
// File: Endpoints/PlanEndpoints.cs
// -----------------------------
using Gym.Core.Dtos;
using Gym.Api.Services;
using Gym.Api.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Api.Endpoints;

public static class PlanEndpoints
{
    public static RouteGroupBuilder MapPlanEndpoints(this RouteGroupBuilder g)
    {
        var group = g.MapGroup("plans").RequireAuthorization();

        group.MapGet("", async (IPlanService svc) => Results.Ok(await svc.All()));
        group.MapGet("/{id:int}", async (int id, IPlanService svc) =>
            await svc.One(id) is { } dto ? Results.Ok(dto) : Results.NotFound());
        group.MapPost("", [Authorize(Roles = Roles.ADMIN)] async (PlanDto dto, IPlanService svc) =>
            Results.Created("/api/plans", await svc.Add(dto)));
        group.MapPut("/{id:int}", [Authorize(Roles = Roles.ADMIN)] async (int id, PlanDto dto, IPlanService svc) =>
        {
            if (id != dto.Id) return Results.BadRequest();
            var updated = await svc.Update(dto);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });
        group.MapDelete("/{id:int}", [Authorize(Roles = Roles.ADMIN)] async (int id, IPlanService svc) =>
            await svc.Delete(id) ? Results.NoContent() : Results.NotFound());
        return g;
    }
}