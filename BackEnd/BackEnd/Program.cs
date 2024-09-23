using BackEnd.Data;
using BackEnd.Extensions;
using BackEnd.Interfaces;
using BackEnd.Mappers;
using BackEnd.Repositories;
using BackEnd.Services;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

services.AddDbContextFactory<ShortenerUrlContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();

services.AddHttpContextAccessor();
services.AddScoped<ILoginInfoRepository, LoginInfoRepository>();
services.AddScoped<ILoginInfoService, LoginInfoService>();

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();

services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

services.AddSingleton(DataBaseMapper.GetMapper());

services.AddSwaggerGen();

// Використовуємо ваше розширення для додавання аутентифікації та авторизації
var jwtProvider = builder.Services.BuildServiceProvider().GetService<IJwtProvider>();
services.AddApiAuthentications(configuration, jwtProvider);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();