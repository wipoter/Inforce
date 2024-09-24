using BackEnd.Models;

namespace BackEnd.Services;

public interface IUserService
{
    Task<User> GetUser(string login);
    Task AddUrlInfo(Guid userId, string longUrl, string shortUrl);
}