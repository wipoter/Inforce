using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Interfaces;

using Microsoft.AspNetCore.Authorization;

public class PermissionRequirement : IAuthorizationRequirement
{
    public Permission[] Permissions { get; }

    public PermissionRequirement(params Permission[] permissions)
    {
        Permissions = permissions;
    }
}
