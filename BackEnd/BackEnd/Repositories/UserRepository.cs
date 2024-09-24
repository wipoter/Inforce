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

    public async Task DeleteShortUrlInfo(Guid userId, Guid infoId)
    {
        var info = context.Users.FindAsync(userId).Result.UrlInfos.FirstOrDefault(u => u.Id == infoId);
        context.UrlInfos.Remove(info);
        await context.SaveChangesAsync();
    }

    public async Task CreateUrlInfoAsynk(Guid userId, UrlInfo urlInfo)
    {
        // Використовуємо await для асинхронного доступу до даних
        var user = await context.Users.FindAsync(userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        var loginInfo = await context.LoginInfos.FirstOrDefaultAsync(l => l.UserId == user.Id);

        if (loginInfo == null)
        {
            throw new Exception("Login info not found");
        }

        if (user == null)
        {
            throw new Exception("User not found.");
        }

        if (loginInfo == null)
        {
            throw new Exception("Login info not found.");
        }

        var urlInfoEntity = new UrlInfoEntity()
        {
            Id = Guid.NewGuid(), // Генеруємо новий ID, оскільки це новий запис
            CreatedBy = loginInfo.Login,
            CreatedDate = DateTime.Now,
            LongUrl = urlInfo.LongUrl,
            ShortUrl = urlInfo.ShortUrl,
            UserId = user.Id // Встановлюємо ID користувача
        };
        
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