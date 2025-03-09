using CharacterApi.DbContext;
using CharacterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CharacterApi.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly CharacterDbContext _characterDbContext;
        public CharacterRepository(CharacterDbContext characterDbContext)
        {
            _characterDbContext = characterDbContext;
        }

        public int CreateCharacter(Character character)
        {
            _characterDbContext.Character.Add(character);

            return _characterDbContext.SaveChanges();
        }

        public Character GetCharacterById(int id)
        {
            return _characterDbContext.Character.Include(c => c.Items).FirstOrDefault(x => x.Id == id);
        }

        public List<Character> GetCharacters()
        {
            return _characterDbContext.Character.ToList();
        }
    }
}
