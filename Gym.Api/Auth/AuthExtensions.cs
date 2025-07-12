
// =============================
// File: Auth/AuthExtensions.cs
// =============================
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gym.Api.Auth;
static class AuthExtensions
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection svc, IConfiguration cfg)
    {
        svc.Configure<JwtSettings>(cfg.GetSection("Jwt"));
        var settings = cfg.GetSection("Jwt").Get<JwtSettings>()!;
        var key = Encoding.UTF8.GetBytes(settings.Key);

        svc.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(o =>
           {
               o.TokenValidationParameters = new()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = settings.Issuer,
                   ValidAudience = settings.Audience,
                   IssuerSigningKey = new SymmetricSecurityKey(key)
               };
           });
        return svc;
    }

    public static void AddBearerAuth(this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions o)
    {
        o.AddSecurityDefinition("Bearer", new()
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header
        });
        o.AddSecurityRequirement(new()
        {
            [new() { Reference = new() { Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme, Id = "Bearer" } }] = new List<string>()
        });
    }
}