using DTOLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShoppingWebApp.CustomHandler
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var validRole = false;
            if (requirement.AllowedRoles == null || requirement.AllowedRoles.Any() == false)
            {
                validRole = true;
            }
            else
            {
                var claims = context.User.Claims;
                var userId = claims.FirstOrDefault(c => c.Type == "UserId").Value;
                var roles = requirement.AllowedRoles;

                validRole = new lPVNgP26wKContext().Users.Where(us => roles.Contains(us.RoleNavigation.RoleName) && us.UserId == int.Parse(userId)).Any();
            }
            if (validRole)
            {
                context.Succeed(requirement);
            } else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
