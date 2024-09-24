using System.Security.Claims;
using BackEnd.Interfaces;
using BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
{
    [HttpGet]
    public async Task<IResult> GetUser(string login)
    {
        var user = await userService.GetUser(login);
        return Results.Ok(user);
    }

    [HttpPost]
    [Authorize(Policy = "User")]
    public async Task<IResult> PostUrlInfo(string longUrl, string shortUrl)
    {
        // Отримати клейми з HttpContext
        var userId = httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == CustomClaims.UserId);

        if (userId is null || !Guid.TryParse(userId.Value, out var id))
        {
            return Results.Empty;
        }

        // Використовувати userId замість Guid.NewGuid()
        await userService.AddUrlInfo(id, longUrl, shortUrl);
        return Results.Ok();
    }
}