using Microsoft.AspNetCore.Authorization;

namespace CharacterApi.Authorization
{
    public class ValidUserIdRequirement : IAuthorizationRequirement
    {
        public ValidUserIdRequirement()
        {
        }
    }
}
