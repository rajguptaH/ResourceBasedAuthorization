using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using ResourceBasedAuthorization.Models;

namespace ResourceBasedAuthorization.Policies.Handlers;

public class ImageFileDTOHandler : AuthorizationHandler<OperationAuthorizationRequirement, ImageFileDTO>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   OperationAuthorizationRequirement requirement,
                                                   ImageFileDTO resource)
    {
        if (requirement.Name == Operations.Read.Name )
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
public static class Operations 
{
    public static OperationAuthorizationRequirement Create =
        new OperationAuthorizationRequirement { Name = nameof(Create) };
    public static OperationAuthorizationRequirement Read =
        new OperationAuthorizationRequirement { Name = nameof(Read) };
    public static OperationAuthorizationRequirement Update =
        new OperationAuthorizationRequirement { Name = nameof(Update) };
    public static OperationAuthorizationRequirement Delete =
        new OperationAuthorizationRequirement { Name = nameof(Delete) };
}