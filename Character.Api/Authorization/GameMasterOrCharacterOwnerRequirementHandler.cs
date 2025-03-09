using CharacterApi.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CharacterApi.Authorization
{
    /// <summary>
    /// Handler that validates GameMaster role of the user or checks if user is owner of the character
    /// </summary>
    public class GameMasterOrCharacterOwnerRequirementHandler : AuthorizationHandler<GameMasterOrCharacterOwnerRequirement>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public GameMasterOrCharacterOwnerRequirementHandler(ICharacterRepository characterRepository, IHttpContextAccessor contextAccessor)
        {
            _characterRepository = characterRepository;
            _contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GameMasterOrCharacterOwnerRequirement requirement)
        {
            try
            {
                //first we take role from the user
                var role = context.User.FindFirst(x => x.Type == ClaimTypes.Role)!.Value;

                //if role is GameMaster (parameter from Program.cs), everything is OK
                if (role.Equals(requirement.Role))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
                else
                {
                    //if user is not GameMaster, we check if user is owner of the character
                    //we need name id from nameIdentifier claim
                    var nameIdentifier = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

                    //we are taking character id from incoming request
                    if (!_contextAccessor!.HttpContext!.Request.RouteValues.TryGetValue("id", out var id))
                    {
                        context.Fail(new AuthorizationFailureReason(this, "User is not GameMaster and is not owner of the character"));
                        return Task.CompletedTask;
                    }

                    //find character owner by id from request
                    var characterOwner = _characterRepository.GetCharacterOwnerId(Int32.Parse((string)id));

                    //if user is character owner, everythig is OK
                    if (nameIdentifier.Equals(characterOwner))
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
                }

                context.Fail(new AuthorizationFailureReason(this, "User is not GameMaster and is not owner of the character"));
                return Task.CompletedTask;
            }
            catch
            {
                context.Fail(new AuthorizationFailureReason(this, "User is not GameMaster and is not owner of the character"));
                return Task.CompletedTask;
            }
        }
    }
}
