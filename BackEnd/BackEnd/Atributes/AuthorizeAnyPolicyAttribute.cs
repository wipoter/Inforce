using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackEnd.Atributes;

public class AuthorizeAnyPolicyAttribute : TypeFilterAttribute
{
    public AuthorizeAnyPolicyAttribute(params string[] policies) : base(typeof(AuthorizeAnyPolicyFilter))
    {
        Arguments = new object[] { policies };
    }
}

public class AuthorizeAnyPolicyFilter(IAuthorizationService authorization, string[] policies)
    : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        foreach (var policy in policies)
        {
            var result = await authorization.AuthorizeAsync(context.HttpContext.User, policy);
            if (result.Succeeded)
            {
                return;
            }
        }

        context.Result = new ForbidResult();
    }
}