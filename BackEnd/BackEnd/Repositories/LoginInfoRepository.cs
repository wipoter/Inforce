using AutoMapper;
using BackEnd.Data;
using BackEnd.Entities;
using BackEnd.Enums;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories;

public class LoginInfoRepository(ShortenerUrlContext context, IMapper mapper): ILoginInfoRepository
{
    public async Task CreateAsync(LoginInfo loginInfo)
    {
        if (await GetByLoginAsync(loginInfo.Login) != null)
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
            Roles = [roleEntity]
        };

        loginInfoEntity.User = user;

        await context.LoginInfos.AddAsync(loginInfoEntity);
        await context.Users.AddAsync(user);

        await context.SaveChangesAsync();
    }

    public async Task<LoginInfo?> GetByLoginAsync(string login)
    {
        var loginInfoEntity = await context.LoginInfos
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Login == login);
        return loginInfoEntity == null ? null : mapper.Map<LoginInfo>(loginInfoEntity);
    }
}