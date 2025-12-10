using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro.Data.Repositories.RefreshTokenRepository
{
    public class RefreshTokenRepository(HelpDeskProContext _context) : IRefreshTokenRepository
    {
        public async Task<Entities.RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }
        public async Task AddAsync(Entities.RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
        }

        public Task Revoke(Entities.RefreshToken token)
        {
            token.RevokedAt = DateTime.UtcNow;
            _context.RefreshTokens.Update(token);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
