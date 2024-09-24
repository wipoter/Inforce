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

    public async Task<string[]> GetUserPermissions(LoginInfo loginInfo)
    {
        // Знаходимо користувача за LoginInfo
        var userEntity = context.LoginInfos
            .Include(l => l.User)
            .ThenInclude(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .FirstOrDefaultAsync(l => l.Id == loginInfo.Id).Result.User;

        // Перевіряємо, чи знайдено користувача
        if (userEntity == null)
        {
            // Можна кинути виняток або повернути порожній масив, залежно від потреб
            return Array.Empty<string>(); // або можна кинути виняток
        }

        // Витягуємо всі дозволи користувача
        var permissions = userEntity.Roles
            .SelectMany(role => role.Permissions.Select(p => p.Name)) // Витягуємо назви дозволів
            .Distinct() // Залишаємо тільки унікальні дозволи
            .ToArray();

        return permissions; // Повертаємо масив дозволів
    }
}