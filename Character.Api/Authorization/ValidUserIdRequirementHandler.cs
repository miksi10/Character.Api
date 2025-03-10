using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CharacterApi.Authorization
{
    public class ValidUserIdRequirementHandler : AuthorizationHandler<ValidUserIdRequirement>
    {
        private readonly UserManager<IdentityUser> _userManager;
        public ValidUserIdRequirementHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidUserIdRequirement requirement)
        {
            try
            {
                var nameIdentifier = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

                //with user manager, we are validating user id extracted from user claims
                var validUserId = _userManager.FindByIdAsync(nameIdentifier).Result;

                //if validUserId is not null, then the user id from token is valid. Otherwise validUserId would be null.
                if (validUserId != null)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                context.Fail(new AuthorizationFailureReason(this, "User id is not valid."));
                return Task.CompletedTask;

            }
            catch
            {
                context.Fail(new AuthorizationFailureReason(this, "User id is not valid."));
                return Task.CompletedTask;
            }
        }
    }
}
