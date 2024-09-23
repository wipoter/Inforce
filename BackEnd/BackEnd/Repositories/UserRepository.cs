using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories;

public class UserRepository(ShortenerUrlContext context, IMapper mapper): IUserRepository
{
    public async Task<User> GetByLogin(string login)
    {
        var loginInfoEntity = await context.LoginInfo
            .AsNoTracking().Include(loginInfoEntity => loginInfoEntity.User)
            .FirstOrDefaultAsync(l => l.Login == login) ?? throw new Exception();
        return mapper.Map<User>(loginInfoEntity.User);
    }
}