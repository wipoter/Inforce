using BackEnd.Services;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Interfaces;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userPermissions = context.User.Claims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .ToList();

        // Перевірка, чи користувач має один з дозволів
        if (requirement.Permissions.Any(p => userPermissions.Contains(p.ToString())))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
