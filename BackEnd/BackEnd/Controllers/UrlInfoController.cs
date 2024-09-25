using System.Security.Claims;
using BackEnd.Atributes;
using BackEnd.Entities;
using BackEnd.Infrastructure;
using BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlInfoController(IUrlInfoService urlInfoService, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
{
    [HttpGet]
    public Task<IResult> GetAllUrlInfoAsync()
    {
        return Task.FromResult(Results.Ok(urlInfoService.GetAllUrlInfo().Result));
    }

    [HttpGet("{urlInfoId}")]
    [AuthorizeAnyPolicy("User", "Admin")]
    public async Task<UrlInfoEntity?> GetUrlInfoById(Guid urlInfoId)
    {
        return await urlInfoService.GetUrlInfoByIdAsync(urlInfoId);
    }
    
    [HttpPost]
    [AuthorizeAnyPolicy("User", "Admin")]
    public async Task<IResult> PostUrlInfoAsync([FromBody] UrlInfoRequest request)
    {
        var userId = httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == CustomClaims.UserId);

        if (userId is null || !Guid.TryParse(userId.Value, out var id))
        {
            return Results.Unauthorized();
        }

        if (string.IsNullOrWhiteSpace(request.LongUrl))
        {
            return Results.BadRequest(new { message = "URL cannot be empty" });
        }

        await urlInfoService.AddUrlInfoAsync(id, request.LongUrl);
        
        return Results.Ok(new { message = "UrlInfo added successfully" });
    }


    [HttpDelete("{urlInfoId}")]
    [Authorize]
    public async Task<IResult> DeleteUrlInfoAsync(Guid urlInfoId)
    {

        var isAdmin = await authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext?.User, "Admin");

        if (isAdmin.Succeeded)
        {
            await urlInfoService.DeleteInfoAsync(default, urlInfoId);
        }
        else
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == CustomClaims.UserId);

            if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Results.Empty;
            }

            await urlInfoService.DeleteInfoAsync(userId, urlInfoId);
        }

        return Results.Ok();
    }


    
}