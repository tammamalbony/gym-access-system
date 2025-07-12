
// Design‑time factory for EF migrations
namespace Gym.Api.Data;
using Microsoft.EntityFrameworkCore.Design;

public class DesignTimeFactory : IDesignTimeDbContextFactory<GymContext>
{
    public GymContext CreateDbContext(string[] args)
    {
        var opts = new DbContextOptionsBuilder<GymContext>()
            .UseMySql("server=localhost;uid=gymapp;pwd=S3cureP@ss!;database=gym_access_system;", 
                      ServerVersion.AutoDetect(""))
            .Options;
        return new(opts);
    }
}