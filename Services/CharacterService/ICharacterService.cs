using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<Character>>> GetAllCharacters();
        Task<ServiceResponse<Character>> GetCharacterById(int Id);
        Task<ServiceResponse<List<Character>>> AddCharacter(Character character);
    }
}