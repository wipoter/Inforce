using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Interfaces;

using Microsoft.AspNetCore.Authorization;

public class PermissionRequirement(params Permission[] permissions) : IAuthorizationRequirement
{
    public Permission[] Permissions { get; } = permissions ?? throw new ArgumentNullException(nameof(permissions));
    
}
