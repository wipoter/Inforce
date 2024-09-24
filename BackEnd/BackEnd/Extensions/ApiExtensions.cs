using System.Text;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Extensions;

public static class ApiExtensions
{
    public static void AddApiAuthentications(this IServiceCollection services, IConfiguration configuration, IJwtProvider jwtProvider)
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
        
        services.AddAuthorization();
    }
    
}