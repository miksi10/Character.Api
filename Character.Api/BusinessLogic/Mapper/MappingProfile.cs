using AutoMapper;
using BusinessLogic.Models;
using CharacterApi.BusinessLogic.Models;
using CharacterApi.Models;

namespace CharacterApi.BusinessLogic.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            #region Character mappings
            CreateMap<Character, CharacterGet>();

            CreateMap<Character, CharacterGetById>();

            CreateMap<CharacterPost, Character>();

            CreateMap<ClassPost, Class>();
            #endregion

            #region Items mappings
            CreateMap<Item, ItemGetById>();

            CreateMap<Item, ItemGet>();

            CreateMap<ItemPost, Item>();

            CreateMap<GrantItem, CharacterItem>();
            #endregion
        }
    }
}
