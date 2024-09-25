using AutoMapper;
using BackEnd.Data;
using BackEnd.Entities;
using BackEnd.Enums;
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

    public async Task DeleteShortUrlInfo(Guid userId, Guid infoId)
    {
        var user = await context.Users.FindAsync(userId);
        var info = user.UrlInfos.FirstOrDefault(u => u.Id == infoId);
        context.UrlInfos.Remove(info);
        await context.SaveChangesAsync();
    }

    public async Task CreateUrlInfoAsync(Guid userId, UrlInfo urlInfo)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return;

        var login = await context.LoginInfos.Where(l => l.UserId == userId).Select(l => l.Login).FirstOrDefaultAsync();

        if (login == null)
            return;
        
        var urlInfoEntity = new UrlInfoEntity()
        {
            Id = Guid.NewGuid(),
            CreatedBy = login,
            CreatedDate = DateTime.Now,
            LongUrl = urlInfo.LongUrl,
            ShortUrl = urlInfo.ShortUrl,
            UserId = userId
        };

        await context.UrlInfos.AddAsync(urlInfoEntity);
        user.UrlInfos.Add(urlInfoEntity);
        await context.SaveChangesAsync();

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