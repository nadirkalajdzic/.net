using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>()
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character character)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            characters.Add(character);
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int Id)
        {
            var serviceResponse = new ServiceResponse<Character>();
            serviceResponse.Data = characters.FirstOrDefault(x => x.Id == Id);
            return serviceResponse;
        }
    }
}