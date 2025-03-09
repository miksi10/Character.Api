using CharacterApi.Models;

namespace CharacterApi.Repository
{
    public interface ICharacterRepository
    {
        List<Character> GetCharacters();

        Character GetCharacterById(int id);

        int CreateCharacter(Character character);

        string GetCharacterOwnerId(int id);
    }
}
