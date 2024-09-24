using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories;

public class UserRepository(ShortenerUrlContext context, IMapper mapper): IUserRepository
{
    public async Task<User> GetByLogin(string login)
    {
        var loginInfoEntity = await context.LoginInfos
            .AsNoTracking().Include(loginInfoEntity => loginInfoEntity.User)
            .FirstOrDefaultAsync(l => l.Login == login) ?? throw new Exception();
        return mapper.Map<User>(loginInfoEntity.User);
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid id)
    {
        var roles = await context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == id)
            .Select(u => u.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
    }
    
}