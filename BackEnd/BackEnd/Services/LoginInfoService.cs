using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services;

public class LoginInfoService(
    IJwtProvider jwtProvider,
    ILoginInfoRepository loginInfoRepository,
    IPasswordHasher passwordHasher,
    IHttpContextAccessor httpContextAccessor)
    : ILoginInfoService
{
    
    public async Task Register(string login, string password)
    {
        var hashedPassword = passwordHasher.Generate(password);
        var user = LoginInfo.Create(Guid.NewGuid(), login, hashedPassword);
        await loginInfoRepository.CreateAsync(user);
    }

    public async Task<string> Login(string login, string password)
    {
        var user = await loginInfoRepository.GetByLogin(login);

        var isPasswordValid = passwordHasher.Verify(password, user.PasswordHash);

        if (!isPasswordValid)
            throw new Exception("Invalid login or password");

        var token = jwtProvider.GenerateToken(user);

        var httpContext = httpContextAccessor.HttpContext;
        httpContext.Response.Cookies.Append("not-jwt-token", token); 

        return token;
    }
}