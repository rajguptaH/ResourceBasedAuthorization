using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using ResourceBasedAuthorization.Models;

namespace ResourceBasedAuthorization.Policies.Handlers;

public class StringHandler : AuthorizationHandler<RequirmentString, KeyValuePair<int, string>>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   RequirmentString requirement,
                                                   KeyValuePair<int,string> resource)
    {
        if (resource.Key == 5)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
public class RequirmentString : IAuthorizationRequirement 
{
  
}