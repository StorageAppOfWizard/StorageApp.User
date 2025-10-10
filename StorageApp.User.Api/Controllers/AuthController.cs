using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorageApp.User.Api.Extensions;
using StorageApp.User.Application.Contracts;
using StorageApp.User.Application.DTO;

namespace StorageApp.User.Api.Controllers
{

    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;
        private readonly IAuthService _authService;

        public AuthController(IJwtService jwtService, IAuthService authService)
        {
            _jwtService = jwtService;
            _authService = authService;
        }



        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            if(!result.IsSuccess)
               return result.ToActionResult();

            var token = _jwtService.GenerateToken(result);

            return Ok(new {token});
        }

        [AllowAnonymous]
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);

            return result.ToActionResult();
        }
    }
}
