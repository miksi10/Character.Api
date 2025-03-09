using Microsoft.AspNetCore.Authorization;

namespace CharacterApi.Authorization
{
    public class GameMasterOrCharacterOwnerRequirement : IAuthorizationRequirement
    {
        public string Role { get; }
        public GameMasterOrCharacterOwnerRequirement(string role)
        {
            Role = role;
        }
    }
}
