using BusinessLogic.Models;
using CharacterApi.BusinessLogic.Models;

namespace CharacterApi.BusinessLogic
{
    public interface ICharacterBusinessLogic
    {
        CommandResponse<List<CharacterGet>> GetCharacters();

        CommandResponse<CharacterGetById> GetCharacterById(int id);

        CommandResponse<CharacterPost> CreateCharacter(CharacterPost characterPost);
    }
}
