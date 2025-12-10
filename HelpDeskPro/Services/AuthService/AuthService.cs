using AutoMapper;
using HelpDeskPro.Consts;
using HelpDeskPro.Data.Repositories.RefreshTokenRepository;
using HelpDeskPro.Data.Repositories.UserRepository;
using HelpDeskPro.Dtos.Auth;
using HelpDeskPro.Entities;
using HelpDeskPro.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace HelpDeskPro.Services.AuthService
{
    public class AuthService(
        IUserRepository _userRepository,
        IRefreshTokenRepository _refreshTokenRepository,
        ILogger<AuthService> _logger,
        IPasswordService _passwordService,
        IConfiguration _configuration,
        IMapper _mapper
    ) : IAuthService
    {
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var language = request.LanguageCode != null && Languages.IsLanguageSupported(request.LanguageCode)
                ? request.LanguageCode!.ToLowerInvariant()
                : user.LanguageCode;

            // 1) Access token
            var accessToken = GenerateAccessToken(user, language);

            // 2) Refresh token (entity + persistencia)
            var refreshTokenEntity = GenerateRefreshTokenEntity(user.Id);
            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenEntity.Token,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken) ?? throw new UnauthorizedAccessException("Invalid refresh token.");
            if (storedToken.RevokedAt is not null)
            {
                throw new UnauthorizedAccessException("Refresh token has been revoked.");
            }

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token has expired.");
            }

            // Cargamos el usuario
            var user = storedToken.User
                       ?? await _userRepository.GetByIdAsync(storedToken.UserId)
                       ?? throw new UnauthorizedAccessException("User not found for this token.");

            var language = user.LanguageCode;

            // Rotación de refresh token: revocamos el actual y creamos uno nuevo
            await _refreshTokenRepository.Revoke(storedToken);
            var newRefreshToken = GenerateRefreshTokenEntity(user.Id);
            await _refreshTokenRepository.AddAsync(newRefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            var newAccessToken = GenerateAccessToken(user, language);

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task RegisterAsync(RegisterUserRequestDto request)
        {
            var emailExists = await _userRepository.EmailExistsAsync(request.Email);
            if (emailExists)
            {
                throw new ConflictException("Email_Already_Registered");
            }
            var user = _mapper.Map<User>(request);

            user.PasswordHash = _passwordService.HashPassword(request.Password);

            user.LanguageCode = 
                !string.IsNullOrWhiteSpace(request.LanguageCode) && 
                Languages.IsLanguageSupported(request.LanguageCode)
                ? request.LanguageCode!.ToLowerInvariant() 
                : "en";

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        // ========================
        // Helpers privados
        // ========================

        private string GenerateAccessToken(User user, string language)
        {
            var secretForKey = _configuration["Authentication:SecretForKey"];
            if (string.IsNullOrWhiteSpace(secretForKey))
            {
                throw new InvalidOperationException("Authentication secret key is not configured.");
            }

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(secretForKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role.ToString()),
                new("locale", language),
            };

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        private static RefreshToken GenerateRefreshTokenEntity(int userId)
        {
            // 32 bytes → 256 bits de entropía
            var randomBytes = RandomNumberGenerator.GetBytes(32);
            var refreshToken = Convert.ToBase64String(randomBytes);

            return new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                RevokedAt = null
            };
        }
    }
}
