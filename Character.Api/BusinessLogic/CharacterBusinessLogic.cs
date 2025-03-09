using AutoMapper;
using BusinessLogic.Models;
using CharacterApi.BusinessLogic.Models;
using CharacterApi.DbContext;
using CharacterApi.Models;
using CharacterApi.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CharacterApi.BusinessLogic
{
    /// <summary>
    /// CharacterBusinessLogic for character manipulation
    /// </summary>
    public class CharacterBusinessLogic : ICharacterBusinessLogic
    {
        #region Private fields
        private readonly IMapper _mapper;
        private readonly ICharacterRepository _characterRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        #endregion

        #region Public constructor
        public CharacterBusinessLogic(IMapper mapper, ICharacterRepository characterRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _characterRepository = characterRepository;
            _contextAccessor = contextAccessor;
        }
        #endregion

        #region Public methods
        public CommandResponse<CharacterPost> CreateCharacter(CharacterPost characterPost)
        {
            var createCharacterResponse = new CommandResponse<CharacterPost>() { Data = characterPost };

            try
            {
                //we need user id from user identity, to put it in createdBy parameter
                characterPost.CreatedBy = _contextAccessor!.HttpContext!.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

                var character = _mapper.Map<Character>(characterPost);

                int rows = _characterRepository.CreateCharacter(character);

                if(rows == 0 )
                {
                    createCharacterResponse.Data = null;
                    createCharacterResponse.Message = new CommandMessage()
                    {
                        Text = "Character is not created",
                        Type = MessageType.Information.ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                createCharacterResponse.Data = null;
                createCharacterResponse.Success = false;
                createCharacterResponse.Message = new CommandMessage()
                {
                    Text = "An error occured while creating character",
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
                var character = _characterRepository.GetCharacterById(id);

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
                var characters = _characterRepository.GetCharacters();

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
        #endregion
    }
}
