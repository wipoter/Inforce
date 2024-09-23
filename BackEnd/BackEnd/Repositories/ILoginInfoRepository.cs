using BackEnd.Data;
using BackEnd.Models;

namespace BackEnd.Repositories;

public interface ILoginInfoRepository
{
    Task CreateAsync(LoginInfo loginInfoEntity);
    Task<LoginInfo> GetByLogin(string login);
}