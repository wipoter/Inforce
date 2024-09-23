using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("AdminPolicy")]
public class UserController(IUserService userService)
{
    [HttpGet]
    public async Task<IResult> GetUser(string login)
    {
        var user = await userService.GetUser(login);
        return Results.Ok(user);
    }
}