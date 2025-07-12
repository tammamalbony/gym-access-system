// =============================
// File: Services/DbInitializer.cs
// =============================
using BCrypt.Net;
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Services;

public static class DbInitializer
{
    public static async Task InitAsync(IServiceProvider sp, IConfiguration cfg)
    {
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<GymContext>();
        await db.Database.EnsureCreatedAsync();

        if (cfg.GetValue<bool>("INIT_DEMO_DATA"))
        {
            await SeedDemoData(db);
        }
    }

    private static async Task SeedDemoData(GymContext db)
    {
        if (!await db.Plans.AnyAsync())
        {
            db.Plans.AddRange(
                new Plan { Name = "Monthly", PriceCents = 3000, DurationMonths = 1, GraceDays = 3, IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Plan { Name = "Quarter", PriceCents = 8500, DurationMonths = 3, GraceDays = 5, IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Plan { Name = "Year", PriceCents = 30000, DurationMonths = 12, GraceDays = 7, IsActive = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );
        }

        if (!await db.AppUsers.AnyAsync())
        {
            string pwd = BCrypt.Net.BCrypt.HashPassword("ChangeMe!");
            db.AppUsers.AddRange(
                new AppUser { Username = "admin", PasswordHash = pwd, Role = UserRole.ADMIN, Email = "admin@example.com", IsEnabled = true, CreatedAt = DateTime.UtcNow },
                new AppUser { Username = "data_entry", PasswordHash = pwd, Role = UserRole.DATA_ENTRY, Email = "data@example.com", IsEnabled = true, CreatedAt = DateTime.UtcNow },
                new AppUser { Username = "support", PasswordHash = pwd, Role = UserRole.SUPPORT, Email = "support@example.com", IsEnabled = true, CreatedAt = DateTime.UtcNow }
            );
        }

        await db.SaveChangesAsync();
    }
}
