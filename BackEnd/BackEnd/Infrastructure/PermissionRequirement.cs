using BackEnd.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Infrastructure;

public class PermissionRequirement(params Permission[] permissions) : IAuthorizationRequirement
{
    public Permission[] Permissions { get; } = permissions;
}
