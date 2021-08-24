using AutoMapper;
using DTOs.Character;
using Models;

namespace _net
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
        }
    }
}