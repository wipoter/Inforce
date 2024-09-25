using BackEnd.BuilderConfigure;
using BackEnd.Data;
using BackEnd.Extensions;
using BackEnd.Infrastructure;
using BackEnd.Mappers;
using BackEnd.Repositories;
using BackEnd.Services;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using AuthorizationOptions = BackEnd.Data.AuthorizationOptions;

var builder = WebApplication.CreateBuilder(args);

Services.SetBuilder(builder);
Services.ServicesAdd();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

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