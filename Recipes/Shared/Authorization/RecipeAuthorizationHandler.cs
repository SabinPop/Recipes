using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Recipes.Shared.Models;
using System.Threading.Tasks;

namespace Recipes.Shared.Authorization
{

    public static class Operations
    {
        public static OperationAuthorizationRequirement Edit =
            new OperationAuthorizationRequirement { Name = nameof(Edit) };

        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = nameof(Delete) };
    }

    public class RecipeAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Recipe>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       OperationAuthorizationRequirement requirement,
                                                       Recipe resource)
        {
            if(context.User.Identity?.Name == resource.Author && 
               ( requirement.Name == Operations.Edit.Name ||
                requirement.Name == Operations.Delete.Name))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
