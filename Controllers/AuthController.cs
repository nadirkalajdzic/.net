using System.Threading.Tasks;
using Data;
using DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO userRegisterDTO)
        {
            var response = await _authRepository.Register(new User { Username = userRegisterDTO.Username }, userRegisterDTO.Password);

            return !response.IsSuccess ? BadRequest(response) : Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDTO userLoginDTO)
        {
            var response = await _authRepository.Login(userLoginDTO.Username, userLoginDTO.Password);

            return !response.IsSuccess ? BadRequest(response) : Ok(response);
        }

    }
}
