using BackEnd.Attributes;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController(IUserService userService)
{
    [HttpGet]
    [PermissionAuthorize(Permission.Read)]
    public async Task<IResult> GetUser(string login)
    {
        var user = await userService.GetUser(login);
        return Results.Ok(user);
    }
}