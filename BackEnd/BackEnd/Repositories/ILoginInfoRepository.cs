using BackEnd.Data;
using BackEnd.Models;

namespace BackEnd.Repositories;

public interface ILoginInfoRepository
{
    Task CreateAsync(LoginInfo loginInfo);
    Task<LoginInfo> GetByLogin(string login);
}