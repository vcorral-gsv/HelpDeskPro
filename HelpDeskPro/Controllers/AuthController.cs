using AutoMapper;
using HelpDeskPro.Dtos.Auth;
using HelpDeskPro.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskPro.Controllers
{
    [AllowAnonymous]
    public class AuthController(IMapper _mapper, ILogger<AuthController> _logger, IAuthService _authService) : HelpDeskProBaseController<AuthController>(_logger, _mapper)
    {
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var result = await _authService.RefreshTokenAsync(request.RefreshToken);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterUserRequestDto request)
        {
            await _authService.RegisterAsync(request);
            var loginResult = await _authService.LoginAsync(new LoginRequestDto
            {
                Email = request.Email,
                Password = request.Password
            });
            return Ok(loginResult);
        }
    }
}
