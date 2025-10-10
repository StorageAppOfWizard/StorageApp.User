using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StorageApp.User.Api.Extensions;
using StorageApp.User.Application.Contracts;
using StorageApp.User.Application.DTO;

namespace StorageApp.User.Api.Controllers
{
    [ApiController]
    [Route("admin/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetAllAsync();
            return result.ToActionResult();
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userService.GetByIdAsync(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            var result = await _userService.CreateAsync(dto);
            return result.ToActionResult();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDTO dto)
        {
            var result = await _userService.UpdateAsync(dto);
            return result.ToActionResult();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(string? id)
        {
            var result = await _userService.DeleteAsync(id);
            return result.ToActionResult();
        }
    }
}

