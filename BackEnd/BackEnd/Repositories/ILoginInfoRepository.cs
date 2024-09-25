using BackEnd.Models;

namespace BackEnd.Repositories;

public interface ILoginInfoRepository
{
    Task CreateAsync(LoginInfo loginInfo);
    Task<LoginInfo?> GetByLoginAsync(string login);
}