// -----------------------------
// File: Endpoints/UserEndpoints.cs
// -----------------------------
using Gym.Api.Dtos;
using Gym.Api.Services;
using Gym.Api.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Api.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder g)
    {
        var group = g.MapGroup("users").RequireAuthorization(Roles.ADMIN);

        group.MapGet("", async (IUserService svc) => Results.Ok(await svc.All()));
        group.MapGet("/{id:int}", async (int id, IUserService svc) =>
            await svc.One(id) is { } dto ? Results.Ok(dto) : Results.NotFound());
        group.MapPost("", async (AppUserDto dto, string password, IUserService svc) =>
            Results.Created("/api/users", await svc.Add(dto, password)));
        group.MapPut("/{id:int}", async (int id, AppUserDto dto, IUserService svc) =>
        {
            if (id != dto.Id) return Results.BadRequest();
            var updated = await svc.Update(dto);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });
        group.MapDelete("/{id:int}", async (int id, IUserService svc) =>
            await svc.Delete(id) ? Results.NoContent() : Results.NotFound());
        group.MapPut("/{id:int}/enable", async (int id, bool enable, IUserService svc) =>
            await svc.SetEnabled(id, enable) ? Results.NoContent() : Results.NotFound());
        group.MapPut("/{id:int}/password", async (int id, string pwd, IUserService svc) =>
            await svc.ChangePassword(id, pwd) ? Results.NoContent() : Results.NotFound());
        return g;
    }
}
