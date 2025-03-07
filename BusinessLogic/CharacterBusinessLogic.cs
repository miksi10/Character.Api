using AutoMapper;
using BusinessLogic.Models;
using CharacterApi.BusinessLogic.Models;
using CharacterApi.DbContext;
using CharacterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CharacterApi.BusinessLogic
{
    public class CharacterBusinessLogic : ICharacterBusinessLogic
    {
        private readonly CharacterDbContext _characterDbContext;
        private readonly IMapper _mapper;
        public CharacterBusinessLogic(CharacterDbContext characterDbContext, IMapper mapper)
        {
            _characterDbContext = characterDbContext;
            _mapper = mapper;
        }

        public CommandResponse<CharacterPost> CreateCharacter(CharacterPost characterPost)
        {
            var createCharacterResponse = new CommandResponse<CharacterPost>() { Data = characterPost };

            try
            {
                var character = _mapper.Map<Character>(characterPost);

                _characterDbContext.Character.Add(character);

                _characterDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                createCharacterResponse.Success = false;
                createCharacterResponse.Message = new CommandMessage()
                {
                    Text = "Failed to create character",
                    Type = MessageType.Error.ToString()
                };
            }

            return createCharacterResponse;
        }

        public CommandResponse<CharacterGetById> GetCharacterById(int id)
        {
            var getCharacterByIdResponse = new CommandResponse<CharacterGetById>();

            try
            {
                var character = _characterDbContext.Character.Include(c => c.Items).FirstOrDefault(x => x.Id == id);

                if (character == null)
                {
                    getCharacterByIdResponse.Message = new CommandMessage()
                    {
                        Text = "There is no character with id: " + id,
                        Type = MessageType.Information.ToString()
                    };

                    return getCharacterByIdResponse;
                }

                getCharacterByIdResponse.Data = _mapper.Map<CharacterGetById>(character);

                foreach(var item in getCharacterByIdResponse.Data.Items)
                {
                    getCharacterByIdResponse.Data.BaseAgility += item.BonusAgility;
                    getCharacterByIdResponse.Data.BaseFaith += item.BonusFaith;
                    getCharacterByIdResponse.Data.BaseIntelligence += item.BonusIntelligence;
                    getCharacterByIdResponse.Data.BaseStrength += item.BonusStrength;
                }
            }
            catch (Exception ex)
            {
                getCharacterByIdResponse.Success = false;
                getCharacterByIdResponse.Message = new CommandMessage()
                { 
                    Text = "Failed to find character with id: " + id,
                    Type = MessageType.Error.ToString()
                };
            }

            return getCharacterByIdResponse;
        }

        public CommandResponse<List<CharacterGet>> GetCharacters()
        {
            var getCharactersResponse = new CommandResponse<List<CharacterGet>>();
            try
            {
                var characters = _characterDbContext.Character.ToList();

                getCharactersResponse.Data = _mapper.Map<List<CharacterGet>>(characters);
            }
            catch (Exception ex)
            {
                getCharactersResponse.Success = false;
                getCharactersResponse.Message = new CommandMessage() 
                {
                    Text = "Failed to show all characters",
                    Type = MessageType.Error.ToString()
                };
            }
            
            return getCharactersResponse;
        }
    }
}
