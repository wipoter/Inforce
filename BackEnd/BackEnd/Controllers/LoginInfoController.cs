using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginInfoController(ILoginInfoService loginInfoService)
{
    [HttpPost]
    public async Task<IResult> Register(string login, string password)
    {
        await loginInfoService.Register(login, password);
        return Results.Ok();
    }

    [HttpGet]
    public async Task<IResult> Login(string login, string password)
    {
        var token = await loginInfoService.Login(login, password);
        
        return Results.Ok(token);
    }

 
}