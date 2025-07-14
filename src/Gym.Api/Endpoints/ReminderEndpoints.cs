// -----------------------------
// File: Endpoints/ReminderEndpoints.cs
// -----------------------------
using Gym.Api.Auth;
using Gym.Api.Services;

namespace Gym.Api.Endpoints;

public static class ReminderEndpoints
{
    public static RouteGroupBuilder MapReminderEndpoints(this RouteGroupBuilder g)
    {
        var group = g.MapGroup("reminders")
            .RequireAuthorization(p => p.RequireRole(Roles.ADMIN));

        group.MapGet("tomorrow", async (IReminderService svc) =>
        {
            var date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
            return Results.Ok(await svc.ExpiringOn(date));
        });

        group.MapPost("{id:long}/send", async (long id, IReminderService svc) =>
            await svc.SendReminder(id) ? Results.NoContent() : Results.NotFound());

        return g;
    }
}
