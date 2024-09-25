using BackEnd.Enums;
using BackEnd.Models;

namespace BackEnd.Repositories;

public interface IUserRepository
{
    Task<User> GetByLogin(string login);
    Task<HashSet<Permission>> GetUserPermissions(Guid id);
    Task DeleteShortUrlInfo(Guid userId, Guid infoId);

    Task CreateUrlInfoAsync(Guid userId, UrlInfo urlInfo);
}