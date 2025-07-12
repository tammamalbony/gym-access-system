
// =============================
// File: Endpoints/MemberEndpoints.cs
// =============================
using Gym.Api.Dtos;
using Gym.Api.Services;
using Gym.Api.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Api.Endpoints;

public static class MemberEndpoints
{
    public static RouteGroupBuilder MapMemberEndpoints(this RouteGroupBuilder g)
    {
        var group = g.MapGroup("members").RequireAuthorization();

        group.MapGet("", async (IMemberService svc) => Results.Ok(await svc.All()));
        group.MapGet("/{id:long}", async (long id, IMemberService svc) =>
            await svc.One(id) is { } dto ? Results.Ok(dto) : Results.NotFound());
        group.MapPost("", [Authorize(Roles = $"{Roles.DATA_ENTRY},{Roles.ADMIN}")]async (MemberDto dto, IMemberService svc) =>
            Results.Created("/api/members", await svc.Add(dto)));
        group.MapPut("/{id:long}", [Authorize(Roles = $"{Roles.DATA_ENTRY},{Roles.ADMIN}")] async (long id, MemberDto dto, IMemberService svc) =>
        {
            if (id != dto.Id) return Results.BadRequest();
            var updated = await svc.Update(dto);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });
        group.MapDelete("/{id:long}", [Authorize(Roles = Roles.ADMIN)] async (long id, IMemberService svc) =>
            await svc.Delete(id) ? Results.NoContent() : Results.NotFound());
        return g;
    }
}