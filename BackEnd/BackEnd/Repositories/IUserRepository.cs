using BackEnd.Models;

namespace BackEnd.Repositories;

public interface IUserRepository
{
    Task<User> GetByLogin(string login);
    Task<HashSet<Permission>> GetUserPermissions(Guid id);
}