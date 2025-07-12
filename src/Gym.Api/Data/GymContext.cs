
// =============================
// File: Data/GymContext.cs
// =============================
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Data;

public class GymContext(DbContextOptions<GymContext> opts) : DbContext(opts)
{
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Plan> Plans => Set<Plan>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Member>(e =>
        {
            e.ToTable("members");
            e.HasKey(m => m.MemberId);
            e.Property(m => m.FirstName).HasColumnName("first_name");
            e.Property(m => m.LastName).HasColumnName("last_name");
            e.Property(m => m.Email).HasColumnName("email");
        });
        b.Entity<Plan>(e =>
        {
            e.ToTable("plans");
            e.HasKey(p => p.PlanId);
            e.Property(p => p.Name).HasColumnName("name");
        });
        b.Entity<AppUser>(e =>
        {
            e.ToTable("app_users");
            e.HasKey(u => u.UserId);
            e.Property(u => u.Username).HasColumnName("username");
        });
    }
}