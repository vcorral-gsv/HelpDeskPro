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
        /// <summary>
        /// Autentica a un usuario y devuelve el token JWT.
        /// </summary>
        /// <param name="request">Credenciales de inicio de sesión</param>
        /// <returns>Respuesta de autenticación con tokens</returns>
        /// <response code="200">Autenticación correcta</response>
        /// <response code="400">Datos de entrada inválidos</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Refresca el token JWT usando el refresh token.
        /// </summary>
        /// <param name="request">Solicitud con refresh token</param>
        /// <returns>Nuevos tokens de autenticación</returns>
        /// <response code="200">Token refrescado correctamente</response>
        /// <response code="400">Refresh token inválido</response>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var result = await _authService.RefreshTokenAsync(request.RefreshToken);
            return Ok(result);
        }

        /// <summary>
        /// Registra un nuevo usuario y devuelve sus tokens de acceso.
        /// </summary>
        /// <param name="request">Datos del usuario a registrar</param>
        /// <returns>Tokens emitidos tras el registro</returns>
        /// <response code="200">Usuario registrado y autenticado</response>
        /// <response code="400">Datos de registro inválidos</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
