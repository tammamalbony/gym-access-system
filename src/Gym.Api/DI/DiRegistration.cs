
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
        .AddScoped<IPlanRepo, PlanRepo>()
        .AddScoped<IFingerprintRepo, FingerprintRepo>()
        .AddScoped<ISubscriptionRepo, SubscriptionRepo>()
        .AddScoped<IPaymentRepo, PaymentRepo>()
        .AddScoped<IControllerRepo, ControllerRepo>()
        .AddScoped<IAccessTokenRepo, AccessTokenRepo>()
        .AddScoped<IControllerTokenStatusRepo, ControllerTokenStatusRepo>()
        .AddScoped<IAccessLogRepo, AccessLogRepo>()
        .AddScoped<IAppUserRepo, AppUserRepo>()
        .AddScoped<IEmailAlertRepo, EmailAlertRepo>();

    public static IServiceCollection AddServices(this IServiceCollection s) => s
        .AddScoped<IMemberService, MemberService>()
        .AddScoped<IPlanService, PlanService>()
        .AddScoped<ISubscriptionService, SubscriptionService>()
        .AddScoped<IDashboardService, DashboardService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<ILogService, LogService>()
        .AddScoped<IReminderService, ReminderService>();
}
