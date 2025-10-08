using Microsoft.AspNetCore.Mvc;
using StorageApp.User.Application.Security;
using StorageApp.User.Domain.Entity;
using StorageApp.User.Domain.Enum;

namespace StorageApp.User.Api.Controllers
{

    [ApiController]
    [Route("/auth/[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet("/login")]
        public async Task<IActionResult> Login()
        {
            var user = new UserModel {
                UserName = "Gabriel",
                Email = "teste@teste.com",
                Id = "ewrwerwerwer",
                PasswordHash = "Lagavi30!"
                ,Role = new List<RoleType> { RoleType.Admin },
            };

            return Ok( _jwtService.GenerateToken(user));
        }
    }
}
