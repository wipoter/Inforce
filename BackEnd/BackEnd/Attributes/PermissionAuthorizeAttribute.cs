using BackEnd.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackEnd.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class PermissionAuthorizeAttribute : TypeFilterAttribute
{
    public PermissionAuthorizeAttribute(params Permission[] permissions) 
        : base(typeof(PermissionAuthorizeFilter))
    {
        Arguments = new object[] { new PermissionRequirement(permissions) };
    }
}

public class PermissionAuthorizeFilter : IAsyncAuthorizationFilter
{
    private readonly IAuthorizationService _authorizationService;
    private readonly PermissionRequirement _requirement;

    public PermissionAuthorizeFilter(IAuthorizationService authorizationService, PermissionRequirement requirement)
    {
        _authorizationService = authorizationService;
        _requirement = requirement;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var result = await _authorizationService.AuthorizeAsync(user, null, _requirement);

        if (!result.Succeeded)
        {
            context.Result = new ForbidResult();
        }
    }
}