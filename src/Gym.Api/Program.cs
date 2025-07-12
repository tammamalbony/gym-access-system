using Gym.Api.Auth;
using Gym.Api.Data;
using Gym.Api.Endpoints;
using Gym.Api.Middleware;
using Gym.Api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using DotNetEnv;

Env.TraversePath().Load();
var builder = WebApplication.CreateBuilder(args);

// ---------------- logging ----------------
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();


// -------------- services -----------------
var cs = builder.Configuration.GetConnectionString("Default")!;
builder.Services.AddDbContext<GymContext>(opt =>
    opt.UseMySql(cs, ServerVersion.AutoDetect(cs)));

builder.Services.AddJwtAuth(builder.Configuration);

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddHostedService<BackupService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new() { Title = "Gym API", Version = "v1" });
    o.AddBearerAuth();
});


// -------------- build  -------------------
var app = builder.Build();

await DbInitializer.InitAsync(app.Services, app.Configuration);

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api")
   .MapAuthEndpoints()
   .MapMemberEndpoints()
   .MapPlanEndpoints()
   .MapSubscriptionEndpoints()
   .MapDashboardEndpoints()
   .MapReminderEndpoints()
   .MapUserEndpoints()
   .MapLogEndpoints();

app.Run();
