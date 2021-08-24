using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.CharacterService;

namespace _net.Controllers
{
    [ApiController]
    [Route("characters")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>()
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        public readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> GetAllCharacters()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("character/{Id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetCharacterById(int Id)
        {
            return Ok(await _characterService.GetCharacterById(Id));
        }

        [HttpPost("character")]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> AddCharacter(Character c)
        {
            return Ok(await _characterService.AddCharacter(c));
        }
    }
}