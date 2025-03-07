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
            CreateMap<Character, CharacterGet>();

            CreateMap<Item, ItemGetById>();

            CreateMap<Character, CharacterGetById>();

            CreateMap<CharacterPost, Character>();

            CreateMap<ClassPost, Class>();
        }
    }
}
