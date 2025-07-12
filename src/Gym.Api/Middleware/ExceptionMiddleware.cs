
// =============================
// File: Middleware/ExceptionMiddleware.cs
// =============================
using System.Text.Json;

namespace Gym.Api.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> log)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        try { await next(ctx); }
        catch (Exception ex)
        {
            log.LogError(ex, "Unhandled error");
            ctx.Response.StatusCode = 500;
            ctx.Response.ContentType = "application/json";
            var payload = JsonSerializer.Serialize(new { message = ex.Message });
            await ctx.Response.WriteAsync(payload);
        }
    }
}
