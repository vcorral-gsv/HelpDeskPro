using HelpDeskPro.Dtos.Auth;

namespace HelpDeskPro.Services.AuthService
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task RegisterAsync(RegisterUserRequestDto request);
    }
}
