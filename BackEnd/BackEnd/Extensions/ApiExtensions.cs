using BackEnd.Enums;
using BackEnd.Infrastructure;
using BackEnd.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Extensions;

public static class ApiExtensions
{
    public static void AddApiAuthentications(this IServiceCollection services, IConfiguration configuration,
        IJwtProvider jwtProvider)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            JwtBearerDefaults.AuthenticationScheme,
            options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtProvider.GenerateSecretKey())
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["not-jwt-token"];
                        return Task.CompletedTask;
                    }
                };
            });
        
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddControllersWithViews();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
            {
                policy.Requirements.Add(new PermissionRequirement(Permission.Create));
                policy.Requirements.Add(new PermissionRequirement(Permission.Read));
                policy.Requirements.Add(new PermissionRequirement(Permission.Delete));
                policy.Requirements.Add(new PermissionRequirement(Permission.Update));
            });
            options.AddPolicy("User", policy =>
            {
                policy.Requirements.Add(new PermissionRequirement(Permission.Create));
                policy.Requirements.Add(new PermissionRequirement(Permission.Read));
                policy.Requirements.Add(new PermissionRequirement(Permission.Delete));
            });
        
        });
        
        
    }
}