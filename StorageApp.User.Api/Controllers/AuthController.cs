using Microsoft.AspNetCore.Mvc;
using StorageApp.User.Application.Contracts;
using StorageApp.User.Application.DTO;
using StorageApp.User.Application.Mappers;

namespace StorageApp.User.Api.Controllers
{

    [ApiController]
    [Route("/auth")]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;
        private readonly IAuthService _authService;

        public AuthController(IJwtService jwtService, IAuthService authService)
        {
            _jwtService = jwtService;
            _authService = authService;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            var result = await _authService.LoginAsync(dto);

            return Ok(_jwtService.GenerateToken(result));
        }

        [HttpPost("/register")]

        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);

            return Ok(result);
        }
    }
}
