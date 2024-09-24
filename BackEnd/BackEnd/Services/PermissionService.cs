using BackEnd.Repositories;

namespace BackEnd.Services;

public class PermissionService(IUserRepository userRepository) : IPermissionService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<HashSet<Permission>> GetPermissionAsync(Guid userId)
    {
        return await  _userRepository.GetUserPermissions(userId);
    }
}