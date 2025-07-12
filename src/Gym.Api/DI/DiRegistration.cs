
// =============================
// File: DI/DiRegistration.cs
// =============================
using Gym.Api.Repositories;
using Gym.Api.Services;
namespace Microsoft.Extensions.DependencyInjection;

public static class DiRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection s) => s
        .AddScoped<IMemberRepo, MemberRepo>()
        .AddScoped<IPlanRepo, PlanRepo>();

    public static IServiceCollection AddServices(this IServiceCollection s) => s
        .AddScoped<IMemberService, MemberService>()
        .AddScoped<IPlanService, PlanService>();
}