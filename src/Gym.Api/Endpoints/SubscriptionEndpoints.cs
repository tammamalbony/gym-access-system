// -----------------------------
// File: Endpoints/SubscriptionEndpoints.cs
// -----------------------------
using Gym.Api.Dtos;
using Gym.Api.Services;
using Gym.Api.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Api.Endpoints;

public static class SubscriptionEndpoints
{
    public static RouteGroupBuilder MapSubscriptionEndpoints(this RouteGroupBuilder g)
    {
        var group = g.MapGroup("subscriptions").RequireAuthorization();

        group.MapGet("", async (ISubscriptionService svc) => Results.Ok(await svc.All()));
        group.MapGet("/{id:long}", async (long id, ISubscriptionService svc) =>
            await svc.One(id) is { } dto ? Results.Ok(dto) : Results.NotFound());
        group.MapGet("member/{mid:long}", async (long mid, ISubscriptionService svc) =>
            await svc.ActiveForMember(mid) is { } dto ? Results.Ok(dto) : Results.NotFound());
        group.MapPost("", [Authorize(Roles = Roles.ADMIN)] async (SubscriptionDto dto, ISubscriptionService svc) =>
            Results.Created("/api/subscriptions", await svc.Add(dto)));
        group.MapPut("/{id:long}", [Authorize(Roles = Roles.ADMIN)] async (long id, SubscriptionDto dto, ISubscriptionService svc) =>
        {
            if (id != dto.Id) return Results.BadRequest();
            var updated = await svc.Update(dto);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });
        group.MapDelete("/{id:long}", [Authorize(Roles = Roles.ADMIN)] async (long id, ISubscriptionService svc) =>
            await svc.Delete(id) ? Results.NoContent() : Results.NotFound());
        return g;
    }
}
