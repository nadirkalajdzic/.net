using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using DTOs.Character;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO addCharacterDTO)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var character = _mapper.Map<Character>(addCharacterDTO);

            character.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == GetUserId());
            _context.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters
                                        .Where(x => x.User.Id == character.User.Id)
                                        .Select(x => _mapper.Map<GetCharacterDTO>(x))
                                        .ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacters = await _context.Characters.Where(x => x.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(x => _mapper.Map<GetCharacterDTO>(x)).ToList(); ;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int Id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            var dbCaracter = await _context.Characters.FirstOrDefaultAsync(x => x.Id == Id);
            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbCaracter);
            return serviceResponse;
        }
    }
}