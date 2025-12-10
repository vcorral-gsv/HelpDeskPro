using HelpDeskPro.Dtos.Auth;

namespace HelpDeskPro.Services.AuthService
{
    /// <summary>
    /// Servicio de autenticación y gestión de tokens.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Autentica al usuario y genera tokens.
        /// </summary>
        /// <param name="request">Credenciales de login</param>
        /// <returns>Tokens de autenticación</returns>
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        /// <summary>
        /// Refresca el token de acceso con un refresh token válido.
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>Tokens actualizados</returns>
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="request">Datos de registro</param>
        Task RegisterAsync(RegisterUserRequestDto request);
    }
}
