using System.Security.Claims;
using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories;

public class LoginInfoRepository(ShortenerUrlContext context, IMapper mapper): ILoginInfoRepository
{
    public async Task CreateAsync(LoginInfo loginInfo)
    {
        if(GetByLogin(loginInfo.Login)!= null)
            return;
        
        var loginInfoEntity = new LoginInfoEntity()
        {
            Id = loginInfo.Id,
            Login = loginInfo.Login,
            PasswordHash = loginInfo.PasswordHash
        };

        var roleEntity = await context.Roles.SingleOrDefaultAsync(r => r.Id == (int)Role.User) ??
                         throw new InvalidOperationException();
        
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            LoginInfo = loginInfoEntity,
            Roles = [roleEntity]// Прив'язуємо користувача до логіну
        };
        
        loginInfoEntity.User = user;
        
        
        
        await context.LoginInfos.AddAsync(loginInfoEntity);
        await context.Users.AddAsync(user);
        
        await context.SaveChangesAsync();
    }

    public async Task<LoginInfo> GetByLogin(string login)
    {
        var loginInfoEntity = await context.LoginInfos
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Login == login) ?? throw new Exception();
        return mapper.Map<LoginInfo>(loginInfoEntity);
    }
}