using BackEnd.Enums;
using BackEnd.Repositories;

namespace BackEnd.Services;

public class PermissionService(IUserRepository userRepository) : IPermissionService
{
    public async Task<HashSet<Permission>> GetPermissionAsync(Guid userId)
    {
        return await  userRepository.GetUserPermissions(userId);
    }
}