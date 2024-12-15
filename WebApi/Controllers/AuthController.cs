using Business.Abstract;
using Entities.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.Login(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.Register(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto dto)
        {
            var result = await _authService.Refresh(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
