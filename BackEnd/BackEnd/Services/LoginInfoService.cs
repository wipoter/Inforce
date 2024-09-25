using BackEnd.Helpers;
using BackEnd.Infrastructure;
using BackEnd.Models;
using BackEnd.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Services;

public class LoginInfoService(
    IJwtProvider jwtProvider,
    ILoginInfoRepository loginInfoRepository,
    IHttpContextAccessor httpContextAccessor)
    : ILoginInfoService
{
    
    public async Task Register(string login, string password)
    {
        var hashedPassword = PasswordHasher.Generate(password);
        var user = new LoginInfo(Guid.NewGuid(), login, hashedPassword);
        
        await loginInfoRepository.CreateAsync(user);
    }

    public async Task<string> Login(string login, string password)
    {
        var loginInfo = await loginInfoRepository.GetByLoginAsync(login);

        var isPasswordValid = PasswordHasher.Verify(password, loginInfo.PasswordHash);

        if (!isPasswordValid)
            throw new Exception("Invalid login or password");

        var token = jwtProvider.GenerateToken(loginInfo);

        var httpContext = httpContextAccessor.HttpContext;
        httpContext?.Response.Cookies.Append("not-jwt-token", token); 

        return token;
    }
}