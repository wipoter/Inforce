using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories;

public class LoginInfoRepository(ShortenerUrlContext context, IMapper mapper): ILoginInfoRepository
{
    public async Task CreateAsync(LoginInfo loginInfo)
    {
        var loginInfoEntity = new LoginInfoEntity()
        {
            Id = loginInfo.Id,
            Login = loginInfo.Login,
            PasswordHash = loginInfo.PasswordHash
        };
        
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            LoginInfo = loginInfoEntity  // Прив'язуємо користувача до логіну
        };
        
        loginInfoEntity.User = user;
        
        await context.LoginInfo.AddAsync(loginInfoEntity);
        await context.User.AddAsync(user);
        
        await context.SaveChangesAsync();
    }

    public async Task<LoginInfo> GetByLogin(string login)
    {
        var loginInfoEntity = await context.LoginInfo
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Login == login) ?? throw new Exception();
        return mapper.Map<LoginInfo>(loginInfoEntity);
    }
}