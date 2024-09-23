using BackEnd.Models;
using BackEnd.Repositories;

namespace BackEnd.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<User> GetUser(string login) => await userRepository.GetByLogin(login);
}