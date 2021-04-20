using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductAPI
{
    public class UserIsActiveHandler : AuthorizationHandler<UserIsActiveRequirement>
    {
        private readonly IUserIsActive userIsActive;

        public UserIsActiveHandler(IUserIsActive userIsActive) 
        {
            this.userIsActive = userIsActive;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIsActiveRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Anonymous))
                return Task.CompletedTask;

            var status = context.User.FindFirst(c => c.Type == ClaimTypes.Anonymous); //context.User.FindFirst("IsActive"); //

            var activeStatus = userIsActive.Get(status.Value == "True" ? 1 : 0);


            if(activeStatus == requirement.IsActive)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
