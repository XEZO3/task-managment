using Domain.IService;
using Domain.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace task_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto) {
            var login = await _userService.Login(dto);
            return Ok(login);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var register = await _userService.Register(dto);
            return Ok(register);
        }
    }
}
