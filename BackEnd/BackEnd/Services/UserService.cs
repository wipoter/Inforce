using BackEnd.Models;
using BackEnd.Repositories;

namespace BackEnd.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<User> GetUser(string login) => await userRepository.GetByLogin(login);

    public async Task AddUrlInfo(Guid userId, string longUrl, string shortUrl)
    {
        var urlInfo = UrlInfo.Create(Guid.NewGuid(), shortUrl, longUrl);
        await userRepository.CreateUrlInfoAsynk(userId, urlInfo);
    }
}