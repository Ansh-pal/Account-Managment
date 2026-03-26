using Microsoft.AspNetCore.Mvc;
using AccountManager.API.Services;
using AccountManager.API.DTOs;

namespace AccountManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            var result = _authService.Register(dto);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var token = _authService.Login(dto);

            if (string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized(new { error = "Invalid username or password" });
            }

            return Ok(new { token });
        }
    }
}