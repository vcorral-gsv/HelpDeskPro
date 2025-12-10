using HelpDeskPro.Entities;
using Microsoft.AspNetCore.Identity;

namespace HelpDeskPro.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string HashPassword(string password)
        {
            // Puedes pasar null o una instancia de User si lo deseas
            return _hasher.HashPassword(null!, password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            PasswordVerificationResult result = _hasher.VerifyHashedPassword(null!, hash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
